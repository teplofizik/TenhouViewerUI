using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mahjong.Helper;

namespace Search.Request
{
    static class RoundInfo
    {
        // shanten
        // wait
        // han
        // fu
        // danger
        // dora
        // deadouts
        // furiten
        // riichi
        // firstriichi
        // dorawait
        // riichicount
        // nakicount
        // openedsets
        // wait
        // tempai
        // chiitoi
        // omotesuji
        // suji
        // senkisuji
        // urasuji
        // matagisuji
        // karatennoten
        // form
        // drowntiles
        // color

        /// <summary>
        /// Игрок дилер?
        /// </summary>
        /// <param name="Q">Запрос</param>
        /// <param name="R">Результаты поиска</param>
        public static void Dealer(Query Q, Result R)
        {
            R.CheckRoundPlayers((r, p) => (r.Dealer == p) == Q.BoolValue);
        }

        /// <summary>
        /// Ветер раунда
        /// </summary>
        /// <param name="Q">Запрос</param>
        /// <param name="R">Результаты поиска</param>
        public static void RoundWind(Query Q, Result R)
        {
            R.CheckRoundPlayers((r, p) => R.R.RoundWind(r.Index) == Q.IntValue);
        }

        /// <summary>
        /// Ветер места
        /// </summary>
        /// <param name="Q">Запрос</param>
        /// <param name="R">Результаты поиска</param>
        public static void PlayerWind(Query Q, Result R)
        {
            R.CheckRoundPlayers((r, p) => R.R.PlayerWind(r.Index, p) == Q.IntValue);
        }

        /// <summary>
        /// Номер раздачи
        /// </summary>
        /// <param name="Q">Запрос</param>
        /// <param name="R">Результаты поиска</param>
        public static void RoundIndex(Query Q, Result R)
        {
            if (Q.Less) R.CheckRound((r) => r.Index <= Q.IntValue);
            else if (Q.Over) R.CheckRound((r) => r.Index >= Q.IntValue);
            else R.CheckRound((r) => r.Index == Q.IntValue);
        }

        /// <summary>
        /// Количество палок ренчана на начало раздачи
        /// </summary>
        /// <param name="Q">Запрос</param>
        /// <param name="R">Результаты поиска</param>
        public static void RenchanStick(Query Q, Result R)
        {
            if (Q.Less) R.CheckRound((r) => r.RenchanStick <= Q.IntValue);
            else if (Q.Over) R.CheckRound((r) => r.RenchanStick >= Q.IntValue);
            else R.CheckRound((r) => r.RenchanStick == Q.IntValue);
        }
    }
}
