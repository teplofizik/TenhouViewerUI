using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Viewer
{
    class PlayerInfo
    {
        /// <summary>
        /// Ник игрока
        /// </summary>
        public string Nickname;

        /// <summary>
        /// Количество игр
        /// </summary>
        public int Count;

        /// <summary>
        /// Ранг в последней встреченной игре
        /// </summary>
        public int Rank;

        /// <summary>
        /// Рейтинг в последней встреченной игре
        /// </summary>
        public int Rating;

        /// <summary>
        /// Пол игрока
        /// </summary>
        public int Sex;

        public PlayerInfo(string Name)
        {
            Count = 0;
            Rank = 0;
            Rating = 0;
            Nickname = Name;
        }
    }
}
