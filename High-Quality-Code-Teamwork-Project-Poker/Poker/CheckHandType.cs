using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poker.Interfaces;
using Poker.Models;

namespace Poker
{
    public class CheckHandType
    {

        public CheckHandType()
        {
        }

        public void CheckStraightFlush(IPlayer player, int[] st1, int[] st2, int[] st3, int[] st4, ref List<Type> Win, ref Type sorted)
        {
            if (player.Type.Current >= -1)
            {
                if (st1.Length >= 5)
                {
                    if (st1[0] + 4 == st1[4])
                    {
                        player.Type.Current = 8;
                        player.Type.Power = (st1.Max()) / 4 + player.Type.Current * 100;
                        Win.Add(new Type() { Power = player.Type.Power, Current = 8 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (st1[0] == 0 && st1[1] == 9 && st1[2] == 10 && st1[3] == 11 && st1[0] + 12 == st1[4])
                    {
                        player.Type.Current = 9;
                        player.Type.Power = (st1.Max()) / 4 + player.Type.Current * 100;
                        Win.Add(new Type() { Power = player.Type.Power, Current = 9 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (st2.Length >= 5)
                {
                    if (st2[0] + 4 == st2[4])
                    {
                        player.Type.Current = 8;
                        player.Type.Power = (st2.Max()) / 4 + player.Type.Current * 100;
                        Win.Add(new Type() { Power = player.Type.Power, Current = 8 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (st2[0] == 0 && st2[1] == 9 && st2[2] == 10 && st2[3] == 11 && st2[0] + 12 == st2[4])
                    {
                        player.Type.Current = 9;
                        player.Type.Power = (st2.Max()) / 4 + player.Type.Current * 100;
                        Win.Add(new Type() { Power = player.Type.Power, Current = 9 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (st3.Length >= 5)
                {
                    if (st3[0] + 4 == st3[4])
                    {
                        player.Type.Current = 8;
                        player.Type.Power = (st3.Max()) / 4 + player.Type.Current * 100;
                        Win.Add(new Type() { Power = player.Type.Power, Current = 8 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (st3[0] == 0 && st3[1] == 9 && st3[2] == 10 && st3[3] == 11 && st3[0] + 12 == st3[4])
                    {
                        player.Type.Current = 9;
                        player.Type.Power = (st3.Max()) / 4 + player.Type.Current * 100;
                        Win.Add(new Type() { Power = player.Type.Power, Current = 9 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (st4.Length >= 5)
                {
                    if (st4[0] + 4 == st4[4])
                    {
                        player.Type.Current = 8;
                        player.Type.Power = (st4.Max()) / 4 + player.Type.Current * 100;
                        Win.Add(new Type() { Power = player.Type.Power, Current = 8 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (st4[0] == 0 && st4[1] == 9 && st4[2] == 10 && st4[3] == 11 && st4[0] + 12 == st4[4])
                    {
                        player.Type.Current = 9;
                        player.Type.Power = (st4.Max()) / 4 + player.Type.Current * 100;
                        Win.Add(new Type() { Power = player.Type.Power, Current = 9 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }

        public void CheckFourOfAKind(IPlayer player, int[] Straight, ref List<Type> Win, ref Type sorted)
        {
            if (player.Type.Current >= -1)
            {
                for (int j = 0; j <= 3; j++)
                {
                    if (Straight[j] / 4 == Straight[j + 1] / 4 && Straight[j] / 4 == Straight[j + 2] / 4 &&
                        Straight[j] / 4 == Straight[j + 3] / 4)
                    {
                        player.Type.Current = 7;
                        player.Type.Power = (Straight[j] / 4) * 4 + player.Type.Current * 100;
                        Win.Add(new Type() { Power = player.Type.Power, Current = 7 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (Straight[j] / 4 == 0 && Straight[j + 1] / 4 == 0 && Straight[j + 2] / 4 == 0 && Straight[j + 3] / 4 == 0)
                    {
                        player.Type.Current = 7;
                        player.Type.Power = 13 * 4 + player.Type.Current * 100;
                        Win.Add(new Type() { Power = player.Type.Power, Current = 7 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }

        public void CheckFullHouse(IPlayer player, ref bool done, int[] Straight, ref List<Type> Win, ref Type sorted)
        {
            if (player.Type.Current >= -1)
            {
                double type = player.Type.Power;
                for (int j = 0; j <= 12; j++)
                {
                    var fh = Straight.Where(o => o / 4 == j).ToArray();
                    if (fh.Length == 3 || done)
                    {
                        if (fh.Length == 2)
                        {
                            if (fh.Max() / 4 == 0)
                            {
                                player.Type.Current = 6;
                                player.Type.Power = 13 * 2 + player.Type.Current * 100;
                                Win.Add(new Type() { Power = player.Type.Power, Current = 6 });
                                sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                                break;
                            }
                            if (fh.Max() / 4 > 0)
                            {
                                player.Type.Current = 6;
                                player.Type.Power = fh.Max() / 4 * 2 + player.Type.Current * 100;
                                Win.Add(new Type() { Power = player.Type.Power, Current = 6 });
                                sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                                break;
                            }
                        }
                        if (!done)
                        {
                            if (fh.Max() / 4 == 0)
                            {
                                player.Type.Power = 13;
                                done = true;
                                j = -1;
                            }
                            else
                            {
                                player.Type.Power = fh.Max() / 4;
                                done = true;
                                j = -1;
                            }
                        }
                    }
                }
                if (player.Type.Current != 6)
                {
                    player.Type.Power = type;
                }
            }
        }

        public void CheckFlush(IPlayer player, ref bool vf, int[] Straight1, ref List<Type> Win, ref Type sorted, IList<Card> Reserve, int i)
        {
            if (player.Type.Current >= -1)
            {
                var f1 = Straight1.Where(o => o % 4 == 0).ToArray();
                var f2 = Straight1.Where(o => o % 4 == 1).ToArray();
                var f3 = Straight1.Where(o => o % 4 == 2).ToArray();
                var f4 = Straight1.Where(o => o % 4 == 3).ToArray();
                if (f1.Length == 3 || f1.Length == 4)
                {
                    if (Reserve[i].Power % 4 == Reserve[i + 1].Power % 4 && Reserve[i].Power % 4 == f1[0] % 4)
                    {
                        if (Reserve[i].Power / 4 > f1.Max() / 4)
                        {
                            player.Type.Current = 5;
                            player.Type.Power = Reserve[i].Power + player.Type.Current * 100;
                            Win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        if (Reserve[i + 1].Power / 4 > f1.Max() / 4)
                        {
                            player.Type.Current = 5;
                            player.Type.Power = Reserve[i + 1].Power + player.Type.Current * 100;
                            Win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (Reserve[i].Power / 4 < f1.Max() / 4 && Reserve[i + 1].Power / 4 < f1.Max() / 4)
                        {
                            player.Type.Current = 5;
                            player.Type.Power = f1.Max() + player.Type.Current * 100;
                            Win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f1.Length == 4)//different cards in hand
                {
                    if (Reserve[i].Power % 4 != Reserve[i + 1].Power % 4 && Reserve[i].Power % 4 == f1[0] % 4)
                    {
                        if (Reserve[i].Power / 4 > f1.Max() / 4)
                        {
                            player.Type.Current = 5;
                            player.Type.Power = Reserve[i].Power + player.Type.Current * 100;
                            Win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            player.Type.Current = 5;
                            player.Type.Power = f1.Max() + player.Type.Current * 100;
                            Win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                    if (Reserve[i + 1].Power % 4 != Reserve[i].Power % 4 && Reserve[i + 1].Power % 4 == f1[0] % 4)
                    {
                        if (Reserve[i + 1].Power / 4 > f1.Max() / 4)
                        {
                            player.Type.Current = 5;
                            player.Type.Power = Reserve[i + 1].Power + player.Type.Current * 100;
                            Win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            player.Type.Current = 5;
                            player.Type.Power = f1.Max() + player.Type.Current * 100;
                            Win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f1.Length == 5)
                {
                    if (Reserve[i].Power % 4 == f1[0] % 4 && Reserve[i].Power / 4 > f1.Min() / 4)
                    {
                        player.Type.Current = 5;
                        player.Type.Power = Reserve[i].Power + player.Type.Current * 100;
                        Win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (Reserve[i + 1].Power % 4 == f1[0] % 4 && Reserve[i + 1].Power / 4 > f1.Min() / 4)
                    {
                        player.Type.Current = 5;
                        player.Type.Power = Reserve[i + 1].Power + player.Type.Current * 100;
                        Win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (Reserve[i].Power / 4 < f1.Min() / 4 && Reserve[i + 1].Power / 4 < f1.Min())
                    {
                        player.Type.Current = 5;
                        player.Type.Power = f1.Max() + player.Type.Current * 100;
                        Win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (f2.Length == 3 || f2.Length == 4)
                {
                    if (Reserve[i].Power % 4 == Reserve[i + 1].Power % 4 && Reserve[i].Power % 4 == f2[0] % 4)
                    {
                        if (Reserve[i].Power / 4 > f2.Max() / 4)
                        {
                            player.Type.Current = 5;
                            player.Type.Power = Reserve[i].Power + player.Type.Current * 100;
                            Win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        if (Reserve[i + 1].Power / 4 > f2.Max() / 4)
                        {
                            player.Type.Current = 5;
                            player.Type.Power = Reserve[i + 1].Power + player.Type.Current * 100;
                            Win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (Reserve[i].Power / 4 < f2.Max() / 4 && Reserve[i + 1].Power / 4 < f2.Max() / 4)
                        {
                            player.Type.Current = 5;
                            player.Type.Power = f2.Max() + player.Type.Current * 100;
                            Win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f2.Length == 4)//different cards in hand
                {
                    if (Reserve[i].Power % 4 != Reserve[i + 1].Power % 4 && Reserve[i].Power % 4 == f2[0] % 4)
                    {
                        if (Reserve[i].Power / 4 > f2.Max() / 4)
                        {
                            player.Type.Current = 5;
                            player.Type.Power = Reserve[i].Power + player.Type.Current * 100;
                            Win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            player.Type.Current = 5;
                            player.Type.Power = f2.Max() + player.Type.Current * 100;
                            Win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                    if (Reserve[i + 1].Power % 4 != Reserve[i].Power % 4 && Reserve[i + 1].Power % 4 == f2[0] % 4)
                    {
                        if (Reserve[i + 1].Power / 4 > f2.Max() / 4)
                        {
                            player.Type.Current = 5;
                            player.Type.Power = Reserve[i + 1].Power + player.Type.Current * 100;
                            Win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            player.Type.Current = 5;
                            player.Type.Power = f2.Max() + player.Type.Current * 100;
                            Win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f2.Length == 5)
                {
                    if (Reserve[i].Power % 4 == f2[0] % 4 && Reserve[i].Power / 4 > f2.Min() / 4)
                    {
                        player.Type.Current = 5;
                        player.Type.Power = Reserve[i].Power + player.Type.Current * 100;
                        Win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (Reserve[i + 1].Power % 4 == f2[0] % 4 && Reserve[i + 1].Power / 4 > f2.Min() / 4)
                    {
                        player.Type.Current = 5;
                        player.Type.Power = Reserve[i + 1].Power + player.Type.Current * 100;
                        Win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (Reserve[i].Power / 4 < f2.Min() / 4 && Reserve[i + 1].Power / 4 < f2.Min())
                    {
                        player.Type.Current = 5;
                        player.Type.Power = f2.Max() + player.Type.Current * 100;
                        Win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (f3.Length == 3 || f3.Length == 4)
                {
                    if (Reserve[i].Power % 4 == Reserve[i + 1].Power % 4 && Reserve[i].Power % 4 == f3[0] % 4)
                    {
                        if (Reserve[i].Power / 4 > f3.Max() / 4)
                        {
                            player.Type.Current = 5;
                            player.Type.Power = Reserve[i].Power + player.Type.Current * 100;
                            Win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        if (Reserve[i + 1].Power / 4 > f3.Max() / 4)
                        {
                            player.Type.Current = 5;
                            player.Type.Power = Reserve[i + 1].Power + player.Type.Current * 100;
                            Win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (Reserve[i].Power / 4 < f3.Max() / 4 && Reserve[i + 1].Power / 4 < f3.Max() / 4)
                        {
                            player.Type.Current = 5;
                            player.Type.Power = f3.Max() + player.Type.Current * 100;
                            Win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f3.Length == 4)//different cards in hand
                {
                    if (Reserve[i].Power % 4 != Reserve[i + 1].Power % 4 && Reserve[i].Power % 4 == f3[0] % 4)
                    {
                        if (Reserve[i].Power / 4 > f3.Max() / 4)
                        {
                            player.Type.Current = 5;
                            player.Type.Power = Reserve[i].Power + player.Type.Current * 100;
                            Win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            player.Type.Current = 5;
                            player.Type.Power = f3.Max() + player.Type.Current * 100;
                            Win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                    if (Reserve[i + 1].Power % 4 != Reserve[i].Power % 4 && Reserve[i + 1].Power % 4 == f3[0] % 4)
                    {
                        if (Reserve[i + 1].Power / 4 > f3.Max() / 4)
                        {
                            player.Type.Current = 5;
                            player.Type.Power = Reserve[i + 1].Power + player.Type.Current * 100;
                            Win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            player.Type.Current = 5;
                            player.Type.Power = f3.Max() + player.Type.Current * 100;
                            Win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f3.Length == 5)
                {
                    if (Reserve[i].Power % 4 == f3[0] % 4 && Reserve[i].Power / 4 > f3.Min() / 4)
                    {
                        player.Type.Current = 5;
                        player.Type.Power = Reserve[i].Power + player.Type.Current * 100;
                        Win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (Reserve[i + 1].Power % 4 == f3[0] % 4 && Reserve[i + 1].Power / 4 > f3.Min() / 4)
                    {
                        player.Type.Current = 5;
                        player.Type.Power = Reserve[i + 1].Power + player.Type.Current * 100;
                        Win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (Reserve[i].Power / 4 < f3.Min() / 4 && Reserve[i + 1].Power / 4 < f3.Min())
                    {
                        player.Type.Current = 5;
                        player.Type.Power = f3.Max() + player.Type.Current * 100;
                        Win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (f4.Length == 3 || f4.Length == 4)
                {
                    if (Reserve[i].Power % 4 == Reserve[i + 1].Power % 4 && Reserve[i].Power % 4 == f4[0] % 4)
                    {
                        if (Reserve[i].Power / 4 > f4.Max() / 4)
                        {
                            player.Type.Current = 5;
                            player.Type.Power = Reserve[i].Power + player.Type.Current * 100;
                            Win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        if (Reserve[i + 1].Power / 4 > f4.Max() / 4)
                        {
                            player.Type.Current = 5;
                            player.Type.Power = Reserve[i + 1].Power + player.Type.Current * 100;
                            Win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (Reserve[i].Power / 4 < f4.Max() / 4 && Reserve[i + 1].Power / 4 < f4.Max() / 4)
                        {
                            player.Type.Current = 5;
                            player.Type.Power = f4.Max() + player.Type.Current * 100;
                            Win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f4.Length == 4)//different cards in hand
                {
                    if (Reserve[i].Power % 4 != Reserve[i + 1].Power % 4 && Reserve[i].Power % 4 == f4[0] % 4)
                    {
                        if (Reserve[i].Power / 4 > f4.Max() / 4)
                        {
                            player.Type.Current = 5;
                            player.Type.Power = Reserve[i].Power + player.Type.Current * 100;
                            Win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            player.Type.Current = 5;
                            player.Type.Power = f4.Max() + player.Type.Current * 100;
                            Win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                    if (Reserve[i + 1].Power % 4 != Reserve[i].Power % 4 && Reserve[i + 1].Power % 4 == f4[0] % 4)
                    {
                        if (Reserve[i + 1].Power / 4 > f4.Max() / 4)
                        {
                            player.Type.Current = 5;
                            player.Type.Power = Reserve[i + 1].Power + player.Type.Current * 100;
                            Win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            player.Type.Current = 5;
                            player.Type.Power = f4.Max() + player.Type.Current * 100;
                            Win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f4.Length == 5)
                {
                    if (Reserve[i].Power % 4 == f4[0] % 4 && Reserve[i].Power / 4 > f4.Min() / 4)
                    {
                        player.Type.Current = 5;
                        player.Type.Power = Reserve[i].Power + player.Type.Current * 100;
                        Win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (Reserve[i + 1].Power % 4 == f4[0] % 4 && Reserve[i + 1].Power / 4 > f4.Min() / 4)
                    {
                        player.Type.Current = 5;
                        player.Type.Power = Reserve[i + 1].Power + player.Type.Current * 100;
                        Win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (Reserve[i].Power / 4 < f4.Min() / 4 && Reserve[i + 1].Power / 4 < f4.Min())
                    {
                        player.Type.Current = 5;
                        player.Type.Power = f4.Max() + player.Type.Current * 100;
                        Win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }
                //ace
                if (f1.Length > 0)
                {
                    if (Reserve[i].Power / 4 == 0 && Reserve[i].Power % 4 == f1[0] % 4 && vf && f1.Length > 0)
                    {
                        player.Type.Current = 5.5;
                        player.Type.Power = 13 + player.Type.Current * 100;
                        Win.Add(new Type() { Power = player.Type.Power, Current = 5.5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (Reserve[i + 1].Power / 4 == 0 && Reserve[i + 1].Power % 4 == f1[0] % 4 && vf && f1.Length > 0)
                    {
                        player.Type.Current = 5.5;
                        player.Type.Power = 13 + player.Type.Current * 100;
                        Win.Add(new Type() { Power = player.Type.Power, Current = 5.5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (f2.Length > 0)
                {
                    if (Reserve[i].Power / 4 == 0 && Reserve[i].Power % 4 == f2[0] % 4 && vf && f2.Length > 0)
                    {
                        player.Type.Current = 5.5;
                        player.Type.Power = 13 + player.Type.Current * 100;
                        Win.Add(new Type() { Power = player.Type.Power, Current = 5.5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (Reserve[i + 1].Power / 4 == 0 && Reserve[i + 1].Power % 4 == f2[0] % 4 && vf && f2.Length > 0)
                    {
                        player.Type.Current = 5.5;
                        player.Type.Power = 13 + player.Type.Current * 100;
                        Win.Add(new Type() { Power = player.Type.Power, Current = 5.5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (f3.Length > 0)
                {
                    if (Reserve[i].Power / 4 == 0 && Reserve[i].Power % 4 == f3[0] % 4 && vf && f3.Length > 0)
                    {
                        player.Type.Current = 5.5;
                        player.Type.Power = 13 + player.Type.Current * 100;
                        Win.Add(new Type() { Power = player.Type.Power, Current = 5.5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (Reserve[i + 1].Power / 4 == 0 && Reserve[i + 1].Power % 4 == f3[0] % 4 && vf && f3.Length > 0)
                    {
                        player.Type.Current = 5.5;
                        player.Type.Power = 13 + player.Type.Current * 100;
                        Win.Add(new Type() { Power = player.Type.Power, Current = 5.5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (f4.Length > 0)
                {
                    if (Reserve[i].Power / 4 == 0 && Reserve[i].Power % 4 == f4[0] % 4 && vf && f4.Length > 0)
                    {
                        player.Type.Current = 5.5;
                        player.Type.Power = 13 + player.Type.Current * 100;
                        Win.Add(new Type() { Power = player.Type.Power, Current = 5.5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (Reserve[i + 1].Power / 4 == 0 && Reserve[i + 1].Power % 4 == f4[0] % 4 && vf)
                    {
                        player.Type.Current = 5.5;
                        player.Type.Power = 13 + player.Type.Current * 100;
                        Win.Add(new Type() { Power = player.Type.Power, Current = 5.5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }

        public void CheckStraight(IPlayer player, int[] Straight, ref List<Type> Win, ref Type sorted)
        {
            if (player.Type.Current >= -1)
            {
                var op = Straight.Select(o => o / 4).Distinct().ToArray();
                for (int j = 0; j < op.Length - 4; j++)
                {
                    if (op[j] + 4 == op[j + 4])
                    {
                        if (op.Max() - 4 == op[j])
                        {
                            player.Type.Current = 4;
                            player.Type.Power = op.Max() + player.Type.Current * 100;
                            Win.Add(new Type() { Power = player.Type.Power, Current = 4 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        }
                        else
                        {
                            player.Type.Current = 4;
                            player.Type.Power = op[j + 4] + player.Type.Current * 100;
                            Win.Add(new Type() { Power = player.Type.Power, Current = 4 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        }
                    }
                    if (op[j] == 0 && op[j + 1] == 9 && op[j + 2] == 10 && op[j + 3] == 11 && op[j + 4] == 12)
                    {
                        player.Type.Current = 4;
                        player.Type.Power = 13 + player.Type.Current * 100;
                        Win.Add(new Type() { Power = player.Type.Power, Current = 4 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }

        public void CheckThreeOfAKind(IPlayer player, int[] Straight, ref List<Type> Win, ref Type sorted)
        {
            if (player.Type.Current >= -1)
            {
                for (int j = 0; j <= 12; j++)
                {
                    var fh = Straight.Where(o => o / 4 == j).ToArray();
                    if (fh.Length == 3)
                    {
                        if (fh.Max() / 4 == 0)
                        {
                            player.Type.Current = 3;
                            player.Type.Power = 13 * 3 + player.Type.Current * 100;
                            Win.Add(new Type() { Power = player.Type.Power, Current = 3 });
                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                        else
                        {
                            player.Type.Current = 3;
                            player.Type.Power = fh[0] / 4 + fh[1] / 4 + fh[2] / 4 + player.Type.Current * 100;
                            Win.Add(new Type() { Power = player.Type.Power, Current = 3 });
                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                    }
                }
            }
        }

        public void CheckTwoPair(IPlayer player, ref List<Type> Win, ref Type sorted,  IList<Card> Reserve, int i)
        {
            if (player.Type.Current >= -1)
            {
                bool msgbox = false;
                for (int tc = 16; tc >= 12; tc--)
                {
                    int max = tc - 12;
                    if (Reserve[i].Power / 4 != Reserve[i + 1].Power / 4)
                    {
                        for (int k = 1; k <= max; k++)
                        {
                            if (tc - k < 12)
                            {
                                max--;
                            }
                            if (tc - k >= 12)
                            {
                                if (Reserve[i].Power / 4 == Reserve[tc].Power / 4 && Reserve[i + 1].Power / 4 == Reserve[tc - k].Power / 4 ||
                                    Reserve[i + 1].Power / 4 == Reserve[tc].Power / 4 && Reserve[i].Power / 4 == Reserve[tc - k].Power / 4)
                                {
                                    if (!msgbox)
                                    {
                                        if (Reserve[i].Power / 4 == 0)
                                        {
                                            player.Type.Current = 2;
                                            player.Type.Power = 13 * 4 + (Reserve[i + 1].Power / 4) * 2 + player.Type.Current * 100;
                                            Win.Add(new Type() { Power = player.Type.Power, Current = 2 });
                                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (Reserve[i + 1].Power / 4 == 0)
                                        {
                                            player.Type.Current = 2;
                                            player.Type.Power = 13 * 4 + (Reserve[i].Power / 4) * 2 + player.Type.Current * 100;
                                            Win.Add(new Type() { Power = player.Type.Power, Current = 2 });
                                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (Reserve[i + 1].Power / 4 != 0 && Reserve[i].Power / 4 != 0)
                                        {
                                            player.Type.Current = 2;
                                            player.Type.Power = (Reserve[i].Power / 4) * 2 + (Reserve[i + 1].Power / 4) * 2 + player.Type.Current * 100;
                                            Win.Add(new Type() { Power = player.Type.Power, Current = 2 });
                                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                    }
                                    msgbox = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        public void CheckPairTwoPair(IPlayer player, ref List<Type> Win, ref Type sorted, IList<Card> Reserve, int i)
        {
            if (player.Type.Current >= -1)
            {
                bool msgbox = false;
                bool msgbox1 = false;
                for (int tc = 16; tc >= 12; tc--)
                {
                    int max = tc - 12;
                    for (int k = 1; k <= max; k++)
                    {
                        if (tc - k < 12)
                        {
                            max--;
                        }
                        if (tc - k >= 12)
                        {
                            if (Reserve[tc].Power / 4 == Reserve[tc - k].Power / 4)
                            {
                                if (Reserve[tc].Power / 4 != Reserve[i].Power / 4 && Reserve[tc].Power / 4 != Reserve[i + 1].Power / 4 && player.Type.Current == 1)
                                {
                                    if (!msgbox)
                                    {
                                        if (Reserve[i + 1].Power / 4 == 0)
                                        {
                                            player.Type.Current = 2;
                                            player.Type.Power = (Reserve[i].Power / 4) * 2 + 13 * 4 + player.Type.Current * 100;
                                            Win.Add(new Type() { Power = player.Type.Power, Current = 2 });
                                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (Reserve[i].Power / 4 == 0)
                                        {
                                            player.Type.Current = 2;
                                            player.Type.Power = (Reserve[i + 1].Power / 4) * 2 + 13 * 4 + player.Type.Current * 100;
                                            Win.Add(new Type() { Power = player.Type.Power, Current = 2 });
                                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (Reserve[i + 1].Power / 4 != 0)
                                        {
                                            player.Type.Current = 2;
                                            player.Type.Power = (Reserve[tc].Power / 4) * 2 + (Reserve[i + 1].Power / 4) * 2 + player.Type.Current * 100;
                                            Win.Add(new Type() { Power = player.Type.Power, Current = 2 });
                                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (Reserve[i].Power / 4 != 0)
                                        {
                                            player.Type.Current = 2;
                                            player.Type.Power = (Reserve[tc].Power / 4) * 2 + (Reserve[i].Power / 4) * 2 + player.Type.Current * 100;
                                            Win.Add(new Type() { Power = player.Type.Power, Current = 2 });
                                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                    }
                                    msgbox = true;
                                }
                                if (player.Type.Current == -1)
                                {
                                    if (!msgbox1)
                                    {
                                        if (Reserve[i].Power / 4 > Reserve[i + 1].Power / 4)
                                        {
                                            if (Reserve[tc].Power / 4 == 0)
                                            {
                                                player.Type.Current = 0;
                                                player.Type.Power = 13 + Reserve[i].Power / 4 + player.Type.Current * 100;
                                                Win.Add(new Type() { Power = player.Type.Power, Current = 1 });
                                                sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                            else
                                            {
                                                player.Type.Current = 0;
                                                player.Type.Power = Reserve[tc].Power / 4 + Reserve[i].Power / 4 + player.Type.Current * 100;
                                                Win.Add(new Type() { Power = player.Type.Power, Current = 1 });
                                                sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                        }
                                        else
                                        {
                                            if (Reserve[tc].Power / 4 == 0)
                                            {
                                                player.Type.Current = 0;
                                                player.Type.Power = 13 + Reserve[i + 1].Power + player.Type.Current * 100;
                                                Win.Add(new Type() { Power = player.Type.Power, Current = 1 });
                                                sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                            else
                                            {
                                                player.Type.Current = 0;
                                                player.Type.Power = Reserve[tc].Power / 4 + Reserve[i + 1].Power / 4 + player.Type.Current * 100;
                                                Win.Add(new Type() { Power = player.Type.Power, Current = 1 });
                                                sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                        }
                                    }
                                    msgbox1 = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        public void CheckPairFromHand(IPlayer player, ref List<Type> Win, ref Type sorted, IList<Card> Reserve, int i)
        {
            if (player.Type.Current >= -1)
            {
                bool msgbox = false;
                if (Reserve[i].Power / 4 == Reserve[i + 1].Power / 4)
                {
                    if (!msgbox)
                    {
                        if (Reserve[i].Power / 4 == 0)
                        {
                            player.Type.Current = 1;
                            player.Type.Power = 13 * 4 + player.Type.Current * 100;
                            Win.Add(new Type() { Power = player.Type.Power, Current = 1 });
                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                        else
                        {
                            player.Type.Current = 1;
                            player.Type.Power = (Reserve[i + 1].Power / 4) * 4 + player.Type.Current * 100;
                            Win.Add(new Type() { Power = player.Type.Power, Current = 1 });
                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                    }
                    msgbox = true;
                }
                for (int tc = 16; tc >= 12; tc--)
                {
                    if (Reserve[i + 1].Power / 4 == Reserve[tc].Power / 4)
                    {
                        if (!msgbox)
                        {
                            if (Reserve[i + 1].Power / 4 == 0)
                            {
                                player.Type.Current = 1;
                                player.Type.Power = 13 * 4 + Reserve[i].Power / 4 + player.Type.Current * 100;
                                Win.Add(new Type() { Power = player.Type.Power, Current = 1 });
                                sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                            else
                            {
                                player.Type.Current = 1;
                                player.Type.Power = (Reserve[i + 1].Power / 4) * 4 + Reserve[i].Power / 4 + player.Type.Current * 100;
                                Win.Add(new Type() { Power = player.Type.Power, Current = 1 });
                                sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                        }
                        msgbox = true;
                    }
                    if (Reserve[i].Power / 4 == Reserve[tc].Power / 4)
                    {
                        if (!msgbox)
                        {
                            if (Reserve[i].Power / 4 == 0)
                            {
                                player.Type.Current = 1;
                                player.Type.Power = 13 * 4 + Reserve[i + 1].Power / 4 + player.Type.Current * 100;
                                Win.Add(new Type() { Power = player.Type.Power, Current = 1 });
                                sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                            else
                            {
                                player.Type.Current = 1;
                                player.Type.Power = (Reserve[tc].Power / 4) * 4 + Reserve[i + 1].Power / 4 + player.Type.Current * 100;
                                Win.Add(new Type() { Power = player.Type.Power, Current = 1 });
                                sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                        }
                        msgbox = true;
                    }
                }
            }
        }

        public void CheckHighCard(IPlayer player, ref List<Type> Win, ref Type sorted, IList<Card> Reserve, int i)
        {
            if (player.Type.Current == -1)
            {
                if (Reserve[i].Power / 4 > Reserve[i + 1].Power / 4)
                {
                    player.Type.Current = -1;
                    player.Type.Power = Reserve[i].Power / 4;
                    Win.Add(new Type() { Power = player.Type.Power, Current = -1 });
                    sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }
                else
                {
                    player.Type.Current = -1;
                    player.Type.Power = Reserve[i + 1].Power / 4;
                    Win.Add(new Type() { Power = player.Type.Power, Current = -1 });
                    sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }
                if (Reserve[i].Power / 4 == 0 || Reserve[i + 1].Power / 4 == 0)
                {
                    player.Type.Current = -1;
                    player.Type.Power = 13;
                    Win.Add(new Type() { Power = player.Type.Power, Current = -1 });
                    sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }
            }
        }
    }
}
