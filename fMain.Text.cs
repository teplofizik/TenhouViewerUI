using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Globalization;
using System.Resources;
using System.Threading;

namespace TenhouViewerUI
{
    public partial class fMain : Form
    {
        private string GetLocalizedString(string Name)
        {
            return rm.GetString(Name, System.Globalization.CultureInfo.CurrentCulture);
        }

        private string GetDefaultStatus()
        {
            return String.Format(GetLocalizedString("Status"), TP.Hashes.Count, TP.Loaded.Count);
        }
    }
}
