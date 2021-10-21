namespace TenhouViewerUI
{
    partial class fPaifuViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fPaifuViewer));
            Controls.Style.ControlStyle controlStyle1 = new Controls.Style.ControlStyle();
            Controls.Style.ControlStyle controlStyle2 = new Controls.Style.ControlStyle();
            Controls.Style.ControlStyle controlStyle3 = new Controls.Style.ControlStyle();
            this.pb = new System.Windows.Forms.PictureBox();
            this.mYaku = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mYakuLang = new System.Windows.Forms.ToolStripMenuItem();
            this.mYakuLangJP = new System.Windows.Forms.ToolStripMenuItem();
            this.mYakuLangEN = new System.Windows.Forms.ToolStripMenuItem();
            this.mYakuLangRU = new System.Windows.Forms.ToolStripMenuItem();
            this.pImage = new System.Windows.Forms.Panel();
            this.pControl = new System.Windows.Forms.Panel();
            this.ckDanger = new System.Windows.Forms.CheckBox();
            this.ckSex = new System.Windows.Forms.CheckBox();
            this.fbSave = new Controls.FlatButton();
            this.ckYaku = new System.Windows.Forms.CheckBox();
            this.ckColor = new System.Windows.Forms.CheckBox();
            this.ckName = new System.Windows.Forms.CheckBox();
            this.ckShanten = new System.Windows.Forms.CheckBox();
            this.cbRound = new System.Windows.Forms.ComboBox();
            this.fbForward = new Controls.FlatButton();
            this.fbBack = new Controls.FlatButton();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).BeginInit();
            this.mYaku.SuspendLayout();
            this.pImage.SuspendLayout();
            this.pControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // pb
            // 
            this.pb.ContextMenuStrip = this.mYaku;
            resources.ApplyResources(this.pb, "pb");
            this.pb.Name = "pb";
            this.pb.TabStop = false;
            // 
            // mYaku
            // 
            this.mYaku.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mYakuLang});
            this.mYaku.Name = "mYaku";
            resources.ApplyResources(this.mYaku, "mYaku");
            // 
            // mYakuLang
            // 
            this.mYakuLang.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mYakuLangJP,
            this.mYakuLangEN,
            this.mYakuLangRU});
            this.mYakuLang.Name = "mYakuLang";
            resources.ApplyResources(this.mYakuLang, "mYakuLang");
            this.mYakuLang.DropDownOpening += new System.EventHandler(this.mYakuLang_DropDownOpening);
            // 
            // mYakuLangJP
            // 
            this.mYakuLangJP.Name = "mYakuLangJP";
            resources.ApplyResources(this.mYakuLangJP, "mYakuLangJP");
            this.mYakuLangJP.Tag = "jp";
            this.mYakuLangJP.Click += new System.EventHandler(this.mYakuLanguage_Click);
            // 
            // mYakuLangEN
            // 
            this.mYakuLangEN.Name = "mYakuLangEN";
            resources.ApplyResources(this.mYakuLangEN, "mYakuLangEN");
            this.mYakuLangEN.Tag = "en";
            this.mYakuLangEN.Click += new System.EventHandler(this.mYakuLanguage_Click);
            // 
            // mYakuLangRU
            // 
            this.mYakuLangRU.Name = "mYakuLangRU";
            resources.ApplyResources(this.mYakuLangRU, "mYakuLangRU");
            this.mYakuLangRU.Tag = "ru";
            this.mYakuLangRU.Click += new System.EventHandler(this.mYakuLanguage_Click);
            // 
            // pImage
            // 
            resources.ApplyResources(this.pImage, "pImage");
            this.pImage.Controls.Add(this.pb);
            this.pImage.Name = "pImage";
            // 
            // pControl
            // 
            this.pControl.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pControl.Controls.Add(this.ckDanger);
            this.pControl.Controls.Add(this.ckSex);
            this.pControl.Controls.Add(this.fbSave);
            this.pControl.Controls.Add(this.ckYaku);
            this.pControl.Controls.Add(this.ckColor);
            this.pControl.Controls.Add(this.ckName);
            this.pControl.Controls.Add(this.ckShanten);
            this.pControl.Controls.Add(this.cbRound);
            this.pControl.Controls.Add(this.fbForward);
            this.pControl.Controls.Add(this.fbBack);
            resources.ApplyResources(this.pControl, "pControl");
            this.pControl.Name = "pControl";
            // 
            // ckDanger
            // 
            resources.ApplyResources(this.ckDanger, "ckDanger");
            this.ckDanger.Checked = true;
            this.ckDanger.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckDanger.Name = "ckDanger";
            this.ckDanger.UseVisualStyleBackColor = true;
            this.ckDanger.CheckedChanged += new System.EventHandler(this.ckShanten_CheckedChanged);
            // 
            // ckSex
            // 
            resources.ApplyResources(this.ckSex, "ckSex");
            this.ckSex.Checked = true;
            this.ckSex.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckSex.Name = "ckSex";
            this.ckSex.UseVisualStyleBackColor = true;
            this.ckSex.CheckedChanged += new System.EventHandler(this.ckShanten_CheckedChanged);
            // 
            // fbSave
            // 
            this.fbSave.BackColor = System.Drawing.Color.LightGray;
            this.fbSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.fbSave.FlatAppearance.BorderSize = 0;
            this.fbSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGreen;
            resources.ApplyResources(this.fbSave, "fbSave");
            this.fbSave.Name = "fbSave";
            this.fbSave.Style = controlStyle1;
            this.fbSave.UseVisualStyleBackColor = false;
            this.fbSave.Click += new System.EventHandler(this.fbSave_Click);
            // 
            // ckYaku
            // 
            resources.ApplyResources(this.ckYaku, "ckYaku");
            this.ckYaku.Checked = true;
            this.ckYaku.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckYaku.Name = "ckYaku";
            this.ckYaku.UseVisualStyleBackColor = true;
            this.ckYaku.CheckedChanged += new System.EventHandler(this.ckShanten_CheckedChanged);
            // 
            // ckColor
            // 
            resources.ApplyResources(this.ckColor, "ckColor");
            this.ckColor.Checked = true;
            this.ckColor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckColor.Name = "ckColor";
            this.ckColor.UseVisualStyleBackColor = true;
            this.ckColor.CheckedChanged += new System.EventHandler(this.ckShanten_CheckedChanged);
            // 
            // ckName
            // 
            resources.ApplyResources(this.ckName, "ckName");
            this.ckName.Checked = true;
            this.ckName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckName.Name = "ckName";
            this.ckName.UseVisualStyleBackColor = true;
            this.ckName.CheckedChanged += new System.EventHandler(this.ckShanten_CheckedChanged);
            // 
            // ckShanten
            // 
            resources.ApplyResources(this.ckShanten, "ckShanten");
            this.ckShanten.Name = "ckShanten";
            this.ckShanten.UseVisualStyleBackColor = true;
            this.ckShanten.CheckedChanged += new System.EventHandler(this.ckShanten_CheckedChanged);
            // 
            // cbRound
            // 
            this.cbRound.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbRound, "cbRound");
            this.cbRound.FormattingEnabled = true;
            this.cbRound.Name = "cbRound";
            this.cbRound.SelectedIndexChanged += new System.EventHandler(this.cbRound_SelectedIndexChanged);
            // 
            // fbForward
            // 
            this.fbForward.BackColor = System.Drawing.Color.LightGray;
            this.fbForward.Cursor = System.Windows.Forms.Cursors.Hand;
            this.fbForward.FlatAppearance.BorderSize = 0;
            this.fbForward.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGreen;
            resources.ApplyResources(this.fbForward, "fbForward");
            this.fbForward.Name = "fbForward";
            this.fbForward.Style = controlStyle2;
            this.fbForward.UseVisualStyleBackColor = false;
            this.fbForward.Click += new System.EventHandler(this.fbForward_Click);
            // 
            // fbBack
            // 
            this.fbBack.BackColor = System.Drawing.Color.LightGray;
            this.fbBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.fbBack.FlatAppearance.BorderSize = 0;
            this.fbBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGreen;
            resources.ApplyResources(this.fbBack, "fbBack");
            this.fbBack.Name = "fbBack";
            this.fbBack.Style = controlStyle3;
            this.fbBack.UseVisualStyleBackColor = false;
            this.fbBack.Click += new System.EventHandler(this.fbBack_Click);
            // 
            // dlgSave
            // 
            resources.ApplyResources(this.dlgSave, "dlgSave");
            // 
            // fPaifuViewer
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pControl);
            this.Controls.Add(this.pImage);
            this.Name = "fPaifuViewer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.fPaifuViewer_FormClosed);
            this.Load += new System.EventHandler(this.fPaifuViewer_Load);
            this.Resize += new System.EventHandler(this.fPaifuViewer_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pb)).EndInit();
            this.mYaku.ResumeLayout(false);
            this.pImage.ResumeLayout(false);
            this.pControl.ResumeLayout(false);
            this.pControl.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pb;
        private System.Windows.Forms.Panel pImage;
        private System.Windows.Forms.Panel pControl;
        private Controls.FlatButton fbBack;
        private Controls.FlatButton fbForward;
        private System.Windows.Forms.ComboBox cbRound;
        private System.Windows.Forms.CheckBox ckShanten;
        private System.Windows.Forms.CheckBox ckName;
        private System.Windows.Forms.CheckBox ckColor;
        private System.Windows.Forms.CheckBox ckYaku;
        private Controls.FlatButton fbSave;
        private System.Windows.Forms.SaveFileDialog dlgSave;
        private System.Windows.Forms.CheckBox ckSex;
        private System.Windows.Forms.CheckBox ckDanger;
        private System.Windows.Forms.ContextMenuStrip mYaku;
        private System.Windows.Forms.ToolStripMenuItem mYakuLang;
        private System.Windows.Forms.ToolStripMenuItem mYakuLangJP;
        private System.Windows.Forms.ToolStripMenuItem mYakuLangEN;
        private System.Windows.Forms.ToolStripMenuItem mYakuLangRU;
    }
}