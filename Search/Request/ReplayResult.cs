using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Search.Request
{
    static class ReplayResult
    {
        // oneside

        /// <summary>
        /// Занятое место
        /// </summary>
        /// <param name="Q">Запрос</param>
        /// <param name="R">Результаты поиска</param>
        public static void Place(Query Q, Result R)
        {
            if (Q.Less) R.CheckReplayPlayers((r, p) => r.Place[p] <= Q.IntValue);
            else if (Q.Over) R.CheckReplayPlayers((r, p) => r.Place[p] >= Q.IntValue);
            else R.CheckReplayPlayers((r, p) => r.Place[p] == Q.IntValue);
        }

        /// <summary>
        /// Баланс на конец игры
        /// </summary>
        /// <param name="Q">Запрос</param>
        /// <param name="R">Результаты поиска</param>
        public static void Balance(Query Q, Result R)
        {
            if (Q.Less) R.CheckReplayPlayers((r, p) => r.Balance[p] <= Q.IntValue);
            else if (Q.Over) R.CheckReplayPlayers((r, p) => r.Balance[p] >= Q.IntValue);
            else R.CheckReplayPlayers((r, p) => r.Balance[p] == Q.IntValue);
        }

        /// <summary>
        /// Результат игры
        /// </summary>
        /// <param name="Q">Запрос</param>
        /// <param name="R">Результаты поиска</param>
        public static void Result(Query Q, Result R)
        {
            if (Q.Less) R.CheckReplayPlayers((r, p) => r.Result[p] <= Q.IntValue);
            else if (Q.Over) R.CheckReplayPlayers((r, p) => r.Result[p] >= Q.IntValue);
            else R.CheckReplayPlayers((r, p) => r.Result[p] == Q.IntValue);
        }
    }
}
