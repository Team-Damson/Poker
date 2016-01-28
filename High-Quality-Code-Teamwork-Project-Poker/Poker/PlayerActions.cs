namespace Poker
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
        }

        public void Call(IPlayer player, ref bool raising, int call, IPot pot)
        {
            raising = false;
            player.Call(call);
            pot.Add(call);
        }

        public void Raise(IPlayer player, ref bool raising, ref double raise, ref int call, IPot pot)
        {
            player.Raise(Convert.ToInt32(raise));
            pot.Add(Convert.ToInt32(raise));
            call = Convert.ToInt32(raise);
            raising = true;
        }

        public void Check(IPlayer player, ref bool raising)
        {
            player.Check();
            raising = false;
        }
    }
}
