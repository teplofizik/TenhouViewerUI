using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mahjong;

namespace Output
{
    static class FormatterExtension
    {
        public static Formatter GetFormatter(this Formatter[] Formats, Field F)
        {
            foreach (Formatter Fmt in Formats)
            {
                if (Fmt.Name.CompareTo(F.FieldName) == 0) return Fmt;
            }
            return null;
        }
    }

    class Formatter
    {
        /// <summary>
        /// Название поля
        /// </summary>
        public string Name;

        /// <summary>
        /// Описание поля
        /// </summary>
        public string Description;

        /// <summary>
        /// Функция форматирования
        /// </summary>
        public Func<Replay, Round, int, string> Format;

        public Formatter(string N, string D, Func<Replay, Round, int, string> F)
        {
            Name = N;
            Description = D;
            Format = F;
        }
    }
}
