using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mahjong;

namespace Output
{
    static class FormatExtension
    {
        private readonly static string[] Ranks = { "新人", "9級", "8級", "7級", "6級", "5級", "4級", "3級", "2級", "1級", "初段", "二段", "三段", "四段", "五段", "六段", "七段", "八段", "九段", "十段" };
        public static string toJapaneseRank(this int Rank)
        {
            if ((Rank < 0) || (Rank >= Ranks.Length)) return "unk";

            return Ranks[Rank];
        }

        public static string toJapaneseRating(this int Rating)
        {
            return String.Format("{0:d}R", Rating);
        }

        private static readonly string[] Name = { "normal", "yao9", "reach4", "ron3", "kan4", "kaze4", "nm" };

        public static string ToDrawReason(this int Index)
        {
            if ((Index < 0) || (Index >= Name.Length)) return "";

            return Name[Index];
        }

        public static string ToWind(this int Index)
        {
            switch (Index)
            {
                case 0: return "東";
                case 1: return "南";
                case 2: return "西";
                case 3: return "北";
            }

            return "";
        }

        public static int AverageRating(this List<Player> Players)
        {
            int Sum = 0;
            foreach (Player P in Players) Sum += P.Rating;

            return (Players.Count > 0) ? (Sum / Players.Count) : 0;
        }

        public static int GetPlayerIndex(this Field F, int Player)
        {
            if (F.Value != null)
            {
                try
                {
                    int T = Convert.ToInt16(F.Value);

                    if ((T >= 1) && (T <= 4)) return T - 1;
                }
                catch { }
            }
            return Player;
        }
    }
}
