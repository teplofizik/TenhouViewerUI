using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Mahjong.Helper;
using Mahjong;
using Mahjong.Steps;

namespace Renderer
{
    class TableRenderer : BasicRenderer
    {
        private TileImage mTiles;
        
        private Mahjong.Replay mR;
        private Mahjong.Round  mRnd;
        
        private bool mRedAri;

        public TableRenderer(Mahjong.Replay R, Mahjong.Round Rnd)
        {
            mTiles = new TileImage("TenhouViewerUI.Tiles", 0.7);

            mR = R;
            mRnd = Rnd;
            mRedAri = R.HasAkaDora();

            Rnd.ExpandData(R);

            setSize(800, 600);
        }

        protected override void InternalDraw(int W, int H, Graphics G)
        {
            // Рисуем фон стола
            DrawBackground(W, H, G);

            // Руки
            DrawHands(W, H, G);

            // Открытые сеты
            DrawSets(W, H, G);

            // Дискард
            DrawDiscard(W, H, G);

            // Стена (хотя бы мёртвая)
            DrawWall(W, H, G);
        }

        private void DrawBackground(int W, int H, Graphics G)
        {
            G.FillRectangle(Brushes.DarkBlue, new Rectangle(0, 0, W, H));
        }

        private void DrawHands(int W, int H, Graphics G)
        {
            for (int i = 0; i < mR.PlayerCount; i++)
            {

            }
        }

        private void DrawSets(int W, int H, Graphics G)
        {
            for (int i = 0; i < mR.PlayerCount; i++)
            {

            }
        }

        private void DrawDiscard(int W, int H, Graphics G)
        {
            for (int i = 0; i < mR.PlayerCount; i++)
            {

            }
        }

        private void DrawWall(int W, int H, Graphics G)
        {

        }
    }
}
