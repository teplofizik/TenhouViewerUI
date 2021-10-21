using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Search.Request
{
    static class ReplayInfo
    {
        // aka
        // kuitan
        // south
        // speedy
        // dan
        // upperdan

        /// <summary>
        /// Количество игроков
        /// </summary>
        /// <param name="Q">Запрос</param>
        /// <param name="R">Результаты поиска</param>
        public static void Players(Query Q, Result R)
        {
            R.CheckReplay((r => r.PlayerCount == Q.IntValue));
        }

        /// <summary>
        /// Лобби
        /// </summary>
        /// <param name="Q">Запрос</param>
        /// <param name="R">Результаты поиска</param>
        public static void Lobby(Query Q, Result R)
        {
            if (Q.Less) R.CheckReplay((r => r.Lobby <= Q.IntValue));
            else if (Q.Over) R.CheckReplay((r => r.Lobby >= Q.IntValue));
            else R.CheckReplay((r => r.Lobby == Q.IntValue));
        }

        /// <summary>
        /// Хэш раздачи
        /// </summary>
        /// <param name="Q">Запрос</param>
        /// <param name="R">Результаты поиска</param>
        public static void Hash(Query Q, Result R)
        {
            R.CheckReplay((r => r.Hash.CompareTo(Q.Value) == 0));
        }
    }
}
