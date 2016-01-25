namespace Poker
{
    using System;
    using Poker.Enums;
    using Poker.Interfaces;
    
    public class Dunno
    {
        private readonly PlayerActions playerActions = new PlayerActions();
        private readonly Random random = new Random();

        private static double RoundN(int sChips, int n)
        {
            double a = Math.Round((sChips / n) / 100d, 0) * 100;
            return a;
        }

        public void HP(IPlayer player, int n, int n1, int call, IPot pot, ref double raise, ref bool raising)
        {
            int randomInteger = this.random.Next(1, 4);

            if (call <= 0)
            {
                this.playerActions.Check(player, ref raising);
            }

            if (call > 0)
            {
                if (randomInteger == 1)
                {
                    if (call <= RoundN(player.Chips, n))
                    {
                        this.playerActions.Call(player, ref raising, call, pot);
                    }
                    else
                    {
                        this.playerActions.Fold(player, ref raising);
                    }
                }

                if (randomInteger == 2)
                {
                    if (call <= RoundN(player.Chips, n1))
                    {
                        this.playerActions.Call(player, ref raising, call, pot);
                    }
                    else
                    {
                        this.playerActions.Fold(player, ref raising);
                    }
                }
            }

            if (randomInteger == 3)
            {
                if (raise == 0)
                {
                    raise = call * 2;
                    this.playerActions.Raise(player, ref raising, ref raise, ref call, pot);
                }
                else
                {
                    if (raise <= RoundN(player.Chips, n))
                    {
                        raise = call * 2;
                        this.playerActions.Raise(player, ref raising, ref raise, ref call, pot);
                    }
                    else
                    {
                        this.playerActions.Fold(player, ref raising);
                    }
                }
            }

            if (player.Chips <= 0)
            {
                player.FoldedTurn = true;
            }
        }

        public void PH(IPlayer player, int n, int n1, int r, int call, IPot pot, ref double raise, ref bool raising, CommunityCardBoard rounds)
        {
            int rnd = this.random.Next(1, 3);

            if (rounds < CommunityCardBoard.Turn)
            {
                if (call <= 0)
                {
                    this.playerActions.Check(player, ref raising);
                }

                if (call > 0)
                {
                    if (call >= RoundN(player.Chips, n1))
                    {
                        this.playerActions.Fold(player, ref raising);
                    }

                    if (raise > RoundN(player.Chips, n))
                    {
                        this.playerActions.Fold(player, ref raising);
                    }

                    if (!player.FoldedTurn)
                    {
                        if (call >= RoundN(player.Chips, n) && call <= RoundN(player.Chips, n1))
                        {
                            this.playerActions.Call(player, ref raising, call, pot);
                        }

                        if (raise <= RoundN(player.Chips, n) && raise >= RoundN(player.Chips, n) / 2)
                        {
                            this.playerActions.Call(player, ref raising, call, pot);
                        }

                        if (raise <= RoundN(player.Chips, n) / 2)
                        {
                            if (raise > 0)
                            {
                                raise = RoundN(player.Chips, n);
                                this.playerActions.Call(player, ref raising, call, pot);
                            }
                            else
                            {
                                raise = call * 2;
                                this.playerActions.Call(player, ref raising, call, pot);
                            }
                        }
                    }
                }
            }

            if (rounds >= CommunityCardBoard.Turn)
            {
                if (call > 0)
                {
                    if (call >= RoundN(player.Chips, n1 - rnd))
                    {
                        this.playerActions.Fold(player, ref raising);
                    }

                    if (raise > RoundN(player.Chips, n - rnd))
                    {
                        this.playerActions.Fold(player, ref raising);
                    }

                    if (!player.FoldedTurn)
                    {
                        if (call >= RoundN(player.Chips, n - rnd) && call <= RoundN(player.Chips, n1 - rnd))
                        {
                            this.playerActions.Call(player, ref raising, call, pot);
                        }

                        if (raise <= RoundN(player.Chips, n - rnd) && raise >= RoundN(player.Chips, n - rnd) / 2)
                        {
                            this.playerActions.Call(player, ref raising, call, pot);
                        }

                        if (raise <= RoundN(player.Chips, n - rnd) / 2)
                        {
                            if (raise > 0)
                            {
                                raise = RoundN(player.Chips, n - rnd);
                                this.playerActions.Raise(player, ref raising, ref raise, ref call, pot);
                            }
                            else
                            {
                                raise = call * 2;
                                this.playerActions.Raise(player, ref raising, ref raise, ref call, pot);
                            }
                        }
                    }
                }

                if (call <= 0)
                {
                    raise = RoundN(player.Chips, r - rnd);
                    this.playerActions.Raise(player, ref raising, ref raise, ref call, pot);
                }
            }

            if (player.Chips <= 0)
            {
                player.FoldedTurn = true;
            }
        }

        public void Smooth(IPlayer player, int n, int r, int call, IPot pot, ref double Raise, ref bool raising)
        {
            if (call <= 0)
            {
                this.playerActions.Check(player, ref raising);
            }
            else
            {
                if (call >= RoundN(player.Chips, n))
                {
                    if (player.Chips > call)
                    {
                        this.playerActions.Call(player, ref raising, call, pot);
                    }
                    else if (player.Chips <= call)
                    {
                        raising = false;
                        player.IsInTurn = false;
                        player.Chips = 0;
                        player.StatusLabel.Text = "Call " + player.Chips;
                        pot.Add(player.Chips);
                        //textboxPot.Text = (int.Parse(textboxPot.Text) + player.Chips).ToString();
                    }
                }
                else
                {
                    if (Raise > 0)
                    {
                        if (player.Chips >= Raise * 2)
                        {
                            Raise *= 2;
                            this.playerActions.Raise(player, ref raising, ref Raise, ref call, pot);
                        }
                        else
                        {
                            this.playerActions.Call(player, ref raising, call, pot);
                        }
                    }
                    else
                    {
                        Raise = call * 2;
                        this.playerActions.Raise(player, ref raising, ref Raise, ref call, pot);
                    }
                }
            }

            if (player.Chips <= 0)
            {
                player.FoldedTurn = true;
            }
        }
    }
}
