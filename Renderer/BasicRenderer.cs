using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Renderer
{
    class BasicRenderer
    {
        private int mWidth;
        private int mHeight;

        public BasicRenderer()
        {
            mWidth = 100;
            mHeight = 100;
        }

        public BasicRenderer(int W, int H)
        {
            setSize(W, H);
        }

        protected void setSize(int W, int H)
        {
            mWidth = W;
            mHeight = H;
        }

        /// <summary>
        /// Рисование картинки
        /// </summary>
        /// <param name="W"></param>
        /// <param name="H"></param>
        /// <param name="G"></param>
        protected virtual void InternalDraw(int W, int H, Graphics G)
        {

        }

        /// <summary>
        /// Получить итоговое изображение
        /// </summary>
        /// <returns></returns>
        public Image Draw()
        {
            Bitmap B = new Bitmap(mWidth, mHeight);
           
            using(Graphics G = Graphics.FromImage(B))
            {
                InternalDraw(B.Width, B.Height, G);
            }

            return B;
        }
    }
}
