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
using Search;
using System.IO;

namespace TenhouViewerUI
{
    public partial class fTournier : NestedForm
    {
        private Color CActive = Color.Transparent;
        private Color CBanned = Color.LightPink;
        private Color CReplacement = Color.Yellow;

        private double[] GamesCols = { 14.0, 6.0, 20.0, 20.0, 20.0, 20.0 };
        private double[] PlayersCols = { 60.0, 10.0, 10.0, 20.0 };
        private double[] ResultCols = { 20.0, 8.0, 10.0, 10.0, 10.0, 10.0, 8.0, 8.0, 8.0, 8.0 };
        private double[] YakuCols = { 80.0, 20.0 };
        private ToolStripMenuItem[] PlayerView, PlayerReplay, PlayerExclude, PlayerInclude,
            PlayerMarkR, PlayerUnmarkR;

        private ResultPack RP;

        public fTournier()
        {
            InitializeComponent();

            PlayerView = new ToolStripMenuItem[] { mViewPlayer1, mViewPlayer2, mViewPlayer3, mViewPlayer4 };
            PlayerReplay = new ToolStripMenuItem[] { mViewPlayer1Replay, mViewPlayer2Replay, mViewPlayer3Replay, mViewPlayer4Replay };
            PlayerExclude = new ToolStripMenuItem[] { mViewPlayer1Exclude, mViewPlayer2Exclude, mViewPlayer3Exclude, mViewPlayer4Exclude };
            PlayerInclude = new ToolStripMenuItem[] { mViewPlayer1Include, mViewPlayer2Include, mViewPlayer3Include, mViewPlayer4Include };

            PlayerMarkR = new ToolStripMenuItem[] { mViewPlayer1MarkReplacement, mViewPlayer2MarkReplacement, mViewPlayer3MarkReplacement, mViewPlayer4MarkReplacement };
            PlayerUnmarkR = new ToolStripMenuItem[] { mViewPlayer1UnmarkReplacement, mViewPlayer2UnmarkReplacement, mViewPlayer3UnmarkReplacement, mViewPlayer4UnmarkReplacement };
        }

        /// <summary>
        /// Игрок забанен?
        /// </summary>
        /// <param name="GI"></param>
        /// <param name="Player"></param>
        /// <returns></returns>
        private bool IsPlayerBanned(GameInfo GI, int Player)
        {
            if (GI.R.PlayerMark[Player]) return true;
            if ((RP.UserField != null) && (RP.UserField as PlayerManage).Banned.Contains(GI.R.R.Players[Player].NickName))
                return true;

            return false;
        }

        /// <summary>
        /// Игрок замены?
        /// </summary>
        /// <param name="GI"></param>
        /// <param name="Player"></param>
        /// <returns></returns>
        private bool IsPlayerReplacement(GameInfo GI, int Player)
        {
            PlayerManage PM = RP.UserField as PlayerManage;
            return (PM.Replacements.Contains(GI.R.R.Players[Player].NickName))
                   || PM.IsReplacementPlayer(GI.R.Hash, Player);
        }

        /// <summary>
        /// Добавить игру в список
        /// </summary>
        private void AddGame(GameInfo GI)
        {
            ListViewItem I = new ListViewItem();

            I.BackColor = !GI.R.ReplayMark ? CActive : CBanned;
            
            I.Tag = GI;
            I.Text = GI.R.R.Date.ToString();
            I.UseItemStyleForSubItems = false;

            I.SubItems.Add(GI.R.R.Lobby.ToString("0000")); // Lobby
            I.SubItems[1].BackColor = I.BackColor;
            for (int i = 0; i < GI.R.R.PlayerCount; i++)
            {
                I.SubItems.Add(GI.GetPlayerInfo(i));
                if (IsPlayerBanned(GI, i) || GI.R.ReplayMark)
                    I.SubItems[i + 2].BackColor = CBanned;
                else if (IsPlayerReplacement(GI, i))
                    I.SubItems[i + 2].BackColor = CReplacement;
                else
                    I.SubItems[i + 2].BackColor = CActive;
            }

            lGames.Items.Add(I);
        }

