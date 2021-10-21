using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Mahjong.Steps
{

    [Serializable]
    [XmlType("Disconnect")]
    class Disconnect : Step
    {
       
        public Disconnect()
            : base()
        {

        }

        public Disconnect(int P)
            : base(P)
        {

        }

        public override StepType Type
        {
            get { return StepType.DISCONNECT; }
        }

        public override string ToString()
        {
            return String.Format("Disconnect ({0:d})", Player);
        }
    }
}
