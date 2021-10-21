using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Mahjong.Steps
{
    public enum StepType
    {
        NONE,
        CONNECT,
        DISCONNECT,
        DRAWTILE,
        DISCARDTILE,
        TSUMO,
        RON,
        NAKI,
        DRAW,
        RIICHI,
        RIICHI1000,
        DORA
    }

    [Serializable]
    [XmlType("Step")]
    public class Step
    {
        // Чьё действие?
        public int Player;

        public Step()
        {
            Player = -1;
        }

        public Step(int P)
        {
            Player = P;
        }

        public virtual StepType Type
        {
            get { return StepType.NONE; }
        }

        public override string ToString()
        {
            return String.Format("Step ({0:d})", Player);
        }
    }
}
