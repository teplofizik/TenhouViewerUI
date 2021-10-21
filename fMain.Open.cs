using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.IO;

using Project;

namespace TenhouViewerUI
{
    public partial class fMain : Form
    {
        private void mProjectImportLogFile_Click(object sender, EventArgs e)
        {
            if (dlgOpenLog.ShowDialog() == DialogResult.OK)
            {
                foreach (string H in LogParser.FromFile(dlgOpenLog.FileName))
                {
                    if (TP.IsGameExists(H)) continue;
                    TP.AddWeb(H);
                }

                lStatus.Text = GetDefaultStatus();
            }
        }

        private void mProjectImportMjlog_Click(object sender, EventArgs e)
        {
            if (dlgOpenMjlog.ShowDialog() == DialogResult.OK)
                TP.AddMjLog(dlgOpenMjlog.FileName);
        }

        private void mProjectImportMyTenhou_Click(object sender, EventArgs e)
        {
            string[] Files = Directory.GetFiles(Binder.GetMyTenhouPath(), "*.mjlog", SearchOption.AllDirectories);
            foreach (string F in Files) TP.AddMjLog(F);
        }

        private void mProjectImportDir_Click(object sender, EventArgs e)
        {
            if (dlgOpenSpecifiedFolder.ShowDialog() == DialogResult.OK)
            {
                string[] Files = Directory.GetFiles(dlgOpenSpecifiedFolder.SelectedPath, "*.mjlog", SearchOption.AllDirectories);
                foreach (string F in Files) TP.AddMjLog(F);
            }
        }

        private void mProjectImportRecent_Click(object sender, EventArgs e)
        {
            string[] Lines = File.ReadAllLines(Binder.GetTenhouConfigFileName());

            foreach (string F in Lines)
            {
                int Start = F.IndexOf("file=");
                if (Start < 0) continue;
                int End = F.IndexOf('&');

                string Hash = F.Substring(Start + 5, End - Start - 5);
                string FN = Binder.GetLocalReplayFileName(Hash);

                if (FN != null)
                    TP.AddMjLog(FN);
                else
                    TP.AddWeb(Hash);
            }
        }

        private void mProjectSave_Click(object sender, EventArgs e)
        {
            TP.Save();
        }
    }
}
