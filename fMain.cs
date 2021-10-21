using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Resources;

using Project;

namespace TenhouViewerUI
{
    public partial class fMain : Form
    {
        const string DefaultProject = ".\\default\\";

        /// <summary>
        /// Для локализации
        /// </summary>
        private ResourceManager rm;

        /// <summary>
        /// Текущий проект
        /// </summary>
        private TenhouProject TP;

        public fMain()
        {
            InitializeComponent();

            rm = new ResourceManager("TenhouViewerUI.fMain", 
                System.Reflection.Assembly.GetExecutingAssembly());
        }

        private void fMain_Load(object sender, EventArgs e)
        {
            TP = TenhouProject.Load(DefaultProject);
            if (TP == null)
                TP = TenhouProject.Create(DefaultProject);

            Tag = TP;
            lStatus.Text = GetDefaultStatus();
            tTimer.Start();
        }

        private void tTimer_Tick(object sender, EventArgs e)
        {
            lStatus.Text = GetDefaultStatus();
            pbLoading.Value = Convert.ToInt32(TP.GetLoadingProgress());
        }

        private void fMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            tTimer.Stop();
        }

        private void mProjectImport_DropDownOpening(object sender, EventArgs e)
        {
            mProjectImportMyTenhou.Enabled = Checker.HasMyTenhouDir();
            mProjectImportRecent.Enabled = Checker.HasTenhouConfig();
        }

        private void mInfo_DropDownOpening(object sender, EventArgs e)
        {
            mInfoGamesList.Enabled = TP.ReplaysLoaded();
            mInfoPlayersList.Enabled = TP.ReplaysLoaded();
        }

        private void mTournier_DropDownOpening(object sender, EventArgs e)
        {
            mTournierResults.Enabled = TP.ReplaysLoaded();
        }

        private void mStat_DropDownOpening(object sender, EventArgs e)
        {
            mStatLast.Enabled = TP.ReplaysLoaded() && (TP.Owner != null);
            //mStatGraph.Enabled = TP.ReplaysLoaded() && (TP.Owner != null);
        }

        private void ShowChild(Form F)
        {
            F.MdiParent = this;
            F.Show();
        }

        private void mmInfoLoader_Click(object sender, EventArgs e)
        {
            ShowChild(new fImport());
        }

        private void mInfoGameList_Click(object sender, EventArgs e)
        {
            ShowChild(new fGames());
        }

        private void mInfoPlayersList_Click(object sender, EventArgs e)
        {
            ShowChild(new fPlayers());
        }

        private void mTournierResults_Click(object sender, EventArgs e)
        {
            ShowChild(new fTournier());
        }

        private void mStatLast_Click(object sender, EventArgs e)
        {
            ShowChild(new fLastStat());
        }

        private void mStatGraph_Click(object sender, EventArgs e)
        {
            ShowChild(new fGraph());
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowChild(new fReplayViewer());
        }

    }
}
