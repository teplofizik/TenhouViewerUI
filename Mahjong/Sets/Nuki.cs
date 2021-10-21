using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Mahjong.Sets
{
    [Serializable]
    [XmlType("Nuki")]
    public class Nuki : Set
    {
        public Nuki(IList<int> Tiles)
         : base(Tiles)
        {

        }

        // Тип сета
        public override SetType Type
        {
            get { return SetType.NUKI; }
        }
    }
}
