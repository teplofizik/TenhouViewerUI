using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mahjong.Sets;

namespace Parse
{
    class NakiDecoder
    {
        private List<int> Tiles = new List<int>();
        private int fromWho;
       // private bool Opened = true;

        private Mahjong.Sets.SetType Type = Mahjong.Sets.SetType.NONE;

        public NakiDecoder(int m)
        {
            fromWho = m & 0x0003;

            // 2 bit: Chi!
            if ((m & (1 << 2)) != 0)
            {
                CHI(m);
                return;
            }

            // 3 bit: Pon!
            if ((m & (1 << 3)) != 0)
            {
                PON(m);
                return;
            }

            // 4 bit: Extended kan!
            if ((m & (1 << 4)) != 0)
            {
                CHAKAN(m);
                return;
            }


            // 5 bit: NUKI (nord wind in 3-player game)
            if ((m & (1 << 5)) != 0)
            {
                int tile = (m & 0xFF00) >> 8;

                Tiles.Add(tile);
                Type = SetType.NUKI;
                return;
            }

            // Open or concealed kan
            KAN(m);
        }

        private void CHI(int m)
        {
            int data = (m & 0xFC00) >> 10;

            int type = data % 3;

            data = data / 3;
            data = (data / 7) * 9 + (data % 7);

            int basetile = data * 4;

            int tile0 = basetile + 4 * 0 + ((m & 0x0018) >> 3);
            int tile1 = basetile + 4 * 1 + ((m & 0x0060) >> 5);
            int tile2 = basetile + 4 * 2 + ((m & 0x0180) >> 7);

            // Type 0: [3]45
            // Type 1: [4]35
            // Type 2: [5]34
            switch (type)
            {
                case 0: // [3] 4 5
                    Tiles.Add(tile0);
                    Tiles.Add(tile1);
                    Tiles.Add(tile2);
                    break;
                case 1: // [4] 3 5
                    Tiles.Add(tile1);
                    Tiles.Add(tile0);
                    Tiles.Add(tile2);
                    break;
                case 2: // [5] 3 4
                    Tiles.Add(tile2);
                    Tiles.Add(tile0);
                    Tiles.Add(tile1);
                    break;
            }

            Type = SetType.CHI;
        }

        private void PON(int m)
        {
            int order = (m & 0x0060) >> 5;
            int data = (m & 0xFE12) >> 9;
            int type = data % 3;

            int basetile = data / 3;
            basetile = basetile * 4;

            int tile0 = basetile + 0;
            int tile1 = basetile + 1;
            int tile2 = basetile + 2;
            int tile3 = basetile + 3;

            switch (order)
            {
                case 0: // 1 2 3
                    Tiles.Add(tile1);
                    Tiles.Add(tile2);
                    Tiles.Add(tile3);
                    break;
                case 1: // 0 2 3
                    Tiles.Add(tile0);
                    Tiles.Add(tile2);
                    Tiles.Add(tile3);
                    break;
                case 2: // 0 1 3
                    Tiles.Add(tile0);
                    Tiles.Add(tile1);
                    Tiles.Add(tile3);
                    break;
                case 3: // 0 1 2
                    Tiles.Add(tile0);
                    Tiles.Add(tile1);
                    Tiles.Add(tile2);
                    break;
            }

            switch (type)
            {
                case 0:
                    break;
                case 1:
                    // Middle tile to head
                    {
                        int T = Tiles[1];
                        Tiles.Remove(Tiles[1]);
                        Tiles.Insert(0, T);
                    }
                    break;
                case 2:
                    // Last tile to head
                    {
                        int T = Tiles[2];
                        Tiles.Remove(Tiles[2]);
                        Tiles.Insert(0, T);
                    }
                    break;
            }

            // 99[9] fromWho = 1
            // 9[9]9 fromWho = 2
            // [9]99 fromWho = 3
            if (fromWho < 3)
            {
                // Last tile to head
                int T = Tiles[2];
                Tiles.Remove(Tiles[2]);
                Tiles.Insert(0, T);
            }
            if (fromWho < 2)
            {
                // Last tile to head
                int T = Tiles[2];
                Tiles.Remove(Tiles[2]);
                Tiles.Insert(0, T);
            }

            Type = SetType.PON;
        }

