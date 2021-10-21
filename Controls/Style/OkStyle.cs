using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Controls.Style
{
    public class OkStyle : ControlStyle
    {
        public OkStyle()
        {
            UnderMouse = Color.LightGreen;
            Active = Color.LimeGreen;
            Inactive = Color.PaleGreen;
            Disabled = Color.DimGray;
        }
    }
}
