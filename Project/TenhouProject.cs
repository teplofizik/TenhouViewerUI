using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Timers;
using System.Xml;
using System.Xml.Serialization;
using System.Diagnostics;

using XMLFile;
using Project.Import;
using Mahjong;
using Parse;

namespace Project
{
    [Serializable]
    public class TenhouProject
    {
        /// <summary>
        /// Имя файла проекта
        /// </summary>
        [XmlIgnore]
        private const string ProjectFileName = "project.tv";

        /// <summary>
        /// Текущая папка проекта
        /// </summary>
        [XmlAttribute]
        public string Dir = null;

        /// <summary>
        /// Ник игрока
        /// </summary>
        [XmlAttribute]
        public string Owner = null;

        /// <summary>
        /// Список хэшей игр
        /// </summary>
        [XmlArray("hash")]
        public List<string> Hashes = new List<string>();

        /// <summary>
        /// Список хэшей игр
        /// </summary>
        [XmlIgnore]
        public List<string> Loaded = new List<string>();

        /// <summary>
        /// Список загруженных игр
        /// </summary>
        [XmlIgnore]
        public List<Replay> Replays = new List<Replay>();

        /// <summary>
        /// Загрузчик игр
        /// </summary>
        [XmlIgnore]
        public Loader L = new Loader();

        /// <summary>
        /// Индекс загрузки игр
        /// </summary>
        [XmlIgnore]
        private int LoadIndex = 0;

        /// <summary>
        /// Таймер для отправки данных
        /// </summary>
        [XmlIgnore]
        private System.Timers.Timer tickTimer;
        [XmlIgnore]
        private ElapsedEventHandler tickTimerHandler;

        public TenhouProject()
        {
            L.onCompleted += new LoaderEventHandler(L_onCompleted);

            tickTimerHandler = new ElapsedEventHandler(tickTimer_Elapsed);

            tickTimer = new System.Timers.Timer(1000 / 50); // 50 Гц
            tickTimer.Elapsed += tickTimerHandler;
            tickTimer.Start();
        }

        /// <summary>
        /// Создать проект в заданной папке
        /// </summary>
        /// <param name="Dir"></param>
        public static TenhouProject Create(string Dir)
        {
            if (!Directory.Exists(Dir)) Directory.CreateDirectory(Dir);
            if (!Directory.Exists(Dir + "logs")) Directory.CreateDirectory(Dir + "logs");

            TenhouProject TP = new TenhouProject();
            TP.Dir = Dir;
            TP.SaveXML(Dir + ProjectFileName);

            return TP;
        }

        /// <summary>
        /// Загружен
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void L_onCompleted(object sender, LoaderEventArgs e)
        {
            Debug.WriteLine(e.Hash + (e.Ok ? ": ok" : ": fail"));

            if (e.Ok && !IsGameExists(e.Hash))
            {
                if (!Hashes.Contains(e.Hash)) Hashes.Add(e.Hash);
                if (!Loaded.Contains(e.Hash)) Loaded.Add(e.Hash);
                SaveGame(e.Hash, e.Result);
            }
        }

        /// <summary>
        /// Файл, связанный с хешем
        /// </summary>
        /// <param name="Hash"></param>
        /// <returns></returns>
        private string GetGameFileName(string Hash)
        {
            if (!Directory.Exists(Dir)) Directory.CreateDirectory(Dir);
            if (!Directory.Exists(Dir + "logs")) Directory.CreateDirectory(Dir + "logs");

            return String.Format("{0:s}logs\\{1:s}.xml", Dir, Hash);
        }

        /// <summary>
        /// Сохранить игру локально
        /// </summary>
        /// <param name="Hash"></param>
        private void SaveGame(string Hash, string Result)
        {
            File.WriteAllText(GetGameFileName(Hash), Result);
        }

        /// <summary>
        /// Проверить, есть ли загруженная игра в локальном каталоге
        /// </summary>
        /// <param name="Hash"></param>
        /// <returns></returns>
        private bool CheckLoadedGame(string Hash)
        {
            return File.Exists(GetGameFileName(Hash));
        }

        /// <summary>
        /// Проверим хэши
        /// </summary>
        private void CheckHashes()
        {
            for (int i = Hashes.Count - 1; i >= 0; i--)
            {
                string H = Hashes[i];
                if (CheckLoadedGame(H)) Loaded.Add(H);
            }
        }

