using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Diagnostics;

using Renderer;
using Mahjong.Helper;
using Output;
using Mahjong;
using Mahjong.Steps;

namespace Paifu
{
    class PaifuRenderer : BasicRenderer
    {
        private readonly Font mFbig = new Font("Arial", 24.0f);
        private readonly Font mFsmall = new Font("Arial", 12.0f);
        private readonly Font mFcomment = new Font("Arial", 10.0f);

        private readonly string[] cWinds = { "東", "南", "西", "北" };

        private Color mDangerColor = Color.FromArgb(200, 204, 119, 0);
        private Color mFuritenColor = Color.FromArgb(200, 204, 119, 0);
        private Color[] mShantenColor = { Color.Green, Color.GreenYellow, Color.Yellow, Color.Orange, Color.OrangeRed, Color.Red, Color.DarkRed };

        private const int cPaddingV = 10;
        private const int cPaddingH = 10;
        private const int cYakuWidth = 180;
        private const int cNoYakuWidth = 70;

        private int mWidth = 1100;
        private int mHeight = 500;
        private int mPlayerColumnWidth = 100;
        private int mRoundColumnWidth = 100;
        private int mTilesColumnWidth = 1050;

        private const int cCostOffset = 95;
        private const int cYakuOffset = 5;

        private const int cInternalPadding = 4;

        private int mInternalWidth;
        private int mInternalHeight;
        private int mFieldHeight;

        private Mahjong.Replay mR;
        private Mahjong.Round  mRnd;
        
        public int mShowShanten = 0;  // show shanten info: false
        public int mShowDanger = 1;   // show danger tiles: true
        public int mShowNames = 1;    // show real nicknames: true
        public int mShowYakuInfo = 1; // show yaku info: true
        public int mShowColor = 1;    // show shanten in colors
        public int mShowSex = 0;      // show player's sex

        public string mLanguage = "jp";

        private bool mRedAri;

        private int mColumn = 0;
        private int mLastTile = -1;

        private int mTileWidth = 0;
        private int mTileHeight = 0;

        private int[] mPlayers = new int[4];
        private int[] mPlayerIndex = new int[4];

        private TileImage mTiles;

        public PaifuRenderer(Mahjong.Replay R, Mahjong.Round Rnd)
        {
            mTiles = new TileImage("TenhouViewerUI.Tiles", 0.7);

            mR = R;
            mRnd = Rnd;
            mRedAri = R.HasAkaDora();

            Rnd.ExpandData(R);

            CalcPlayersPositions();
            CalcDimensions();

            setSize(mWidth, mHeight);
        }

        protected override void InternalDraw(int W, int H, System.Drawing.Graphics G)
        {
            DrawBorders(G);
            DrawRoundInfo(G);
            DrawSteps(G);
            for (int i = 0; i < mR.PlayerCount; i++)
            {
                DrawHandInfo(G, i);
                DrawStartHand(G, i);
                DrawLastHand(G, i);
            }
        }

        /// <summary>
        /// Рассчитать позиции игроков
        /// </summary>
        private void CalcPlayersPositions()
        {
            // fill Players array:
            for (int i = 0; i < mR.PlayerCount; i++)
            {
                mPlayers[i] = (i + mRnd.Dealer) % mR.PlayerCount;
                mPlayerIndex[i] = (mR.PlayerCount - mRnd.Dealer + i) % mR.PlayerCount;
            }
        }

        /// <summary>
        /// Рассчитать размеры основных элементов
        /// </summary>
        private void CalcDimensions()
        {
            Size S = mTiles.getVerticalSize();

            mTileWidth = S.Width;
            mTileHeight = S.Height;

            int CircleCount;
            {
                int StepCount = 12;
                for (int i = 0; i < mR.PlayerCount; i++)
                {
                    int PlayerStepCount = mRnd.getStepCount(i);
                    if (PlayerStepCount > StepCount) StepCount = PlayerStepCount;
                }

                CircleCount = StepCount + 4;
            }

            mTilesColumnWidth = mTileWidth * CircleCount;
            mTilesColumnWidth += (mShowYakuInfo != 0) ? cYakuWidth : cNoYakuWidth;

            mWidth = mRoundColumnWidth + mPlayerColumnWidth + mTilesColumnWidth + cPaddingH * 2;
            mHeight = 2 * cPaddingV + mR.PlayerCount * (2 * cInternalPadding + 6 * mTileHeight);

            mInternalWidth = mWidth - 2 * cPaddingH;
            mInternalHeight = mHeight - 2 * cPaddingV;

            mRoundColumnWidth = mTileWidth * 5;

            mFieldHeight = mInternalHeight / mR.PlayerCount;
        }

