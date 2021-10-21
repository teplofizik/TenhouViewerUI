using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Mahjong.Sets
{
    public enum SetType
    {
        NONE,
        NUKI,
        CHI,
        PON,
        ANKAN,
        MINKAN,
        CHAKAN
    }

    [Serializable]
    [XmlType("Set")]
    public class Set
    {
        // Тайлы в составе сета
        protected List<int> mTiles;

        // От кого?
        public int FromWho;

        public int[] Tiles
        {
            get { return mTiles.ToArray(); }
        }

        public Set(IList<int> Tiles)
        {
            mTiles = new List<int>(Tiles);
            FromWho = -1;
        }

        public Set(IList<int> Tiles, int From)
        {
            mTiles = new List<int>(Tiles);
            FromWho = From;
        }

        // Тип сета
        public virtual SetType Type
        {
            get { return SetType.NONE; }
        }

        public override string ToString()
        {
            string Result = Type.ToString()+ " [";
            foreach (var T in Tiles) Result += new Tile(T).TileName;

            return Result + "]";
        }
    }
}
