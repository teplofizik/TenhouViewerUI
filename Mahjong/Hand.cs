using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Mahjong
{
    [Serializable]
    [XmlType("Hand")]
    public class Hand
    {
        // Тайлы
        public List<int> Tiles;

        // Сеты
        public List<Sets.Set> Naki = new List<Mahjong.Sets.Set>();

        // Число шантен
        // public int Shanten;

        public Hand()
        {

        }

        public Hand(Hand H)
        {
            Tiles = new List<int>(H.Tiles);
            Tiles.Sort();
            Naki = new List<Mahjong.Sets.Set>(H.Naki);
        }

        public Hand(int[] T)
        {
            Tiles = new List<int>(T);
            Tiles.Sort();
            Naki = new List<Mahjong.Sets.Set>();
        }

        public void makeNaki(Sets.Set S)
        {
            // Удалим все тайлы из руки, которые вошли в сет
            int[] ToRem = S.Tiles;
            for (int i = 0; i < ToRem.Length; i++)
                Tiles.Remove(ToRem[i]);

            if(S.GetType() == typeof(Mahjong.Sets.Chakan))
            {
                foreach(var N in Naki)
                {
                    if(N.Tiles.Intersect(S.Tiles).Count() > 0)
                    {
                        S.FromWho = N.FromWho;
                        Naki.Remove(N);
                        Naki.Add(S);
                        break;
                    }
                }
            }
            else
                Naki.Add(S);
        }
    }
}
