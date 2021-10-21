using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Mahjong.Steps
{
    [Serializable]
    [XmlType("DrawTile")]
    class DrawTile : Step
    {
        public int Tile;

        public DrawTile()
            : base()
        {

        }

        public DrawTile(int P, int T)
            : base(P)
        {
            Tile = T;
        }

        public override StepType Type
        {
            get { return StepType.DRAWTILE; }
        }

        public override string ToString()
        {
            return String.Format("Draw tile ({0:d}): {1:s}", Player, new Tile(Tile).TileName);
        }
    }
}
