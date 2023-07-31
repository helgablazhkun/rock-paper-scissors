using rock_paper_scissors.Data;

namespace rock_paper_scissors.Extensions
{
    public static class GameExtensions
    {
        public static GameSession GetOpponentSession(this Game game, GameSession player)
        {
            if(game.Player2 == null) {
                    throw new Exception("Opponent session is null");
            }

            return game.Player1.SessionId == player.SessionId
                ? game.Player2
                : game.Player1;
        }

        public static GameSession GetPlayerSession(this Game game, string playerSessionId)
        {
            if(game.Player2 == null) {
                    throw new Exception("Opponent session is null");
            }

            return game.Player1.SessionId == playerSessionId
                ? game.Player1
                : game.Player2;
        }
    }
}