using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using Mahjong;
using Output;
using Search;

namespace Viewer
{
    class GameInfo
    {
        /// <summary>
        /// Реплей
        /// </summary>
        public LightResult R;

        /// <summary>
        /// Цвет фона для отображения игры в списке
        /// </summary>
        public Color BackColor
        {
            get
            {
                if (R.R == null) return Color.Red;

                return (R.R.Lobby == 0) ? Color.LightGreen : Color.LightYellow;
            }
        }

        /// <summary>
        /// Информация об игроке
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        public string GetPlayerInfo(int Index)
        {
            if (Index >= R.R.PlayerCount) return String.Empty;

            return String.Format("{0:d}: {1:s}", R.R.Place[Index], R.R.Players[Index].NickName);
        }

        /// <summary>
        /// Информация об игроке 
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        public string GetPlayerRankedInfo(int Index)
        {
            if (Index >= R.R.PlayerCount) return String.Empty;
            Player P = R.R.Players[Index];

            return String.Format("{1:s} ({2:s} {3:d}R) - {0:d}位 ({4:+#;-#}.0)",
                                R.R.Place[Index],
                                P.NickName,
                                P.Rank.toJapaneseRank(),
                                P.Rating,
                                R.R.Result[Index]);
        }

        public GameInfo(Replay R)
        {
            this.R = new Result(R);
        }


        public GameInfo(LightResult R)
        {
            this.R = R;
        }
    }
}
