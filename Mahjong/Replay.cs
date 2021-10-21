using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Mahjong
{
    [Serializable]
    [XmlType("Replay")]
    public class Replay
    {
        /// <summary>
        /// Хэш игры
        /// </summary>
        public string Hash;

        /// <summary>
        /// Номер лобби
        /// </summary>
        public int Lobby;

        /// <summary>
        /// Тип лобби
        /// </summary>
        public LobbyType LobbyType;

        /// <summary>
        /// Количество игроков
        /// </summary>
        public int PlayerCount;

        /// <summary>
        /// Время игры
        /// </summary>
        public DateTime Date;

        /// <summary>
        /// Список игроков
        /// </summary>
        public List<Player> Players = new List<Player>();

        /// <summary>
        /// Список раздач
        /// </summary>
        public List<Round> Rounds = new List<Round>();

        /// <summary>
        /// Баланс на конец игры
        /// </summary>
        public List<int> Balance = new List<int>();

        /// <summary>
        /// Результат раздачи
        /// </summary>
        public List<int> Result = new List<int>();

        /// <summary>
        /// Занятое место
        /// </summary>
        public List<int> Place = new List<int>();
    }
}
