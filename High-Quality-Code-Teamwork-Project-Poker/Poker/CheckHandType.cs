namespace Poker
{
    using System.Collections.Generic;
    using System.Linq;
    using Poker.Interfaces;
    using Poker.Models;

    public class CheckHandType
    {
        //public CheckHandType()
        //{
        //}

        public void CheckStraightFlush(IPlayer player, int[] spades, int[] hearts, int[] diamonds, int[] clubs, ref List<Type> win, ref Type sorted)
        {
            if (player.Type.Current >= -1)
            {
                if (spades.Length >= 5)
                {
                    if (spades[0] + 4 == spades[4])
                    {
                        player.Type.Current = 8;
                        player.Type.Power = spades.Max() / 4 + player.Type.Current * 100;

                        win.Add(new Type()
                                    {
                                        Power = player.Type.Power,
                                        Current = player.Type.Current
                                    });

                        sorted = win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }

                    if (spades[0] == 0 && spades[1] == 9 && spades[2] == 10 && spades[3] == 11 && spades[0] + 12 == spades[4])
                    {
                        player.Type.Current = 9;
                        player.Type.Power = spades.Max() / 4 + player.Type.Current * 100;

                        win.Add(new Type()
                                    {
                                        Power = player.Type.Power,
                                        Current = player.Type.Current
                                    });

                        sorted = win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }

                if (hearts.Length >= 5)
                {
                    if (hearts[0] + 4 == hearts[4])
                    {
                        player.Type.Current = 8;
                        player.Type.Power = hearts.Max() / 4 + player.Type.Current * 100;

                        win.Add(new Type()
                                    {
                                        Power = player.Type.Power,
                                        Current = player.Type.Current
                                    });

                        sorted = win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }

                    if (hearts[0] == 0 && hearts[1] == 9 && hearts[2] == 10 && hearts[3] == 11 && hearts[0] + 12 == hearts[4])
                    {
                        player.Type.Current = 9;
                        player.Type.Power = (hearts.Max()) / 4 + player.Type.Current * 100;

                        win.Add(new Type()
                                    {
                                        Power = player.Type.Power,
                                        Current = player.Type.Current
                                    });

                        sorted = win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }

                if (diamonds.Length >= 5)
                {
                    if (diamonds[0] + 4 == diamonds[4])
                    {
                        player.Type.Current = 8;
                        player.Type.Power = (diamonds.Max()) / 4 + player.Type.Current * 100;

                        win.Add(new Type()
                                    {
                                        Power = player.Type.Power,
                                        Current = player.Type.Current
                                    });

                        sorted = win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }

                    if (diamonds[0] == 0 && diamonds[1] == 9 && diamonds[2] == 10 && diamonds[3] == 11 && diamonds[0] + 12 == diamonds[4])
                    {
                        player.Type.Current = 9;
                        player.Type.Power = (diamonds.Max()) / 4 + player.Type.Current * 100;

                        win.Add(new Type()
                                    {
                                        Power = player.Type.Power,
                                        Current = player.Type.Current
                                    });

                        sorted = win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }

                if (clubs.Length >= 5)
                {
                    if (clubs[0] + 4 == clubs[4])
                    {
                        player.Type.Current = 8;
                        player.Type.Power = (clubs.Max()) / 4 + player.Type.Current * 100;

                        win.Add(new Type()
                                    {
                                        Power = player.Type.Power,
                                        Current = player.Type.Current
                                    });

                        sorted = win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }

                    if (clubs[0] == 0 && clubs[1] == 9 && clubs[2] == 10 && clubs[3] == 11 && clubs[0] + 12 == clubs[4])
                    {
                        player.Type.Current = 9;
                        player.Type.Power = (clubs.Max()) / 4 + player.Type.Current * 100;

                        win.Add(new Type()
                                    {
                                        Power = player.Type.Power,
                                        Current = player.Type.Current
                                    });

                        sorted = win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }
            }
        }

        public void CheckFourOfAKind(IPlayer player, int[] straight, ref List<Type> win, ref Type sorted)
        {
            if (player.Type.Current >= -1)
            {
                for (int i = 0; i <= 3; i++)
                {
                    if (straight[i] / 4 == straight[i + 1] / 4 && straight[i] / 4 == straight[i + 2] / 4 &&
                        straight[i] / 4 == straight[i + 3] / 4)
                    {
                        player.Type.Current = 7;
                        player.Type.Power = (straight[i] / 4) * 4 + player.Type.Current * 100;

                        win.Add(new Type()
                                    {
                                        Power = player.Type.Power,
                                        Current = player.Type.Current
                                    });

                        sorted = win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }

                    if (straight[i] / 4 == 0 && straight[i + 1] / 4 == 0 && straight[i + 2] / 4 == 0 && straight[i + 3] / 4 == 0)
                    {
                        player.Type.Current = 7;
                        player.Type.Power = 13 * 4 + player.Type.Current * 100;

                        win.Add(new Type()
                                    {
                                        Power = player.Type.Power,
                                        Current = player.Type.Current
                                    });

                        sorted = win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }
            }
        }

        public void CheckFullHouse(IPlayer player, ref bool done, int[] straight, ref List<Type> win, ref Type sorted)
        {
            if (player.Type.Current >= -1)
            {
                double type = player.Type.Power;

                for (int i = 0; i <= 12; i++)
                {
                    var fh = straight.Where(o => o / 4 == i).ToArray();
                    if (fh.Length == 3 || done)
                    {
                        if (fh.Length == 2)
                        {
                            if (fh.Max() / 4 == 0)
                            {
                                player.Type.Current = 6;
                                player.Type.Power = 13 * 2 + player.Type.Current * 100;
                                win.Add(new Type()
                                            {
                                                Power = player.Type.Power,
                                                Current = player.Type.Current
                                            });

                                sorted = win
                                    .OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                                break;
                            }

                            if (fh.Max() / 4 > 0)
                            {
                                player.Type.Current = 6;
                                player.Type.Power = fh.Max() / 4 * 2 + player.Type.Current * 100;
                                win.Add(new Type()
                                            {
                                                Power = player.Type.Power,
                                                Current = player.Type.Current
                                            });

                                sorted = win
                                    .OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                                break;
                            }
                        }

                        if (!done)
                        {
                            if (fh.Max() / 4 == 0)
                            {
                                player.Type.Power = 13;
                                done = true;
                                i = -1;
                            }
                            else
                            {
                                player.Type.Power = fh.Max() / 4;
                                done = true;
                                i = -1;
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

        public void CheckFlush(IPlayer player, ref bool vf, int[] straight, ref List<Type> win, ref Type sorted, IList<Card> reserve, int i)
        {
            if (player.Type.Current >= -1)
            {
                var f1 = straight.Where(o => o % 4 == 0).ToArray();
                var f2 = straight.Where(o => o % 4 == 1).ToArray();
                var f3 = straight.Where(o => o % 4 == 2).ToArray();
                var f4 = straight.Where(o => o % 4 == 3).ToArray();

                if (f1.Length == 3 || f1.Length == 4)
                {
                    if (reserve[i].Power % 4 == reserve[i + 1].Power % 4 && reserve[i].Power % 4 == f1[0] % 4)
                    {
                        if (reserve[i].Power / 4 > f1.Max() / 4)
                        {
                            player.Type.Current = 5;
                            player.Type.Power = reserve[i].Power + player.Type.Current * 100;
                            win.Add(new Type()
                                        {
                                            Power = player.Type.Power,
                                            Current = player.Type.Current
                                        });
                            sorted = win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();
                            vf = true;
                        }

                        if (reserve[i + 1].Power / 4 > f1.Max() / 4)
                        {
                            player.Type.Current = 5;
                            player.Type.Power = reserve[i + 1].Power + player.Type.Current * 100;
                            win.Add(new Type()
                                        {
                                            Power = player.Type.Power,
                                            Current = player.Type.Current
                                        });

                            sorted = win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();
                            vf = true;
                        }
                        else if (reserve[i].Power / 4 < f1.Max() / 4 && reserve[i + 1].Power / 4 < f1.Max() / 4)
                        {
                            player.Type.Current = 5;
                            player.Type.Power = f1.Max() + player.Type.Current * 100;
                            win.Add(new Type()
                                        {
                                            Power = player.Type.Power,
                                            Current = player.Type.Current
                                        });
                            sorted = win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();
                            vf = true;
                        }
                    }
                }

                if (f1.Length == 4)//different cards in hand
                {
                    if (reserve[i].Power % 4 != reserve[i + 1].Power % 4 && reserve[i].Power % 4 == f1[0] % 4)
                    {
                        if (reserve[i].Power / 4 > f1.Max() / 4)
                        {
                            player.Type.Current = 5;
                            player.Type.Power = reserve[i].Power + player.Type.Current * 100;
                            win.Add(new Type()
                                        {
                                            Power = player.Type.Power,
                                            Current = player.Type.Current
                                        });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            player.Type.Current = 5;
                            player.Type.Power = f1.Max() + player.Type.Current * 100;
                            win.Add(new Type()
                                        {
                                            Power = player.Type.Power,
                                            Current = player.Type.Current
                                        });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }

                    if (reserve[i + 1].Power % 4 != reserve[i].Power % 4 && reserve[i + 1].Power % 4 == f1[0] % 4)
                    {
                        if (reserve[i + 1].Power / 4 > f1.Max() / 4)
                        {
                            player.Type.Current = 5;
                            player.Type.Power = reserve[i + 1].Power + player.Type.Current * 100;
                            win.Add(new Type()
                                        {
                                            Power = player.Type.Power,
                                            Current = player.Type.Current
                                        });

                            sorted = win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();
                            vf = true;
                        }
                        else
                        {
                            player.Type.Current = 5;
                            player.Type.Power = f1.Max() + player.Type.Current * 100;
                            win.Add(new Type()
                                        {
                                            Power = player.Type.Power,
                                            Current = 5
                                        });

                            sorted = win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();
                            vf = true;
                        }
                    }
                }

                if (f1.Length == 5)
                {
                    if (reserve[i].Power % 4 == f1[0] % 4 && reserve[i].Power / 4 > f1.Min() / 4)
                    {
                        player.Type.Current = 5;
                        player.Type.Power = reserve[i].Power + player.Type.Current * 100;
                        win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }

                    if (reserve[i + 1].Power % 4 == f1[0] % 4 && reserve[i + 1].Power / 4 > f1.Min() / 4)
                    {
                        player.Type.Current = 5;
                        player.Type.Power = reserve[i + 1].Power + player.Type.Current * 100;
                        win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (reserve[i].Power / 4 < f1.Min() / 4 && reserve[i + 1].Power / 4 < f1.Min())
                    {
                        player.Type.Current = 5;
                        player.Type.Power = f1.Max() + player.Type.Current * 100;
                        win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (f2.Length == 3 || f2.Length == 4)
                {
                    if (reserve[i].Power % 4 == reserve[i + 1].Power % 4 && reserve[i].Power % 4 == f2[0] % 4)
                    {
                        if (reserve[i].Power / 4 > f2.Max() / 4)
                        {
                            player.Type.Current = 5;
                            player.Type.Power = reserve[i].Power + player.Type.Current * 100;
                            win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }

                        if (reserve[i + 1].Power / 4 > f2.Max() / 4)
                        {
                            player.Type.Current = 5;
                            player.Type.Power = reserve[i + 1].Power + player.Type.Current * 100;
                            win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (reserve[i].Power / 4 < f2.Max() / 4 && reserve[i + 1].Power / 4 < f2.Max() / 4)
                        {
                            player.Type.Current = 5;
                            player.Type.Power = f2.Max() + player.Type.Current * 100;
                            win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }

                if (f2.Length == 4)//different cards in hand
                {
                    if (reserve[i].Power % 4 != reserve[i + 1].Power % 4 && reserve[i].Power % 4 == f2[0] % 4)
                    {
                        if (reserve[i].Power / 4 > f2.Max() / 4)
                        {
                            player.Type.Current = 5;
                            player.Type.Power = reserve[i].Power + player.Type.Current * 100;
                            win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            player.Type.Current = 5;
                            player.Type.Power = f2.Max() + player.Type.Current * 100;
                            win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }

                    if (reserve[i + 1].Power % 4 != reserve[i].Power % 4 && reserve[i + 1].Power % 4 == f2[0] % 4)
                    {
                        if (reserve[i + 1].Power / 4 > f2.Max() / 4)
                        {
                            player.Type.Current = 5;
                            player.Type.Power = reserve[i + 1].Power + player.Type.Current * 100;
                            win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            player.Type.Current = 5;
                            player.Type.Power = f2.Max() + player.Type.Current * 100;
                            win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }

                if (f2.Length == 5)
                {
                    if (reserve[i].Power % 4 == f2[0] % 4 && reserve[i].Power / 4 > f2.Min() / 4)
                    {
                        player.Type.Current = 5;
                        player.Type.Power = reserve[i].Power + player.Type.Current * 100;
                        win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }

                    if (reserve[i + 1].Power % 4 == f2[0] % 4 && reserve[i + 1].Power / 4 > f2.Min() / 4)
                    {
                        player.Type.Current = 5;
                        player.Type.Power = reserve[i + 1].Power + player.Type.Current * 100;
                        win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (reserve[i].Power / 4 < f2.Min() / 4 && reserve[i + 1].Power / 4 < f2.Min())
                    {
                        player.Type.Current = 5;
                        player.Type.Power = f2.Max() + player.Type.Current * 100;
                        win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (f3.Length == 3 || f3.Length == 4)
                {
                    if (reserve[i].Power % 4 == reserve[i + 1].Power % 4 && reserve[i].Power % 4 == f3[0] % 4)
                    {
                        if (reserve[i].Power / 4 > f3.Max() / 4)
                        {
                            player.Type.Current = 5;
                            player.Type.Power = reserve[i].Power + player.Type.Current * 100;
                            win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }

                        if (reserve[i + 1].Power / 4 > f3.Max() / 4)
                        {
                            player.Type.Current = 5;
                            player.Type.Power = reserve[i + 1].Power + player.Type.Current * 100;
                            win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (reserve[i].Power / 4 < f3.Max() / 4 && reserve[i + 1].Power / 4 < f3.Max() / 4)
                        {
                            player.Type.Current = 5;
                            player.Type.Power = f3.Max() + player.Type.Current * 100;
                            win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }

                if (f3.Length == 4)//different cards in hand
                {
                    if (reserve[i].Power % 4 != reserve[i + 1].Power % 4 && reserve[i].Power % 4 == f3[0] % 4)
                    {
                        if (reserve[i].Power / 4 > f3.Max() / 4)
                        {
                            player.Type.Current = 5;
                            player.Type.Power = reserve[i].Power + player.Type.Current * 100;
                            win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            player.Type.Current = 5;
                            player.Type.Power = f3.Max() + player.Type.Current * 100;
                            win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }

                    if (reserve[i + 1].Power % 4 != reserve[i].Power % 4 && reserve[i + 1].Power % 4 == f3[0] % 4)
                    {
                        if (reserve[i + 1].Power / 4 > f3.Max() / 4)
                        {
                            player.Type.Current = 5;
                            player.Type.Power = reserve[i + 1].Power + player.Type.Current * 100;
                            win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            player.Type.Current = 5;
                            player.Type.Power = f3.Max() + player.Type.Current * 100;
                            win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }

                if (f3.Length == 5)
                {
                    if (reserve[i].Power % 4 == f3[0] % 4 && reserve[i].Power / 4 > f3.Min() / 4)
                    {
                        player.Type.Current = 5;
                        player.Type.Power = reserve[i].Power + player.Type.Current * 100;
                        win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }

                    if (reserve[i + 1].Power % 4 == f3[0] % 4 && reserve[i + 1].Power / 4 > f3.Min() / 4)
                    {
                        player.Type.Current = 5;
                        player.Type.Power = reserve[i + 1].Power + player.Type.Current * 100;
                        win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (reserve[i].Power / 4 < f3.Min() / 4 && reserve[i + 1].Power / 4 < f3.Min())
                    {
                        player.Type.Current = 5;
                        player.Type.Power = f3.Max() + player.Type.Current * 100;
                        win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (f4.Length == 3 || f4.Length == 4)
                {
                    if (reserve[i].Power % 4 == reserve[i + 1].Power % 4 && reserve[i].Power % 4 == f4[0] % 4)
                    {
                        if (reserve[i].Power / 4 > f4.Max() / 4)
                        {
                            player.Type.Current = 5;
                            player.Type.Power = reserve[i].Power + player.Type.Current * 100;
                            win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }

                        if (reserve[i + 1].Power / 4 > f4.Max() / 4)
                        {
                            player.Type.Current = 5;
                            player.Type.Power = reserve[i + 1].Power + player.Type.Current * 100;
                            win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (reserve[i].Power / 4 < f4.Max() / 4 && reserve[i + 1].Power / 4 < f4.Max() / 4)
                        {
                            player.Type.Current = 5;
                            player.Type.Power = f4.Max() + player.Type.Current * 100;
                            win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }

                if (f4.Length == 4)//different cards in hand
                {
                    if (reserve[i].Power % 4 != reserve[i + 1].Power % 4 && reserve[i].Power % 4 == f4[0] % 4)
                    {
                        if (reserve[i].Power / 4 > f4.Max() / 4)
                        {
                            player.Type.Current = 5;
                            player.Type.Power = reserve[i].Power + player.Type.Current * 100;
                            win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            player.Type.Current = 5;
                            player.Type.Power = f4.Max() + player.Type.Current * 100;
                            win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }

                    if (reserve[i + 1].Power % 4 != reserve[i].Power % 4 && reserve[i + 1].Power % 4 == f4[0] % 4)
                    {
                        if (reserve[i + 1].Power / 4 > f4.Max() / 4)
                        {
                            player.Type.Current = 5;
                            player.Type.Power = reserve[i + 1].Power + player.Type.Current * 100;
                            win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            player.Type.Current = 5;
                            player.Type.Power = f4.Max() + player.Type.Current * 100;
                            win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }

                if (f4.Length == 5)
                {
                    if (reserve[i].Power % 4 == f4[0] % 4 && reserve[i].Power / 4 > f4.Min() / 4)
                    {
                        player.Type.Current = 5;
                        player.Type.Power = reserve[i].Power + player.Type.Current * 100;
                        win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }

                    if (reserve[i + 1].Power % 4 == f4[0] % 4 && reserve[i + 1].Power / 4 > f4.Min() / 4)
                    {
                        player.Type.Current = 5;
                        player.Type.Power = reserve[i + 1].Power + player.Type.Current * 100;
                        win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (reserve[i].Power / 4 < f4.Min() / 4 && reserve[i + 1].Power / 4 < f4.Min())
                    {
                        player.Type.Current = 5;
                        player.Type.Power = f4.Max() + player.Type.Current * 100;
                        win.Add(new Type() { Power = player.Type.Power, Current = 5 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }
                //ace
                if (f1.Length > 0)
                {
                    if (reserve[i].Power / 4 == 0 && reserve[i].Power % 4 == f1[0] % 4 && vf && f1.Length > 0)
                    {
                        player.Type.Current = 5.5;
                        player.Type.Power = 13 + player.Type.Current * 100;
                        win.Add(new Type() { Power = player.Type.Power, Current = 5.5 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (reserve[i + 1].Power / 4 == 0 && reserve[i + 1].Power % 4 == f1[0] % 4 && vf && f1.Length > 0)
                    {
                        player.Type.Current = 5.5;
                        player.Type.Power = 13 + player.Type.Current * 100;
                        win.Add(new Type() { Power = player.Type.Power, Current = 5.5 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }

                if (f2.Length > 0)
                {
                    if (reserve[i].Power / 4 == 0 && reserve[i].Power % 4 == f2[0] % 4 && vf && f2.Length > 0)
                    {
                        player.Type.Current = 5.5;
                        player.Type.Power = 13 + player.Type.Current * 100;
                        win.Add(new Type() { Power = player.Type.Power, Current = 5.5 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (reserve[i + 1].Power / 4 == 0 && reserve[i + 1].Power % 4 == f2[0] % 4 && vf && f2.Length > 0)
                    {
                        player.Type.Current = 5.5;
                        player.Type.Power = 13 + player.Type.Current * 100;
                        win.Add(new Type() { Power = player.Type.Power, Current = 5.5 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }

                if (f3.Length > 0)
                {
                    if (reserve[i].Power / 4 == 0 && reserve[i].Power % 4 == f3[0] % 4 && vf && f3.Length > 0)
                    {
                        player.Type.Current = 5.5;
                        player.Type.Power = 13 + player.Type.Current * 100;
                        win.Add(new Type() { Power = player.Type.Power, Current = 5.5 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (reserve[i + 1].Power / 4 == 0 && reserve[i + 1].Power % 4 == f3[0] % 4 && vf && f3.Length > 0)
                    {
                        player.Type.Current = 5.5;
                        player.Type.Power = 13 + player.Type.Current * 100;
                        win.Add(new Type() { Power = player.Type.Power, Current = 5.5 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }

                if (f4.Length > 0)
                {
                    if (reserve[i].Power / 4 == 0 && reserve[i].Power % 4 == f4[0] % 4 && vf && f4.Length > 0)
                    {
                        player.Type.Current = 5.5;
                        player.Type.Power = 13 + player.Type.Current * 100;
                        win.Add(new Type() { Power = player.Type.Power, Current = 5.5 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (reserve[i + 1].Power / 4 == 0 && reserve[i + 1].Power % 4 == f4[0] % 4 && vf)
                    {
                        player.Type.Current = 5.5;
                        player.Type.Power = 13 + player.Type.Current * 100;
                        win.Add(new Type() { Power = player.Type.Power, Current = 5.5 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
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
