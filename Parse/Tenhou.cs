using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.IO.Compression;

using Mahjong;

namespace Parse
{
    class Tenhou
    {
        public Replay Rp = new Replay();
        private Round Rnd = new Round();

        private WallGenerator Generator;

        // .xml
        public void OpenPlainText(string Filename, string Hash)
        {
            Rp.Hash = Hash;
            Rp.Date = GetDateTimeFromHash(Hash);

            XmlReader R = XmlReader.Create(Filename);
            ParseBlock(R);
        }

        // .mjlog
        public void OpenGZ(string Filename, string Hash)
        {
            Rp.Hash = Hash;
            Rp.Date = GetDateTimeFromHash(Hash);

            FileStream File = new FileStream(Filename, FileMode.Open, FileAccess.Read);
            GZipStream Stream = new GZipStream(File, CompressionMode.Decompress);
            XmlReader R = XmlReader.Create(Stream);

            ParseBlock(R);
        }

        private void ParseBlock(XmlReader R)
        {
            R.MoveToContent();
            while (R.Read())
            {
                if (R.NodeType == XmlNodeType.Element)
                {
                    switch (R.Name)
                    {
                        // Начало раздачи, номер и тип лобби
                        case "GO": GO(R); break;
                        // Информация об игроке, переподключение игрока
                        case "UN": UN(R); break;
                        // Дисконнект игрока
                        case "BYE": BYE(R); break;
                        // Seed для генерации стен
                        case "SHUFFLE": SHUFFLE(R); break;
                        // Начальные руки
                        case "INIT": INIT(R); break;
                        // Дилер
                        case "TAIKYOKU": TAIKYOKU(R); break;
                        // ничья
                        case "RYUUKYOKU": RYUUKYOKU(R); break;
                        // Объявление
                        case "N": N(R);  break;
                        // Новый индиктор доры
                        case "DORA": DORA(R); break;
                        // Рон или цумо
                        case "AGARI": AGARI(R); break;
                        // Объявление риичи (состоит из двух шагов)
                        case "REACH": REACH(R); break;
                        // Иное действие
                        default: ACTION(R); break;
                    }
                }
            }
        }

        // Тип и номер лобби
        private void GO(XmlReader R)
        {
            Rp.Lobby = R.GetInt32("lobby");
            Rp.LobbyType = (LobbyType)R.GetInt32("type");

            Rp.PlayerCount = (Rp.LobbyType.HasFlag(LobbyType.SANMA)) ? 3 : 4;
        }

        // Игроки
        private void UN(XmlReader R)
        {
            int[] Dan = R.GetIntArray("dan");
            int[] Rate = R.GetIntArray("rate");
            string[] Sex = R.GetStringArray("sx");

            if (Dan == null)
            {
                // Переподключение
                for (int i = 0; i < Rp.Players.Count; i++)
                {
                    string NickName = R.GetString(String.Format("n{0:d}", i));

                    if (NickName != null)
                    {
                        // Это он.
                        Rnd.Steps.Add(new Mahjong.Steps.Connect(i));
                        break;
                    }
                }
            }
            else
            {
                // Пересборка информации об игроках
                Rp.Players.Clear();
                for (int i = 0; i < Dan.Length; i++)
                {
                    Mahjong.Player P = new Mahjong.Player();

                    string NickName = R.GetString(String.Format("n{0:d}", i));
                    if (NickName != null) P.NickName = Uri.UnescapeDataString(NickName);

                    P.Rank = Dan[i];
                    P.Rating = Rate[i];
                    P.Sex = Sex[i].DecodeSex();

                    Rp.Players.Add(P);
                }
            }
        }

        // Дисконнект.
        private void BYE(XmlReader R)
        {
            int Who = R.GetInt32("who");

            if (Rnd != null) Rnd.Steps.Add(new Mahjong.Steps.Disconnect(Who));
        }

        // Генерация стены
        private void SHUFFLE(XmlReader R)
        {
            string Seed = R.GetString("seed");

            Generator = new WallGenerator(Seed);
        }

