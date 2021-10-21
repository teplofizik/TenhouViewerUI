using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mahjong;
using Mahjong.Helper;
using System.Windows.Forms;
using Output;
using Viewer;
using System.ComponentModel;
using System.Drawing;

namespace TenhouViewerUI
{
    public partial class fTournier : NestedForm
    {
        private List<PlayerInfo> Players = new List<PlayerInfo>();

        private PlayerInfo GetPlayerInfo(string Name)
        {
            foreach (PlayerInfo P in Players)
            {
                if (P.Nickname.CompareTo(Name) == 0) return P;
            }

            // Не найдено
            PlayerInfo NP = new PlayerInfo(Name);
            Players.Add(NP);
            return NP;
        }

        /// <summary>
        /// Составить статистику по игрокам
        /// </summary>
        private void CalcPlayers()
        {
            for (int i = 0; i < RP.Results.Count; i++)
            {
                Replay R = RP.Results[i].R;

                for (int p = 0; p < R.PlayerCount; p++)
                {
                    PlayerInfo PI = GetPlayerInfo(R.Players[p].NickName);
                    PI.Count++;
                    PI.Rating = R.Players[p].Rating;
                    PI.Rank = R.Players[p].Rank;
                    PI.Sex = R.Players[p].Sex;
                }
            }

            Players.Sort((a, b) => ((a.Count < b.Count) ? 1 : ((a.Count == b.Count) ? 0 : -1)));
            Players.Sort((a, b) => String.Compare(a.Nickname, b.Nickname));
        }

        /// <summary>
        /// Заполнить таблицу
        /// </summary>
        private void LoadPlayerList()
        {
            PlayerManage PM = RP.UserField as PlayerManage;

            lGames.BeginUpdate();
            foreach (PlayerInfo PI in Players)
            {
                ListViewItem I = new ListViewItem();
                I.Tag = PI;
                I.Text = PI.Nickname;
                I.SubItems.Add(PI.Rank.toJapaneseRank());
                I.SubItems.Add(String.Format("{0:d}R", PI.Rating));
                I.SubItems.Add(PI.Count.ToString());

                I.BackColor = PM.Banned.Contains(PI.Nickname) ? CBanned :
                    (PM.Replacements.Contains(PI.Nickname) ? CReplacement : CActive);

                lPlayers.Items.Add(I);
            }
            lGames.EndUpdate();
        }

        /// <summary>
        /// Обновить отображение игрока
        /// </summary>
        /// <param name="PI"></param>
        private void UpdatePlayerInfo(PlayerInfo PI)
        {
            foreach (ListViewItem I in lPlayers.Items)
            {
                if (I.Tag == PI)
                {
                    PlayerManage PM = RP.UserField as PlayerManage;

                    I.BackColor = PM.Banned.Contains(PI.Nickname) ? CBanned :
                        (PM.Replacements.Contains(PI.Nickname) ? CReplacement : CActive);
                    break;
                }
            }
        }

        /// <summary>
        /// Обновить данные в списке игр
        /// </summary>
        /// <param name="PI"></param>
        private void UpdateAffectedGameInfo(PlayerInfo PI)
        {
            lGames.BeginUpdate();
            foreach (ListViewItem I in lGames.Items)
            {
                var GI = I.Tag as GameInfo;

                if (GI.R.R.HasSuchPlayer(PI.Nickname)) UpdateGameInfo(GI);
            }
            lGames.EndUpdate();
        }

        /// <summary>
        /// Получить выбранную игру
        /// </summary>
        /// <returns></returns>
        private PlayerInfo GetSelectedPlayer()
        {
            return (lPlayers.SelectedItems.Count > 0) ? lPlayers.SelectedItems[0].Tag as PlayerInfo : null;
        }


        private void mPlayer_Opening(object sender, CancelEventArgs e)
        {
            var PI = GetSelectedPlayer();
            if (PI == null)
            {
                e.Cancel = true;
                return;
            }

            PlayerManage PM = RP.UserField as PlayerManage;
            bool Banned = PM.Banned.Contains(PI.Nickname);
            bool Replacement = PM.Replacements.Contains(PI.Nickname);

            mPlayerExclude.Visible = !Banned;
            mPlayerInclude.Visible = Banned;
            mPlayerMark.Visible = !Replacement && !Banned;
            mPlayerUnmark.Visible = Replacement && !Banned;

            // Построить меню реплеев
            mPlayerReplays.DropDownItems.Clear();
            for (int i = 0; i < RP.Results.Count; i++)
            {
                Replay R = RP.Results[i].R;
                int Index = R.GetPlayerIndex(PI.Nickname);
                if (Index < 0) continue;

                string Text;
                
                if(R.PlayerCount == 3)
                    Text = String.Format("{0:s} [{1:s}, {2:s}, {3:s}]",
                                          R.Lobby.ToString(),
                                          R.Players[0].NickName,
                                          R.Players[1].NickName,
                                          R.Players[2].NickName);
                else
                    Text = String.Format("{0:s} [{1:s}, {2:s}, {3:s}, {4:s}]",
                                          R.Lobby.ToString(),
                                          R.Players[0].NickName,
                                          R.Players[1].NickName,
                                          R.Players[2].NickName,
                                          R.Players[3].NickName);

                Image Img = (RP.Results[i].PlayerMark[Index]) ? imgState.Images[0] :
                    (PM.IsReplacementPlayer(R.Hash, Index) ? imgState.Images[1] : null);

                ToolStripDropDownItem I = new ToolStripMenuItem(Text, Img);
                I.Tag = R.Hash;
                I.Click += new EventHandler(Replay_Click);

                mPlayerReplays.DropDownItems.Add(I);
            }

        }

        void Replay_Click(object sender, EventArgs e)
        {
            string Hash = (sender as ToolStripMenuItem).Tag.ToString();
            tbTabs.SelectTab(tGames);

            for (int i = 0; i < lGames.Items.Count; i++)
            {
                var GI = lGames.Items[i].Tag as GameInfo;

                lGames.Select();
                if (GI.R.Hash.CompareTo(Hash) == 0)
                {
                    lGames.Items[i].Focused = true;
                    lGames.Items[i].Selected = true;
                    lGames.Items[i].EnsureVisible();
                    break;
                }
            }

        }

        /// <summary>
        /// Включить или исключить игрока из результата
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mPlayerExcludeInclude_Click(object sender, EventArgs e)
        {
            var PI = GetSelectedPlayer();
            if (PI == null) return;

            int Value = Convert.ToInt32(((sender as ToolStripDropDownItem).Tag));
            PlayerManage PM = RP.UserField as PlayerManage;

            if (Value == 0)
            {
                // Add to banned
                if (!PM.Banned.Contains(PI.Nickname)) PM.Banned.Add(PI.Nickname);
            }
            else
                PM.Banned.Remove(PI.Nickname);

            UpdatePlayerInfo(PI);
            UpdateAffectedGameInfo(PI);
        }

        /// <summary>
        /// Отметить или снять отметку игрока замены
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mPlayerMarkUnmark_Click(object sender, EventArgs e)
        {
            var PI = GetSelectedPlayer();
            if (PI == null) return;

            int Value = Convert.ToInt32(((sender as ToolStripDropDownItem).Tag));
            PlayerManage PM = RP.UserField as PlayerManage;

            if (Value == 1)
            {
                // Add to replacement
                if (!PM.Replacements.Contains(PI.Nickname)) PM.Replacements.Add(PI.Nickname);
            }
            else
                PM.Replacements.Remove(PI.Nickname);

            UpdatePlayerInfo(PI);
            UpdateAffectedGameInfo(PI);
        }
    }
}
