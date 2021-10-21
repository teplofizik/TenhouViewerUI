using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Mahjong.Sets
{
    [Serializable]
    [XmlType("Chakan")]
    public class Chakan : Set
    {
        public Chakan(IList<int> Tiles)
            : base(Tiles)
        {

        }

        // Дополнение до кана
        public Chakan(Pon P, int Tile)
            : base(new List<int>(P.Tiles), P.FromWho)
        {
            mTiles = new List<int>(P.Tiles);
            FromWho = P.FromWho;

            if(mTiles != null) mTiles.Add(Tile);
        }

        // Тип сета
        public override SetType Type
        {
            get { return SetType.CHAKAN; }
        }
    }
}
