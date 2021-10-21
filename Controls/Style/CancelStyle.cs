using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Controls.Style
{
    public class CancelStyle : ControlStyle
    {
        public CancelStyle()
        {
            UnderMouse = Color.DarkSalmon;
            Active = Color.Tomato;
            Inactive = Color.LightSalmon;
            Disabled = Color.DimGray;
        }
    }
}
