using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parse
{
    static class DecompositeExtension
    {
        public static int[] DecompositeInt(this string S)
        {
            string[] delimiter = new string[] { "," };
            string[] Temp;
            int[] Result = null;

            if (S == null) return null;

            Temp = S.Split(delimiter, StringSplitOptions.None);
            Result = new int[Temp.Length];

            for (int i = 0; i < Temp.Length; i++)
            {
                // Отрежем текст до точки
                int Index = Temp[i].IndexOf('.');
                if (Index >= 0) Temp[i] = Temp[i].Substring(0, Index);

                Result[i] = Convert.ToInt32(Temp[i]);
            }

            return Result;
        }

        public static string[] DecompositeString(this string S)
        {
            string[] delimiter = new string[] { "," };

            if (S == null) return null;

            return S.Split(delimiter, StringSplitOptions.None);
        }

        public static uint[] DecompositeHexList(this string Text)
        {
            string[] delimiter = new string[] { "," };
            string[] Temp;
            uint[] Result = null;

            if (Text == null) return null;

            Temp = Text.Split(delimiter, StringSplitOptions.None);
            Result = new uint[Temp.Length];

            for (int i = 0; i < Temp.Length; i++)
            {
                // Отрежем текст до точки
                int Index = Temp[i].IndexOf('.');
                if (Index >= 0) Temp[i] = Temp[i].Substring(0, Index);

                Result[i] = Convert.ToUInt32(Temp[i], 16);
            }

            return Result;
        }

        public static void Fill<T>(this List<T> Data, int Size, T Value)
        {
            Data.Clear();

            for (int i = 0; i < Size; i++) Data.Add(Value);
        }

        public static int DecodeSex(this string Sex)
        {
            switch (Sex)
            {
                case "M": return 1;
                case "F": return 2;
                default: return 0;
            }
        }

        public static int DecodeDraw(this string Reason)
        {
            switch (Reason)
            {
                // 0: 9 terminals/honors
                case "yao9": return 0;
                // 1: 4 consecutive riichi calls
                case "reach4": return 1;
                // 2: three simultaneous ron calls (triple ron)
                case "ron3": return 2;
                // 3: four declared kans
                case "kan4": return 3;
                // 4: same wind discard on first round
                case "kaze4": return 4;
                // 5: nagashi mangan (all terminal/honor discards)
                case "nm": return 5;
                default: return -1;
            }
        }

        public static int DecodeTile(this string Tag)
        {
            try
            {
                return Convert.ToInt16(Tag.Substring(1));
            }
            catch
            {
                return -1;
            }
        }

        public static string SubstringAfter(this string Base, string Substring)
        {
            int Delimiter = Base.IndexOf(Substring);
            if (Delimiter < 0) return "";

            return Base.Substring(Delimiter + 1);
        }

        public static uint[] ToUIntArray(this byte[] Data)
        {
            if (Data == null) return null;

            uint[] Result = new uint[Data.Length / 4];

            for (int i = 0; i < Result.Length; i++) Result[i] = (uint)(Data[i * 4 + 0] << 0) |
                                                                (uint)(Data[i * 4 + 1] << 8) |
                                                                (uint)(Data[i * 4 + 2] << 16) |
                                                                (uint)(Data[i * 4 + 3] << 24);

            return Result;
        }
    }
}
