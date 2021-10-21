using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Extension.Forms
{
    public static class ProgressBarColor
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
        private static void SetState(this ProgressBar pBar, int state)
        {
            SendMessage(pBar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
        }

        /// <summary>
        /// Установить обычный цвет прогресс бара (зелёный)
        /// </summary>
        /// <param name="pBar"></param>
        public static void SetColorNormal(this ProgressBar pBar)
        {
            SetState(pBar, 1);
        }

        /// <summary>
        /// Установить предупреждающий цвет прогресс бара (жёлтый)
        /// </summary>
        /// <param name="pBar"></param>
        public static void SetColorWarning(this ProgressBar pBar)
        {
            SetState(pBar, 3);
        }

        /// <summary>
        /// Установить цвет прогресс бара, сопутствующий ошибке (красный)
        /// </summary>
        /// <param name="pBar"></param>
        public static void SetColorError(this ProgressBar pBar)
        {
            SetState(pBar, 2);
        }
    }
}
