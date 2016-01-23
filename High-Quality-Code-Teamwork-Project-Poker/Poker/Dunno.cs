namespace Poker
{
    using System;
    using System.Windows.Forms;
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

        public void HP(IPlayer player, int n, int n1, int call, TextBox textboxPot, ref double Raise, ref bool raising)
        {
            int rnd = this.random.Next(1, 4);

            if (call <= 0)
            {
                this.playerActions.Check(player, ref raising);
            }

            if (call > 0)
            {
                if (rnd == 1)
                {
                    if (call <= RoundN(player.Chips, n))
                    {
                        this.playerActions.Call(player, ref raising, ref call, textboxPot);
                    }
                    else
                    {
                        this.playerActions.Fold(player, ref raising);
                    }
                }

                if (rnd == 2)
                {
                    if (call <= RoundN(player.Chips, n1))
                    {
                        this.playerActions.Call(player, ref raising, ref call, textboxPot);
                    }
                    else
                    {
                        this.playerActions.Fold(player, ref raising);
                    }
                }
            }

            if (rnd == 3)
            {
                if (Raise == 0)
                {
                    Raise = call * 2;
                    this.playerActions.Raise(player, ref raising, ref Raise, ref call, textboxPot);
                }
                else
                {
                    if (Raise <= RoundN(player.Chips, n))
                    {
                        Raise = call * 2;
                        this.playerActions.Raise(player, ref raising, ref Raise, ref call, textboxPot);
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

        public void PH(IPlayer player, int n, int n1, int r, int call, TextBox textboxPot, ref double Raise, ref bool raising, double rounds)
        {
            int rnd = this.random.Next(1, 3);

            if (rounds < 2)
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

                    if (Raise > RoundN(player.Chips, n))
                    {
                        this.playerActions.Fold(player, ref raising);
                    }

                    if (!player.FoldedTurn)
                    {
                        if (call >= RoundN(player.Chips, n) && call <= RoundN(player.Chips, n1))
                        {
                            this.playerActions.Call(player, ref raising, ref call, textboxPot);
                        }

                        if (Raise <= RoundN(player.Chips, n) && Raise >= RoundN(player.Chips, n) / 2)
                        {
                            this.playerActions.Call(player, ref raising, ref call, textboxPot);
                        }

                        if (Raise <= RoundN(player.Chips, n) / 2)
                        {
                            if (Raise > 0)
                            {
                                Raise = RoundN(player.Chips, n);
                                this.playerActions.Call(player, ref raising, ref call, textboxPot);
                            }
                            else
                            {
                                Raise = call * 2;
                                this.playerActions.Call(player, ref raising, ref call, textboxPot);
                            }
                        }
                    }
                }
            }

            if (rounds >= 2)
            {
                if (call > 0)
                {
                    if (call >= RoundN(player.Chips, n1 - rnd))
                    {
                        this.playerActions.Fold(player, ref raising);
                    }

                    if (Raise > RoundN(player.Chips, n - rnd))
                    {
                        this.playerActions.Fold(player, ref raising);
                    }

                    if (!player.FoldedTurn)
                    {
                        if (call >= RoundN(player.Chips, n - rnd) && call <= RoundN(player.Chips, n1 - rnd))
                        {
                            this.playerActions.Call(player, ref raising, ref call, textboxPot);
                        }

                        if (Raise <= RoundN(player.Chips, n - rnd) && Raise >= RoundN(player.Chips, n - rnd) / 2)
                        {
                            this.playerActions.Call(player, ref raising, ref call, textboxPot);
                        }

                        if (Raise <= RoundN(player.Chips, n - rnd) / 2)
                        {
                            if (Raise > 0)
                            {
                                Raise = RoundN(player.Chips, n - rnd);
                                this.playerActions.Raise(player, ref raising, ref Raise, ref call, textboxPot);
                            }
                            else
                            {
                                Raise = call * 2;
                                this.playerActions.Raise(player, ref raising, ref Raise, ref call, textboxPot);
                            }
                        }
                    }
                }

                if (call <= 0)
                {
                    Raise = RoundN(player.Chips, r - rnd);
                    this.playerActions.Raise(player, ref raising, ref Raise, ref call, textboxPot);
                }
            }

            if (player.Chips <= 0)
            {
                player.FoldedTurn = true;
            }
        }

        public void Smooth(IPlayer player, int n, int r, int call, TextBox textboxPot, ref double Raise, ref bool raising, ref double rounds)
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
                        this.playerActions.Call(player, ref raising, ref call, textboxPot);
                    }
                    else if (player.Chips <= call)
                    {
                        raising = false;
                        player.IsInTurn = false;
                        player.Chips = 0;
                        player.StatusLabel.Text = "Call " + player.Chips;
                        textboxPot.Text = (int.Parse(textboxPot.Text) + player.Chips).ToString();
                    }
                }
                else
                {
                    if (Raise > 0)
                    {
                        if (player.Chips >= Raise * 2)
                        {
                            Raise *= 2;
                            this.playerActions.Raise(player, ref raising, ref Raise, ref call, textboxPot);
                        }
                        else
                        {
                            this.playerActions.Call(player, ref raising, ref call, textboxPot);
                        }
                    }
                    else
                    {
                        Raise = call * 2;
                        this.playerActions.Raise(player, ref raising, ref Raise, ref call, textboxPot);
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
