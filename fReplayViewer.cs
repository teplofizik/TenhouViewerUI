using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Mahjong;
using Renderer;

namespace TenhouViewerUI
{
    public partial class fReplayViewer : NestedForm
    {
        private string Hash;
        private Replay R;

        public fReplayViewer()
        {
            InitializeComponent();
        }

        private void fReplayViewer_Load(object sender, EventArgs e)
        {
            CheckProject();

            //TileImage I = new TileImage("TenhouViewerUI.Tiles");
           // Image Img = I.getTest();

            //pb.Image = Img;
        }

        /// <summary>
        /// При загрузке проекта
        /// </summary>
        protected override void OnProjectLoad()
        {
            Hash = TP.Hashes[0];

            Text = "Replay viewer: " + Hash;
            R = TP.GetGameReplay(Hash);

            TableRenderer Rd = new TableRenderer(R, R.Rounds[0]);
            pb.Image = Rd.Draw();
        }

        /// <summary>
        /// При выгрузе проекта
        /// </summary>
        protected override void OnProjectUnload()
        {

        }
    }
}
