using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Search;
using Tournier;
using Output;
using System.Windows.Forms;
using Viewer;

namespace TenhouViewerUI
{
    public partial class fTournier : NestedForm
    {
        private string YakuLang = "en";

        private void BuildYakuTable()
        {
            PlayerManage PM = RP.UserField as PlayerManage;
            List<Search.Result> Results = new List<Search.Result>();
            foreach (var R in RP.Results)
            {
                Search.Result Res = new Search.Result(R);

                for (int i = 0; i < Res.R.PlayerCount; i++)
                {
                    if (PM.Banned.Contains(Res.R.Players[i].NickName)) Res.PlayerMark[i] = true;
                }

                Results.Add(Res);
            }
            
            List<Tournier.Result> Temp = new Tournier.Tournier(Results).Analyze();
            int[] Yaku = new int[55];
            for (int j = 0; j < 55; j++) Yaku[j] = 0;

            // Calc yaku
            for (int i = 0; i < Temp.Count; i++)
            {
                Tournier.Result R = Temp[i];
                for (int j = 0; j < 55; j++) Yaku[j] += R.Yaku[j];
            }

            List<ListViewItem> LV = new List<ListViewItem>();
            for (int i = 0; i < Yaku.Length; i++)
            {
                if (Yaku[i] == 0) continue;

                ListViewItem I = new ListViewItem();
                I.Tag = i;
                I.Text = YakuName.GetYakuName(YakuLang, i);
                I.SubItems.Add(Convert.ToString(Yaku[i]));

                LV.Add(I);
            }

            lYaku.BeginUpdate();
            lYaku.Items.Clear();
            lYaku.Items.AddRange(LV.ToArray());
            lYaku.EndUpdate();
            ResizeColumns(lYaku, YakuCols);
        }

        private void mYakuLang_DropDownOpening(object sender, EventArgs e)
        {
            mYakuLangEN.Checked = (YakuLang.CompareTo("en") == 0);
            mYakuLangJP.Checked = (YakuLang.CompareTo("jp") == 0);
            mYakuLangRU.Checked = (YakuLang.CompareTo("ru") == 0);
        }

        private void mYakuLanguage_Click(object sender, EventArgs e)
        {
            YakuLang = (sender as ToolStripDropDownItem).Tag.ToString();

            Settings.Default.YakuLang = YakuLang;
            // Обновим названия
            lYaku.BeginUpdate();
            foreach (ListViewItem I in lYaku.Items)
            {
                int Y = (int)I.Tag;

                I.Text = YakuName.GetYakuName(YakuLang, Y);
            }
            lYaku.EndUpdate();
        }
    }
}
