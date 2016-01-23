namespace Poker
{
    using System;
    using System.Windows.Forms;
    using Poker.Interfaces;

    public class PlayerActions
    {
        public void Fold(IPlayer player, ref bool raising)
        {
            raising = false;
            player.StatusLabel.Text = "Fold";
            player.IsInTurn = false;
            player.FoldedTurn = true;
        }

        public void Call(IPlayer player, ref bool raising, ref int call, TextBox textboxPot)
        {
            raising = false;
            player.IsInTurn = false;
            player.Chips -= call;
            player.StatusLabel.Text = "Call " + call;
            textboxPot.Text = (int.Parse(textboxPot.Text) + call).ToString();
        }

        public void Raise(IPlayer player, ref bool raising, ref double Raise, ref int call, TextBox textboxPot)
        {
            player.Chips -= Convert.ToInt32(Raise);
            player.StatusLabel.Text = "Raise " + Raise;
            textboxPot.Text = (int.Parse(textboxPot.Text) + Convert.ToInt32(Raise)).ToString();
            call = Convert.ToInt32(Raise);
            raising = true;
            player.IsInTurn = false;
        }

        public void Check(IPlayer player, ref bool raising)
        {
            player.StatusLabel.Text = "Check";
            player.IsInTurn = false;
            raising = false;
        }
    }
}
