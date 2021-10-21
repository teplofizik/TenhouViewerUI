namespace TenhouViewerUI
{
    partial class fImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fImport));
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.lInfo = new System.Windows.Forms.Label();
            this.lbLog = new System.Windows.Forms.ListBox();
            this.tMain = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // pbProgress
            // 
            resources.ApplyResources(this.pbProgress, "pbProgress");
            this.pbProgress.Maximum = 1000;
            this.pbProgress.Name = "pbProgress";
            // 
            // lInfo
            // 
            resources.ApplyResources(this.lInfo, "lInfo");
            this.lInfo.Name = "lInfo";
            // 
            // lbLog
            // 
            this.lbLog.FormattingEnabled = true;
            resources.ApplyResources(this.lbLog, "lbLog");
            this.lbLog.Name = "lbLog";
            // 
            // tMain
            // 
            this.tMain.Tick += new System.EventHandler(this.tMain_Tick);
            // 
            // fImport
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbLog);
            this.Controls.Add(this.lInfo);
            this.Controls.Add(this.pbProgress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fImport";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.fImport_FormClosed);
            this.Load += new System.EventHandler(this.fImport_Load);
            this.Shown += new System.EventHandler(this.fImport_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pbProgress;
        private System.Windows.Forms.Label lInfo;
        private System.Windows.Forms.ListBox lbLog;
        private System.Windows.Forms.Timer tMain;
    }
}