using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TenhouViewerUI
{
    public partial class fSearch : NestedForm
    {
        // Сверху выбор условий поиска
        // Таблица результатов
        // user=Nyaha

        public fSearch()
        {
            InitializeComponent();
        }

        private void fSearch_Load(object sender, EventArgs e)
        {
            CheckProject();
        }

        /// <summary>
        /// При загрузке проекта
        /// </summary>
        protected override void OnProjectLoad()
        {
            
        }

        /// <summary>
        /// При выгрузе проекта
        /// </summary>
        protected override void OnProjectUnload()
        {

        }
    }
}
