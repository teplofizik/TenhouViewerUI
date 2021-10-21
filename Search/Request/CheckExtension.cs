using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Search.Request
{
    static class CheckExtension
    {
        // Проверка игрока на соответствие условиям
        public static void CheckPlayer(this Result R, Func<Mahjong.Player, bool> F)
        {
            for (int p = 0; p < R.R.PlayerCount; p++)
            {
                if (!F(R.R.Players[p])) R.PlayerMark[p] = true;
            }
        }

        // Проверка игры на соответствие условиям
        public static void CheckReplay(this Result R, Func<Mahjong.Replay, bool> F)
        {
            if (!F(R.R)) R.ReplayMark = true;
        }

        // Проверка раздачи на соответствие условиям
        public static void CheckRound(this Result R, Func<Mahjong.Round, bool> F)
        {
            for (int r = 0; r < R.R.Rounds.Count; r++)
            {
                if (!F(R.R.Rounds[r])) R.RoundMark[r] = true;
            }
        }

        // Проверка игроков в отдельной раздаче на соответствие условиям
        public static void CheckRoundPlayers(this Result R, Func<Mahjong.Round, int, bool> F)
        {
            for (int r = 0; r < R.R.Rounds.Count; r++)
            {
                Mahjong.Round Rnd = R.R.Rounds[r];

                for (int p = 0; p < R.R.PlayerCount; p++)
                {
                    if (!F(Rnd, p)) R.HandMark[r][p] = true;
                }
            }
        }

        // Проверка игроков в реплее на соответствие условиям
        public static void CheckReplayPlayers(this Result R, Func<Mahjong.Replay, int, bool> F)
        {
            for (int p = 0; p < R.R.PlayerCount; p++)
            {
                if (!F(R.R, p)) R.PlayerMark[p] = true;
            }
        }

        public static int FromSex(this string S)
        {
            switch (S)
            {
                case "m":
                case "M":
                case "male": return 1;
                case "f":
                case "F":
                case "female": return 2;
            }
            return 0;
        }
    }
}
