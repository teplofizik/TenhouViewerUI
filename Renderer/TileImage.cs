using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;
using System.Drawing;
using System.Diagnostics;

namespace Renderer
{
    class TileImage
    {
        string[] mTilesName = { "",
                               "_1m", "_2m", "_3m", "_4m", "_5m", "_6m", "_7m", "_8m", "_9m", "",
                               "_1p", "_2p", "_3p", "_4p", "_5p", "_6p", "_7p", "_8p", "_9p", "",
                               "_1s", "_2s", "_3s", "_4s", "_5s", "_6s", "_7s", "_8s", "_9s", "",
                               "_1z", "_2z", "_3z", "_4z", "_5z", "_6z", "_7z"
                             };

        string[] mSpecificTilesName = { "_0m", "_0p", "_0s", "_0z", "tsumogiri" };


        // 0 - внизу, 1 - справа, 2 - напротив, 3 - справа
        private Image[,] mTiles = new Image[4, 38];
        private Image[,] mSpecific = new Image[4, 5];

        private Size mHS, mVS;
        private Image mH, mV;
        private ResourceManager mRM;

        private double mScale;

        public TileImage(string Resource)
        {
            mRM = new ResourceManager(Resource, typeof(TileImage).Assembly);
            mScale = 1;

            BuildTileset();
        }

        public TileImage(string Resource, double Scale)
        {
             mRM = new ResourceManager(Resource, typeof(TileImage).Assembly);
             mScale = Scale;

             BuildTileset();
        }

        /// <summary>
        /// Создать набор тайлов
        /// </summary>
        private void BuildTileset()
        {
            mH = getDrawable("horizontal");
            mV = getDrawable("vertical");

            mHS = mH.Size;
            mVS = mV.Size;

            for (int p = 0; p < 4; p++)
            {
                for (int i = 0; i < 38; i++)
                {
                    if (i % 10 == 0)
                        mTiles[p,i] = null;
                    else
                        mTiles[p,i] = getTileImage(mTilesName, i, p);
                }

                for (int i = 0; i < mSpecificTilesName.Length; i++)
                    mSpecific[p, i] = getTileImage(mSpecificTilesName, i, p);
            }

            mH.Dispose();
            mV.Dispose();
            mH = null;
            mV = null;
        }

        /// <summary>
        /// Тест
        /// </summary>
        /// <returns></returns>
        public Image getTest()
        {
            int W = 640;
            int H = 480;

            Bitmap B = new Bitmap(W, H);
            using (Graphics G = Graphics.FromImage(B))
            {
                G.FillRectangle(Brushes.Black, new Rectangle(0,0,W,H));

                int X = 10;
                for (int i = 31; i < 38; i++)
                {
                    G.DrawImage(mTiles[0, i], new Point(X, 50));
                    G.DrawImage(mTiles[2, i], new Point(X, 90));
                    X += mTiles[0, i].Width;
                }

                int Y = 10;
                for (int i = 31; i < 38; i++)
                {
                    G.DrawImage(mTiles[1, i], new Point(300, Y));
                    G.DrawImage(mTiles[3, i], new Point(400, Y));
                    Y += mTiles[1, i].Height;
                }
            }

            return B;
        }

        /// <summary>
        /// Получить изображение тайла
        /// </summary>
        /// <param name="Index"></param>
        /// <param name="Player"></param>
        /// <returns></returns>
        protected Image getTileImage(String[] Names, int Index, int Player)
        {
            string Name;
            if (Index >= Names.Length) return null;

            Name = Names[Index];
            if (Name.Length == 0) return null;

            Image I = getDrawable(Name);
            if (I == null) return null;

            switch (Player)
            {
                case 1: // право
                    I.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    Overlay(I, mH);
                    //Debug.WriteLine("[1] Overlay(I, H);");
                    break;
                case 2: // напротив
                    I.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    Overlay(I, mV);
                    //Debug.WriteLine("[2] Overlay(I, V);");
                    break;
                case 3: // лево
                    I.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    Overlay(I, mH);
                    //Debug.WriteLine("[3] Overlay(I, H);");
                    break;
            }

            return I;
        }

        /// <summary>
        /// Получить картинку из ресурсов, которую можно изменять
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        private Image getDrawable(string Name)
        {
            Image I = (Bitmap)mRM.GetObject(Name);
            if (I == null) return null;

            return new Bitmap(I, new Size(Convert.ToInt32(I.Width * mScale), Convert.ToInt32(I.Height * mScale)));
        }

        /// <summary>
        /// Нарисовать
        /// </summary>
        /// <param name="Dest"></param>
        /// <param name="O"></param>
        private void Overlay(Image Dest, Image O)
        {
            Bitmap B = Dest as Bitmap;
            B.SetPixel(0, 0, Color.Transparent);
            B.SetPixel(B.Width - 1, 0, Color.Transparent);
            B.SetPixel(0, B.Height - 1, Color.Transparent);
            B.SetPixel(B.Width - 1, B.Height - 1, Color.Transparent);

            using (Graphics G = Graphics.FromImage(Dest))
            {
                G.DrawImage(O, new Point(0, 0));
            }
        }

        /// <summary>
        /// Картинка цумогири (пайфу)
        /// </summary>
        /// <param name="Orientation"></param>
        /// <returns></returns>
        public Image getTsumogiri(int Orientation)
        {
            return mSpecific[Orientation, 4];
        }

        /// <summary>
        /// Картинка закрытого тайла
        /// </summary>
        /// <param name="Orientation"></param>
        /// <returns></returns>
        public Image getClosed(int Orientation)
        {
            return mSpecific[Orientation, 3];
        }

        /// <summary>
        /// Картинка красной пятёрки
        /// </summary>
        /// <param name="Orientation"></param>
        /// <returns></returns>
        public Image getRed(int Orientation, int Index)
        {
            int Type = Index / 10;
            return mSpecific[Orientation, Type];
        }

        /// <summary>
        /// Картинка обычного тайла
        /// </summary>
        /// <param name="Orientation"></param>
        /// <returns></returns>
        public Image getRegular(int Orientation, int Index)
        {
            return mTiles[Orientation, Index];
        }

        /// <summary>
        /// Размер вертикального тайла
        /// </summary>
        /// <returns></returns>
        public Size getVerticalSize()
        {
            return mVS;
        }

        /// <summary>
        /// Размер горизонтального тайла
        /// </summary>
        /// <returns></returns>
        public Size getHorizontalSize()
        {
            return mHS;
        }
    }
}
