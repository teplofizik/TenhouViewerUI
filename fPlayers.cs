using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;

using Mahjong;
using Output;
using Viewer;

namespace TenhouViewerUI
{
    public partial class fPlayers : NestedForm
    {
        private double[] Cols = { 60.0, 10.0, 10.0, 20.0 };
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
            for (int i = 0; i < TP.Replays.Count; i++)
            {
                Replay R = TP.Replays[i];

                for (int p = 0; p < R.PlayerCount; p++)
                {
                    PlayerInfo PI = GetPlayerInfo(R.Players[p].NickName);
                    PI.Count++;
                    PI.Rating = R.Players[p].Rating;
                    PI.Rank = R.Players[p].Rank;
                    PI.Sex = R.Players[p].Sex;
                }
            }

            Players.Sort((a, b) => ((a.Count < b.Count) ? 1 : ((a.Count==b.Count) ? 0 : -1)));
        }

        /// <summary>
        /// Заполнить таблицу
        /// </summary>
        private void BuildList()
        {
            lGames.BeginUpdate();
            foreach (PlayerInfo PI in Players)
            {
                ListViewItem I = new ListViewItem();
                
                I.Text = PI.Nickname;
                I.SubItems.Add(PI.Rank.toJapaneseRank());
                I.SubItems.Add(String.Format("{0:d}R", PI.Rating));
                I.SubItems.Add(PI.Count.ToString());

                I.BackColor = ((TP.Owner != null) && (PI.Nickname.CompareTo(TP.Owner) == 0)) ? Color.LightBlue : Color.Transparent;

                lGames.Items.Add(I);
            }
            lGames.EndUpdate();
        }

        private void UpdateList()
        {
            lGames.BeginUpdate();
            foreach (ListViewItem I in lGames.Items)
            {
                Color C = ((TP.Owner != null) && (I.Text.CompareTo(TP.Owner) == 0)) ? Color.LightBlue : Color.Transparent;
                if (I.BackColor != C) I.BackColor = C;
            }
            lGames.EndUpdate();
        }

        public fPlayers()
        {
            InitializeComponent();
        }

        private void fPlayers_Resize(object sender, EventArgs e)
        {
            lGames.Width = ClientSize.Width - 24;
            lGames.Height = ClientSize.Height - lGames.Top - 12;

            ResizeColumns(lGames, Cols);
        }

        private void fPlayers_Load(object sender, EventArgs e)
        {
            CheckProject();
            CalcPlayers();

            ResizeColumns(lGames, Cols);
            BuildList();
        }

        private void mPlayer_Opening(object sender, CancelEventArgs e)
        {
            if (lGames.SelectedItems.Count == 0)
            {
                e.Cancel = true;
                return;
            }
        }

        private void mPlayerSetNick_Click(object sender, EventArgs e)
        {
            if (lGames.SelectedItems.Count == 0) return;

            TP.Owner = lGames.SelectedItems[0].Text;
            UpdateList();
        }
    }
}
