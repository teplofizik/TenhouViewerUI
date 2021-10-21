using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace Project.Import
{
    class MjLogImporter : BasicImporter
    {
        /// <summary>
        /// Имя файла для загрузки
        /// </summary>
        private string FN = String.Empty;

        public MjLogImporter(string Hash, string FileName) : base(Hash)
        {
            FN = FileName;
        }

        #region Члены Importer

        public override void Start()
        {
            base.Start();

            if (!File.Exists(FN))
            {
                SetCompleted("File not found");
                return;
            }

            try
            {
                FileStream F = new FileStream(FN, FileMode.Open, FileAccess.Read);
                GZipStream Stream = new GZipStream(F, CompressionMode.Decompress);

                using (StreamReader Reader = new StreamReader(Stream, Encoding.UTF8))
                {
                    ResultString = Reader.ReadToEnd();
                    SetCompleted(null);
                }
            }
            catch (Exception E)
            {
                SetCompleted(E.Message);
            }
        }

        #endregion
    }
}
