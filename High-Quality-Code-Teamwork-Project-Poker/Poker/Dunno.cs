using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Poker
{
    public class Dunno
    {
        PlayerActions playerActions = new PlayerActions();

        private static double RoundN(int sChips, int n)
        {
            double a = Math.Round((sChips / n) / 100d, 0) * 100;
            return a;
        }

        public void HP(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower, int n, int n1, int call,TextBox textboxPot, ref double Raise, ref bool raising)
        {
           
            Random rand = new Random();
            int rnd = rand.Next(1, 4);
            if (call <= 0)
            {
                playerActions.Check(ref sTurn, sStatus, ref raising);
            }
            if (call > 0)
            {
                if (rnd == 1)
                {
                    if (call <= RoundN(sChips, n))
                    {
                        playerActions.Call(ref sChips, ref sTurn, sStatus, ref raising, ref call, textboxPot);
                    }
                    else
                    {
                        playerActions.Fold(ref sTurn, ref sFTurn, sStatus, ref raising);
                    }
                }
                if (rnd == 2)
                {
                    if (call <= RoundN(sChips, n1))
                    {
                        playerActions.Call(ref sChips, ref sTurn, sStatus, ref raising, ref call, textboxPot);
                    }
                    else
                    {
                        playerActions.Fold(ref sTurn, ref sFTurn, sStatus, ref raising);
                    }
                }
            }
            if (rnd == 3)
            {
                if (Raise == 0)
                {
                    Raise = call * 2;
                    playerActions.Raised(ref sChips, ref sTurn, sStatus, ref raising, ref Raise, ref call, textboxPot);
                }
                else
                {
                    if (Raise <= RoundN(sChips, n))
                    {
                        Raise = call * 2;
                        playerActions.Raised(ref sChips, ref sTurn, sStatus, ref raising, ref Raise, ref call, textboxPot);
                    }
                    else
                    {
                        playerActions.Fold(ref sTurn, ref sFTurn, sStatus, ref raising);
                    }
                }
            }
            if (sChips <= 0)
            {
                sFTurn = true;
            }
        }
        public void PH(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int n, int n1, int r, int call, TextBox textboxPot, ref double Raise, ref bool raising,double rounds)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 3);
            if (rounds < 2)
            {
                if (call <= 0)
                {
                    playerActions.Check(ref sTurn, sStatus, ref raising);
                }
                if (call > 0)
                {
                    if (call >= RoundN(sChips, n1))
                    {

                        playerActions.Fold(ref sTurn, ref sFTurn, sStatus, ref raising);
                    }
                    if (Raise > RoundN(sChips, n))
                    {
                        playerActions.Fold(ref sTurn, ref sFTurn, sStatus, ref raising);
                    }
                    if (!sFTurn)
                    {
                        if (call >= RoundN(sChips, n) && call <= RoundN(sChips, n1))
                        {
                            playerActions.Call(ref sChips, ref sTurn, sStatus, ref raising, ref call, textboxPot);
                        }
                        if (Raise <= RoundN(sChips, n) && Raise >= (RoundN(sChips, n)) / 2)
                        {
                            playerActions.Call(ref sChips, ref sTurn, sStatus, ref raising, ref call, textboxPot);
                        }
                        if (Raise <= (RoundN(sChips, n)) / 2)
                        {
                            if (Raise > 0)
                            {
                                Raise = RoundN(sChips, n);
                                playerActions.Call(ref sChips, ref sTurn, sStatus, ref raising, ref call, textboxPot);
                            }
                            else
                            {
                                Raise = call * 2;
                                playerActions.Call(ref sChips, ref sTurn, sStatus, ref raising, ref call, textboxPot);
                            }
                        }

                    }
                }
            }
            if (rounds >= 2)
            {
                if (call > 0)
                {
                    if (call >= RoundN(sChips, n1 - rnd))
                    {
                        playerActions.Fold(ref sTurn, ref sFTurn, sStatus, ref raising);
                    }
                    if (Raise > RoundN(sChips, n - rnd))
                    {
                        playerActions.Fold(ref sTurn, ref sFTurn, sStatus, ref raising);
                    }
                    if (!sFTurn)
                    {
                        if (call >= RoundN(sChips, n - rnd) && call <= RoundN(sChips, n1 - rnd))
                        {
                            playerActions.Call(ref sChips, ref sTurn, sStatus, ref raising, ref call, textboxPot);
                        }
                        if (Raise <= RoundN(sChips, n - rnd) && Raise >= (RoundN(sChips, n - rnd)) / 2)
                        {
                            playerActions.Call(ref sChips, ref sTurn, sStatus, ref raising, ref call, textboxPot);
                        }
                        if (Raise <= (RoundN(sChips, n - rnd)) / 2)
                        {
                            if (Raise > 0)
                            {
                                Raise = RoundN(sChips, n - rnd);
                                playerActions.Raised(ref sChips, ref sTurn, sStatus, ref raising, ref Raise, ref call, textboxPot);
                            }
                            else
                            {
                                Raise = call * 2;
                                playerActions.Raised(ref sChips, ref sTurn, sStatus, ref raising, ref Raise, ref call, textboxPot);
                            }
                        }
                    }
                }
                if (call <= 0)
                {
                    Raise = RoundN(sChips, r - rnd);
                    playerActions.Raised(ref sChips, ref sTurn, sStatus, ref raising, ref Raise, ref call, textboxPot);
                }
            }
            if (sChips <= 0)
            {
                sFTurn = true;
            }
        }
        public void Smooth(ref int botChips, ref bool botTurn, ref bool botFTurn, Label botStatus, int name, int n, int r, int call, TextBox textboxPot, ref double Raise, ref bool raising, ref double rounds)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 3);
            if (call <= 0)
            {
                playerActions.Check(ref botTurn, botStatus, ref raising);
            }
            else
            {
                if (call >= RoundN(botChips, n))
                {
                    if (botChips > call)
                    {
                        playerActions.Call(ref botChips, ref botTurn, botStatus, ref raising, ref call, textboxPot);
                    }
                    else if (botChips <= call)
                    {
                        raising = false;
                        botTurn = false;
                        botChips = 0;
                        botStatus.Text = "Call " + botChips;
                        textboxPot.Text = (int.Parse(textboxPot.Text) + botChips).ToString();
                    }
                }
                else
                {
                    if (Raise > 0)
                    {
                        if (botChips >= Raise * 2)
                        {
                            Raise *= 2;
                            playerActions.Raised(ref botChips, ref botTurn, botStatus, ref raising, ref Raise, ref call, textboxPot);

                        }
                        else
                        {
                            playerActions.Call(ref botChips, ref botTurn, botStatus, ref raising, ref call, textboxPot);
                        }
                    }
                    else
                    {
                        Raise = call * 2;
                        playerActions.Raised(ref botChips, ref botTurn, botStatus, ref raising, ref Raise, ref call, textboxPot);

                    }
                }
            }
            if (botChips <= 0)
            {
                botFTurn = true;
            }
        }
    }
}
