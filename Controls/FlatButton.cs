using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Controls.Style;

namespace Controls
{
    class FlatButton : System.Windows.Forms.Button
    {
        private ControlStyle mStyle;

        public FlatButton()
        {
            FlatStyle = FlatStyle.Flat;

            mStyle = new ControlStyle();
            UpdateView();
        }

        public FlatButton(ControlStyle Style)
        {
            mStyle = Style;
            UpdateView();
        }

        public ControlStyle Style
        {
            get { return mStyle; }
            set { mStyle = value; UpdateView(); }
        }

        /// <summary>
        /// Обновить цвета
        /// </summary>
        protected void UpdateView()
        {
            if (Enabled)
            {
                BackColor = mStyle.Inactive;
                FlatAppearance.BorderSize = 0;
                FlatAppearance.MouseDownBackColor = mStyle.UnderMouse;
                FlatAppearance.MouseDownBackColor = mStyle.Active;
                Cursor = Cursors.Hand;
            }
            else
            {
                BackColor = mStyle.Disabled;
                Cursor = Cursors.Default;
            }
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
 	         base.OnEnabledChanged(e);
             UpdateView();
        }
    }
}
