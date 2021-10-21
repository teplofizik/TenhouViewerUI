namespace TenhouViewerUI
{
    partial class fPlayers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fPlayers));
            this.lGames = new System.Windows.Forms.ListView();
            this.colNickname = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colRank = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colRating = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mPlayer = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mPlayerSetNick = new System.Windows.Forms.ToolStripMenuItem();
            this.mPlayer.SuspendLayout();
            this.SuspendLayout();
            // 
            // lGames
            // 
            this.lGames.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colNickname,
            this.colRank,
            this.colRating,
            this.colCount});
            this.lGames.ContextMenuStrip = this.mPlayer;
            this.lGames.FullRowSelect = true;
            resources.ApplyResources(this.lGames, "lGames");
            this.lGames.MultiSelect = false;
            this.lGames.Name = "lGames";
            this.lGames.UseCompatibleStateImageBehavior = false;
            this.lGames.View = System.Windows.Forms.View.Details;
            // 
            // colNickname
            // 
            resources.ApplyResources(this.colNickname, "colNickname");
            // 
            // colRank
            // 
            resources.ApplyResources(this.colRank, "colRank");
            // 
            // colRating
            // 
            resources.ApplyResources(this.colRating, "colRating");
            // 
            // colCount
            // 
            resources.ApplyResources(this.colCount, "colCount");
            // 
            // mPlayer
            // 
            this.mPlayer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mPlayerSetNick});
            this.mPlayer.Name = "mPlayer";
            resources.ApplyResources(this.mPlayer, "mPlayer");
            this.mPlayer.Opening += new System.ComponentModel.CancelEventHandler(this.mPlayer_Opening);
            // 
            // mPlayerSetNick
            // 
            this.mPlayerSetNick.Name = "mPlayerSetNick";
            resources.ApplyResources(this.mPlayerSetNick, "mPlayerSetNick");
            this.mPlayerSetNick.Click += new System.EventHandler(this.mPlayerSetNick_Click);
            // 
            // fPlayers
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lGames);
            this.Name = "fPlayers";
            this.Load += new System.EventHandler(this.fPlayers_Load);
            this.Resize += new System.EventHandler(this.fPlayers_Resize);
            this.mPlayer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lGames;
        private System.Windows.Forms.ColumnHeader colNickname;
        private System.Windows.Forms.ColumnHeader colCount;
        private System.Windows.Forms.ColumnHeader colRank;
        private System.Windows.Forms.ColumnHeader colRating;
        private System.Windows.Forms.ContextMenuStrip mPlayer;
        private System.Windows.Forms.ToolStripMenuItem mPlayerSetNick;
    }
}