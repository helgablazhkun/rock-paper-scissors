using rock_paper_scissors.Data;

namespace rock_paper_scissors.Extensions
{
    public static class GameSessionExtensions
    {
        public static Weapon? GetWeaponByRound(this GameSession player, int round) {
            switch (round)
            {
                case 1:
                {
                    return player.WeaponRound1;
                }

                case 2:
                {
                    return player.WeaponRound2;
                }

                case 3:
                {
                    return player.WeaponRound3;
                }

                case 4:
                {
                    return player.WeaponRound4;
                }

                default:
                {
                    return player.WeaponRound5;
                }
            }
        }

        public static void UpdatePlayerWeapon(this GameSession player, int round, Weapon weapon) {
            switch (round)
            {
                case 1:
                {
                    player.WeaponRound1 = weapon;
                    break;
                }

                case 2:
                {
                    player.WeaponRound2 = weapon;
                    break;
                }

                case 3:
                {
                    player.WeaponRound3 = weapon;
                    break;
                }

                case 4:
                {
                    player.WeaponRound4 = weapon;
                    break;
                }

                default:
                {
                    player.WeaponRound5 = weapon;
                    break;
                }
            }
        }

        public static GameClientSession ConvertWeaponToClient(this GameSession player)
        {
           return new GameClientSession {
              Nick = player.Nick,
              WeaponRound1 = player.WeaponRound1.ToString() ?? String.Empty,
              WeaponRound2 = player.WeaponRound2.ToString() ?? String.Empty,
              WeaponRound3 = player.WeaponRound3.ToString() ?? String.Empty,
              WeaponRound4 = player.WeaponRound4.ToString() ?? String.Empty,
              WeaponRound5 = player.WeaponRound5.ToString() ?? String.Empty,
           };
        }

    }
}