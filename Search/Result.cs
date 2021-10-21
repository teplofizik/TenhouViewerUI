using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Mahjong;
using Parse;

namespace Search
{
    // Результат для использования при поиске
    [Serializable]
    public class LightResult
    {
        [XmlIgnore]
        public Replay R;

        /// <summary>
        /// Хеш раздачи (для сопоставления при загрузке)
        /// </summary>
        public string Hash;

        // Метка годности игры
        public bool ReplayMark;
        // Метка годности игрока
        public List<bool> PlayerMark = new List<bool>();

        public LightResult()
        {

        }

        public LightResult(Replay R)
        {
            this.R = R;
            this.Hash = R.Hash;
            ReplayMark = false;
            PlayerMark.Fill(R.PlayerCount, false);
        }
    }

    // Результат для использования при поиске
    [Serializable]
    public class Result : LightResult
    {
        // Метка годности раздачи
        public List<bool> RoundMark = new List<bool>();

        // Раздачи
        public List<bool[]> HandMark = new List<bool[]>();

        public Result()
        {

        }

        public Result(Replay R) : base(R)
        {
            RoundMark.Fill(R.Rounds.Count, false);

            for (int i = 0; i < R.Rounds.Count; i++)
            {
                bool[] Marks = new bool[R.PlayerCount];
                HandMark.Add(Marks);
            }
        }

        public Result(LightResult Res)
        {
            Hash = Res.Hash;
            R = Res.R;
            ReplayMark = Res.ReplayMark;
            PlayerMark = new List<bool>(Res.PlayerMark);

            RoundMark.Fill(R.Rounds.Count, false);
            for (int i = 0; i < R.Rounds.Count; i++)
            {
                bool[] Marks = new bool[R.PlayerCount];
                HandMark.Add(Marks);
            }
        }

        public Result(Result Result)
        {
            PlayerMark.Fill(R.PlayerCount, false);

            // Только метки раздач остаются
            RoundMark = new List<bool>(Result.RoundMark);

            for (int i = 0; i < R.Rounds.Count; i++)
            {
                bool[] Marks = new bool[R.PlayerCount];

                HandMark.Add(Marks);
            }
        }

        public bool Check()
        {
            if (ReplayMark) return false;

            for (int i = 0; i < RoundMark.Count; i++)
            {
                if (RoundMark[i]) continue;

                for (int j = 0; j < R.PlayerCount; j++)
                {
                    if (!PlayerMark[j] && !HandMark[i][j]) return true;
                }
            }

            return false;
        }

        public OutputResult Build()
        {
            OutputResult Res = new OutputResult();

            Res.Skip = ReplayMark;
            for (int i = 0; i < RoundMark.Count; i++)
            {
                bool[] Hands = new bool[R.PlayerCount];

                for (int j = 0; j < R.PlayerCount; j++)
                {
                    Hands[j] = !PlayerMark[j] && !RoundMark[i] && !HandMark[i][j];
                }

                Res.HandMark.Add(Hands);
            }

            return Res;
        }
    }

    /// <summary>
    /// Результат для использования в выдаче
    /// </summary>
    public class OutputResult
    {
        public bool Skip;
        public List<bool[]> HandMark = new List<bool[]>();

        public void Add(OutputResult X)
        {
            for (int i = 0; i < X.HandMark.Count; i++)
            {
                for (int j = 0; j < X.HandMark[i].Length; j++)
                {
                    if (X.HandMark[i][j]) HandMark[i][j] = true;
                }
            }
        }
    }
}