        private void INIT(XmlReader R)
        {
            // Добавим новую раздачу
            Rnd = new Mahjong.Round();
            Rnd.Index = Rp.Rounds.Count;
            Rp.Rounds.Add(Rnd);

            // Создание стены
            if (Generator != null)
            {
                Generator.Generate(Rp.Rounds.Count - 1);

                Rnd.Wall = new Mahjong.Wall();
                Rnd.Wall.Tiles = Generator.GetWall();
                Rnd.Wall.Dice = Generator.GetDice();
            }

            int[] Seed = R.GetIntArray("seed");
            int[] Balance = R.GetIntArray("ten");
            int Dealer = R.GetInt32("oya");

            for (int i = 0; i < Rp.PlayerCount; i++)
                Rnd.Hands.Add(R.GetIntArray(String.Format("hai{0:d}", i)));

            // Указатель доры и палки
            Rnd.RenchanStick = Seed[1];
            Rnd.RiichiStick = Seed[2];
            Rnd.Dora = Seed[5];

            // Дилер
            Rnd.Dealer = Dealer;

            // Баланс на начало раздачи
            Rnd.Balance = new List<int>(Balance);
            Rnd.Balance.ForEach(a => a *= 100);

            Rnd.BalanceAfter.Fill(Rp.PlayerCount, 0);
            Rnd.Pay.Fill(Rp.PlayerCount, 0);
        }

        private void TAIKYOKU(XmlReader R)
        {

        }

        // Ничья
        private void RYUUKYOKU(XmlReader R)
        {
            int Reason = R.GetString("type").DecodeDraw();
            if (Rnd != null) Rnd.Steps.Add(new Mahjong.Steps.Draw(Reason));

            CheckScore(R, null);
            CheckEnd(R);
        }

        // Объявление
        private void N(XmlReader R)
        {
            int Who = R.GetInt32("who");
            int m = R.GetInt32("m");

            NakiDecoder Naki = new NakiDecoder(m);

            Rnd.Steps.Add(new Mahjong.Steps.Naki(Who, Naki.Build()));
        }

        // Открылся указатель доры
        private void DORA(XmlReader R)
        {
            int Hai = R.GetInt32("hai");
            if (Rnd != null) Rnd.Steps.Add(new Mahjong.Steps.Dora(Hai));
        }

        // Объявление победы
        private void AGARI(XmlReader R)
        {
            Mahjong.Agari Agari = new Mahjong.Agari();

            int Who = R.GetInt32("who");
            int From = R.GetInt32("fromWho");

            // Забранные палки с депозита
            int[] Ba = R.GetIntArray("ba");

            Agari.Who = Who;
            Agari.From = From;
            Agari.Ba = new List<int>(Ba);
            Agari.Pay.Fill(Rp.PlayerCount, 0);

            if (Who == From)
            {
                // Цумо
                if (Rnd != null) Rnd.Steps.Add(new Mahjong.Steps.Tsumo(Who));
            }
            else
            {
                // Рон
                if (Rnd != null) Rnd.Steps.Add(new Mahjong.Steps.Ron(Who, From));
            }

            // Рука
            int[] Hai = R.GetIntArray("hai");
            // Выигрышный тайл
            int Machi = R.GetInt32("machi");

            Agari.Tiles = new List<int>(Hai);
            Agari.Machi = Machi;

            // Доры и урадоры
            int[] Dora = R.GetIntArray("doraHai");
            int[] UraDora = R.GetIntArray("doraHaiUra");

            Agari.Dora = new List<int>(Dora);
            if(UraDora != null) Agari.UraDora = new List<int>(UraDora);

            // Список яку
            {
                bool Yakuman = false;
                int[] Yaku = R.GetIntArray("yaku");
                if (Yaku == null)
                {
                    Yakuman = true;
                    Yaku = R.GetIntArray("yakuman");
                }

                if (Yakuman)
                {
                    for (int i = 0; i < Yaku.Length; i++) Agari.Yaku.Add(new Mahjong.Yaku(Yaku[i], 13));
                }
                else
                {
                    for (int i = 0; i < Yaku.Length / 2; i++)
                    {
                        int YakuIndex = Yaku[i * 2];
                        int YakuCost = Yaku[i * 2 + 1];

                        if(YakuCost > 0) Agari.Yaku.Add(new Mahjong.Yaku(YakuIndex, YakuCost));
                    }
                }
            }

            // Стоимость руки
            {
                int[] Ten = R.GetIntArray("ten");

                Agari.Fu = Ten[0];
                Agari.Cost = Ten[1];
            }

            Rnd.Agari.Add(Agari);

            CheckScore(R, Agari);
            CheckEnd(R);
        }

