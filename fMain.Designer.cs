namespace TenhouViewerUI
{
    partial class fMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fMain));
            this.mMenu = new System.Windows.Forms.MenuStrip();
            this.mProject = new System.Windows.Forms.ToolStripMenuItem();
            this.mProjectNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mProjectSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mProjectOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mProjectSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mProjectImport = new System.Windows.Forms.ToolStripMenuItem();
            this.mProjectImportRecent = new System.Windows.Forms.ToolStripMenuItem();
            this.mProjectImportSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mProjectImportMyTenhou = new System.Windows.Forms.ToolStripMenuItem();
            this.mProjectImportDir = new System.Windows.Forms.ToolStripMenuItem();
            this.mProjectImportMjlog = new System.Windows.Forms.ToolStripMenuItem();
            this.mProjectImportSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.mProjectImportLogFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mProjectImportLog = new System.Windows.Forms.ToolStripMenuItem();
            this.mProjectImportUrl = new System.Windows.Forms.ToolStripMenuItem();
            this.mInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.mmInfoLoader = new System.Windows.Forms.ToolStripMenuItem();
            this.mInfoGamesList = new System.Windows.Forms.ToolStripMenuItem();
            this.mInfoPlayersList = new System.Windows.Forms.ToolStripMenuItem();
            this.mTournier = new System.Windows.Forms.ToolStripMenuItem();
            this.mTournierResults = new System.Windows.Forms.ToolStripMenuItem();
            this.mStat = new System.Windows.Forms.ToolStripMenuItem();
            this.mStatLast = new System.Windows.Forms.ToolStripMenuItem();
            this.mStatGraph = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dlgOpenLog = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lFiller = new System.Windows.Forms.ToolStripStatusLabel();
            this.pbLoading = new System.Windows.Forms.ToolStripProgressBar();
            this.dlgOpenMjlog = new System.Windows.Forms.OpenFileDialog();
            this.tTimer = new System.Windows.Forms.Timer(this.components);
            this.dlgOpenSpecifiedFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.mMenu.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mMenu
            // 
            this.mMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mProject,
            this.mInfo,
            this.mTournier,
            this.mStat});
            resources.ApplyResources(this.mMenu, "mMenu");
            this.mMenu.Name = "mMenu";
            // 
            // mProject
            // 
            this.mProject.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mProjectNew,
            this.mProjectSave,
            this.mProjectOpen,
            this.mProjectSep1,
            this.mProjectImport});
            this.mProject.Name = "mProject";
            resources.ApplyResources(this.mProject, "mProject");
            // 
            // mProjectNew
            // 
            resources.ApplyResources(this.mProjectNew, "mProjectNew");
            this.mProjectNew.Name = "mProjectNew";
            // 
            // mProjectSave
            // 
            this.mProjectSave.Name = "mProjectSave";
            resources.ApplyResources(this.mProjectSave, "mProjectSave");
            this.mProjectSave.Click += new System.EventHandler(this.mProjectSave_Click);
            // 
            // mProjectOpen
            // 
            resources.ApplyResources(this.mProjectOpen, "mProjectOpen");
            this.mProjectOpen.Name = "mProjectOpen";
            // 
            // mProjectSep1
            // 
            this.mProjectSep1.Name = "mProjectSep1";
            resources.ApplyResources(this.mProjectSep1, "mProjectSep1");
            // 
            // mProjectImport
            // 
            this.mProjectImport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mProjectImportRecent,
            this.mProjectImportSep1,
            this.mProjectImportMyTenhou,
            this.mProjectImportDir,
            this.mProjectImportMjlog,
            this.mProjectImportSep2,
            this.mProjectImportLogFile,
            this.mProjectImportLog,
            this.mProjectImportUrl});
            this.mProjectImport.Name = "mProjectImport";
            resources.ApplyResources(this.mProjectImport, "mProjectImport");
            this.mProjectImport.DropDownOpening += new System.EventHandler(this.mProjectImport_DropDownOpening);
            // 
            // mProjectImportRecent
            // 
            this.mProjectImportRecent.Name = "mProjectImportRecent";
            resources.ApplyResources(this.mProjectImportRecent, "mProjectImportRecent");
            this.mProjectImportRecent.Click += new System.EventHandler(this.mProjectImportRecent_Click);
            // 
            // mProjectImportSep1
            // 
            this.mProjectImportSep1.Name = "mProjectImportSep1";
            resources.ApplyResources(this.mProjectImportSep1, "mProjectImportSep1");
            // 
            // mProjectImportMyTenhou
            // 
            this.mProjectImportMyTenhou.Name = "mProjectImportMyTenhou";
            resources.ApplyResources(this.mProjectImportMyTenhou, "mProjectImportMyTenhou");
            this.mProjectImportMyTenhou.Click += new System.EventHandler(this.mProjectImportMyTenhou_Click);
            // 
            // mProjectImportDir
            // 
            this.mProjectImportDir.Name = "mProjectImportDir";
            resources.ApplyResources(this.mProjectImportDir, "mProjectImportDir");
            this.mProjectImportDir.Click += new System.EventHandler(this.mProjectImportDir_Click);
            // 
            // mProjectImportMjlog
            // 
            this.mProjectImportMjlog.Name = "mProjectImportMjlog";
            resources.ApplyResources(this.mProjectImportMjlog, "mProjectImportMjlog");
            this.mProjectImportMjlog.Click += new System.EventHandler(this.mProjectImportMjlog_Click);
            // 
            // mProjectImportSep2
            // 
            this.mProjectImportSep2.Name = "mProjectImportSep2";
            resources.ApplyResources(this.mProjectImportSep2, "mProjectImportSep2");
            // 
            // mProjectImportLogFile
            // 
            this.mProjectImportLogFile.Name = "mProjectImportLogFile";
            resources.ApplyResources(this.mProjectImportLogFile, "mProjectImportLogFile");
            this.mProjectImportLogFile.Click += new System.EventHandler(this.mProjectImportLogFile_Click);
            // 
            // mProjectImportLog
            // 
            resources.ApplyResources(this.mProjectImportLog, "mProjectImportLog");
            this.mProjectImportLog.Name = "mProjectImportLog";
            // 
            // mProjectImportUrl
            // 
            resources.ApplyResources(this.mProjectImportUrl, "mProjectImportUrl");
            this.mProjectImportUrl.Name = "mProjectImportUrl";
            // 
            // mInfo
            // 
            this.mInfo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mmInfoLoader,
            this.mInfoGamesList,
            this.mInfoPlayersList});
            this.mInfo.Name = "mInfo";
            resources.ApplyResources(this.mInfo, "mInfo");
            this.mInfo.DropDownOpening += new System.EventHandler(this.mInfo_DropDownOpening);
            // 
            // mmInfoLoader
            // 
            this.mmInfoLoader.Name = "mmInfoLoader";
            resources.ApplyResources(this.mmInfoLoader, "mmInfoLoader");
            this.mmInfoLoader.Click += new System.EventHandler(this.mmInfoLoader_Click);
            // 
            // mInfoGamesList
            // 
            this.mInfoGamesList.Name = "mInfoGamesList";
            resources.ApplyResources(this.mInfoGamesList, "mInfoGamesList");
            this.mInfoGamesList.Click += new System.EventHandler(this.mInfoGameList_Click);
            // 
            // mInfoPlayersList
            // 
            this.mInfoPlayersList.Name = "mInfoPlayersList";
            resources.ApplyResources(this.mInfoPlayersList, "mInfoPlayersList");
            this.mInfoPlayersList.Click += new System.EventHandler(this.mInfoPlayersList_Click);
            // 
            // mTournier
            // 
            this.mTournier.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mTournierResults});
            this.mTournier.Name = "mTournier";
            resources.ApplyResources(this.mTournier, "mTournier");
            this.mTournier.DropDownOpening += new System.EventHandler(this.mTournier_DropDownOpening);
            // 
            // mTournierResults
            // 
            this.mTournierResults.Name = "mTournierResults";
            resources.ApplyResources(this.mTournierResults, "mTournierResults");
            this.mTournierResults.Click += new System.EventHandler(this.mTournierResults_Click);
            // 
            // mStat
            // 
            this.mStat.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mStatLast,
            this.mStatGraph,
            this.testToolStripMenuItem});
            this.mStat.Name = "mStat";
            resources.ApplyResources(this.mStat, "mStat");
            this.mStat.DropDownOpening += new System.EventHandler(this.mStat_DropDownOpening);
            // 
            // mStatLast
            // 
            this.mStatLast.Name = "mStatLast";
            resources.ApplyResources(this.mStatLast, "mStatLast");
            this.mStatLast.Click += new System.EventHandler(this.mStatLast_Click);
            // 
            // mStatGraph
            // 
            this.mStatGraph.Name = "mStatGraph";
            resources.ApplyResources(this.mStatGraph, "mStatGraph");
            this.mStatGraph.Click += new System.EventHandler(this.mStatGraph_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            resources.ApplyResources(this.testToolStripMenuItem, "testToolStripMenuItem");
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // dlgOpenLog
            // 
            resources.ApplyResources(this.dlgOpenLog, "dlgOpenLog");
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lStatus,
            this.lFiller,
            this.pbLoading});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // lStatus
            // 
            this.lStatus.Name = "lStatus";
            resources.ApplyResources(this.lStatus, "lStatus");
            // 
            // lFiller
            // 
            this.lFiller.Name = "lFiller";
            resources.ApplyResources(this.lFiller, "lFiller");
            this.lFiller.Spring = true;
            // 
            // pbLoading
            // 
            this.pbLoading.Name = "pbLoading";
            resources.ApplyResources(this.pbLoading, "pbLoading");
            // 
            // dlgOpenMjlog
            // 
            resources.ApplyResources(this.dlgOpenMjlog, "dlgOpenMjlog");
            // 
            // tTimer
            // 
            this.tTimer.Tick += new System.EventHandler(this.tTimer_Tick);
            // 
            // dlgOpenSpecifiedFolder
            // 
            resources.ApplyResources(this.dlgOpenSpecifiedFolder, "dlgOpenSpecifiedFolder");
            // 
            // fMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.mMenu);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mMenu;
            this.Name = "fMain";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.fMain_FormClosed);
            this.Load += new System.EventHandler(this.fMain_Load);
            this.mMenu.ResumeLayout(false);
            this.mMenu.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mMenu;
        private System.Windows.Forms.ToolStripMenuItem mProject;
        private System.Windows.Forms.ToolStripMenuItem mProjectNew;
        private System.Windows.Forms.ToolStripMenuItem mProjectOpen;
        private System.Windows.Forms.ToolStripSeparator mProjectSep1;
        private System.Windows.Forms.ToolStripMenuItem mProjectImport;
        private System.Windows.Forms.ToolStripMenuItem mProjectImportRecent;
        private System.Windows.Forms.ToolStripMenuItem mProjectImportMyTenhou;
        private System.Windows.Forms.ToolStripMenuItem mProjectImportLogFile;
        private System.Windows.Forms.ToolStripMenuItem mProjectImportLog;
        private System.Windows.Forms.ToolStripMenuItem mProjectImportUrl;
        private System.Windows.Forms.ToolStripSeparator mProjectImportSep1;
        private System.Windows.Forms.ToolStripMenuItem mProjectImportDir;
        private System.Windows.Forms.ToolStripSeparator mProjectImportSep2;
        private System.Windows.Forms.ToolStripMenuItem mProjectImportMjlog;
        private System.Windows.Forms.OpenFileDialog dlgOpenLog;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lStatus;
        private System.Windows.Forms.ToolStripMenuItem mProjectSave;
        private System.Windows.Forms.OpenFileDialog dlgOpenMjlog;
        private System.Windows.Forms.Timer tTimer;
        private System.Windows.Forms.ToolStripMenuItem mInfo;
        private System.Windows.Forms.ToolStripMenuItem mmInfoLoader;
        private System.Windows.Forms.FolderBrowserDialog dlgOpenSpecifiedFolder;
        private System.Windows.Forms.ToolStripMenuItem mInfoGamesList;
        private System.Windows.Forms.ToolStripStatusLabel lFiller;
        private System.Windows.Forms.ToolStripProgressBar pbLoading;
        private System.Windows.Forms.ToolStripMenuItem mInfoPlayersList;
        private System.Windows.Forms.ToolStripMenuItem mTournier;
        private System.Windows.Forms.ToolStripMenuItem mTournierResults;
        private System.Windows.Forms.ToolStripMenuItem mStat;
        private System.Windows.Forms.ToolStripMenuItem mStatLast;
        private System.Windows.Forms.ToolStripMenuItem mStatGraph;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
    }
}

