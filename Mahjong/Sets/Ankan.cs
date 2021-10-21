using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Mahjong.Sets
{
    [Serializable]
    [XmlType("Ankan")]
    public class Ankan : Set
    {
        public Ankan(IList<int> Tiles) : base(Tiles)
        { }

        // Тип сета
        public override SetType Type
        {
            get { return SetType.ANKAN; }
        }
    }
}
