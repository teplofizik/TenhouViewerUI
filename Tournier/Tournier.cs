using System;
using System.Collections.Generic;
using System.Text;

using Mahjong;
using Mahjong.Helper;
using Mahjong.Statistic;

namespace Tournier
{
    class Tournier
    {
        List<Search.Result> GamesList;
        private List<Result> Results = new List<Result>();

        public Tournier(List<Search.Result> Games)
        {
            GamesList = Games;
        }

        public List<Result> Analyze()
        {
            if (GamesList != null)
            {
                Results.Clear();

                // Analyze player's results
                for (int i = 0; i < GamesList.Count; i++)
                {
                    Replay R = GamesList[i].R;

                    if (GamesList[i].ReplayMark) continue;

                    for (int p = 0; p < R.PlayerCount; p++)
                    {
                        if (GamesList[i].PlayerMark[p]) continue;

                        Result Res = GetPlayerResult(R.Players[p].NickName);
                        Res.AddResult(R.Place[p], R.Balance[p], R.Result[p]);
                        Res.Replays.Add(R);

                        Res.Rank = R.Players[p].Rank;
                        Res.Rating = R.Players[p].Rating;

                        for (int r = 0; r < R.Rounds.Count; r++)
                        {
                            Round Rnd = R.Rounds[r];

                            if (GamesList[i].RoundMark[r]) continue;
                            if (GamesList[i].HandMark[r][p]) continue;

                            Res.RoundCount++;

                            // Собрал руку
                            if (Rnd.hasAgari(p))
                            {
                                Yaku[] YL = Rnd.getYaku(p);

                                // Calc yaku
                                for (int j = 0; j < YL.Length; j++)
                                {
                                    Yaku Y = YL[j];

                                    if (Y.Index > 51) // Dora
                                        Res.Yaku[Y.Index] += Y.Cost;
                                    else
                                        Res.Yaku[Y.Index]++;
                                }

                                Res.AgariCount++;
                            }

                            // Накинул
                            if (Rnd.isLoser(p)) Res.RonCount++;

                            // Выплаты
                            if (Rnd.Pay[p] >= 0)
                            {
                                if (Rnd.IsDraw())            Res.DrawAcquisitions += Rnd.Pay[p];
                                else if (Rnd.isTsumoAgari()) Res.TsumoAcquisitions += Rnd.Pay[p];
                                else if (Rnd.isRonAgari())   Res.RonAcquisitions += Rnd.Pay[p];

                                Res.TotalAcquisitions += Rnd.Pay[p];
                            }
                            else
                            {
                                if (Rnd.isLoser(p)) Res.RonLosses += Rnd.Pay[p];
                                else if (Rnd.IsDraw()) Res.DrawLosses += Rnd.Pay[p];
                                else if (Rnd.isTsumoAgari()) Res.TsumoLosses += Rnd.Pay[p];
                                Res.TotalLosses += Rnd.Pay[p];
                            }

                            //if (HasFuriten(Rnd, p)) Res.Furiten++;
                            //if (HasTempai(Rnd, p)) Res.Tempai++;
                            if (Rnd.isDeclaredRiichi(p))
                            {
                                Res.RiichiCount++;

                                if (Rnd.hasAgari(p))
                                {
                                    Res.RiichiWinCount++;

                                    Yaku[] Y = Rnd.getYaku(p);
                                    for (int j = 0; j < Y.Length; j++)
                                    {
                                        if (Y[j].Index == 2)
                                        {
                                            Res.IppatsuCount++;
                                            break;
                                        }
                                    }
                                }
                            }

                            if (Rnd.OpenedSets(p) > 0) Res.OpenedSetsCount++;
                        }
                    }
                }
            }

            return Results;
        }

        private Result GetPlayerResult(string Name)
        {
            for (int i = 0; i < Results.Count; i++)
                if (Results[i].NickName.CompareTo(Name) == 0)
                    return Results[i];

            // Not found. Create new:
            Result NewResult = new Result(Name);
            Results.Add(NewResult);

            return NewResult;
        }
    }
}
