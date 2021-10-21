using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Mahjong
{
    [Serializable]
    [XmlType("Yaku")]
    public class Yaku
    {
        [XmlAttribute]
        public int Index;

        [XmlAttribute]
        public int Cost;

        public Yaku()
        {

        }

        public Yaku(int I, int C)
        {
            Index = I;
            Cost = C;
        }
    }
}