        // Объявление риичи
        private void REACH(XmlReader R)
        {
            int Who = R.GetInt32("who");
            int S = R.GetInt32("step");

            switch (S)
            {
                case 1: if (Rnd != null) Rnd.Steps.Add(new Mahjong.Steps.Riichi(Who)); break;
                case 2:
                    // "ten":
                    if (Rnd != null)
                    {
                        Rnd.Steps.Add(new Mahjong.Steps.Riichi1000(Who));
                        Rnd.Pay[Who] -= 1000;
                    }
                    break;
            }
        }

        // Результат раздачи
        private void CheckScore(XmlReader R, Mahjong.Agari A)
        {
            int[] Score = R.GetIntArray("sc");

            for (int i = 0; i < Score.Length / 2; i++)
            {
                int Balance = Score[i * 2 + 0] * 100;
                int Pay = Score[i * 2 + 1] * 100;

                Rnd.Pay[i] += Pay;
                Rnd.BalanceAfter[i] = Balance;

                if (A != null) A.Pay[i] = Pay;
            }
        }

        // Результат игры
        private void CheckEnd(XmlReader R)
        {
            int[] Owari = R.GetIntArray("owari");
            if (Owari == null) return;

            for (int i = 0; i < Rp.PlayerCount; i++)
            {
                Rp.Balance.Add(Owari[i * 2] * 100);
                Rp.Result.Add(Owari[i * 2 + 1]);
            }

            // Рассчитаем занятые места
            int[] ListOrder = new int[Rp.PlayerCount];
            for (int i = 0; i < Rp.PlayerCount; i++) ListOrder[i] = Rp.Result[i];

            Array.Sort(ListOrder);
            Array.Reverse(ListOrder);

            Rp.Place.Fill(Rp.PlayerCount, 0);

            for (int i = 0; i < Rp.PlayerCount; i++)
            {
                int Index = 0;
                for (int j = 0; j < Rp.PlayerCount; j++) if (ListOrder[i] == Rp.Result[j]) Index = j;

                Rp.Place[Index] = i + 1;
            }
        }

        // Взять тайл со стены
        private void DrawTile(int Who, int Tile)
        {
            Rnd.Steps.Add(new Mahjong.Steps.DrawTile(Who, Tile));
        }

        // Сбросить тайл с руки
        private void DiscardTile(int Who, int Tile)
        {
            Rnd.Steps.Add(new Mahjong.Steps.DiscardTile(Who, Tile));
        }

        private void ACTION(XmlReader R)
        {
            int Tile = R.Name.DecodeTile();

            if (Tile < 0) return;

            switch (R.Name[0])
            {
                case 'T': DrawTile(0, Tile); break;
                case 'U': DrawTile(1, Tile); break;
                case 'V': DrawTile(2, Tile); break;
                case 'W': DrawTile(3, Tile); break;

                case 'D': DiscardTile(0, Tile); break;
                case 'E': DiscardTile(1, Tile); break;
                case 'F': DiscardTile(2, Tile); break;
                case 'G': DiscardTile(3, Tile); break;
            }
        }

        private DateTime GetDateTimeFromHash(string Hash)
        {
            // yyyymmdd
            int Year = Convert.ToInt16(Hash.Substring(0, 4));
            int Month = Convert.ToInt16(Hash.Substring(4, 2));
            int Day = Convert.ToInt16(Hash.Substring(6, 2));
            int Hour = Convert.ToInt16(Hash.Substring(8, 2));

            DateTime Result = new DateTime(Year, Month, Day, Hour, 0, 0);

            return Result;
        }
    }
}
