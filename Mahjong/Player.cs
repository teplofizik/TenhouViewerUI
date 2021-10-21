using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Output;

namespace Mahjong
{
    [Serializable]
    [XmlType("Player")]
    public class Player
    {
        // Ник игрока
        public string NickName;

        // Рейтинг игрока
        public int Rating;

        // Ранг игрока
        public int Rank;

        // Пол игрока (0 - комп, 1 - муж, 2 - жен)
        public int Sex;

        public override string ToString()
        {
            return String.Format("{0:s} ({1:s} {2:d}R)", NickName, Rank.toJapaneseRank(), Rating);
        }
    }
}
