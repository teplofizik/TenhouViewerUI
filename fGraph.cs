using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;

using Mahjong;
using Mahjong.Helper;

namespace TenhouViewerUI
{
    public partial class fGraph : NestedForm
    {
        Color[] Ranking = { Color.Black,      // rookie
                            Color.FromArgb(128,   0,   0), // 9 ku
                            Color.FromArgb(128,  64,   0), // 8 ku
                            Color.FromArgb(128, 128,   0), // 7 ku
                            Color.FromArgb( 64, 128,   0), // 6 ku
                            Color.FromArgb(  0, 128,  64), // 5 ku
                            Color.FromArgb(  0, 128, 128), // 4 ku
                            Color.FromArgb(  0,  64, 128), // 3 ku
                            Color.FromArgb(  0,   0, 128), // 2 ku
                            Color.FromArgb(128,   0, 128), // 1 ku
                            Color.FromArgb(0xD3, 0x00, 0x68), // 1 dan
                            Color.FromArgb(0xFF, 0x18, 0x00), // 2 dan
                            Color.FromArgb(0x00, 0xC1, 0x2B), // 3 dan
                            Color.FromArgb(0x92, 0xEC, 0x00), // 4 dan
                            Color.FromArgb(0x9E, 0x28, 0x62), // 5 dan 
                            Color.FromArgb(0xBF, 0x3D, 0x30), // 6 dan 
                            Color.FromArgb(0x24, 0x91, 0x3C), // 7 dan 
                            Color.FromArgb(0x7E, 0xB1, 0x2C), // 8 dan
                            Color.FromArgb(0x89, 0x00, 0x43), // 9 dan
                            Color.FromArgb(0xA6, 0x10, 0x00), // yay 
                          };

        public fGraph()
        {
            InitializeComponent();
        }

        private void fGraph_Resize(object sender, EventArgs e)
        {
            int W = ClientSize.Width;
            int H = ClientSize.Height;

            pbGraph.Width = W - 24;
            pbGraph.Height = H - 24;
        }

        private void fGraph_Load(object sender, EventArgs e)
        {
            CheckProject();
        }

        /// <summary>
        /// Проект загружен
        /// </summary>
        protected override void OnProjectLoad()
        {
            DrawSelectedGraph();
        }

        private void DrawSelectedGraph()
        {
            DrawGraph(CollectData(TP.Owner), CollectColors(TP.Owner));
        }

        private double[] CollectData(string NickName)
        {
            List<double> Data = new List<double>();

            foreach(var H in TP.Hashes)
            {
                Replay R = TP.GetGameReplay(H);
                int Player = R.GetPlayerIndex(NickName);

                if(Player >= 0) Data.Add(R.Players[Player].Rating);
            }

            return Data.ToArray();
        }

        private int[] CollectColors(string NickName)
        {
            List<int> Data = new List<int>();

            foreach (var H in TP.Hashes)
            {
                Replay R = TP.GetGameReplay(H);
                int Player = R.GetPlayerIndex(NickName);

                if (Player >= 0) Data.Add(R.Players[Player].Rank);
            }

            return Data.ToArray();
        }

        private void DrawGraph(double[] Data, int[] Color)
        {
            int W = pbGraph.Width;
            int H = pbGraph.Height;
            int PaddingH = 10, PaddingW = 10;

            Bitmap B = new Bitmap(W, H);
            Graphics G = Graphics.FromImage(B);

            G.FillRectangle(Brushes.White, new Rectangle(0, 0, W, H));
            {
                var Min = Data.Min();
                var Max = Data.Max();

                if (Min != Max)
                {
                    int Index = Data.Length - 1;
                    Point P = Point.Empty;

                    for (int X = W - 1 - PaddingW; (X >= PaddingW) && (Index >= 0); X--, Index--)
                    {
                        var Value = Data[Index];

                        double sizedH = H - PaddingH * 2;
                        double Y = H - PaddingH - sizedH * (Value - Min) / (Max - Min);

                        if (P == Point.Empty)
                        {
                            P = new Point(Convert.ToInt32(X), Convert.ToInt32(Y));
                        }

                        Point P2 = new Point(Convert.ToInt32(X), Convert.ToInt32(Y));
                        G.DrawLine(new Pen(new SolidBrush(Ranking[Color[Index]])), P, P2);
                        P = P2;
                    }
                }
            }
            G.Dispose();

            pbGraph.Image = B;
        }

        private void fGraph_ResizeEnd(object sender, EventArgs e)
        {
            DrawSelectedGraph();
        }
    }
}
