using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Output;

namespace Mahjong.Helper
{
    static class HelperExtensions
    {
        /// <summary>
        /// Участвует ли игрок в раздаче?
        /// </summary>
        /// <param name="R"></param>
        /// <param name="NickName"></param>
        /// <returns></returns>
        public static bool HasSuchPlayer(this Replay R, string NickName)
        {
            return R.Players.Exists(P => (P.NickName.CompareTo(NickName) == 0));
        }

        /// <summary>
        /// Номер игрока в раздаче
        /// </summary>
        /// <param name="R"></param>
        /// <param name="NickName"></param>
        /// <returns>Номер игрока, если есть, иначе -1</returns>
        public static int GetPlayerIndex(this Replay R, string NickName)
        {
            for (int i = 0; i < R.PlayerCount; i++)
            {
                var P = R.Players[i];
                if (P.NickName.CompareTo(NickName) == 0) return i;
            }

            return -1;
        }

        /// <summary>
        /// Число шантен для руки
        /// </summary>
        /// <param name="H">Рука</param>
        /// <returns></returns>
        public static int GetShanten(this Hand H)
        {
            return new ShantenCalculator(H).GetShanten();
        }

        /// <summary>
        /// Собрал ли игрок руку?
        /// </summary>
        /// <param name="R"></param>
        /// <param name="Player"></param>
        /// <returns></returns>
        public static bool hasAgari(this Round R, int Player)
        {
            return R.Agari.Exists(A => (A.Who == Player));
        }
        
        /// <summary>
        /// Ничья?
        /// </summary>
        /// <param name="R"></param>
        /// <param name="Player"></param>
        /// <returns></returns>
        public static bool IsDraw(this Round R)
        {
            return R.Steps.Exists(S => (S.Type == Steps.StepType.DRAW));
        }

        /// <summary>
        /// Причина ничьей
        /// </summary>
        /// <param name="R"></param>
        /// <param name="Player"></param>
        /// <returns></returns>
        public static int DrawReason(this Round R)
        {
            foreach (Steps.Step S in R.Steps)
                if (S.Type == Steps.StepType.DRAW) return (S as Steps.Draw).Reason + 1;

            return -1;
        }

