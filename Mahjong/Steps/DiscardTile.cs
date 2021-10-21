using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Mahjong.Steps
{
    [Serializable]
    [XmlType("DiscardTile")]
    class DiscardTile : Step
    {
        public int Tile;

        public DiscardTile()
            : base()
        {

        }

        public DiscardTile(int P, int T)
            : base(P)
        {
            Tile = T;
        }

        public override StepType Type
        {
            get { return StepType.DISCARDTILE; }
        }

        public override string ToString()
        {
            return String.Format("Discard tile ({0:d}): {1:s}", Player, new Tile(Tile).TileName);
        }
    }
}