        private void KAN(int m)
        {
            int tile = (m & 0xFF00) >> 8;

            int basetile = (tile / 4) * 4;

            int order = tile % 4;

            int tile0 = basetile + 0;
            int tile1 = basetile + 1;
            int tile2 = basetile + 2;
            int tile3 = basetile + 3;

            switch (order)
            {
                case 0: // 0 1 2 3
                    Tiles.Add(tile0);
                    Tiles.Add(tile1);
                    Tiles.Add(tile2);
                    Tiles.Add(tile3);
                    break;
                case 1: // 1 0 2 3
                    Tiles.Add(tile1);
                    Tiles.Add(tile0);
                    Tiles.Add(tile2);
                    Tiles.Add(tile3);
                    break;
                case 2: // 2 0 1 3
                    Tiles.Add(tile2);
                    Tiles.Add(tile0);
                    Tiles.Add(tile1);
                    Tiles.Add(tile3);
                    break;
                case 3: // 3 0 1 2
                    Tiles.Add(tile3);
                    Tiles.Add(tile0);
                    Tiles.Add(tile1);
                    Tiles.Add(tile2);
                    break;
            }

            if (fromWho != 0)
            {
                // Open kan
                Type = Mahjong.Sets.SetType.MINKAN;
            }
            else
            {
                // Concealed kan
                Type = Mahjong.Sets.SetType.ANKAN;
              //  Opened = false;
            }
        }

        private void CHAKAN(int m)
        {
            var order = (m & 0x0060) >> 5;
            var data = (m & 0xFE00) >> 9;
            var type = data % 3;

            int basetile = data / 3;
            basetile = basetile * 4;

            int tile0 = basetile + 0;
            int tile1 = basetile + 1;
            int tile2 = basetile + 2;
            int tile3 = basetile + 3;

            switch (order)
            {
                case 0: // 1 2 3
                    Tiles.Add(tile1);
                    Tiles.Add(tile2);
                    Tiles.Add(tile3);
                    break;
                case 1: // 0 2 3
                    Tiles.Add(tile0);
                    Tiles.Add(tile2);
                    Tiles.Add(tile3);
                    break;
                case 2: // 0 1 3
                    Tiles.Add(tile0);
                    Tiles.Add(tile1);
                    Tiles.Add(tile3);
                    break;
                case 3: // 0 1 2
                    Tiles.Add(tile0);
                    Tiles.Add(tile1);
                    Tiles.Add(tile2);
                    break;
            }

            switch (type)
            {
                case 0:
                    break;
                case 1:
                    // Middle tile to head
                    {
                        int T = Tiles[1];
                        Tiles.Remove(Tiles[1]);
                        Tiles.Insert(0, T);
                    }
                    break;
                case 2:
                    // Last tile to head
                    {
                        int T = Tiles[2];
                        Tiles.Remove(Tiles[2]);
                        Tiles.Insert(0, T);
                    }
                    break;
            }

            switch (order)
            {
                case 0: Tiles.Insert(0, tile0); break;
                case 1: Tiles.Insert(0, tile1); break;
                case 2: Tiles.Insert(0, tile2); break;
                case 3: Tiles.Insert(0, tile3); break;
            }

            Type = SetType.CHAKAN;
        }

        public Set Build()
        {
            Set S;

            switch (Type)
            {
                case SetType.NUKI: S = new Nuki(Tiles); break;
                case SetType.CHI: S = new Chi(Tiles, fromWho); break;
                case SetType.PON: S = new Pon(Tiles, fromWho); break;
                case SetType.ANKAN: S = new Ankan(Tiles); break;
                case SetType.MINKAN: S = new Minkan(Tiles, fromWho); break;
                case SetType.CHAKAN: S = new Chakan(Tiles); break;
                default:
                    S = new Set(Tiles, fromWho);
                    break;
            }

            return S;
        }
    }
}
