using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mahjong.Helper;

namespace Search.Request
{
    static class RoundResult
    {
        // rononriichi
        // yaku

        /// <summary>
        /// Игрок собрал руку
        /// </summary>
        /// <param name="Q">Запрос</param>
        /// <param name="R">Результаты поиска</param>
        public static void Agari(Query Q, Result R)
        {
            R.CheckRoundPlayers((r, p) => r.hasAgari(p) == Q.BoolValue);
        }

        /// <summary>
        /// Игрок набросил
        /// </summary>
        /// <param name="Q">Запрос</param>
        /// <param name="R">Результаты поиска</param>
        public static void Loser(Query Q, Result R)
        {
            R.CheckRoundPlayers((r, p) => r.isLoser(p) == Q.BoolValue);
        }

        /// <summary>
        /// Победа по цумо
        /// </summary>
        /// <param name="Q">Запрос</param>
        /// <param name="R">Результаты поиска</param>
        public static void Tsumo(Query Q, Result R)
        {
            R.CheckRoundPlayers((r, p) => r.hasTsumoAgari(p) == Q.BoolValue);
        }

        /// <summary>
        /// Победа по рону
        /// </summary>
        /// <param name="Q">Запрос</param>
        /// <param name="R">Результаты поиска</param>
        public static void Ron(Query Q, Result R)
        {
            R.CheckRoundPlayers((r, p) => (!r.hasTsumoAgari(p) && r.hasAgari(p)) == Q.BoolValue);
        }

        /// <summary>
        /// Ничья?
        /// </summary>
        /// <param name="Q">Запрос</param>
        /// <param name="R">Результаты поиска</param>
        public static void Draw(Query Q, Result R)
        {
            R.CheckRound((r) => r.IsDraw() == Q.BoolValue);
        }

        /// <summary>
        /// Ничья с причиной
        /// </summary>
        /// <param name="Q">Запрос</param>
        /// <param name="R">Результаты поиска</param>
        public static void DrawReason(Query Q, Result R)
        {
            R.CheckRound((r) => r.DrawReason() == Q.IntValue);
        }

        /// <summary>
        /// Количество действий
        /// </summary>
        /// <param name="Q">Запрос</param>
        /// <param name="R">Результаты поиска</param>
        public static void Steps(Query Q, Result R)
        {
            if (Q.Less) R.CheckRoundPlayers((r, p) => r.getStepCount(p) <= Q.IntValue);
            else if (Q.Over) R.CheckRoundPlayers((r, p) => r.getStepCount(p) >= Q.IntValue);
            else R.CheckRoundPlayers((r, p) => r.getStepCount(p) == Q.IntValue);
        }

        /// <summary>
        /// Выплата
        /// </summary>
        /// <param name="Q">Запрос</param>
        /// <param name="R">Результаты поиска</param>
        public static void Payment(Query Q, Result R)
        {
            if (Q.Less) R.CheckRoundPlayers((r, p) => r.Pay[p] <= Q.IntValue);
            else if (Q.Over) R.CheckRoundPlayers((r, p) => r.Pay[p] >= Q.IntValue);
            else R.CheckRoundPlayers((r, p) => r.Pay[p] == Q.IntValue);
        }
    }
}
