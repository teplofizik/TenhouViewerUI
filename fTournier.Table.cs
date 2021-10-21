using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Search;
using Tournier;
using System.Windows.Forms;
using Viewer;

namespace TenhouViewerUI
{
    public partial class fTournier : NestedForm
    {
        private List<Tournier.Result> TR = null;
        private List<Tournier.Result> TR_Replacement = null;

        private void CalcResult()
        {
            CalcPartialResult(false);
            CalcPartialResult(true);
        }

        private void CalcPartialResult(bool Replacement)
        {
            PlayerManage PM = RP.UserField as PlayerManage;
            List<Search.Result> Results = new List<Search.Result>();
            foreach (var R in RP.Results)
            {
                Search.Result Res = new Search.Result(R);

                for (int i = 0; i < Res.R.PlayerCount; i++)
                {
                    string NN = Res.R.Players[i].NickName;
                    bool Repl = PM.IsReplacementPlayer(Res.Hash, i) || PM.Replacements.Contains(NN);

                    if (PM.Banned.Contains(NN)) Res.PlayerMark[i] = true;
                    if (Repl != Replacement) Res.PlayerMark[i] = true;
                }

                Results.Add(Res);
            }
            
            List<Tournier.Result> Temp = new Tournier.Tournier(Results).Analyze();

            // Сортировка по умолчанию
            Temp = Temp.OrderByDescending(o => o.TotalPoints).ToList();
            Temp = Temp.OrderBy(o => o.AveragePlace).ToList();
            if (Replacement) Temp = Temp.OrderByDescending(o => o.Places.Count).ToList();

            if (Replacement)
                TR_Replacement = Temp;
            else
                TR = Temp;
        }

        private ListViewItem CreateLine(Tournier.Result R, bool Replacement)
        {
            var I = new ListViewItem();
            var P = new Plotter(R);

            I.BackColor = (Replacement) ? CReplacement : CActive;
            I.Text = P.GetValue(lResult.Columns[0].Tag.ToString());
            for (int i = 1; i < lResult.Columns.Count; i++)
                I.SubItems.Add(P.GetValue(lResult.Columns[i].Tag.ToString()));

            return I;
        }

        private void BuildResultTable()
        {
            List<ListViewItem> Active = new List<ListViewItem>();
            List<ListViewItem> Replacement = new List<ListViewItem>();

            foreach (var R in TR) Active.Add(CreateLine(R, false));
            foreach (var R in TR_Replacement) Replacement.Add(CreateLine(R, true));

            lResult.BeginUpdate();
            lResult.Items.Clear();
            lResult.Items.AddRange(Active.ToArray());
            lResult.Items.AddRange(Replacement.ToArray());
            lResult.EndUpdate();
            ResizeColumns(lResult, ResultCols);
        }
    }
}
