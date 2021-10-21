using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Mahjong.Steps
{
    [Serializable]
    [XmlType("Tsumo")]
    class Tsumo : Step
    {
       
        public Tsumo()
            : base()
        {

        }

        public Tsumo(int P)
            : base(P)
        {

        }

        public override StepType Type
        {
            get { return StepType.TSUMO; }
        }

        public override string ToString()
        {
            return String.Format("Tsumo ({0:d})", Player);
        }
    }
}