        /// <summary>
        /// Обновить отображение игры
        /// </summary>
        /// <param name="GI"></param>
        private void UpdateGameInfo(GameInfo GI)
        {
            foreach (ListViewItem I in lGames.Items)
            {
                if (I.Tag == GI)
                {
                    I.BackColor = !GI.R.ReplayMark ? CActive : CBanned;
                    I.SubItems[1].BackColor = I.BackColor;
                    for (int i = 0; i < GI.R.R.PlayerCount; i++)
                    {
                        if(IsPlayerBanned(GI, i) || GI.R.ReplayMark)
                            I.SubItems[i + 2].BackColor = CBanned;
                        else if(IsPlayerReplacement(GI, i))
                            I.SubItems[i + 2].BackColor = CReplacement;
                        else
                            I.SubItems[i + 2].BackColor = CActive;
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// Получить выбранную игру
        /// </summary>
        /// <returns></returns>
        private GameInfo GetSelectedGame()
        {
            return (lGames.SelectedItems.Count > 0) ? lGames.SelectedItems[0].Tag as GameInfo : null;
        }

        /// <summary>
        /// Загрузить игры
        /// </summary>
        private void LoadGames()
        {
            if (File.Exists(TP.Dir + "tournier.xml"))
            {
                RP = ResultPack.Load(TP.Dir, "tournier.xml", TP);
                if (RP.UserField == null) RP.UserField = new PlayerManage();


                lGames.BeginUpdate();
                for (int i = 0; i < RP.Results.Count; i++)
                    AddGame(new GameInfo(RP.Results[i]));
                lGames.EndUpdate();
            }
            else
            {
                RP = new ResultPack();
                RP.UserField = new PlayerManage();

                lGames.BeginUpdate();
                for (int i = 0; i < TP.Hashes.Count; i++)
                {
                    LightResult R = new LightResult(TP.GetGameReplay(TP.Hashes[i]));
                    RP.Results.Add(R);
                    AddGame(new GameInfo(R));
                }
                lGames.EndUpdate();
            }
        }

        /// <summary>
        /// Загрузить игры, которые ещё не загружены
        /// </summary>
        private void LoadOtherGames()
        {
            lGames.BeginUpdate();
            for (int i = 0; i < TP.Hashes.Count; i++)
            {
                if (RP.IsExists(TP.Hashes[i])) continue;

                LightResult R = new LightResult(TP.GetGameReplay(TP.Hashes[i]));
                RP.Results.Add(R);
                AddGame(new GameInfo(R));
            }
            lGames.EndUpdate();
        }

        /// <summary>
        /// Сохранить
        /// </summary>
        private void SaveGames()
        {
            RP.Save(TP.Dir, "tournier.xml");
        }

        protected override void OnProjectLoad()
        {
            LoadGames();
            CalcPlayers();
            LoadPlayerList();
        }

        protected override void OnProjectUnload()
        {
            SaveGames();

            lPlayers.Items.Clear();
            lGames.Items.Clear();
            RP = null;
        }

        private void fTournier_Load(object sender, EventArgs e)
        {
            ResizeColumns(lGames, GamesCols);
            ResizeColumns(lPlayers, PlayersCols);
            ResizeColumns(lResult, ResultCols);
            ResizeColumns(lYaku, YakuCols);
            CheckProject();
        }

        private void ResizeTables(int W, int H)
        {
            lGames.Width = W;
            lGames.Height = H;

            lPlayers.Width = W;
            lPlayers.Height = H;

            lResult.Width = W;
            lResult.Height = H;

            lYaku.Width = W;
            lYaku.Height = H;
        }

        private void fTournier_Resize(object sender, EventArgs e)
        {
            lGames.BeginUpdate();
            lPlayers.BeginUpdate();
            lResult.BeginUpdate();
            lYaku.BeginUpdate();

            if (WindowState == FormWindowState.Maximized)
            {
                tbTabs.Left = 0;
                tbTabs.Top = 0;
                tbTabs.Width = ClientSize.Width;
                tbTabs.Height = ClientSize.Height;
            }
            else
            {
                tbTabs.Left = 12;
                tbTabs.Top = 12;
                tbTabs.Width = ClientSize.Width - 24;
                tbTabs.Height = ClientSize.Height - 24;
            }
            if (tbTabs.SelectedTab == tGames)
                ResizeTables(tGames.ClientSize.Width, tGames.ClientSize.Height);
            else if (tbTabs.SelectedTab == tPlayers)
                ResizeTables(tPlayers.ClientSize.Width, tPlayers.ClientSize.Height);
            else if (tbTabs.SelectedTab == tTable)
                ResizeTables(tTable.ClientSize.Width, tTable.ClientSize.Height);

            ResizeColumns(lGames, GamesCols);
            ResizeColumns(lPlayers, PlayersCols);
            ResizeColumns(lResult, ResultCols);
            ResizeColumns(lYaku, YakuCols);

            lYaku.EndUpdate();
            lPlayers.EndUpdate();
            lGames.EndUpdate();
            lResult.EndUpdate();
        }

        private void fTournier_ResizeEnd(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Адаптировать контекстное меню
        /// </summary>
        /// <param name="Index"></param>
        private void SetupMenuForPlayer(GameInfo GI, int Index)
        {
            PlayerManage PM = RP.UserField as PlayerManage;
            bool IsTempReplacement = PM.IsReplacementPlayer(GI.R.Hash, Index);
            bool IsReplacement = PM.Replacements.Contains(GI.R.R.Players[Index].NickName);
            bool IsBanned = PM.Banned.Contains(GI.R.R.Players[Index].NickName);

            PlayerView[Index].Visible = (GI.R.R.PlayerCount > Index);

            if (GI.R.R.PlayerCount <= Index) return;
            PlayerView[Index].Text = GI.GetPlayerRankedInfo(Index);
            PlayerView[Index].Image = IsPlayerBanned(GI, Index) ? 
                imgState.Images[0] :
                (IsPlayerReplacement(GI, Index) ? imgState.Images[1] : null);

            PlayerExclude[Index].Visible = !GI.R.PlayerMark[Index] && !IsBanned;
            PlayerInclude[Index].Visible = GI.R.PlayerMark[Index] && !IsBanned;

            PlayerMarkR[Index].Visible = !IsTempReplacement && !IsReplacement && !IsBanned && !GI.R.PlayerMark[Index];
            PlayerUnmarkR[Index].Visible = IsTempReplacement && !IsReplacement && !IsBanned && !GI.R.PlayerMark[Index];
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
            for (int i = 0; i < GI.R.R.PlayerCount; i++)
                SetupMenuForPlayer(GI, i);

            mViewExcludeGame.Visible = !GI.R.ReplayMark;
            mViewIncludeGame.Visible = GI.R.ReplayMark;
            mViewLoadOther.Enabled = lGames.Items.Count != RP.Results.Count;
        }

        private void mViewPlayerReplay_Click(object sender, EventArgs e)
        {
            var GI = GetSelectedGame();
            if (GI == null) return;

            int Player = Convert.ToInt32(((sender as ToolStripDropDownItem).Tag));

            TP.ViewReplayExternal(GI.R.R.Hash, Player);
        }

        private void mViewPlayerExclude_Click(object sender, EventArgs e)
        {
            var GI = GetSelectedGame();
            if (GI == null) return;

            int Player = Convert.ToInt32(((sender as ToolStripDropDownItem).Tag));
            GI.R.PlayerMark[Player] = true;

            UpdateGameInfo(GI);
        }

        private void mViewPlayerInclude_Click(object sender, EventArgs e)
        {
            var GI = GetSelectedGame();
            if (GI == null) return;

            int Player = Convert.ToInt32(((sender as ToolStripDropDownItem).Tag));
            GI.R.PlayerMark[Player] = false;

            UpdateGameInfo(GI);
        }

        private void mViewPlayerMarkReplacement_Click(object sender, EventArgs e)
        {
            var GI = GetSelectedGame();
            if (GI == null) return;

            int Player = Convert.ToInt32(((sender as ToolStripDropDownItem).Tag));
            PlayerManage PM = RP.UserField as PlayerManage;

            if (!PM.IsReplacementPlayer(GI.R.Hash, Player))
                PM.TempReplacements.Add(new TempReplacement(GI.R.Hash, Player));

            UpdateGameInfo(GI);
        }

        private void mViewPlayerUnmarkReplacement_Click(object sender, EventArgs e)
        {
            var GI = GetSelectedGame();
            if (GI == null) return;

            int Player = Convert.ToInt32(((sender as ToolStripDropDownItem).Tag));
            PlayerManage PM = RP.UserField as PlayerManage;

            for (int i = 0; i < PM.TempReplacements.Count; i++)
            {
                var TR = PM.TempReplacements[i];

                if ((TR.Hash.CompareTo(GI.R.Hash) == 0) && TR.Player == Player)
                {
                    PM.TempReplacements.Remove(TR);
                    break;
                }
            }

            UpdateGameInfo(GI);
        }

        private void mViewPaifu_Click(object sender, EventArgs e)
        {
            var GI = GetSelectedGame();
            if (GI == null) return;

            Form F = new fPaifuViewer(GI.R.Hash, 0);
            F.MdiParent = MdiParent;
            F.Show();
        }

        private void mViewExcludeIncludeGame_Click(object sender, EventArgs e)
        {
            var GI = GetSelectedGame();
            if (GI == null) return;

            int Value = Convert.ToInt32(((sender as ToolStripDropDownItem).Tag));
            GI.R.ReplayMark = (Value != 1);
            UpdateGameInfo(GI);
        }

        private void fTournier_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveGames();
        }

        private void tbTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbTabs.SelectedTab == tGames) lGames.Select();
            if (tbTabs.SelectedTab == tPlayers) lPlayers.Select();
            if (tbTabs.SelectedTab == tTable)
            {
                CalcResult();
                BuildResultTable();
                lResult.Select();
            }
            if (tbTabs.SelectedTab == tYaku)
            {
                BuildYakuTable();
                lYaku.Select();
            }
        }
    }
}