        private void DrawBorders(Graphics G)
        {
            Pen P = new Pen(Color.Black, 2.0f);
            Brush Br = new SolidBrush(Color.White);

            // fill background
            G.FillRectangle(Br, new Rectangle(cPaddingH, cPaddingV, mInternalWidth, mInternalHeight));

            // Paifu border
            G.DrawRectangle(P, new Rectangle(cPaddingH, cPaddingV, mInternalWidth, mInternalHeight));

            // Round border
            G.DrawRectangle(P, new Rectangle(cPaddingH, cPaddingV, mRoundColumnWidth, mInternalHeight));

            // Players border
            G.DrawRectangle(P, new Rectangle(cPaddingH + mRoundColumnWidth, cPaddingV, mPlayerColumnWidth, mInternalHeight));

            // Draw horisontal lines
            for (int i = 0; i < 4; i++) G.DrawLine(P, 
                cPaddingH + mRoundColumnWidth, 
                cPaddingV + mFieldHeight * i, 
                cPaddingH + mInternalWidth, 
                cPaddingV + mFieldHeight * i);
        }

        /// <summary>
        /// Информация о раздаче
        /// </summary>
        /// <param name="G"></param>
        private void DrawRoundInfo(Graphics G)
        {
            int[] DoraPointer = mRnd.getDoraTiles();
            int[] UraDoraPointer = mRnd.getUraDoraTiles();

            int CurrentRound = mR.Rounds.IndexOf(mRnd);

            string Round = mR.RoundName(CurrentRound);

            float X = cPaddingH;
            float Y = cPaddingV * 2;
            PointF Pointer = new PointF(X, Y);

            Pointer = DrawCenteredString(G, Color.Black, mFbig, Round, Pointer, mRoundColumnWidth);
            Pointer = DrawCenteredString(G, Color.Black, mFcomment, mR.Date.ToString("dd.MM.yyyy"), Pointer, mRoundColumnWidth);
            Pointer = DrawCenteredString(G, Color.Black, mFcomment, String.Format("Renchan stick: {0:d}", mRnd.RenchanStick), Pointer, mRoundColumnWidth);
            Pointer = DrawCenteredString(G, Color.Black, mFcomment, String.Format("Riichi stick: {0:d}", mRnd.RiichiStick), Pointer, mRoundColumnWidth);

            Pointer.Y += cPaddingV;
            int DoraY = Convert.ToInt32(Pointer.Y);
            Pointer.Y += mTileHeight * 1.2f;

            int UraDoraY = Convert.ToInt32(Pointer.Y);

            // Ura
            for (int i = 0; i < 4; i++)
                DrawDoraTile(G, i, UraDoraY, (i < UraDoraPointer.Length) ? UraDoraPointer[i] : -1);

            // Dora
            for (int i = 0; i < 4; i++)
                DrawDoraTile(G, i, DoraY, (i < DoraPointer.Length) ? DoraPointer[i] : -1);
        }

