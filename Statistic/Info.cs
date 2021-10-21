using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Statistic
{
    class Info
    {
        /// <summary>
        /// Ник игрока
        /// </summary>
        public string NickName;

        /// <summary>
        /// Ранг игрока (текст)
        /// </summary>
        public string Rank;

        /// <summary>
        /// Рейтинг игрока
        /// </summary>
        public int Rating;

        /// <summary>
        /// Количество игр
        /// </summary>
        public int Games;

        /// <summary>
        /// Количество раздач
        /// </summary>
        public int Rounds;

        /// <summary>
        /// Среднее занятое место
        /// </summary>
        public double AveragePlace;

        /// <summary>
        /// Счётчики занятых мест
        /// </summary>
        public int[] PlaceCounters = new int[4];

        /// <summary>
        /// Процент наброса 
        /// </summary>
        public double Loser;

        /// <summary>
        /// Сколько раз набросил
        /// </summary>
        public int RonCount;

        /// <summary>
        /// Сколько раз набросил
        /// </summary>
        public int RonFromRiichi;

        /// <summary>
        /// Сколько раз ушёл в минус?
        /// </summary>
        public int MinusCount;

        /// <summary>
        /// Сколько раз объявил риичи
        /// </summary>
        public int RiichiCount;

        /// <summary>
        /// Сколько успешно завершённых риичи
        /// </summary>
        public int SuccessfulRiichiCount;

        /// <summary>
        /// Сколько раз было иппацу
        /// </summary>
        public int IppatsuCount;

        /// <summary>
        /// Стоимость рук
        /// </summary>
        public List<int> HandCost = new List<int>();

        /// <summary>
        /// Количество собранных рук
        /// </summary>
        public int AgariCount
        {
            get { return HandCost.Count; }
        }
    }
}
