using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Poker
{
    public class PlayerActions
    {
        public void Fold(ref bool sTurn, ref bool sFTurn, Label sStatus,ref bool raising)
        {
            raising = false;
            sStatus.Text = "Fold";
            sTurn = false;
            sFTurn = true;
        }

        public void Call(ref int sChips, ref bool sTurn, Label sStatus, ref bool raising,ref int call, TextBox textboxPot)
        {
            raising = false;
            sTurn = false;
            sChips -= call;
            sStatus.Text = "Call " + call;
            textboxPot.Text = (int.Parse(textboxPot.Text) + call).ToString();
        }

        public void Raised(ref int sChips, ref bool sTurn, Label sStatus, ref bool raising, ref double Raise, ref int call, TextBox textboxPot)
        {
            sChips -= Convert.ToInt32(Raise);
            sStatus.Text = "Raise " + Raise;
            textboxPot.Text = (int.Parse(textboxPot.Text) + Convert.ToInt32(Raise)).ToString();
            call = Convert.ToInt32(Raise);
            raising = true;
            sTurn = false;
        }

        public void Check(ref bool cTurn, Label cStatus, ref bool raising)
        {
            cStatus.Text = "Check";
            cTurn = false;
            raising = false;
        }
    }
}
