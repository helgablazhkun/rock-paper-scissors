using rock_paper_scissors.Data;
using rock_paper_scissors.Extensions;
using Microsoft.EntityFrameworkCore;

namespace rock_paper_scissors.Services
{
    public interface IGameDataService
    {
        Task<Game?> FindGameWaitingForPlayer();
        Task<Game> CreateGame(string sessionId, string nick);
        Task<Game> AddPlayerToGame(Game game, string sessionId, string nick);
        Task<Game> FindGameBySessionId(string playerSessionId);
        Task<Game> UpdateScoreForPlayer(Game game, string playerSessionId, int round, Weapon weapon);

    }

    public class GameDataService : IGameDataService
    {
        private readonly GameDbContext _gameDbContext;

        public GameDataService(GameDbContext gameDbContext)
        {
            _gameDbContext = gameDbContext;
        }

        public async Task<Game> AddPlayerToGame(Game game, string sessionId, string nick)
        {
            _gameDbContext.Attach(game);
            game.Player2 = new GameSession
            {
                SessionId = sessionId,
                Nick = nick
            };

            await _gameDbContext.SaveChangesAsync();

            return game;
        }

        public async Task<Game> CreateGame(string sessionId, string nick)
        {
            Game game = new()
            {
                Id = Guid.NewGuid(),
                Player1 = new GameSession
                {
                    SessionId = sessionId,
                    Nick = nick
                }
            };

            _gameDbContext.Games.Add(game);

            await _gameDbContext.SaveChangesAsync();
            return game;
        }

        public async Task<Game?> FindGameWaitingForPlayer()
        {
            return await _gameDbContext.Games
                .Include(g => g.Player1)
                .Include(g => g.Player2)
                .FirstOrDefaultAsync(g => g.Player2 == null);
        }

        public async Task<Game> FindGameBySessionId(string playerSessionId)
        {
            return await _gameDbContext.Games
                .Include(g => g.Player1)
                .Include(g => g.Player2)
                .FirstAsync(g =>
                    g.Player1.SessionId == playerSessionId || g.Player2 != null && g.Player2.SessionId == playerSessionId);
        }

        public async Task<Game> UpdateScoreForPlayer(Game game, string playerSessionId, int round, Weapon weapon)
        {
            _gameDbContext.Attach(game);
            var player = game.GetPlayerSession(playerSessionId);

            player.UpdatePlayerWeapon(round, weapon);

            await _gameDbContext.SaveChangesAsync();

            return game;
        }
    }
}