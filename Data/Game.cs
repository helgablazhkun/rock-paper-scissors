using Microsoft.EntityFrameworkCore;

namespace rock_paper_scissors.Data
{
    public class Game
    {
        public Guid Id { get; set; }
        public GameSession Player1 { get; set; }
        public GameSession? Player2 { get; set; }

    }
    [Owned]
    public class GameSession
    {
        public string Nick { get; set; }
        public Weapon? WeaponRound1 { get; set; }
        public Weapon? WeaponRound2  { get; set; }
        public Weapon? WeaponRound3  { get; set; }
        public Weapon? WeaponRound4  { get; set; }
        public Weapon? WeaponRound5  { get; set; }
    }

}