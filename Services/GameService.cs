using rock_paper_scissors.Data;

namespace rock_paper_scissors.Services
{
    public interface IGameService
    {
        GameResult GetGameRoundResult(Weapon playerChoice, Weapon opponentChoice);
        bool IsGameFinished(int round, GameSession playerSession, GameSession opponentSession);
        GameResult GetGameResult(GameSession playerSession, GameSession opponentSession);
    }

    public class GameService : IGameService
    {
        public GameResult GetGameRoundResult(Weapon playerChoice, Weapon opponentChoice)
        {
            return _gameResults
                .Single(gr => gr.player1Choice == playerChoice && gr.player2Choice == opponentChoice)
                .result;
        }

        public bool IsGameFinished(int round, GameSession playerSession, GameSession opponentSession)
        {
          if(round == 5) {
            return true;
          }

          List<GameResult> playerResults = GetPlayerGameResult(playerSession, opponentSession);

          int playerWins = playerResults
                        .Where(r => r == GameResult.Win).Count();
          int oppositeWins = GetPlayerGameResult(opponentSession, playerSession)
                        .Where(r => r == GameResult.Win).Count();

          return playerWins == 3 || oppositeWins == 3;
        }

        public GameResult GetGameResult(GameSession playerSession, GameSession opponentSession)
        {
            int playerScore = GetPlayerGameResult(playerSession, opponentSession)
                        .Where(r => r == GameResult.Win).Count();
            int oppositeWins = GetPlayerGameResult(opponentSession, playerSession)
                        .Where(r => r == GameResult.Win).Count();

            return playerScore == oppositeWins ? GameResult.Draw: playerScore > oppositeWins? GameResult.Win : GameResult.Lose;
        }

        private List<GameResult> GetPlayerGameResult(GameSession playerSession, GameSession opponentSession) {
            List<GameResult> playerResults = new ();

            if(playerSession.WeaponRound1.HasValue && opponentSession.WeaponRound1.HasValue){
              playerResults.Add(GetGameRoundResult(playerSession.WeaponRound1.Value, opponentSession.WeaponRound1.Value));
            }

            if(playerSession.WeaponRound2.HasValue && opponentSession.WeaponRound2.HasValue){
              playerResults.Add(GetGameRoundResult(playerSession.WeaponRound2.Value, opponentSession.WeaponRound2.Value));
            }

            if(playerSession.WeaponRound3.HasValue && opponentSession.WeaponRound3.HasValue){
              playerResults.Add(GetGameRoundResult(playerSession.WeaponRound3.Value, opponentSession.WeaponRound3.Value));
            }

            if(playerSession.WeaponRound4.HasValue && opponentSession.WeaponRound4.HasValue){
              playerResults.Add(GetGameRoundResult(playerSession.WeaponRound4.Value, opponentSession.WeaponRound4.Value));
            }

            if(playerSession.WeaponRound5.HasValue && opponentSession.WeaponRound5.HasValue){
              playerResults.Add(GetGameRoundResult(playerSession.WeaponRound5.Value, opponentSession.WeaponRound5.Value));
            }

            return playerResults;
        }

        private readonly List<(Weapon player1Choice, Weapon player2Choice, GameResult result)> _gameResults =
            new()
            {
                (Weapon.Rock, Weapon.Rock, GameResult.Draw),
                (Weapon.Rock, Weapon.Paper, GameResult.Lose),
                (Weapon.Rock, Weapon.Scissors, GameResult.Win),
                (Weapon.Paper, Weapon.Rock, GameResult.Win),
                (Weapon.Paper, Weapon.Paper, GameResult.Draw),
                (Weapon.Paper, Weapon.Scissors, GameResult.Lose),
                (Weapon.Scissors, Weapon.Rock, GameResult.Lose),
                (Weapon.Scissors, Weapon.Paper, GameResult.Win),
                (Weapon.Scissors, Weapon.Scissors, GameResult.Draw),
            };
    }
}