        /// <summary>
        /// Создать проект в заданной папке
        /// </summary>
        /// <param name="Dir"></param>
        public void Save()
        {
            this.SaveXML(Dir + ProjectFileName);
        }

        /// <summary>
        /// Загрузить проект из папки
        /// </summary>
        /// <param name="Dir"></param>
        public static TenhouProject Load(string Dir)
        {
            if (!Directory.Exists(Dir)) return null;
            if (!File.Exists(Dir + ProjectFileName)) return null;

            try
            {
                TenhouProject TP = XMLFile.XMLFile.LoadXML<TenhouProject>(Dir + ProjectFileName);
                TP.Dir = Dir;
                TP.CheckHashes();

                return TP;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Существует ли игра среди загруженных?
        /// </summary>
        /// <param name="Hash"></param>
        /// <returns></returns>
        public bool IsGameExists(string Hash)
        {
            return Loaded.Contains(Hash) && CheckLoadedGame(Hash);
        }

        /// <summary>
        /// Посмотреть реплей в тенхоклиенте
        /// </summary>
        /// <param name="Hash"></param>
        /// <param name="Player"></param>
        public void ViewReplayExternal(string Hash, int Player)
        {
            Binder.RunReplayViewer(GetGameFileName(Hash), Hash, Player);
        }

        /// <summary>
        /// Получить реплей игры
        /// </summary>
        /// <param name="Hash"></param>
        /// <returns></returns>
        public Replay GetGameReplay(string Hash)
        {
            if (!Loaded.Contains(Hash)) return null;

            for (int i = 0; i < Replays.Count; i++)
            {
                if (Replays[i].Hash.CompareTo(Hash) == 0) return Replays[i];
            }

            return LoadReplay(Hash);
        }

        /// <summary>
        /// Прогресс загрузки реплеев в память
        /// </summary>
        /// <returns></returns>
        public double GetLoadingProgress()
        {
            return (Hashes.Count > 0) ? Math.Min(100.0, 100.0 * LoadIndex / Hashes.Count) : 0.0 ;
        }

        /// <summary>
        /// Реплеи загружены?
        /// </summary>
        /// <returns></returns>
        public bool ReplaysLoaded()
        {
            return (LoadIndex >= Loaded.Count) && (Hashes.Count > 0);
        }

        /// <summary>
        /// Загрузить реплей
        /// </summary>
        /// <param name="Hash"></param>
        /// <returns></returns>
        private Replay LoadReplay(string Hash)
        {
            Tenhou T = new Tenhou();
            T.OpenPlainText(GetGameFileName(Hash), Hash);

            Replays.Add(T.Rp);
            return T.Rp;
        }

        /// <summary>
        /// Добавить хэши из списка
        /// </summary>
        /// <param name="Hashes"></param>
        public void ImportHashes(List<string> Hashes)
        {
            if (Hashes == null) return;

            foreach(var H in Hashes)
            {
                if (IsGameExists(H)) continue;
                this.Hashes.Add(H);
            }
        }

        /// <summary>
        /// Добавить загрузчик с сети
        /// </summary>
        /// <param name="Hash"></param>
        public void AddWeb(string Hash)
        {
            if (Loaded.Contains(Hash)) return;
            if (!Hashes.Contains(Hash)) Hashes.Add(Hash);

            if(!CheckLoadedGame(Hash))
                L.AddImporter(new WebImporter(Hash));
            else
                Loaded.Add(Hash);
        }

        /// <summary>
        /// Добавить загрузчик из mjlog
        /// </summary>
        /// <param name="Hash"></param>
        /// <param name="FileName"></param>
        public void AddMjLog(string FileName)
        {
            string Hash = Binder.GetHashFromFileName(FileName);
            if (Loaded.Contains(Hash)) return;
            if (!Hashes.Contains(Hash)) Hashes.Add(Hash);
            if (!CheckLoadedGame(Hash))
                L.AddImporter(new MjLogImporter(Hash, FileName));
            else
                Loaded.Add(Hash);
        }

        /// <summary>
        /// Таймер для фоновых процессов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tickTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            L.Tick();

            if (LoadIndex < Loaded.Count)
            {
                GetGameReplay(Hashes[LoadIndex]);
                LoadIndex++;
            }
        }
    }
}
