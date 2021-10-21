using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Mahjong.Steps
{
    [Serializable]
    [XmlType("Connect")]
    class Connect : Step
    {
       
        public Connect()
            : base()
        {

        }

        public Connect(int P)
            : base(P)
        {

        }

        public override StepType Type
        {
            get { return StepType.CONNECT; }
        }

        public override string ToString()
        {
            return String.Format("Connect ({0:d})", Player);
        }
    }
}
