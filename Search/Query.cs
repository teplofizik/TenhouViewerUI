using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Search
{
    class Query
    {
        // Функция-проверятор
        public Action<Query, Result> Function;

        // Значение функции для проверки
        public int IntValue;
        public string Value;

        public bool Less = false;
        public bool Over = false;

        public Query(Action<Query, Result> F, string V)
        {
            Value = V;
            Function = F;

            try
            {
                IntValue = Convert.ToInt32(Value);
            }
            catch { }
        }

        public bool BoolValue
        {
            get { return (IntValue == 1); }
        }
    }
}
