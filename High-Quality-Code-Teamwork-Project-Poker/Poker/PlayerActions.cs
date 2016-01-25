﻿namespace Poker
{
    using System;
    using System.Windows.Forms;
    using Poker.Interfaces;
    using Poker.Models;

    public class PlayerActions
    {
        public void Fold(IPlayer player, ref bool raising)
        {
            raising = false;
            player.Fold();
            //player.StatusLabel.Text = "Fold";
            //player.IsInTurn = false;
            //player.FoldedTurn = true;
        }

        public void Call(IPlayer player, ref bool raising, int call, IPot pot)
        {
            raising = false;
            player.Call(call);
            //player.IsInTurn = false;
            //player.Chips -= call;
            //player.StatusLabel.Text = "Call " + call;
            pot.Add(call);
            //textboxPot.Text = (int.Parse(textboxPot.Text) + call).ToString();
        }

        public void Raise(IPlayer player, ref bool raising, ref double raise, ref int call, IPot pot)
        {
            //player.Chips -= Convert.ToInt32(Raise);
            //player.StatusLabel.Text = "Raise " + Raise;
            player.Raise(Convert.ToInt32(raise));
            pot.Add(Convert.ToInt32(raise));
            //textboxPot.Text = (int.Parse(textboxPot.Text) + Convert.ToInt32(RaiseAmount)).ToString();
            call = Convert.ToInt32(raise);
            raising = true;
            //player.IsInTurn = false;
        }

        public void Check(IPlayer player, ref bool raising)
        {
            player.Check();
            //player.StatusLabel.Text = "Check";
            //player.IsInTurn = false;
            raising = false;
        }
    }
}
