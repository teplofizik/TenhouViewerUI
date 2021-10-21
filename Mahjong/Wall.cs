using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Mahjong
{
    [Serializable]
    [XmlType("Wall")]
    public class Wall
    {
        // Тайлы в стене
        public int[] Tiles;

        // Кубики
        public int[] Dice;
    }
}
