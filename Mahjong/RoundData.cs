using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mahjong.Helper;
using Mahjong.Steps;

namespace Mahjong
{
    /// <summary>
    /// Все руки, опасные тайлы, ожидания и т.д.
    /// </summary>
    public class RoundData
    {
        public class FullStepInfo
        {
            public Hand Hand;
            public int  Shanten;
            public int[] WaitingList;
            public bool Furiten = false;
            public int Player;
            public int[] Danger = new int[0];

            public FullStepInfo(int P)
            {
                Hand = null;
                Shanten = 0;
                WaitingList = new int[0];

                Player = P;
            }

            public FullStepInfo(int P, Hand H)
            {
                Hand = H;
                Player = P;
                Shanten = -1;
                WaitingList = new int[0];
            }

            public FullStepInfo(int P, Hand H, IList<int> Discard)
            {
                Hand = H;
                Player = P;

                var SC = new ShantenCalculator(H);
                Shanten = SC.GetShanten();
                WaitingList = SC.WaitingList().ToArray();

                if(Discard != null)
                    Furiten = IsFuriten(Discard, WaitingList);
                else
                    Furiten = false;
            }

            private bool IsFuriten(IList<int> Discard, IList<int> Waitings)
            {
                for (int i = 0; i < Waitings.Count; i++)
                    if (Discard.Contains(Waitings[i])) return true;

                return false;
            }

            public override string ToString()
            {
                if (Hand == null)
                {
                    if (Player < 0)
                        return "Info: None (no player)";
                    else
                        return String.Format("Info: None (player {0:d})", Player);
                }
                else if (Shanten == -1)
                    return String.Format("Agari: player {0:d}", Player);
                else
                    return String.Format("Step: player {0:d}, shanten {1:d}", Player, Shanten);
            }
        }

        private List<FullStepInfo> mHands;
        private bool[] mTempai;
        private bool[] mFuriten;
        private List<int>[] mDiscard;
        private List<int>[] mWaiting;

        public FullStepInfo[] Info
        {
            get { return mHands.ToArray(); }
        }

        public RoundData(Replay R, Round Rnd)
        {
            mHands = new List<FullStepInfo>();
            mTempai = new bool[R.PlayerCount];
            mFuriten = new bool[R.PlayerCount];
            mDiscard = new List<int>[R.PlayerCount];
            mWaiting = new List<int>[R.PlayerCount];

            Hand[] TempHands = new Hand[R.PlayerCount];

            List<int>[] TempWaitings = new List<int>[R.PlayerCount];
            for (int i = 0; i < R.PlayerCount; i++)
            {
                TempHands[i] = new Hand(Rnd.Hands[i]);

                mDiscard[i] = new List<int>();
                mWaiting[i] = new List<int>();
                mTempai[i] = false;
                mFuriten[i] = false;
                mHands.Add(new FullStepInfo(i, new Hand(Rnd.Hands[i]), mDiscard[i]));
            }

            for (int i = 0; i < Rnd.Steps.Count; i++)
            {
                Step S = Rnd.Steps[i];

                switch (S.Type)
                {
                    case StepType.DRAWTILE: DrawTile(TempHands[S.Player], S.Player, (S as DrawTile).Tile); break;
                    case StepType.DISCARDTILE: DiscardTile(TempHands[S.Player], S.Player, (S as DiscardTile).Tile); break;
                    case StepType.NAKI: Naki(TempHands[S.Player], S.Player, (S as Naki).Set); break;
                    case StepType.TSUMO: Agari(TempHands[S.Player], S.Player); break;
                    case StepType.RON: Agari(TempHands[S.Player], S.Player); break;
                    default: mHands.Add(new FullStepInfo(S.Player)); break;
                }
            }
        }

        /// <summary>
        /// Взять тайл
        /// </summary>
        /// <param name="Player"></param>
        /// <param name="Tile"></param>
        private void DrawTile(Hand H, int Player, int Tile)
        {
            H.Tiles.Add(Tile);

            var SI = new FullStepInfo(Player);
            SI.Danger = GetDangerTiles(Player);
            mHands.Add(SI);
        }

        /// <summary>
        /// Сбросить тайл
        /// </summary>
        /// <param name="Player"></param>
        /// <param name="Tile"></param>
        private void DiscardTile(Hand H, int Player, int Tile)
        {
            H.Tiles.Remove(Tile);
            mDiscard[Player].Add(new Tile(Tile).TileId);

            var SI = new FullStepInfo(Player, new Hand(H), mDiscard[Player]);
            if (SI.Shanten == 0) mTempai[Player] = true;
            mWaiting[Player] = new List<int>(SI.WaitingList);
            mFuriten[Player] = SI.Furiten;
            SI.Danger = GetDangerTiles(Player);
            mHands.Add(SI);
        }

        /// <summary>
        /// Объявление
        /// </summary>
        /// <param name="Player"></param>
        /// <param name="Set"></param>
        private void Naki(Hand H, int Player, Sets.Set Set)
        {
            H.makeNaki(Set);
            var SI = new FullStepInfo(Player, new Hand(H), mDiscard[Player]);
            
            SI.Danger = GetDangerTiles(Player);
            if (SI.Shanten == 0) mTempai[Player] = true;
            mHands.Add(SI);
        }

        /// <summary>
        /// Цумо или рон
        /// </summary>
        /// <param name="Player"></param>
        private void Agari(Hand H, int Player)
        {
            var SI = new FullStepInfo(Player, new Hand(H));
            SI.Danger = GetDangerTiles(Player);
            mHands.Add(SI);
        }

        /// <summary>
        /// Получить список опасных тайлов
        /// </summary>
        /// <param name="Player"></param>
        /// <returns></returns>
        private int[] GetDangerTiles(int Player)
        {
            List<int> Tiles = new List<int>();

            for (int i = 0; i < mWaiting.Length; i++)
            {
                if (i == Player) continue;
                if (mFuriten[i]) continue;

                Tiles.AddRange(mWaiting[i]);
            }

            return Tiles.ToArray();
        }
    }
}
