using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mahjong.Statistic
{
    static class StatisticExtension
    {
        /// <summary>
        /// Возвращает количество открытых сетов
        /// </summary>
        /// <param name="R">Раздача</param>
        /// <param name="Player">Номер игрока</param>
        /// <returns></returns>
        public static int OpenedSets(this Round R, int Player)
        {
            int Count = 0;

            foreach (Steps.Step S in R.Steps)
            {
                if (S.Player != Player) continue;

                if (S.Type == Steps.StepType.NAKI)
                {
                    Steps.Naki N = S as Steps.Naki;

                    if ((N.Set.Type == Sets.SetType.CHAKAN) || (N.Set.Type == Sets.SetType.NUKI)) continue;

                    Count++;
                }
            }

            return Count;
        }
    }
}
