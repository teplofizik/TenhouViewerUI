using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Output
{
    class Field
    {
        // Название поля
        public string FieldName = null;

        // Содержание
        public string Value = null;
    }

    class FieldList
    {
        /// <summary>
        /// Список полей для вывода
        /// </summary>
        public List<Field> Fields = new List<Field>();

        /// <summary>
        /// Выводить только один результат из раздачи
        /// </summary>
        public bool ReplayOnce = false;
    }
}
