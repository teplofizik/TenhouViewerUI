using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Controls.Style
{
    public class ControlStyle
    {
        /// <summary>
        /// Цвет активного контрола (мышь наведена)
        /// </summary>
        public Color UnderMouse = Color.LawnGreen;

        /// <summary>
        /// Цвет активного контрола
        /// </summary>
        public Color Active = Color.LightGreen;

        /// <summary>
        /// Цвет неактивного контрола
        /// </summary>
        public Color Inactive = Color.LightGray;

        /// <summary>
        /// Цвет отключённого контрола
        /// </summary>
        public Color Disabled = Color.DimGray;

    }
}
