using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extension.Math
{
    static class Comparsion
    {

        public static bool IsBetween(this double Value, double A, double B) 
        {
            if (A > B)
                return ((Value <= A) && (Value >= B));
            else
                return ((Value <= B) && (Value >= A));

        }
    }
}
