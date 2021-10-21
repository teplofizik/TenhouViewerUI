using Controls;
namespace TenhouViewerUI
{
    partial class fGames
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fGames));
            this.lGames = new Controls.AdvancedListView();
            this.colDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLobby = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPlayer1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPlayer2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPlayer3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPlayer4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mViewPlayer1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mViewPlayer2 = new System.Windows.Forms.ToolStripMenuItem();
            this.mViewPlayer3 = new System.Windows.Forms.ToolStripMenuItem();
            this.mViewPlayer4 = new System.Windows.Forms.ToolStripMenuItem();
            this.mViewSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mViewPaifu = new System.Windows.Forms.ToolStripMenuItem();
            this.mView.SuspendLayout();
            this.SuspendLayout();
            // 
            // lGames
            // 
            this.lGames.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDate,
            this.colLobby,
            this.colPlayer1,
            this.colPlayer2,
            this.colPlayer3,
            this.colPlayer4});
            this.lGames.ContextMenuStrip = this.mView;
            this.lGames.FullRowSelect = true;
            resources.ApplyResources(this.lGames, "lGames");
            this.lGames.MultiSelect = false;
            this.lGames.Name = "lGames";
            this.lGames.UseCompatibleStateImageBehavior = false;
            this.lGames.View = System.Windows.Forms.View.Details;
            // 
            // colDate
            // 
            resources.ApplyResources(this.colDate, "colDate");
            // 
            // colLobby
            // 
            resources.ApplyResources(this.colLobby, "colLobby");
            // 
            // colPlayer1
            // 
            resources.ApplyResources(this.colPlayer1, "colPlayer1");
            // 
            // colPlayer2
            // 
            resources.ApplyResources(this.colPlayer2, "colPlayer2");
            // 
            // colPlayer3
            // 
            resources.ApplyResources(this.colPlayer3, "colPlayer3");
            // 
            // colPlayer4
            // 
            resources.ApplyResources(this.colPlayer4, "colPlayer4");
            // 
            // mView
            // 
            this.mView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mViewPaifu,
            this.mViewSep1,
            this.mViewPlayer1,
            this.mViewPlayer2,
            this.mViewPlayer3,
            this.mViewPlayer4});
            this.mView.Name = "mView";
            resources.ApplyResources(this.mView, "mView");
            this.mView.Opening += new System.ComponentModel.CancelEventHandler(this.mView_Opening);
            // 
            // mViewPlayer1
            // 
            this.mViewPlayer1.Name = "mViewPlayer1";
            resources.ApplyResources(this.mViewPlayer1, "mViewPlayer1");
            this.mViewPlayer1.Tag = "0";
            this.mViewPlayer1.Click += new System.EventHandler(this.mViewPlayer_Click);
            // 
            // mViewPlayer2
            // 
            this.mViewPlayer2.Name = "mViewPlayer2";
            resources.ApplyResources(this.mViewPlayer2, "mViewPlayer2");
            this.mViewPlayer2.Tag = "1";
            this.mViewPlayer2.Click += new System.EventHandler(this.mViewPlayer_Click);
            // 
            // mViewPlayer3
            // 
            this.mViewPlayer3.Name = "mViewPlayer3";
            resources.ApplyResources(this.mViewPlayer3, "mViewPlayer3");
            this.mViewPlayer3.Tag = "2";
            this.mViewPlayer3.Click += new System.EventHandler(this.mViewPlayer_Click);
            // 
            // mViewPlayer4
            // 
            this.mViewPlayer4.Name = "mViewPlayer4";
            resources.ApplyResources(this.mViewPlayer4, "mViewPlayer4");
            this.mViewPlayer4.Tag = "3";
            this.mViewPlayer4.Click += new System.EventHandler(this.mViewPlayer_Click);
            // 
            // mViewSep1
            // 
            this.mViewSep1.Name = "mViewSep1";
            resources.ApplyResources(this.mViewSep1, "mViewSep1");
            // 
            // mViewPaifu
            // 
            this.mViewPaifu.Name = "mViewPaifu";
            resources.ApplyResources(this.mViewPaifu, "mViewPaifu");
            this.mViewPaifu.Click += new System.EventHandler(this.mViewPaifu_Click);
            // 
            // fGames
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lGames);
            this.DoubleBuffered = true;
            this.Name = "fGames";
            this.Load += new System.EventHandler(this.fGames_Load);
            this.Resize += new System.EventHandler(this.fGames_Resize);
            this.mView.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AdvancedListView lGames;
        private System.Windows.Forms.ColumnHeader colDate;
        private System.Windows.Forms.ColumnHeader colLobby;
        private System.Windows.Forms.ColumnHeader colPlayer1;
        private System.Windows.Forms.ColumnHeader colPlayer2;
        private System.Windows.Forms.ColumnHeader colPlayer3;
        private System.Windows.Forms.ColumnHeader colPlayer4;
        private System.Windows.Forms.ContextMenuStrip mView;
        private System.Windows.Forms.ToolStripMenuItem mViewPlayer1;
        private System.Windows.Forms.ToolStripMenuItem mViewPlayer2;
        private System.Windows.Forms.ToolStripMenuItem mViewPlayer3;
        private System.Windows.Forms.ToolStripMenuItem mViewPlayer4;
        private System.Windows.Forms.ToolStripMenuItem mViewPaifu;
        private System.Windows.Forms.ToolStripSeparator mViewSep1;
    }
}