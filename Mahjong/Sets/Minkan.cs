using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Mahjong.Sets
{
    [Serializable]
    [XmlType("Minkan")]
    public class Minkan : Set
    {
        public Minkan(IList<int> Tiles, int From)
            : base(Tiles, From)
        { }

        // Тип сета
        public override SetType Type
        {
            get { return SetType.MINKAN; }
        }
    }
}
