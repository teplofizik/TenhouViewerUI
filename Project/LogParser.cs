using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace Project
{
    static class LogParser
    {
        /// <summary>
        /// Получить список хешей из файла
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static List<string> FromFile(string FileName)
        {
            string[] Lines = File.ReadAllLines(FileName);
            return FromString(Lines);
        }

        /// <summary>
        /// Получить список хешей из строки
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static List<string> FromString(string[] Lines)
        {
            List<string> Result = new List<string>();

            for (int i = 0; i < Lines.Length; i++)
            {
                TenhouHash H = ExtractHash(Lines[i]);

                if ((H != null) && !Result.Contains(H.DecodedHash))
                    Result.Add(H.DecodedHash);
            }

            return Result;
        }

        /// <summary>
        /// Извлечь хеш из ссылки
        /// </summary>
        /// <param name="Line"></param>
        /// <returns></returns>
        private static TenhouHash ExtractHash(string Line)
        {
            const string url = "http://tenhou.net/0/?log=";

            int Pos = Line.IndexOf(url);
            int PosEndTW = Line.IndexOf("&tw=");
            int PosEndTS = Line.IndexOf("&ts=");
            int PosEnd = PosEndTW;

            if ((PosEndTW != -1) && (PosEndTS != -1)) PosEnd = (PosEndTW > PosEndTS) ? PosEndTS : PosEndTW;
            if (PosEnd == -1) PosEnd = Line.IndexOf("\">");

            if (Pos >= 0)
            {
                Pos += url.Length;
                string Hash = Line.Substring(Pos, PosEnd - Pos);

                return new TenhouHash(Hash);
            }

            return null;
        }
    }
}
