using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Mahjong.Steps
{
    [Serializable]
    [XmlType("Ron")]
    class Ron : Step
    {
        public int From;
       
        public Ron()
            : base()
        {
            From = -1;
        }

        public Ron(int P, int FromWho)
            : base(P)
        {
            From = FromWho;
        }

        public override StepType Type
        {
            get { return StepType.RON; }
        }

        public override string ToString()
        {
            return String.Format("Ron ({0:d})", Player);
        }
    }
}
