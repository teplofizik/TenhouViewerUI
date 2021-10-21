using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Controls.Style
{
    public class CommonStyle : ControlStyle
    {
        public CommonStyle()
        {
            UnderMouse = Color.PaleGoldenrod;
            Active = Color.Khaki;
            Inactive = Color.LemonChiffon;
            Disabled = Color.DimGray;
        }
    }
}
