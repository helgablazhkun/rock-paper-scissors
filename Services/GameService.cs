using rock_paper_scissors.Data;

public interface IGameService
{
    GameResult GetGameRoundResult(Weapon player1Choice, Weapon player2Choice);
    GameResult GetGameResult(GameSession player1Session, GameSession player2Session);
}

public class GameService : IGameService
{
    public GameResult GetGameRoundResult(Weapon player1Choice, Weapon player2Choice)
    {
        return _gameResults
            .Single(gr => gr.player1Choice == player1Choice && gr.player2Choice == player2Choice)
            .result;
    }

    public GameResult GetGameResult(GameSession player1Session, GameSession player2Session)
    {
        List<GameResult> playerResults = new () {
          GetGameRoundResult(player1Session.WeaponRound1, player2Session.WeaponRound1),
          GetGameRoundResult(player1Session.WeaponRound2, player2Session.WeaponRound2),
          GetGameRoundResult(player1Session.WeaponRound3, player2Session.WeaponRound3)
        };

        if(player1Session.WeaponRound4.HasValue && player2Session.WeaponRound4.HasValue){
          playerResults.Add(GetGameRoundResult(player1Session.WeaponRound4.Value, player2Session.WeaponRound4.Value));
        }

        if(player1Session.WeaponRound5.HasValue && player2Session.WeaponRound5.HasValue){
          playerResults.Add(GetGameRoundResult(player1Session.WeaponRound5.Value, player2Session.WeaponRound5.Value));
        }

        return playerResults.Where(r => r == GameResult.Win).Count() > 2 ? GameResult.Win: GameResult.Lose;
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