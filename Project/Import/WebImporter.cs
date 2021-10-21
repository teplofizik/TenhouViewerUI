using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace Project.Import
{
    /// <summary>
    /// Загрузка лога с сервера тенхо
    /// </summary>
    class WebImporter : BasicImporter
    {
        private string[] Servers = {
                                    "http://e.mjv.jp/0/log/?",
                                    "http://ee.mjv.jp/0/log/?",
                                    "http://ff.mjv.jp/0/log/?"
                                   };

        /// <summary>
        /// Загрузчик
        /// </summary>
        private WebClient C;

        /// <summary>
        /// Номер сервера, попытка
        /// </summary>
        private int Server = 0;

        public WebImporter(string Hash) : base (Hash)
        {
            C = new WebClient();
            C.DownloadProgressChanged += new DownloadProgressChangedEventHandler(C_DownloadProgressChanged);
            C.DownloadStringCompleted += new DownloadStringCompletedEventHandler(C_DownloadStringCompleted);
        }

        /// <summary>
        /// Завершена загрузка строки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void C_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                ResultString = e.Result;
                SetCompleted(null);
            }
            else
            {
                Server++;
                if (Server < Servers.Length)
                    TryServer(Servers[Server] + Hash);
                else
                {
                    SetCompleted(e.Error.Message);
                }
            }
        }

        /// <summary>
        /// Изменился прогресс загрузки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void C_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Percent = e.ProgressPercentage;
        }

        /// <summary>
        /// Попробовать скачать файл
        /// </summary>
        /// <param name="Url"></param>
        private void TryServer(string Url)
        {
            Percent = 0;
            C.DownloadStringAsync(new Uri(Url));
        }

        #region Члены Importer

        /// <summary>
        /// Начать загрузку
        /// </summary>
        public override void Start()
        {
            if (!IsCompleted && !Busy)
            {
                base.Start();

                Server = 0;
                TryServer(Servers[0] + Hash);
            }
        }

        #endregion
    }
}
