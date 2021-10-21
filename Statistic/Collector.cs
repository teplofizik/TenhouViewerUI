using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Project;
using Search;
using Tournier;
using Output;
using Mahjong;
using Mahjong.Helper;

namespace Statistic
{
    static class Collector
    {
        /// <summary>
        /// Получить заданное число игр
        /// </summary>
        /// <param name="TP"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        static public List<Search.Result> FindResults(TenhouProject TP, int Count)
        {
            List<Search.Result> Results = new List<Search.Result>();

            for (int i = TP.Hashes.Count - 1; i >= 0; i--)
            {
                Replay R = TP.GetGameReplay(TP.Hashes[i]);
                int Player = R.GetPlayerIndex(TP.Owner);

        //        if (R.Lobby != 0) continue;
                if (Player == -1) continue;

                var Res = new Search.Result(R);
                for (int j = 0; j < R.PlayerCount; j++) Res.PlayerMark[j] = (j != Player);
                Results.Add(Res);

                if (Results.Count == Count) break;
            }

            return Results;
        }

        /// <summary>
        /// Обработать реплеи
        /// </summary>
        /// <param name="Results"></param>
        /// <param name="From"></param>
        /// <param name="To"></param>
        /// <returns></returns>
        static public Info ProcessData(List<Search.Result> Results, string NickName, int From, int To)
        {
            if (Results.Count < To) return null;

            List<Search.Result> Temp = new List<Search.Result>();
            for (int i = From; i < To; i++) Temp.Add(Results[i]);

            var TR = new Tournier.Tournier(Temp).Analyze();
            if (TR.Count == 0) return null;
            var Owner = TR[0];

            var I = new Info();
            FillPlayerInfo(Temp[0].R, NickName, ref I);
            CheckReplays(Temp, NickName, ref I);
            I.AveragePlace = Owner.AveragePlace;
            I.Loser = (Owner.RoundCount > 0) ? Owner.RonCount * 100.0 / Owner.RoundCount : 0.0;
            I.RonCount = Owner.RonCount;
            I.Games = Temp.Count;

            I.RiichiCount = Owner.RiichiCount;
            I.SuccessfulRiichiCount = Owner.RiichiWinCount;
            I.IppatsuCount = Owner.IppatsuCount;

            for (int i = 0; i < 4; i++) I.PlaceCounters[i] = Owner.Places.Count(a => a == i + 1);

            return I;
        }

        static private void CheckReplays(List<Search.Result> Results, string NickName, ref Info I)
        {
            I.MinusCount = 0;
            I.Rounds = 0;
            I.HandCost.Clear();
            I.RonFromRiichi = 0;
            foreach (var R in Results)
            {
                int Player = R.R.GetPlayerIndex(NickName);

                // Количество раздач
                I.Rounds += R.R.Rounds.Count;

                // Количество уходов в минуса
                if (R.R.Balance[Player] < 0) I.MinusCount++;

                foreach (var Rnd in R.R.Rounds)
                {
                    if (Rnd.hasAgari(Player))
                    {
                        I.HandCost.Add(Rnd.HandCost(Player));
                    }
                    if (Rnd.isLoser(Player) && Rnd.isDeclaredRiichi(Player))
                        I.RonFromRiichi++;
                }
            }
        }

        static private void FillPlayerInfo(Replay R, string NickName, ref Info I)
        {
            // Общая информация об игроке
            int Player = R.GetPlayerIndex(NickName);

            I.NickName = NickName;
            I.Rank = R.Players[Player].Rank.toJapaneseRank();
            I.Rating = R.Players[Player].Rating;
        }

    }
}
