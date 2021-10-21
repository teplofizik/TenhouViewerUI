using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Mahjong;
using Renderer;
using Paifu;

namespace TenhouViewerUI
{
    public partial class fPaifuViewer : NestedForm
    {
        private string mHash;
        private Replay mR = null;
        private int mRound;

        public fPaifuViewer(string H, int R) : base()
        {
            mHash = H;
            mRound = R;

            InitializeComponent();
        }

        private void fPaifuViewer_Load(object sender, EventArgs e)
        {
            CheckButtons();
            CheckProject();

            Size = Settings.Default.PaifuSize;
            ckSex.Checked = Settings.Default.ShowSex;
            ckYaku.Checked = Settings.Default.ShowYaku;
            ckName.Checked = Settings.Default.ShowName;
            ckColor.Checked = Settings.Default.ShowColor;
            ckDanger.Checked = Settings.Default.ShowDanger;
            ckShanten.Checked = Settings.Default.ShowShanten;
        }                    
                             
        private void fPaifuViewer_FormClosed(object sender, FormClosedEventArgs e)
        {                    
            Settings.Default.PaifuSize = Size;
            Settings.Default.ShowSex = ckSex.Checked;
            Settings.Default.ShowYaku = ckYaku.Checked;
            Settings.Default.ShowName = ckName.Checked;
            Settings.Default.ShowColor = ckColor.Checked;
            Settings.Default.ShowDanger = ckDanger.Checked;
            Settings.Default.ShowShanten = ckShanten.Checked;

            Settings.Default.Save();
        }

        private void mYakuLang_DropDownOpening(object sender, EventArgs e)
        {
            string YakuLang = Settings.Default.YakuLang;
            mYakuLangEN.Checked = (YakuLang.CompareTo("en") == 0);
            mYakuLangJP.Checked = (YakuLang.CompareTo("jp") == 0);
            mYakuLangRU.Checked = (YakuLang.CompareTo("ru") == 0);
        }

        private void mYakuLanguage_Click(object sender, EventArgs e)
        {
            Settings.Default.YakuLang = (sender as ToolStripDropDownItem).Tag.ToString();

            UpdatePaifu();
        }
        /// <summary>
        /// При загрузке проекта
        /// </summary>
        protected override void OnProjectLoad()
        {
            Text = "Paifu viewer: " + mHash;
            mR = TP.GetGameReplay(mHash);

            cbRound.Items.Clear();
            foreach (var R in mR.Rounds)
            {
                R.ExpandData(mR);
                cbRound.Items.Add(R);
            }
            cbRound.SelectedIndex = 0;

            CheckButtons();
            UpdatePaifu();
        }

        /// <summary>
        /// Обновить картинку
        /// </summary>
        private void UpdatePaifu()
        {
            PaifuRenderer PR = new PaifuRenderer(mR, mR.Rounds[mRound]);
            PR.mShowColor = ckColor.Checked ? 1 : 0;
            PR.mShowNames = ckName.Checked ? 1 : 0;
            PR.mShowShanten = ckShanten.Checked ? 1 : 0;
            PR.mShowYakuInfo = ckYaku.Checked ? 1 : 0;
            PR.mShowDanger = ckDanger.Checked ? 1 : 0;
            PR.mShowSex = ckSex.Checked ? 1 : 0;
            PR.mLanguage = Settings.Default.YakuLang;

            pb.Image = PR.Draw();
            pb.Width = pb.Image.Width;
            pb.Height = pb.Image.Height;

            cbRound.SelectedIndex = mRound;
        }

        /// <summary>
        /// проверить доступность кнопок
        /// </summary>
        private void CheckButtons()
        {
            fbBack.Enabled = (mR != null) && (mRound > 0);
            fbForward.Enabled = (mR != null) && (mRound < mR.Rounds.Count - 1);
        }

        /// <summary>
        /// При выгрузе проекта
        /// </summary>
        protected override void OnProjectUnload()
        {

        }

        private void fPaifuViewer_Resize(object sender, EventArgs e)
        {
            pControl.Width = ClientSize.Width;
            pImage.Top = pControl.Height;
            pImage.Width = ClientSize.Width;
            pImage.Height = ClientSize.Height - pControl.Height;
        }

        private void fbBack_Click(object sender, EventArgs e)
        {
            if (mR == null) return;

            if (mRound > 0) mRound--;
            UpdatePaifu();
            CheckButtons();
        }

        private void cbRound_SelectedIndexChanged(object sender, EventArgs e)
        {
            mRound = cbRound.SelectedIndex;
            UpdatePaifu();
            CheckButtons();
        }

        private void fbForward_Click(object sender, EventArgs e)
        {
            if (mR == null) return;

            if (mRound < mR.Rounds.Count - 1) mRound++;
            UpdatePaifu();
            CheckButtons();
        }

        private void ckShanten_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePaifu();
        }

        private void fbSave_Click(object sender, EventArgs e)
        {
            if(dlgSave.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                pb.Image.Save(dlgSave.FileName, System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
