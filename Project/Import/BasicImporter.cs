using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Import
{
    class BasicImporter : Importer
    {
        /// <summary>
        /// Хеш игры
        /// </summary>
        protected string Hash = null;

        /// <summary>
        /// Занято?
        /// </summary>
        protected bool Busy = false;

        /// <summary>
        /// Текущий процент выполнения
        /// </summary>
        protected int Percent = 0;

        /// <summary>
        /// Закончена загрузка?
        /// </summary>
        protected bool IsCompleted = false;

        /// <summary>
        /// Результат загрузки.
        /// </summary>
        protected string ResultString = null;

        /// <summary>
        /// Описание ошибки.
        /// </summary>
        private string ErrorString = null;

        public BasicImporter(string Hash)
        {
            this.Hash = Hash;
        }

        /// <summary>
        /// Установить текст ошибки
        /// </summary>
        /// <param name="Text"></param>
        private void SetError(string Text)
        {
            ErrorString = Text;
        }

        /// <summary>
        /// Закончили
        /// </summary>
        protected void SetCompleted(string Text)
        {
            Percent = 100;
            Busy = false;
            IsCompleted = true;

            SetError(Text);
        }

        #region Члены Importer

        public virtual void Start()
        {
            Busy = true;
            Percent = 0;
        }

        public int Progress()
        {
            return Percent;
        }

        public bool Completed()
        {
            return IsCompleted;
        }

        public string Result()
        {
            return ResultString;
        }

        public string Error()
        {
            return ErrorString;
        }

        public string GetHash()
        {
            return Hash;
        }

        #endregion
    }
}
