using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Project;

namespace TenhouViewerUI
{
    public partial class fImport : NestedForm
    {
        private LoaderEventHandler onCompleted;

        public fImport()
        {
            InitializeComponent();

            onCompleted = new LoaderEventHandler(L_onCompleted);
        }

        /// <summary>
        /// При загрузке проекта
        /// </summary>
        protected override void OnProjectLoad()
        {
            TP.L.onCompleted += onCompleted;
        }

        /// <summary>
        /// При выгрузке проекта
        /// </summary>
        protected override void OnProjectUnload()
        {
            TP.L.onCompleted -= onCompleted;
        }

        private void L_onCompleted(object sender, LoaderEventArgs e)
        {
            BeginInvoke((MethodInvoker)delegate()
            {
                lbLog.Items.Add(e.Hash + (e.Ok ? ": ok" : ": error (" + e.Error + ")"));
            });
        }

        private void UpdateStatus()
        {
            pbProgress.Value = Convert.ToInt32(TP.L.GetProgress() * pbProgress.Maximum / 100.0);
            lInfo.Text = String.Format("{0:s} ({1:d}/{2:d})", TP.L.GetStatus(), TP.L.GetCompletedCount(), TP.L.GetCount());
        }

        private void fImport_Load(object sender, EventArgs e)
        {
            tMain.Start();
        }

        private void fImport_FormClosed(object sender, FormClosedEventArgs e)
        {
            tMain.Stop();
            TP.L.onCompleted -= onCompleted;
        }

        private void tMain_Tick(object sender, EventArgs e)
        {
            CheckProject();
            UpdateStatus();
        }

        private void fImport_Shown(object sender, EventArgs e)
        {
            CheckProject();
            UpdateStatus();
        }
    }
}
