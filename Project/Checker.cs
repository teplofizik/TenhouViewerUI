using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Project
{
    static class Checker
    {
        /// <summary>
        /// Имеется ли на компьютере папка "My Tenhou"?
        /// </summary>
        /// <returns></returns>
        public static bool HasMyTenhouDir()
        {
            return Directory.Exists(Binder.GetMyTenhouPath());
        }

        /// <summary>
        /// Имеется ли на компьютере конфигурационный файл клиента Tenhou?
        /// </summary>
        /// <returns></returns>
        public static bool HasTenhouConfig()
        {
            return File.Exists(Binder.GetTenhouConfigFileName());
        }
    }
}
