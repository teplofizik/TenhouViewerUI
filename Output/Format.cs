using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mahjong;
using Mahjong.Helper;

using Search;

namespace Output
{
    static class Format
    {
        readonly static Formatter[] FormatList = new Formatter[] {
            new Formatter("link",
                          "Ссылка на раздачу",
                          ((R, Rnd, p) => String.Format("http://tenhou.net/0/?log={0:s}&ts={1:d}&tw={2:d}\t",
                                                        R.Hash, Rnd.Index, p))),
            new Formatter("nickname",
                          "Ник игрока",
                          ((R, Rnd, p) => String.Format("{0:s}\t", R.Players[p].NickName))),
            new Formatter("rating",
                          "Рейтинг игрока",
                          ((R, Rnd, p) => String.Format("{0:d}\t", R.Players[p].Rating))),
            new Formatter("jrating",
                          "Рейтинг игрока",
                          ((R, Rnd, p) => String.Format("{0:d}R\t", R.Players[p].Rating))),
            new Formatter("rank",
                          "Ранг игрока",
                          ((R, Rnd, p) => String.Format("{0:d}\t", R.Players[p].Rank))),
            new Formatter("jrank",
                          "Ранг игрока (по-японски)",
                          ((R, Rnd, p) => String.Format("{0:s}\t", R.Players[p].Rank.toJapaneseRank()))),
            new Formatter("place",
                          "Занятое место игрока",
                          ((R, Rnd, p) => String.Format("{0:d}\t", R.Place[p]))),            
            new Formatter("tablerating",
                          "Средний рейтинг стола",
                          ((R, Rnd, p) => String.Format("{0:d}\t", R.Players.AverageRating()))),
            new Formatter("jtablerating",
                          "Средний рейтинг стола",
                          ((R, Rnd, p) => String.Format("{0:d}R\t", R.Players.AverageRating()))),
            new Formatter("pay",
                          "Выплата за раздачу",
                          ((R, Rnd, p) => String.Format("{0:d}\t", Rnd.Pay[p]))),
            new Formatter("dealer",
                          "Был дилером в раздаче",
                          ((R, Rnd, p) => String.Format("{0:d}\t", (Rnd.Dealer == p) ? 1 : 0))),
            new Formatter("resbalance",
                          "Баланс на конец игры",
                          ((R, Rnd, p) => String.Format("{0:d}\t", R.Balance[p]))),
            new Formatter("result",
                          "Результат игры",
                          ((R, Rnd, p) => String.Format("{0:d}\t", R.Result[p]))),
            new Formatter("tsumo",
                          "Выиграл раздачу по цумо",
                          ((R, Rnd, p) => String.Format("{0:d}\t", Rnd.hasTsumoAgari(p) ? 1 : 0))),
            new Formatter("ron",
                          "Выиграл раздачу по рону",
                          ((R, Rnd, p) => String.Format("{0:d}\t", (Rnd.hasAgari(p) && !Rnd.hasTsumoAgari(p)) ? 1 : 0))),
            new Formatter("agaricount",
                          "Количество собранных за игру рук",
                          ((R, Rnd, p) => String.Format("{0:d}\t", R.AgariCount(p)))),
            new Formatter("tsumocount",
                          "Количество собранных за игру рук по цумо",
                          ((R, Rnd, p) => String.Format("{0:d}\t", R.TsumoCount(p)))),
            new Formatter("from",
                          "Ник набросившего игрока",
                          ((R, Rnd, p) => String.Format("{0:s}\t", (Rnd.Loser() >= 0) ? R.Players[Rnd.Loser()].NickName : ""))),
            new Formatter("winner",
                          "Выиграл раздачу",
                          ((R, Rnd, p) => String.Format("{0:d}\t", Rnd.hasAgari(p) ? 1 : 0))),
            new Formatter("loser",
                          "Набросил в рон",
                          ((R, Rnd, p) => String.Format("{0:d}\t", Rnd.isLoser(p) ? 1 : 0))),
            new Formatter("concealed",
                          "Не открывал руку",
                          ((R, Rnd, p) => String.Format("{0:d}\t", Rnd.isConcealed(p) ? 1 : 0))),
            new Formatter("cost",
                          "Стоимость руки",
                          ((R, Rnd, p) => String.Format("{0:d}\t", Rnd.HandCost(p)))),
            new Formatter("han",
                          "Количество хан в руке",
                          ((R, Rnd, p) => String.Format("{0:d}\t", Rnd.HanCount(p)))),
            new Formatter("fu",
                          "Количество фу в руке",
                          ((R, Rnd, p) => String.Format("{0:d}\t", Rnd.FuCount(p)))),
            new Formatter("step",
                          "Количество ходов за раздачу",
                          ((R, Rnd, p) => String.Format("{0:d}\t", Rnd.getStepCount(p)))),
            new Formatter("round",
                          "Порядковый номер текущей раздачи",
                          ((R, Rnd, p) => String.Format("{0:d}\t", Rnd.Index))),
            new Formatter("roundcount",
                          "Количество раздач",
                          ((R, Rnd, p) => String.Format("{0:d}\t", R.Rounds.Count))),
            new Formatter("lobby",
                          "Номер игрового лобби",
                          ((R, Rnd, p) => String.Format("{0:d}\t", R.Lobby))),
            new Formatter("roundindex",
                          "Номер текущей раздачи",
                          ((R, Rnd, p) => String.Format("{0:d}\t", R.RoundIndex(Rnd.Index)))),
            new Formatter("roundwind",
                          "Ветер раунда",
                          ((R, Rnd, p) => String.Format("{0:s}\t", R.RoundWind(Rnd.Index).ToWind()))),
            new Formatter("playerwind",
                          "Ветер игрока",
                          ((R, Rnd, p) => String.Format("{0:s}\t", R.PlayerWind(Rnd.Index, p).ToWind()))),
            new Formatter("players",
                          "Колиество игроков",
                          ((R, Rnd, p) => String.Format("{0:d}\t", R.PlayerCount))),
            new Formatter("draw",
                          "Раздача закончилась ничьей",
                          ((R, Rnd, p) => String.Format("{0:d}\t", Rnd.IsDraw() ? 1 : 0))),
            new Formatter("drawreason",
                          "Раздача закончилась ничьей",
                          ((R, Rnd, p) => String.Format("{0:s}\t", Rnd.DrawReason().ToDrawReason()))),
            // "tempaicount" Сколько раз выходил в темпай
            // "waiting" Количество ожиданий при темпае
            // "yaku" Список яку (англ)
            // "jyaku" Список яку (яп)
            // "type" Тип лобби
            // "danger" Максимальное количес
            // "furiten" Было ли в раздаче у игрока состояние фуритена
        };

        static public List<string> FormatResults(List<Result> Results, FieldList Fields)
        {
            List<string> Output = new List<string>();

            foreach (Result R in Results)
            {
                bool Skip = false;

                if (!R.ReplayMark) continue;

                OutputResult OR = R.Build();

                for (int r = 0; (r < R.R.Rounds.Count) && !Skip; r++)
                {
                    for (int p = 0; (p < R.R.PlayerCount) && !Skip; p++)
                    {
                        if (!OR.HandMark[r][p]) continue;

                        Output.Add(FormatRound(R.R, r, p, Fields));

                        if (Fields.ReplayOnce)
                        {
                            Skip = true;
                            break;
                        }
                    }

                }
            }

            return Output;
        }

        static private string FormatRound(Replay R, int Round, int Player, FieldList Fields)
        {
            Round Rnd = R.Rounds[Round];
            string Result = "";

            foreach (Field F in Fields.Fields)
            {
                // В поле номер игрока может быть переопределён
                int P = F.GetPlayerIndex(Player);

                Formatter Fmt = FormatList.GetFormatter(F);
                if (Fmt != null) Result += Fmt.Format(R, Rnd, P);
            }

            return Result;
        }
    }
}
