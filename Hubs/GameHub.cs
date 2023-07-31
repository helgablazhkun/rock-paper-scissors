using Microsoft.AspNetCore.SignalR;
using rock_paper_scissors.Constants;
using rock_paper_scissors.Data;
using rock_paper_scissors.Extensions;
using rock_paper_scissors.Services;

namespace rock_paper_scissors.Hubs
{
    public class GameHub : Hub
    {
        private readonly IGameDataService _gameDataService;
        private readonly IGameService _gameService;

        public GameHub(IGameDataService gameDataService, IGameService gameService)
        {
            _gameDataService = gameDataService;
            _gameService = gameService;
        }

        public async Task AskServer(string someTextFromClient)
        {
            string tempString;
            if (someTextFromClient == "hey")
            {
                tempString = "message was 'hey'";
            }
            else
            {
                tempString = "message was something else";
            }
            await Clients.Client(Context.ConnectionId).SendAsync("AskServerResponse", tempString);
        }

        public async Task JoinGame(string playerNick)
        {
            Game? existingGame = await _gameDataService.FindGameWaitingForPlayer();

            if (existingGame == null)
            {
                await _gameDataService.CreateGame(Context.ConnectionId, playerNick);
                await Clients.All.SendAsync(GameEvents.WaitingForSecondPlayerToJoin);
            }
            else
            {
                existingGame = await _gameDataService.AddPlayerToGame(existingGame, Context.ConnectionId, playerNick);

                if(existingGame.Player2 == null) {
                    throw new Exception("Player2 is not join");
                }

                await Clients.Client(existingGame.Player1.SessionId).SendAsync(GameEvents.StartTheGame);
                await Clients.Client(existingGame.Player2.SessionId).SendAsync(GameEvents.StartTheGame);
            }
        }

        public async Task MakeChoice(int round, string weapon)
        {
            var game = await _gameDataService.FindGameBySessionId(Context.ConnectionId);

            var playerWeapon = (Weapon)Enum.Parse(typeof(Weapon), weapon);
            await _gameDataService.UpdateScoreForPlayer(game, Context.ConnectionId, round, playerWeapon);

            var playerSession = game.GetPlayerSession(Context.ConnectionId);
            var opponentSession = game.GetOpponentSession(playerSession);

            var opponentWeapon = opponentSession.GetWeaponByRound(round);
            if (opponentWeapon.HasValue)
            {
                await NotifyPlayerWithRoundResult(playerSession.SessionId, playerWeapon, opponentWeapon.Value);
                await NotifyPlayerWithRoundResult(opponentSession.SessionId, opponentWeapon.Value, playerWeapon);

                if(_gameService.IsGameFinished(playerSession, opponentSession)){
                    await NotifyPlayerWithGameResult(playerSession, opponentSession);
                    await NotifyPlayerWithGameResult(opponentSession, playerSession);
                }
            }
            else
            {
                await Clients.Caller.SendAsync(GameEvents.WaitingForPlayerToMakeChoice);
            }
        }

        private async Task NotifyPlayerWithRoundResult(string playerSessionId, Weapon playerWeapon, Weapon opponentWeapon){
            var playerResult = _gameService.GetGameRoundResult(
                playerWeapon,
                opponentWeapon).ToString();

            await Clients.Client(playerSessionId)
                .SendAsync(GameEvents.RoundEnd, new { PlayerResult = playerResult,  OpponentWeapon = opponentWeapon.ToString()});
        }

        private async Task NotifyPlayerWithGameResult(GameSession playerSession, GameSession opponentSession){
            var playerResult = _gameService.GetGameResult(
                playerSession,
                opponentSession).ToString();

            await Clients.Client(playerSession.SessionId)
                .SendAsync(GameEvents.GameEnd,
                new {
                        PlayerResult = playerResult,
                        PlayerSession = playerSession.ConvertWeaponToClient(),
                        OpponentSession = opponentSession.ConvertWeaponToClient()
                    });
        }

    }
}