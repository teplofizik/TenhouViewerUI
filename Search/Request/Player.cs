using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Search.Request
{
    static class Player
    {
        // sex

        /// <summary>
        /// Игроки с заданным ником
        /// </summary>
        /// <param name="Q">Запрос</param>
        /// <param name="R">Результаты поиска</param>
        public static void NickName(Query Q, Result R)
        {
            R.CheckPlayer(p => p.NickName.CompareTo(Q.Value) == 0);
        }

        /// <summary>
        /// Игроки с заданным полом
        /// </summary>
        /// <param name="Q">Запрос</param>
        /// <param name="R">Результаты поиска</param>
        public static void Sex(Query Q, Result R)
        {
            int Sex = Q.Value.FromSex();
            
            R.CheckPlayer(p => p.Sex == Sex);
        }

        /// <summary>
        /// Игроки с заданным рангом
        /// </summary>
        /// <param name="Q">Запрос</param>
        /// <param name="R">Результаты поиска</param>
        public static void Rank(Query Q, Result R)
        {
            if (Q.Less)      R.CheckPlayer(p => p.Rank <= Q.IntValue);
            else if (Q.Over) R.CheckPlayer(p => p.Rank >= Q.IntValue);
            else             R.CheckPlayer(p => p.Rank == Q.IntValue);
        }

        /// <summary>
        /// Игроки с заданным рейтингом
        /// </summary>
        /// <param name="Q">Запрос</param>
        /// <param name="R">Результаты поиска</param>
        public static void Rating(Query Q, Result R)
        {
            if (Q.Less) R.CheckPlayer(p => p.Rating <= Q.IntValue);
            else if (Q.Over) R.CheckPlayer(p => p.Rating >= Q.IntValue);
            else R.CheckPlayer(p => p.Rating == Q.IntValue);
        }
    }
}
