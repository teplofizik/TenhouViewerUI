using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Mahjong.Steps
{
    [Serializable]
    [XmlType("Naki")]
    class Naki : Step
    {
        public Sets.Set Set;

        public Naki()
            : base()
        {
        }

        public Naki(int P, Sets.Set S)
            : base(P)
        {
            Set = S;
        }

        public override StepType Type
        {
            get { return StepType.NAKI; }
        }

        public override string ToString()
        {
            return String.Format("Naki ({0:d}): {1:s}", Player, Set.ToString());
        }
    }
}
