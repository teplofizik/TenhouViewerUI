using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Parse
{
    static class ReaderExtension
    {
        public static int GetInt32(this XmlReader R, string Name)
        {
            return Convert.ToInt32(R.GetAttribute(Name));
        }

        public static int[] GetIntArray(this XmlReader R, string Name)
        {
            return R.GetAttribute(Name).DecompositeInt();
        }

        public static string GetString(this XmlReader R, string Name)
        {
            return R.GetAttribute(Name);
        }

        public static string[] GetStringArray(this XmlReader R, string Name)
        {
            return R.GetAttribute(Name).DecompositeString();
        }
    }
}