        /// <summary>
        /// Собрал ли игрок руку по цумо?
        /// </summary>
        /// <param name="R"></param>
        /// <param name="Player"></param>
        /// <returns></returns>
        public static bool hasTsumoAgari(this Round R, int Player)
        {
            return R.Agari.Exists(A => (A.Who == Player) && (A.Who == A.From));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="R"></param>
        /// <param name="Player"></param>
        /// <returns></returns>
        public static Yaku[] getYaku(this Round R, int Player)
        {
            foreach (Agari A in R.Agari)
                if (A.Who == Player) return A.Yaku.ToArray();

            return null;
        }

        /// <summary>
        /// Победа в раздаче по рону?
        /// </summary>
        /// <param name="R"></param>
        /// <param name="Player"></param>
        /// <returns></returns>
        public static bool isRonAgari(this Round R)
        {
            return R.Agari.Exists(A => (A.Who != A.From));
        }

        /// <summary>
        /// Победа в раздаче по цумо?
        /// </summary>
        /// <param name="R"></param>
        /// <param name="Player"></param>
        /// <returns></returns>
        public static bool isTsumoAgari(this Round R)
        {
            return R.Agari.Exists(A => (A.Who == A.From));
        }

        /// <summary>
        /// Набросил ли игрок?
        /// </summary>
        /// <param name="R"></param>
        /// <param name="Player"></param>
        /// <returns></returns>
        public static bool isLoser(this Round R, int Player)
        {
            return R.Agari.Exists(A => (A.From == Player)  && (A.From != A.Who));
        }

        /// <summary>
        /// Кто набросил?
        /// </summary>
        /// <param name="R"></param>
        /// <param name="Player"></param>
        /// <returns></returns>
        public static int Loser(this Round R)
        {
            foreach (Agari A in R.Agari)
                if (A.From != A.Who) return A.From;

            return -1;
        }

        /// <summary>
        /// Объявил риичи?
        /// </summary>
        /// <param name="R"></param>
        /// <param name="Player"></param>
        /// <returns></returns>
        public static bool isDeclaredRiichi(this Round R, int Player)
        {
            return R.Steps.Exists(S => (S.Type == Steps.StepType.RIICHI1000) && (S.Player == Player));
        }

        /// <summary>
        /// Не открывал руку?
        /// </summary>
        /// <param name="R"></param>
        /// <param name="Player"></param>
        /// <returns></returns>
        public static bool isConcealed(this Round R, int Player)
        {
            foreach (Steps.Step S in R.Steps)
            {
                if ((S.Type == Steps.StepType.NAKI) && (S.Player == Player))
                {
                    Sets.Set Set = (S as Steps.Naki).Set;

                    switch (Set.Type)
                    {
                        case Sets.SetType.CHI:
                        case Sets.SetType.PON:
                        case Sets.SetType.MINKAN:
                            return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Количество ходов игрока
        /// </summary>
        /// <param name="R"></param>
        /// <param name="Player"></param>
        /// <returns></returns>
        public static int getActionCount(this Round R, int Player)
        {
            return R.Steps.Count(S => (S.Player == Player));
        }

        /// <summary>
        /// Количество ходов игрока
        /// </summary>
        /// <param name="R"></param>
        /// <param name="Player"></param>
        /// <returns></returns>
        public static int getStepCount(this Round R, int Player)
        {
            return R.Steps.Count(S => ((S.Player == Player) && (S.Type == Steps.StepType.DISCARDTILE)));
        }
        /// <summary>
        /// Есть ли у игрока в данной раздаче заданное яку?
        /// </summary>
        /// <param name="R"></param>
        /// <param name="Player"></param>
        /// <param name="Yaku"></param>
        /// <returns></returns>
        public static bool hasYaku(this Round R, int Player, int Yaku)
        {
            foreach (Agari A in R.Agari)
            {
                if (Player != A.Who) continue;

                if (A.Yaku.Exists(Y => Y.Index == Yaku)) return true;
            }
            return false;
        }

        /// <summary>
        /// Стоимость руки
        /// </summary>
        /// <param name="R"></param>
        /// <param name="Player"></param>
        /// <returns></returns>
        public static int HandCost(this Round R, int Player)
        {
            Agari A = R.Agari.Find(a => (Player == a.Who));
            return (A != null) ? A.Cost : 0;
        }

        /// <summary>
        /// Количество хан в руке
        /// </summary>
        /// <param name="R"></param>
        /// <param name="Player"></param>
        /// <returns></returns>
        public static int HanCount(this Round R, int Player)
        {
            Agari A = R.Agari.Find(a => (Player == a.Who));
            return (A != null) ? A.Yaku.Sum(Y => Y.Cost) : 0;
        }

        /// <summary>
        /// Количество фу в руке
        /// </summary>
        /// <param name="R"></param>
        /// <param name="Player"></param>
        /// <returns></returns>
        public static int FuCount(this Round R, int Player)
        {
            Agari A = R.Agari.Find(a => (Player == a.Who));
            return (A != null) ? A.Fu : 0;
        }

        /// <summary>
        /// Сколько раз собирал руку?
        /// </summary>
        /// <param name="R"></param>
        /// <param name="Player"></param>
        /// <returns></returns>
        public static int AgariCount(this Replay R, int Player)
        {
            return R.Rounds.Count(Rnd => Rnd.hasAgari(Player));
        }

        /// <summary>
        /// Сколько раз собирал руку по цумо?
        /// </summary>
        /// <param name="R"></param>
        /// <param name="Player"></param>
        /// <returns></returns>
        public static int TsumoCount(this Replay R, int Player)
        {
            return R.Rounds.Count(Rnd => Rnd.hasTsumoAgari(Player));
        }

        /// <summary>
        /// Номер раздачи (0 - 1 восток, 1 - 2 восток...)
        /// </summary>
        /// <param name="R"></param>
        /// <param name="Player"></param>
        /// <returns></returns>
        public static int RoundIndex(this Replay R, int Index)
        {
            int Count = 0;
            int LastDealer = R.Rounds[0].Dealer;

            foreach (Round Rnd in R.Rounds)
            {
                if (LastDealer != Rnd.Dealer)
                {
                    LastDealer = Rnd.Dealer;
                    Count++;
                }

                if (Index == Rnd.Index) return Count;
            }

            return Count;
        }

        /// <summary>
        /// Ветер раздачи (0 - восток, 1 - юг...)
        /// </summary>
        /// <param name="R"></param>
        /// <param name="Player"></param>
        /// <returns></returns>
        public static int RoundWind(this Replay R, int Index)
        {
            return (RoundIndex(R, Index) / R.PlayerCount);
        }

        /// <summary>
        /// Ветер и номер раздачи (восток-1 и т.д.)
        /// </summary>
        /// <param name="R"></param>
        /// <param name="Player"></param>
        /// <returns></returns>
        public static string RoundName(this Replay R, int Index)
        {
            return String.Format("{0:s}{1:d}", R.RoundWind(Index).ToWind(), (R.RoundIndex(Index) % R.PlayerCount) + 1);
        }

        /// <summary>
        /// Ветер раздачи (0 - восток, 1 - юг...)
        /// </summary>
        /// <param name="R"></param>
        /// <param name="Player"></param>
        /// <returns></returns>
        public static int PlayerWind(this Replay R, int Index, int Player)
        {
            return (RoundIndex(R, Index) + Player) % R.PlayerCount;
        }

        /// <summary>
        /// Есть ли красные доры?
        /// </summary>
        /// <param name="R"></param>
        /// <returns></returns>
        public static bool HasAkaDora(this Replay R)
        {
            return (!R.LobbyType.HasFlag(LobbyType.NOAKA));
        }
    }
}
