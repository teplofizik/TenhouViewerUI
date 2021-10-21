using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Output;

namespace Mahjong.Steps
{
    [Serializable]
    [XmlType("Draw")]
    class Draw : Step
    {
        // 0: 9 terminals/honors
        // 1: 4 consecutive riichi calls
        // 2: three simultaneous ron calls (triple ron)
        // 3: four declared kans
        // 4: same wind discard on first round
        // 5: nagashi mangan (all terminal/honor discards)
        public int Reason;

        public Draw()
            : base()
        {

        }

        public Draw(int R)
            : base()
        {
            Reason = R;
        }

        public override StepType Type
        {
            get { return StepType.DRAW; }
        }

        public override string ToString()
        {
            return String.Format("Draw: ", Reason.ToDrawReason());
        }

    }
}
