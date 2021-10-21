using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Mahjong.Sets
{
    [Serializable]
    [XmlType("Pon")]
    public class Pon : Set
    {
        public Pon(IList<int> Tiles, int From)
            : base(Tiles, From)
        { }

        // Тип сета
        public override SetType Type
        {
            get { return SetType.PON; }
        }
    }
}
