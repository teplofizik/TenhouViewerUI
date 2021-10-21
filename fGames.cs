using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Project;
using Mahjong;
using Viewer;

namespace TenhouViewerUI
{
    public partial class fGames : NestedForm
    {
        double[] Cols = { 14.0, 6.0, 20.0, 20.0, 20.0, 20.0 };

        /// <summary>
        /// Добавить игру в список
        /// </summary>
        private ListViewItem GetItemForGame(GameInfo GI)
        {
            ListViewItem I = new ListViewItem();

            I.Tag = GI;
            I.BackColor = GI.BackColor;
            I.Text = GI.R.R.Date.ToString();

            I.SubItems.Add(GI.R.R.Lobby.ToString("0000")); // Lobby
            for (int i = 0; i < 4; i++) I.SubItems.Add(GI.GetPlayerInfo(i));

            return I;
        }

        private GameInfo GetSelectedGame()
        {
            return (lGames.SelectedItems.Count > 0) ? lGames.SelectedItems[0].Tag as GameInfo : null;
        }

        /// <summary>
        /// Загрузить игры
        /// </summary>
        private void LoadGames()
        {
            List<ListViewItem> Items = new List<ListViewItem>();
            for (int i = 0; i < TP.Hashes.Count; i++)
            {
                Replay R = TP.GetGameReplay(TP.Hashes[i]);
                if(R != null) Items.Add(GetItemForGame(new GameInfo(R)));
            }

            lGames.BeginUpdate();
            lGames.Items.AddRange(Items.ToArray());
            lGames.EndUpdate();
        }

        public fGames()
        {
            InitializeComponent();
        }

        private void fGames_Resize(object sender, EventArgs e)
        {
            lGames.Width = ClientSize.Width - 24;
            lGames.Height = ClientSize.Height - lGames.Top - 12;

            ResizeColumns(lGames, Cols);
        }

        private void fGames_Load(object sender, EventArgs e)
        {
            ResizeColumns(lGames, Cols);
            CheckProject();

            LoadGames();
        }

        private void mView_Opening(object sender, CancelEventArgs e)
        {
            var GI = GetSelectedGame();
            if (GI == null)
            {
                e.Cancel = true;
                return;
            }

            // Запишем ники игроков
            mViewPlayer1.Text = GI.GetPlayerRankedInfo(0);
            mViewPlayer2.Text = GI.GetPlayerRankedInfo(1);
            mViewPlayer3.Text = GI.GetPlayerRankedInfo(2);
            mViewPlayer4.Text = GI.GetPlayerRankedInfo(3);
            mViewPlayer4.Visible = GI.R.R.PlayerCount > 3;
        }

        private void mViewPlayer_Click(object sender, EventArgs e)
        {
            var GI = GetSelectedGame();
            if (GI == null) return;

            int Player = Convert.ToInt32(((sender as ToolStripDropDownItem).Tag));

            TP.ViewReplayExternal(GI.R.R.Hash, Player);
        }

        private void mViewPaifu_Click(object sender, EventArgs e)
        {
            var GI = GetSelectedGame();
            if (GI == null) return;

            Form F = new fPaifuViewer(GI.R.Hash, 0);
            F.MdiParent = MdiParent;
            F.Show();
        }
    }
}
