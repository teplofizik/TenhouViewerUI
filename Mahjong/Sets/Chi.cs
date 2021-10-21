using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Mahjong.Sets
{
    [Serializable]
    [XmlType("Chi")]
    public class Chi : Set
    {
        public Chi(IList<int> Tiles, int From)
            : base(Tiles, From)
        { }

        // Тип сета
        public override SetType Type
        {
            get { return SetType.CHI; }
        }
    }
}