        /// <summary>
        /// Информация о балансе и игроке
        /// </summary>
        /// <param name="G"></param>
        /// <param name="Index"></param>
        private void DrawHandInfo(Graphics G, int Index)
        {
            int Player = mPlayers[Index];

            float X = cPaddingH + mRoundColumnWidth;
            float Y = Index * mFieldHeight + cPaddingV;
            PointF Pointer = new PointF(X, Y);

            string PlayerRank = String.Format("{0:s} {1:d}R", 
                mR.Players[Player].Rank.toJapaneseRank(), 
                mR.Players[Player].Rating);

            string NickName = mR.Players[Player].NickName;
            if (mShowNames == 0)
            {
                NickName = Convert.ToChar(Convert.ToByte('A') + Player) + "-san";
            }

            Pointer = DrawCenteredString(G, Color.Black, mFbig, cWinds[Index], Pointer, mPlayerColumnWidth);
            Pointer = DrawCenteredString(G, Color.Black, mFsmall, NickName, Pointer, mPlayerColumnWidth);
            Pointer = DrawCenteredString(G, Color.Black, mFsmall, PlayerRank, Pointer, mPlayerColumnWidth);
            Pointer.Y += cPaddingV;

            Pointer = DrawCenteredString(G, Color.Black, mFsmall, String.Format("{0:d}", mRnd.Balance[Player] * 100), Pointer, mPlayerColumnWidth);
            Pointer = DrawCenteredString(G, ((mRnd.Pay[Player] >= 0) ? Color.Green : Color.Red), mFsmall, String.Format("{0:d}", mRnd.Pay[Player]), Pointer, mPlayerColumnWidth);

            if (mShowSex != 0)
            {
                string Sex = "";
                Color Fill = Color.Gray;
                switch (mR.Players[Player].Sex)
                {
                    case 0: Sex = "COM"; break;
                    case 1: Fill = Color.Cyan; Sex = "男"; break;
                    case 2: Fill = Color.LightPink; Sex = "女"; break;
                }

                SizeF Size = G.MeasureString(Sex, mFsmall);

                int Height = Convert.ToInt32(Size.Height);
                int tX = cPaddingH + mRoundColumnWidth + 1;
                int tY = cPaddingV + mFieldHeight * (Index + 1) - Height - 1;

                G.FillRectangle(new SolidBrush(Fill), tX, tY, mPlayerColumnWidth - 2, Height);

                // Show sex kanji
                {
                    Brush Br = new SolidBrush(Color.Black);

                    float fX = tX + (mPlayerColumnWidth - Size.Width - 2) / 2;
                    float fY = tY + 1;

                    // Draw wind indicator
                    G.DrawString(Sex, mFsmall, Br, fX, fY);
                }
            }
        }

        /// <summary>
        /// Стартовая рука
        /// </summary>
        /// <param name="Index"></param>
        private void DrawStartHand(Graphics G, int Index)
        {
            int Player = mPlayers[Index];
            int Pos = 0;

            for (int i = 0; i < mRnd.Hands[Player].Length; i++)
            {
                int Tile = mRnd.Hands[Player][i];
                if (Tile == -1) continue;

                Pos = DrawHandTile(G, Index, Tile, Pos, 0, 0, RotateFlipType.RotateNoneFlipNone, false);
            }

            if (mShowShanten != 0)
                DrawShanten(G, Index, 0, Convert.ToString(new Hand(mRnd.Hands[Player]).GetShanten()));
        }

