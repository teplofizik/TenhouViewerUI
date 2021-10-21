using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Controls.Style
{
    public class ModeStyle : ControlStyle
    {
        public ModeStyle()
        {
            UnderMouse = Color.Goldenrod;
            Active = Color.DarkGoldenrod;
            Inactive = Color.Gold;
            Disabled = Color.LightGreen;
        }
    }
}
