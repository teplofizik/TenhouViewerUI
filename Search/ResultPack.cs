using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

using XMLFile;
using Project;
using Viewer;

namespace Search
{
    /// <summary>
    /// Для хранения результатов
    /// </summary>
    [Serializable]
    [XmlInclude(typeof(PlayerManage))]
    public class ResultPack
    {
        /// <summary>
        /// Список результатов
        /// </summary>
        public List<LightResult> Results = new List<LightResult>();

        /// <summary>
        /// Для хранения всякого специфического
        /// </summary>
        public object UserField;

        public ResultPack()
        {

        }

        /// <summary>
        /// Сохранить как xml
        /// </summary>
        /// <param name="ProjectDir"></param>
        /// <param name="FileName"></param>
        public void Save(string ProjectDir, string FileName)
        {
            this.SaveXML(ProjectDir + FileName);
        }
        
        /// <summary>
        /// Загрузить проект из папки
        /// </summary>
        /// <param name="Dir"></param>
        public static ResultPack Load(string ProjectDir, string FileName, TenhouProject TP)
        {
            try
            {
                ResultPack RP = XMLFile.XMLFile.LoadXML<ResultPack>(ProjectDir + FileName);

                // Load
                foreach (LightResult R in RP.Results)
                    R.R = TP.GetGameReplay(R.Hash);

                return RP;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Есть ли такая игра в списке?
        /// </summary>
        /// <param name="Hash"></param>
        /// <returns></returns>
        public bool IsExists(string Hash)
        {
            foreach (LightResult R in Results)
            {
                if (R.Hash.CompareTo(Hash) == 0) return true;
            }

            return false;
        }
    }
}
