using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

using Project.Import;

namespace Project
{
    public class LoaderEventArgs : EventArgs
    {
        /// <summary>
        /// Хеш раздачи
        /// </summary>
        public string Hash;

        /// <summary>
        /// Успешно завершено
        /// </summary>
        public bool Ok;

        /// <summary>
        /// Результат
        /// </summary>
        public string Result;

        /// <summary>
        /// Строка ошибки
        /// </summary>
        public string Error;

        public LoaderEventArgs(string H, bool O, string R, string E)
        {
            Hash = H;
            Ok = O;
            Result = R;
            Error = E;
        }
    }

    public delegate void LoaderEventHandler(object sender, LoaderEventArgs e);

    public class Loader
    {
        /// <summary>
        /// Загрузка завершена
        /// </summary>
        public event LoaderEventHandler onCompleted;

        /// <summary>
        /// Загрузчик
        /// </summary>
        public List<Importer> I = new List<Importer>();

        /// <summary>
        /// Мьютекс
        /// </summary>
        private object Locker = new object();

        /// <summary>
        /// Текущая задача
        /// </summary>
        private int CurrentJob = -1;

        /// <summary>
        /// Запустить первую попавшуюся работу
        /// </summary>
        /// <param name="From"></param>
        private void StartFirstJob(int From)
        {
            for (int i = From; i < I.Count; i++)
            {
                if (!I[i].Completed())
                {
                    Debug.WriteLine("Started job: " + I[i].GetHash());

                    CurrentJob = i;
                    I[i].Start();
                    return;
                }
            }

            // Нема
            CurrentJob = -1;
        }

        public void AddImporter(Importer Imp)
        {
            lock (Locker)
            {
                I.Add(Imp);
            }
        }

        /// <summary>
        /// Счётчик, переключение задач
        /// </summary>
        public void Tick()
        {
            if (CurrentJob < 0)
            {
                // Задач нет
                if (I.Count > 0)
                    StartFirstJob(0);
            }
            else
            {
                Importer J = I[CurrentJob];
                if (J.Completed())
                {
                    if (onCompleted != null)
                    {
                        bool Ok = J.Result() != null;

                        onCompleted(this, new LoaderEventArgs(J.GetHash(), Ok, J.Result(), J.Error()));
                    }
                    StartFirstJob(CurrentJob + 1);
                }
            }
        }

        /// <summary>
        /// Сколько ж скачано (в процентах)?
        /// </summary>
        /// <returns></returns>
        public double GetProgress()
        {
            int Total = GetCount();

            return (Total > 0) ? 100.0 * GetCompletedCount() / Total : 0.0;
        }

        /// <summary>
        /// Количество задач
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            lock (Locker)
            {
                return I.Count;
            }
        }

        /// <summary>
        /// Количество завершённых задач
        /// </summary>
        /// <returns></returns>
        public int GetCompletedCount()
        {
            int Count = 0;
            lock (Locker)
            {
                for (int i = 0; i < I.Count; i++)
                {
                    if (I[i].Completed()) Count++;
                }
            }

            return Count;
        }

        /// <summary>
        /// Текстовое сообщение о текущем статусе
        /// </summary>
        /// <returns></returns>
        public string GetStatus()
        {
            if (CurrentJob >= 0)
                return I[CurrentJob].GetHash();
            else
                return "Stopped";
        }
    }
}
