using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mahjong
{
    class Tile
    {
        public string TileName;
        public int Value;
        public int Index;
        public int TileId;
        public bool Red;
        public string TileType;

        // Is Akadora disabled?
        public bool AkaNashi = false;

        public int DoraType;

        //1  - 1 ман
        //↓
        //9  - 9 ман
        //10 - не определено
        //11 - 1 пин
        //↓
        //19 - 9 пин
        //20 - не определено
        //21 - 1 соу
        //↓
        //29 - 9 соу
        //30 - не определено
        //31 - 東 (восток)
        //32 - 南 (юг)
        //33 - 西 (запад)
        //34 - 北 (север)
        //35 - 白 (белый дракон)
        //36 - 発 (зелёный дракон)
        //37 - 中 (красный дракон)

        public Tile(int Index)
        {
            this.Index = Index;

            TileId = CalcTileId(Index);
            DoraType = CalcDoraType(TileId);

            Value = (TileId % 10); // Позиция в масти
            Red = ((Value == 5) && ((Index & 0x03) == 0) && (Index < 36 * 3));

            if (Index < 36 * 1) // Ман
            {
                TileType = "m";
            }
            else if (Index < 36 * 2) // Пин
            {
                TileType = "p";
            }
            else if (Index < 36 * 3) // Соу
            {
                TileType = "s";
            }
            else // Благородные
            {
                TileType = "z";
            }

            if (Red && !AkaNashi)
            {
                TileName = "0" + TileType;
            }
            else
            {
                TileName = Convert.ToString(Value) + TileType;
            }
        }

        private int CalcTileId(int Index)
        {
            int Id = (Index / 4) + 1; // Tile index

            // compensate spaces
            if (Id > 9) Id++;
            if (Id > 19) Id++;
            if (Id > 29) Id++;

            return Id;
        }

        // If it is a dora pointer...
        private int CalcDoraType(int Id)
        {
            int Dora = Id + 1;

            // pin, sou, man
            if (Dora % 10 == 0) Dora = Dora - 9;
            // winds
            if (Dora == 35) Dora = 31;
            // dracones
            if (Dora == 38) Dora = 35;

            return Dora;
        }
    }
}
