using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Mahjong.Steps
{
    [Serializable]
    [XmlType("Riichi")]
    class Riichi1000 : Step
    {
       
        public Riichi1000()
            : base()
        {

        }

        public Riichi1000(int P)
            : base(P)
        {

        }

        public override StepType Type
        {
            get { return StepType.RIICHI1000; }
        }

        public override string ToString()
        {
            return String.Format("Riichi 1000 ({0:d})", Player);
        }
    }
}
