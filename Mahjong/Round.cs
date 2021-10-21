using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Output;
using Mahjong.Helper;

namespace Mahjong
{
    [Serializable]
    [XmlType("Round")]
    public class Round
    {
        // Номер раздачи
        public int Index;

        /// <summary>
        /// Баланс перед раздачей
        /// </summary>
        public List<int> Balance = new List<int>();
        /// <summary>
        /// Баланс после раздачи
        /// </summary>
        public List<int> BalanceAfter = new List<int>();

        /// <summary>
        /// Выплаты в результате раздачи
        /// </summary>
        public List<int> Pay = new List<int>();

        /// <summary>
        /// Начальные руки
        /// </summary>
        public List<int[]> Hands = new List<int[]>();

        /// <summary>
        /// Выигрыш
        /// </summary>
        public List<Agari> Agari = new List<Agari>();

        /// <summary>
        /// Действия
        /// </summary>
        public List<Steps.Step> Steps = new List<Steps.Step>();

        /// <summary>
        /// Указатели дор
        /// </summary>
        public int Dora;

        /// <summary>
        /// Кто дилер?
        /// </summary>
        public int Dealer;

        /// <summary>
        /// Количество риичи-палок перед началом раздачи
        /// </summary>
        public int RiichiStick;

        /// <summary>
        /// Количество ренчанов
        /// </summary>
        public int RenchanStick;

        /// <summary>
        /// Разбор игры (ожидания, руки, шантен)
        /// </summary>
        [XmlIgnore]
        public RoundData Data = null;

        /// <summary>
        /// Стена
        /// </summary>
        public Wall Wall;

        private Replay mR = null;

        public void ExpandData(Replay R)
        {
            if (mR == null) mR = R;
            if (Data == null) Data = new RoundData(R, this);
        }

        /// <summary>
        /// Получить тайлы указателей дор
        /// </summary>
        /// <returns></returns>
        public int[] getDoraTiles()
        {
            List<int> DoraList = new List<int>();
            // 5,7,9,11 - dora pointer

            if (Wall != null)
                for (int i = 0; i < 1 + Steps.Count(S => S.Type == Mahjong.Steps.StepType.DORA); i++)
                    DoraList.Add(Wall.Tiles[5 + i * 2]);

            return DoraList.ToArray();
        }

        /// <summary>
        /// Получить тайлы указателей ура-дор
        /// </summary>
        /// <returns></returns>
        public int[] getUraDoraTiles()
        {
            List<int> DoraList = new List<int>();
            // 4,6,8,10 - ura pointer

            if (Wall != null)
                for (int i = 0; i < 1 + Steps.Count(S => S.Type == Mahjong.Steps.StepType.DORA); i++)
                    DoraList.Add(Wall.Tiles[4 + i * 2]);

            return DoraList.ToArray();
        }

        /// <summary>
        /// Получить следующий шаг
        /// </summary>
        /// <returns></returns>
        public Steps.Step getNextStep(int Step)
        {
            for(int i = Step + 1; i < Steps.Count; i++)
            {
                Steps.Step S = Steps[i];
                switch(S.Type)
                {
                    case Mahjong.Steps.StepType.CONNECT: continue;
                    case Mahjong.Steps.StepType.DISCONNECT: continue;
                    default: return S;
                }
            }

            return null;
        }


        /// <summary>
        /// Получить предыдущий шаг
        /// </summary>
        /// <returns></returns>
        public Steps.Step getPrevStep(int Step)
        {
            for (int i = Step - 1; i >= 0; i--)
            {
                Steps.Step S = Steps[i];
                switch (S.Type)
                {
                    case Mahjong.Steps.StepType.CONNECT: continue;
                    case Mahjong.Steps.StepType.DISCONNECT: continue;
                    default: return S;
                }
            }

            return null;
        }

        public override string ToString()
        {
            if (this.IsDraw())
                return String.Format("{0:s}: Draw {1:s}", getRoundName(), this.DrawReason().ToDrawReason());
            else if (this.isTsumoAgari())
            {
                var A = Agari[0];
                string Nick = getNickName(A.Who);

                return String.Format("{0:s}: Tsumo by {1:s} ({2:d})", getRoundName(), Nick, A.Cost);
            }
            else
            {
                string Loser = getNickName(Agari[0].From);
                string Winner = "";
                for (int i = 0; i < Agari.Count; i++)
                {
                    var A = Agari[i];
                    Winner += String.Format("{0:s} ({1:d})", getNickName(A.Who), A.Cost);

                    if (i != Agari.Count - 1) Winner += ", ";
                }
                return String.Format("{0:s}: Ron by {1:s} from {2:s}", getRoundName(), Winner, Loser);
            }
        }

        private string getRoundName()
        {
            return (mR == null) ? String.Format("Round {0:d}", Index + 1) : mR.RoundName(Index);
        }

        private string getNickName(int Player)
        {
            return (mR == null) ? Player.ToString() : mR.Players[Player].NickName;
        }
    }
}