        /// <summary>
        /// Нарисовать последнюю руку
        /// </summary>
        /// <param name="Index"></param>
        private void DrawLastHand(Graphics G, int Index)
        {
            int Player = mPlayers[Index];
            int Pos = 0;

            // Last hand

            var Info = mRnd.Data.Info;
            var I = Info.Last(PI => (PI.Player == Player) && (PI.Hand != null));

            Hand Hand = I.Hand;
            int[] Tiles = Hand.Tiles.ToArray();

            for (int i = 0; i < Tiles.Length; i++)
            {
                int Tile = Tiles[i];
                if (Tile == -1) continue;
                if (Tile == mLastTile) continue;

                bool Danger = false;
                if (I.Danger.Length > 0)
                    Danger = I.Danger.Contains(new Tile(Tile).TileId);

                Pos = DrawHandTile(G, Index, Tile, Pos, 5, 0, RotateFlipType.RotateNoneFlipNone, Danger);
            }
            Pos += mTileWidth / 2;
            if (mRnd.hasAgari(Player))
            {
                bool Danger = false;
                if (I.Danger.Length > 0)
                    Danger = I.Danger.Contains(new Tile(mLastTile).TileId);

                Pos = DrawHandTile(G, Index, mLastTile, Pos, 5, 0, RotateFlipType.RotateNoneFlipNone, Danger);
                Pos += mTileWidth / 2;
            }

            if (mShowShanten != 0)
                DrawShanten(G, Index, 5, (I.Shanten >= 0) ? I.Shanten.ToString() : "アガリ");

            for (int i = 0; i < Hand.Naki.Count; i++)
            {
                Mahjong.Sets.Set N = Hand.Naki[Hand.Naki.Count - i - 1];

                switch (N.Type)
                {
                    case Mahjong.Sets.SetType.NUKI:
                        Pos = DrawHandTile(G, Index, N.Tiles[0], Pos, 5, 0, RotateFlipType.RotateNoneFlipNone, false);
                        break;

                    case Mahjong.Sets.SetType.CHI:
                        for (int j = 0; j < 3; j++)
                        {
                            RotateFlipType Rotate = (j == 0) ? RotateFlipType.Rotate90FlipNone : RotateFlipType.RotateNoneFlipNone;
                            Pos = DrawHandTile(G, Index, N.Tiles[j], Pos, 5, 0, Rotate, false);
                        }
                        break;
                    case Mahjong.Sets.SetType.PON:
                        for (int j = 0; j < 3; j++)
                        {
                            RotateFlipType Rotate = (j == (3 - N.FromWho)) ? RotateFlipType.Rotate90FlipNone : RotateFlipType.RotateNoneFlipNone;

                            // 1: AB[C] 2: A[B]C 3: [A]BC
                            Pos = DrawHandTile(G, Index, N.Tiles[j], Pos, 5, 0, Rotate, false);
                        }
                        break;
                    case Mahjong.Sets.SetType.ANKAN:
                        for (int j = 0; j < 4; j++)
                        {
                            int Tile = N.Tiles[j];

                            // Close first and last tiles
                            if ((j == 0) || (j == 3)) Tile = -1;
                            Pos = DrawHandTile(G, Index, Tile, Pos, 5, 0, RotateFlipType.RotateNoneFlipNone, false);
                        }
                        break;
                    case Mahjong.Sets.SetType.MINKAN:
                        for (int j = 0; j < 4; j++)
                        {
                            RotateFlipType Rotate = RotateFlipType.RotateNoneFlipNone;

                            if (((N.FromWho == 1) && (j == 3)) ||
                                ((N.FromWho == 2) && (j == 1)) ||
                                ((N.FromWho == 3) && (j == 0))) Rotate = RotateFlipType.Rotate90FlipNone;

                            Pos = DrawHandTile(G, Index, N.Tiles[j], Pos, 5, 0, Rotate, false);
                        }
                        break;
                    case Mahjong.Sets.SetType.CHAKAN:
                        for (int j = 0; j < 4; j++)
                        {
                            RotateFlipType Rotate = RotateFlipType.RotateNoneFlipNone;
                            int YOffset = 0;

                            if (j == (3 - N.FromWho))
                            {
                                Rotate = RotateFlipType.Rotate90FlipNone;
                            }
                            // Added tile
                            if (j == (4 - N.FromWho))
                            {
                                Rotate = RotateFlipType.Rotate90FlipNone;
                                Pos -= mTileHeight;
                                YOffset = -mTileWidth;
                            }

                            Pos = DrawHandTile(G, Index, N.Tiles[j], Pos, 5, YOffset, Rotate, false);
                        }
                        break;
                }

                Pos += mTileWidth / 2;
            }
        }

