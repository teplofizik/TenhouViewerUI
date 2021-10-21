using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Resources;

using Output;
using Search;
using Tournier;
using Mahjong;
using Mahjong.Helper;
using Statistic;
using Extension.Forms;
using Extension.Math;

namespace TenhouViewerUI
{
    public partial class fLastStat : NestedForm
    {
        /// <summary>
        /// Сколько последних игр брать
        /// </summary>
        private int[] Limits = { 5, 10, 15, 25, 50, 100 };

        // Цвета для среднего места
        private double[] AveragePlaceColors = { 1.0, 1.8, 2.5, 4.0 };
        // Цвета для набросов
        private double[] LoserColors = { 0.0, 5.0, 15.0, 100.0 };
        // Цвета для ухода в минус
        private double[] MinusColors = { 0.0, 5.0, 10.0, 100.0 };
        // Для собранных рук
        private double[] AgariColors = { 100.0, 20.0, 10.0, 0.0 };
        // Стоимость собранных рук
        private double[] AvgCostColors = { 0.0, 2000.0, 8000.0, 64000.0 };
        // Для успешных риичи
        private double[] RiichiColors = { 100.0, 20.0, 10.0, 0.0 };


        /// <summary>
        /// Для локализации
        /// </summary>
        private ResourceManager rm;

        /// <summary>
        /// Список реплеев
        /// </summary>
        private List<Search.Result> Results = new List<Search.Result>();

        public fLastStat()
        {
            InitializeComponent();

            rm = new ResourceManager("TenhouViewerUI.fLastStat",
                System.Reflection.Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Локализованная строа
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        private string GetLocalizedString(string Name)
        {
            return rm.GetString(Name, System.Globalization.CultureInfo.CurrentCulture);
        }

        private Color GetParamColor(double Value, double[] Limits)
        {
            if (Value.IsBetween(Limits[0], Limits[1])) return Color.DarkGreen;
            if (Value.IsBetween(Limits[1], Limits[2])) return Color.DarkOliveGreen;
            if (Value.IsBetween(Limits[2], Limits[3])) return Color.DarkRed;

            return Color.Black;
        }

        /// <summary>
        /// Посчитать статистику
        /// </summary>
        /// <param name="Period"></param>
        private void CalcStatistics(int Period)
        {
            Info I = Collector.ProcessData(Results, TP.Owner, 0, Period);
            if (I == null) return;

            // Общая информация об игроке
            lNick.Text = I.NickName;
            lRankValue.Text = I.Rank;
            lRating.Text = String.Format("{0:d}R", I.Rating);

            // Среднее место
            lAveragePlaceValue.Text = String.Format("{0:0.00}", I.AveragePlace);
            lAveragePlaceValue.ForeColor = GetParamColor(I.AveragePlace, AveragePlaceColors);

            // Места
            lPlaces.Text = String.Format("({0:d}, {1:d}, {2:d}, {3:d})",
                                         I.PlaceCounters[0],
                                         I.PlaceCounters[1],
                                         I.PlaceCounters[2],
                                         I.PlaceCounters[3]);

            // Заронились
            lLoserValue.Text = String.Format("{0:0.00}% ({1:d})", I.Loser, I.RonCount);
            lLoserValue.ForeColor = GetParamColor(I.Loser, LoserColors);

            // Ушёл в минус
            lFalledValue.Text = String.Format("{0:0.00}% ({1:d})", I.MinusCount * 100.0 / I.Games, I.MinusCount);
            lFalledValue.ForeColor = GetParamColor(I.MinusCount * 100.0 / I.Games, MinusColors);

            // Собранные руки
            lCompletedValue.Text = String.Format("{0:0.00}% ({1:d})", I.AgariCount * 100.0 / I.Rounds, I.AgariCount);
            lCompletedValue.ForeColor = GetParamColor(I.AgariCount * 100.0 / I.Rounds, AgariColors);

            // Стоимость руки
            int AverageCost = Convert.ToInt32(I.HandCost.Average());
            lCostValue.Text = String.Format("{0:d}", AverageCost);
            lCostValue.ForeColor = GetParamColor(AverageCost, AvgCostColors);

            // Успешные риичи
            double SuccessfulRiichi = (I.RiichiCount > 0) ? I.SuccessfulRiichiCount * 100.0 / I.RiichiCount : 0.0;
            double Ippatsu = (I.SuccessfulRiichiCount > 0) ? I.IppatsuCount * 100.0 / I.SuccessfulRiichiCount : 0.0;
            lRiichiOkValue.Text = String.Format("{0:0.00}% ({1:d})", SuccessfulRiichi, I.SuccessfulRiichiCount);
            lRiichiOkValue.ForeColor = (I.RiichiCount > 0) ? GetParamColor(SuccessfulRiichi, RiichiColors) : Color.Black;
            lIppatsuValue.Text = String.Format("{0:0.00}% ({1:d})", Ippatsu, I.IppatsuCount);
            lIppatsuValue.ForeColor = (I.SuccessfulRiichiCount > 0) ? GetParamColor(Ippatsu, RiichiColors) : Color.Black;
            
            // Наброс из риичи
            double FromRiichi = (I.RonCount > 0) ? I.RonFromRiichi * 100.0 / I.RonCount : 0.0;
            lFromRiichiValue.Text = String.Format("{0:0.00}% ({1:d})", FromRiichi, I.RonFromRiichi);
            lFromRiichiValue.ForeColor = (I.RonCount > 0) ? GetParamColor(FromRiichi, LoserColors) : Color.Black;
        }

        /// <summary>
        /// Загрузить игры из лобби 0000 заданного игрока
        /// </summary>
        private void LoadRelatedGames(int Period)
        {
            Results = Collector.FindResults(TP, Limits.Last()*2);
        }

        /// <summary>
        /// При загрузке проекта
        /// </summary>
        protected override void OnProjectLoad()
        {
            LoadRelatedGames(Limits.Last());

            int Games = Results.Count;
            for (int i = 0; i < Limits.Length; i++)
            {
                if (Limits[i] > Games) break;
                cbPeriod.Items.Add(String.Format(GetLocalizedString("LastGames"), Limits[i]));
            }

            if (Games >= Limits[0])
            {
                cbPeriod.SelectedIndex = 0;
                CalcStatistics(Limits[0]);
            }
        }

        /// <summary>
        /// При выгрузе проекта
        /// </summary>
        protected override void OnProjectUnload()
        {

        }

        private void fLastStat_Load(object sender, EventArgs e)
        {
            CheckProject();
        }

        private void fLastStat_Resize(object sender, EventArgs e)
        {
            int W = ClientSize.Width;
            int H = ClientSize.Height;

            cbPeriod.Width = W - 24;
        }

        private void cbPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcStatistics(Limits[cbPeriod.SelectedIndex]);
        }
    }
}
