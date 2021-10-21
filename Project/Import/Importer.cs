using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Import
{
    public interface Importer
    {
        /// <summary>
        /// Начать загрузку
        /// </summary>
        void Start();

        /// <summary>
        /// Прогресс выполнения текущей задачи
        /// </summary>
        /// <returns></returns>
        int Progress();

        /// <summary>
        /// Звершено?
        /// </summary>
        /// <returns></returns>
        bool Completed();

        /// <summary>
        /// Текстовый результат загрузки (если имеется)
        /// </summary>
        /// <returns></returns>
        string Result();

        /// <summary>
        /// Текстовый результат ошибки, если завершено, а результат ноль
        /// </summary>
        /// <returns></returns>
        string Error();

        /// <summary>
        /// Хеш игры, которую загружаем
        /// </summary>
        /// <returns></returns>
        string GetHash();
    }
}