        /// <summary>
        /// Нарисовать действия
        /// </summary>
        /// <param name="G"></param>
        private void DrawSteps(Graphics G)
        {
            int LastPlayer = -1;

            for (int i = 0; i < mRnd.Steps.Count; i++)
            {
                var S = mRnd.Steps[i];
                var I = mRnd.Data.Info[i + 4];

                Debug.WriteLine("DrawSteps: " + S.ToString());

                switch (S.Type)
                {
                    case StepType.DRAWTILE:
                        {
                            if (S.Player == mRnd.Dealer) mColumn++;

                            Step NextStep = mRnd.getNextStep(i);

                            // Is tsumogiri
                            bool Tsumogiri = ((NextStep.Type == StepType.DISCARDTILE) &&
                                ((NextStep as DiscardTile).Tile == (S as DrawTile).Tile));

                            bool Tsumo = ((NextStep.Type == StepType.TSUMO) &&
                                (NextStep.Player == S.Player));

                            bool Danger = false;
                            if (I.Danger.Length > 0)
                                Danger = I.Danger.Contains(new Tile((S as DrawTile).Tile).TileId);

                            mLastTile = (S as DrawTile).Tile;
                            LastPlayer = mPlayerIndex[S.Player];

                            string Comment = (Tsumo) ? "ツモ" : "";
                            DrawTsumoTile(G, mPlayerIndex[S.Player], mLastTile, Comment, Tsumogiri, Danger);

                            if (Tsumo) DrawYaku(G, mPlayerIndex[S.Player], S.Player);
                        }
                        break;
                    case StepType.DISCARDTILE:
                        {
                            mLastTile = (S as DiscardTile).Tile;

                            Step NextStep = mRnd.getNextStep(i);
                            Step PrevStep = mRnd.getPrevStep(i);

                            // Need to find nearest riichi declaration step
                            bool Riichi = (PrevStep.Type == StepType.RIICHI) &&
                                           (PrevStep.Player == S.Player);

                            bool Ron = ((NextStep.Type == StepType.RON) &&
                                        ((NextStep as Ron).From == S.Player));

                            bool Danger = false;
                            if (I.Danger.Length > 0)
                                Danger = I.Danger.Contains(new Tile((S as DiscardTile).Tile).TileId);

                            string Comment = "";

                            if (Ron)
                                Comment = "ロン";
                            else if (Riichi)
                                Comment = "リーチ";

                            LastPlayer = mPlayerIndex[S.Player];
                            DrawDiscardTile(G, mPlayerIndex[S.Player], (S as DiscardTile).Tile, Comment, Danger, I.Furiten, I.Shanten);
                        }
                        break;
                    case StepType.NAKI:
                        {
                            Naki N = S as Naki;
                            // Need to find nearest draw or discard tile step
                            string NakiType = "unk";
                            bool Kan = false;

                            switch (N.Set.Type)
                            {
                                case Mahjong.Sets.SetType.NUKI: NakiType = "抜き"; break;
                                case Mahjong.Sets.SetType.CHI: NakiType = "チー"; break;
                                case Mahjong.Sets.SetType.PON: NakiType = "ポン"; break;
                                case Mahjong.Sets.SetType.ANKAN: NakiType = "カン"; Kan = true; break;
                                case Mahjong.Sets.SetType.MINKAN: NakiType = "カン"; Kan = true; break;
                                case Mahjong.Sets.SetType.CHAKAN: NakiType = "カン"; Kan = true; break;
                            }

                            if (LastPlayer > mPlayerIndex[S.Player]) mColumn++;
                            LastPlayer = mPlayerIndex[S.Player];

                            bool Danger = false;
                            if (I.Danger.Length > 0)
                                Danger = I.Danger.Contains(new Tile(mLastTile).TileId);

                            DrawTsumoTile(G, mPlayerIndex[S.Player], mLastTile, NakiType, false, Danger);

                           // if (Kan && (N.Set.Type != Mahjong.Sets.SetType.ANKAN)) mColumn++;
                            // Can be ron after chakan or ankan!
                        }
                        break;
                    case StepType.RON:
                        {
                            if ((LastPlayer > mPlayerIndex[S.Player]) || (S.Player == mRnd.Dealer)) mColumn++;

                            DrawRon(G, mPlayerIndex[S.Player], "ロン", true);
                            DrawYaku(G, mPlayerIndex[S.Player], S.Player);
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Нарисовать отцентрированную в заданных пределах строку
        /// </summary>
        /// <param name="C"></param>
        /// <param name="F"></param>
        /// <param name="S"></param>
        /// <param name="Pointer"></param>
        /// <param name="Width"></param>
        /// <returns></returns>
        private PointF DrawCenteredString(Graphics G, Color C, Font F, string S, PointF Pointer, int Width)
        {
            Brush Br = new SolidBrush(C);

            SizeF Size = G.MeasureString(S, F);
            float fX = Pointer.X + (Width - Size.Width) / 2;
            float fY = Pointer.Y + Size.Height / 10;

            // Draw wind indicator
            G.DrawString(S, F, Br, fX, fY);

            return new PointF(Pointer.X, fY + Size.Height);
        }

        /// <summary>
        /// Нарисовать тайл из руки
        /// </summary>
        /// <param name="Index"></param>
        /// <param name="Tile"></param>
        /// <param name="Pos"></param>
        /// <param name="Line"></param>
        /// <param name="YOffset"></param>
        /// <param name="Rotate"></param>
        /// <param name="Danger"></param>
        /// <returns></returns>
        private int DrawHandTile(Graphics G, int Index, int Tile, int Pos, int Line, int YOffset, RotateFlipType Rotate, bool Danger)
        {
            Image TileImage;
            if (Tile == -1)
                TileImage = mTiles.getClosed(0);
            else
            {
                if (Danger && (mShowDanger != 0))
                    TileImage = getColorizedTile(Tile, Rotate, mDangerColor);
                else
                    TileImage = getTile(Tile, Rotate);
            }
            int X = cPaddingH + mRoundColumnWidth + mPlayerColumnWidth + cInternalPadding + Pos + mTileWidth;
            int Y = Index * mFieldHeight + cPaddingV + cInternalPadding + (mTileHeight * Line) + YOffset + mTileHeight - TileImage.Height;

            G.DrawImage(TileImage, new Point(X, Y));

            return Pos + TileImage.Width;
        }

        /// <summary>
        /// Нарисовать тайл, взятый со стены
        /// </summary>
        /// <param name="Index"></param>
        /// <param name="Tile"></param>
        /// <param name="Comment"></param>
        /// <param name="Tsumogiri"></param>
        /// <param name="Danger"></param>
        private void DrawTsumoTile(Graphics G, int Index, int Tile, string Comment, bool Tsumogiri, bool Danger)
        {
            int X = cPaddingH + mRoundColumnWidth + mPlayerColumnWidth + cInternalPadding + (mColumn) * mTileWidth;
            int Y = Index * mFieldHeight + cPaddingV + cInternalPadding + (mTileHeight * 2);

            Image TileImage;
            if (Tsumogiri)
                TileImage = mTiles.getTsumogiri(0);
            else
            {
                if (Danger && !Tsumogiri && (mShowDanger != 0))
                    TileImage = getColorizedTile(Tile, 0, mDangerColor);
                else
                    TileImage = getTile(Tile, 0);
            }

            G.DrawImage(TileImage, new Point(X, Y));
            DrawCenteredString(G, Color.Black, mFcomment, Comment, new PointF(X, Y - G.MeasureString(Comment, mFcomment).Height), mTileWidth);
        }

        /// <summary>
        /// Нарисовать тайл, сброшенный из руки
        /// </summary>
        /// <param name="Index"></param>
        /// <param name="Tile"></param>
        /// <param name="Comment"></param>
        /// <param name="Danger"></param>
        /// <param name="Furiten"></param>
        /// <param name="Shanten"></param>
        private void DrawDiscardTile(Graphics G, int Index, int Tile, string Comment, bool Danger, bool Furiten, int Shanten)
        {
            int X = cPaddingH + mRoundColumnWidth + mPlayerColumnWidth + cInternalPadding + (mColumn) * mTileWidth;
            int Y = Index * mFieldHeight + cPaddingV + cInternalPadding + (mTileHeight * 3);

            Image TileImage;
            if (Danger && (mShowDanger != 0))
                TileImage = getColorizedTile(Tile, 0, mDangerColor);
            else
                TileImage = getTile(Tile, 0);

            G.DrawImage(TileImage, new Point(X, Y));
            DrawCenteredString(G, Color.Black, mFcomment, Comment, new PointF(X, Y + mTileHeight), mTileWidth);

            if (mShowShanten != 0)
            {
                if (Furiten)
                {
                    Brush Gray = new SolidBrush(Color.FromArgb(200, Color.Black));
                    G.FillRectangle(Gray, X, Y + TileImage.Height, TileImage.Width, TileImage.Height / 10);
                }
                else if ((Shanten >= 0) && (Shanten < mShantenColor.Length))
                {
                    if (mShowColor != 0)
                    {
                        Brush BrColor = new SolidBrush(mShantenColor[Shanten]);
                        G.FillRectangle(BrColor, X, Y + TileImage.Height, TileImage.Width, TileImage.Height / 10);
                    }
                    if ((Comment.CompareTo("") == 0) && (Shanten > 0)) DrawCenteredString(G, Color.Black, mFcomment, Shanten.ToString(), new PointF(X, Y + mTileHeight), mTileWidth);
                }
            }
        }

        /// <summary>
        /// Нарисовать тайл указателя доры
        /// </summary>
        /// <param name="G"></param>
        /// <param name="Index"></param>
        /// <param name="Y"></param>
        /// <param name="Tile"></param>
        private void DrawDoraTile(Graphics G, int Index, int Y, int Tile)
        {
            int X = cPaddingH + Index * mTileWidth + mTileWidth / 2;

            G.DrawImage(getTile(Tile, 0), new Point(X, Y));
        }

        /// <summary>
        /// Нарисовать объявление победы по рону
        /// </summary>
        /// <param name="Index"></param>
        /// <param name="Comment"></param>
        /// <param name="Winner"></param>
        private void DrawRon(Graphics G, int Index, string Comment, bool Winner)
        {
            int Line = (Winner) ? 2 : 3; // draw tile line or discard tile line

            int X = cPaddingH + mRoundColumnWidth + mPlayerColumnWidth + cInternalPadding + (mColumn + 1 + ((!Winner) ? 1 : -1)) * mTileWidth;
            int Y = Index * mFieldHeight + cPaddingV + cInternalPadding + (mTileHeight * Line);

            DrawCenteredString(G, Color.Black, mFcomment, Comment, new PointF(X, Y + mTileHeight / 2 - G.MeasureString(Comment, mFcomment).Height / 2), mTileWidth);
        }

        /// <summary>
        /// Нарисовать список яку
        /// </summary>
        /// <param name="Index"></param>
        /// <param name="Player"></param>
        private void DrawYaku(Graphics G, int Index, int Player)
        {
            int X = cPaddingH + mRoundColumnWidth + mPlayerColumnWidth + mTilesColumnWidth - cInternalPadding - cYakuWidth;
            int Y = Index * mFieldHeight + cPaddingV + cInternalPadding;

            Font F = mFcomment;
            Brush Br = new SolidBrush(Color.Black);

            if (mShowYakuInfo == 0) return;

            Yaku[] YakuArray = mRnd.getYaku(Player);
            for (int i = 0; i < YakuArray.Length; i++)
            {
                int Yaku = YakuArray[i].Index;
                int Cost = YakuArray[i].Cost;

                string YakuName = Output.YakuName.GetYakuName(mLanguage, Yaku);

                G.DrawString(YakuName, F, Br, X, Y);
                G.DrawString(Cost.ToString(), F, Br, X + cCostOffset, Y);

                Y += Convert.ToInt32(G.MeasureString(YakuName, F).Height + cYakuOffset);
            }

            {
                string Fu = String.Format("{0:d}符", mRnd.FuCount(Player));
                string Han = String.Format("{0:d}翻", mRnd.HanCount(Player));

                G.DrawString(Fu, F, Br, X, Y);
                G.DrawString(Han, F, Br, X + cCostOffset, Y);

                Y += Convert.ToInt32(G.MeasureString(Fu, F).Height + cYakuOffset);
            }

            {
                string Cost = String.Format("{0:d}点", mRnd.HandCost(Player));
                G.DrawString(Cost, F, new SolidBrush(Color.Green), X, Y);
            }
        }

        /// <summary>
        /// Нарисовать число шантен
        /// </summary>
        /// <param name="Index"></param>
        /// <param name="Line"></param>
        /// <param name="Text"></param>
        private void DrawShanten(Graphics G, int Index, int Line, string Text)
        {
            int X = cPaddingH + mRoundColumnWidth + mPlayerColumnWidth + cInternalPadding;
            int Y = Index * mFieldHeight + cPaddingV + cInternalPadding + (mTileHeight * Line);

            DrawCenteredString(G, Color.Gray, mFcomment, Text, new PointF(X, Y + mTileHeight / 2 - G.MeasureString(Text, mFcomment).Height / 2), mTileWidth);
        }

        /// <summary>
        /// Получить картинку тайла
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        private Image getTile(int Index, RotateFlipType Rotate)
        {
            //Debug.WriteLine(String.Format("getTile({0:d})", Index));
            
            int Orientation;
            switch(Rotate)
            {
                case RotateFlipType.Rotate90FlipNone: Orientation = 3; break;
                case RotateFlipType.Rotate270FlipNone: Orientation = 1; break;
                default: Orientation = 0; break;
            }

            if (Index >= 0)
            {
                Tile T = new Tile(Index);
                if (mRedAri && T.Red)
                    return mTiles.getRed(Orientation, T.TileId);
                else
                    return mTiles.getRegular(Orientation, T.TileId);
            }
            else
                return mTiles.getClosed(Orientation);
        }

        /// <summary>
        /// Получить картинку тайла
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        private Image getColorizedTile(int Index, RotateFlipType Rotate, Color C)
        {
            //Debug.WriteLine(String.Format("getColorizedTile({0:d})", Index));

            Image I = new Bitmap(getTile(Index, Rotate));

            using (Graphics G = Graphics.FromImage(I))
            {
                Brush B = new SolidBrush(Color.FromArgb(50, C));
                G.FillRectangle(B, new Rectangle(0, 0, I.Width, I.Height));
            }

            return I;
        }
    }
}
