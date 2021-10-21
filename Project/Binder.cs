using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;

namespace Project
{
    static class Binder
    {
        /// <summary>
        /// Путь до папки с реплеями
        /// </summary>
        /// <returns></returns>
        static public string GetMyTenhouPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\My Tenhou";
        }

        /// <summary>
        /// Путь до программы Tenhou
        /// </summary>
        /// <returns></returns>
        static public string GetTenhouPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\C-EGG\\tenhou\\130\\";
        }

        /// <summary>
        /// Путь до конфигурационного файла программы Tenhou
        /// </summary>
        /// <returns></returns>
        static public string GetTenhouConfigFileName()
        {
            return GetTenhouPath() + "config.ini";
        }

        /// <summary>
        /// Получить хеш из имени файла
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        static public string GetHashFromFileName(string FileName)
        {
            string Hash = Path.GetFileNameWithoutExtension(FileName);

            int Sep = Hash.IndexOf('&');
            if (Sep >= 0) Hash = Hash.Substring(0, Sep);

            return Hash;
        }

        /// <summary>
        /// Получить имя файла локальной копии реплея
        /// </summary>
        /// <param name="Hash"></param>
        /// <returns></returns>
        static public string GetLocalReplayFileName(string Hash)
        {
            string[] Files = Directory.GetFiles(Binder.GetMyTenhouPath(), Hash + "*.mjlog", SearchOption.AllDirectories);
            
            return (Files.Length > 0) ? Files[0] : null;
        }

        /// <summary>
        /// Открыть реплей в просмотрщие реплеев
        /// </summary>
        /// <param name="Hash"></param>
        /// <param name="Player"></param>
        static public void RunReplayViewer(string LocalFileName, string Hash, int Player)
        {
            // Загрузим реплей
            if (!File.Exists(LocalFileName)) return;

            // Сохраним во временный файл .mjlog
            string TempDir = String.Format("{0:s}tenhouviewer\\", Path.GetTempPath());
            string TempFN = String.Format("{0:s}{1:s}&tw={2:d}.mjlog", TempDir, Hash, Player);
            if (!Directory.Exists(TempDir)) Directory.CreateDirectory(TempDir);

            using (FileStream inFile = new FileStream(LocalFileName, FileMode.Open))
            {
                using (FileStream outFile = File.Create(TempFN))
                {
                    using (GZipStream Compress = new GZipStream(outFile, CompressionMode.Compress))
                    {
                        byte[] buffer = new byte[4096];
                        int numRead;

                        while ((numRead = inFile.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            Compress.Write(buffer, 0, numRead);
                        }
                    }
                }
            }

            // Выполним.
            Process proc = new Process();
            proc.StartInfo.FileName = TempFN;
            proc.StartInfo.UseShellExecute = true;
            proc.Start();    
        }
    }
}
