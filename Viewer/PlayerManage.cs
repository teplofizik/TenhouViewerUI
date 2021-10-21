using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Viewer
{
    [Serializable]
    public class PlayerManage
    {
        /// <summary>
        /// Список исключённых игроков
        /// </summary>
        public List<string> Banned = new List<string>();

        /// <summary>
        /// Список игроков замены
        /// </summary>
        public List<string> Replacements = new List<string>();

        /// <summary>
        /// Список основных игроков играющих как игроки замены (с указанием игр)
        /// </summary>
        public List<TempReplacement> TempReplacements = new List<TempReplacement>();

        /// <summary>
        /// Является ли игрок в игре заменяющим (на нерегулярной основе)
        /// </summary>
        /// <param name="Hash"></param>
        /// <param name="Player"></param>
        /// <returns></returns>
        public bool IsReplacementPlayer(string Hash, int Player)
        {
            foreach (var TR in TempReplacements)
            {
                if ((TR.Hash.CompareTo(Hash) == 0) && (TR.Player == Player)) return true;
            }

            return false;
        }
    }

    public class TempReplacement
    {
        /// <summary>
        /// Ник игрока
        /// </summary>
        public int Player;

        /// <summary>
        /// В какой игре
        /// </summary>
        public string Hash;


        public TempReplacement()
        {

        }

        public TempReplacement(string H, int P)
        {
            Hash = H;
            Player = P;
        }
    }
}
