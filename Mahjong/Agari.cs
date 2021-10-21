using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Mahjong
{
    [Serializable]
    [XmlType("Agari")]
    public class Agari
    {
        // Кто?
        public int Who;

        // С кого?
        public int From;

        // Список яку
        public List<Yaku> Yaku = new List<Yaku>();
        
        // Рука
        public List<int> Tiles = new List<int>();
        public int Machi;

        // Стоимость
        public int Cost;
        public int Fu;

        // Забранные палки
        public List<int> Ba = new List<int>();

        // Выплаты
        public List<int> Pay = new List<int>();

        // Указатели дор и ура-дор
        public List<int> Dora = new List<int>();
        public List<int> UraDora = new List<int>();
    }
}
