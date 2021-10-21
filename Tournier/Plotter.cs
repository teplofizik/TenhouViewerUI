using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Output;

namespace Tournier
{
    class Plotter
    {
        private Result R;

        public Plotter(Result Res)
        {
            R = Res;
        }

        public string GetValue(string Name)
        {
            switch (Name)
            {
                case "nickname": return String.Format("{0:s}", R.NickName); 
                case "rank": return String.Format("{0:s}", R.Rank.toJapaneseRank()); 
                case "rating": return String.Format("{0:d}R", R.Rating); 
                case "games": return String.Format("{0:d}", R.Places.Count); 
                case "placelist": return String.Format("{0:s}", R.GetPlaceList()); 
                case "place": return String.Format("{0:f}", R.AveragePlace);
                case "points": return String.Format("{0:d}", R.TotalPoints); 
                case "balance": return String.Format("{0:d}", R.TotalBalance);
                case "ron": return String.Format("{0:d}", R.RonCount);
                case "agari": return String.Format("{0:d}", R.AgariCount); 
                case "rounds": return String.Format("{0:d}", R.RoundCount);

                case "ronperc": return String.Format("{0:f}", (R.RoundCount > 0) ? (100.0f * R.RonCount / R.RoundCount) : 0.0f);
                case "agariperc": return String.Format("{0:f}", (R.RoundCount > 0) ? (100.0f * R.AgariCount / R.RoundCount) : 0.0f); 

                case "acq": return String.Format("+{0:d}", R.TotalAcquisitions); 

                case "acqron": return String.Format("{0:d}", R.RonAcquisitions); 
                case "acqdraw": return String.Format("{0:d}", R.DrawAcquisitions); 
                case "acqtsumo": return String.Format("{0:d}", R.TsumoAcquisitions); 

                case "loss": return String.Format("{0:d}", R.TotalLosses);

                case "lossron": return String.Format("{0:d}", R.RonLosses);
                case "lossdraw": return String.Format("{0:d}", R.DrawLosses); 
                case "losstsumo": return String.Format("{0:d}", R.TsumoLosses);

                case "1st": return String.Format("{0:f}%", GetPlacePercent(R, 1)); 
                case "2nd": return String.Format("{0:f}%", GetPlacePercent(R, 2)); 
                case "3rd": return String.Format("{0:f}%", GetPlacePercent(R, 3)); 
                case "4th": return String.Format("{0:f}%", GetPlacePercent(R, 4)); 

                //case "furiten": return String.Format("{0:d}", R.Furiten);
                //case "tempai": return String.Format("{0:d}", R.Tempai); 

                case "riichi": return String.Format("{0:d}", R.RiichiCount); 
                case "ippatsu": return String.Format("{0:d}", R.IppatsuCount); 
                case "riichiwin": return String.Format("{0:d}", R.RiichiWinCount); 
                case "lossriichi": return String.Format("{0:d}", -1000 * (R.RiichiCount - R.RiichiWinCount)); 
                case "opened": return String.Format("{0:d}", R.OpenedSetsCount); 
            }

            return "";
        }

        private float GetPlacePercent(Result R, int Place)
        {
            int Count = 0;

            if (R.Places.Count == 0) return 0.0f;

            for (int i = 0; i < R.Places.Count; i++)
            {
                if (R.Places[i] == Place) Count++;
            }

            return 100.0f * Count / R.Places.Count;
        }
    }
}
