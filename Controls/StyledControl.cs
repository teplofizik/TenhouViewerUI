using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Controls.Style;

namespace Controls
{
    class StyledControl : Label
    {
        private ControlStyle mStyle = new ControlStyle();

        public StyledControl()
        {
            AutoSize = false;
            TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            UpdateView();
        }

        public ControlStyle Style
        {
            get { return mStyle; }
            set { mStyle = value; UpdateView(); }
        }

        protected virtual void UpdateView()
        {

        }
    }
}
