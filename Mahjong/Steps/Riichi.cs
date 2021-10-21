using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Mahjong.Steps
{
    [Serializable]
    [XmlType("Riichi")]
    class Riichi : Step
    {
       
        public Riichi()
            : base()
        {

        }

        public Riichi(int P)
            : base(P)
        {

        }

        public override StepType Type
        {
            get { return StepType.RIICHI; }
        }

        public override string ToString()
        {
            return String.Format("Riichi ({0:d})", Player);
        }
    }
}
