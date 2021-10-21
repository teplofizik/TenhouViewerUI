using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using Project;

namespace TenhouViewerUI
{
    public class NestedForm : Form
    {
        protected TenhouProject TP;

        /// <summary>
        /// Отмасштабировать столбцы
        /// </summary>
        /// <param name="LV"></param>
        /// <param name="Percents"></param>
        protected void ResizeColumns(ListView LV, double[] Percents)
        {
            for (int i = 0; (i < LV.Columns.Count) && (i < Percents.Length); i++)
            {
                LV.Columns[i].Width = Convert.ToInt32(LV.ClientSize.Width * Percents[i] / 100.0);
            }
        }

        /// <summary>
        /// При загрузке проекта
        /// </summary>
        protected virtual void OnProjectLoad()
        {

        }

        /// <summary>
        /// При загрузке проекта
        /// </summary>
        protected virtual void OnProjectUnload()
        {

        }

        /// <summary>
        /// Проверить актуальность загруженного проекта
        /// </summary>
        protected void CheckProject()
        {
            var lTP = MdiParent.Tag as TenhouProject;

            if (lTP != TP)
            {
                if (TP != null)
                    OnProjectUnload();

                TP = lTP;
                OnProjectLoad();
            }
        }

        #region Масштабирование

        #endregion
    }
}
