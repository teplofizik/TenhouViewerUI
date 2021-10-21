using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Mahjong.Steps
{
    [Serializable]
    [XmlType("Dora")]
    class Dora : Step
    {
        public int Tile;
       
        public Dora()
            : base()
        {
            Tile = -1;
        }

        public Dora(int T)
            : base()
        {
            Tile = T;
        }

        public override StepType Type
        {
            get { return StepType.DORA; }
        }

        public override string ToString()
        {
            return String.Format("New dora pointer: {0:s}", new Tile(Tile).TileName);
        }
    }
}
