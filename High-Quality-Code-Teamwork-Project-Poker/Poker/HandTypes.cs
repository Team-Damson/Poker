using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Poker
{
    public class HandTypes
    {
        Dunno type = new Dunno();

        public void HighCard(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower, int call, TextBox textboxPot, ref double Raise, ref bool raising)
        {
            type.HP(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower, 20, 25, call, textboxPot, ref Raise, ref raising);
        }
        public void PairTable(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower, int call, TextBox textboxPot, ref double Raise, ref bool raising)
        {
            type.HP(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower, 16, 25, call, textboxPot, ref Raise, ref raising);
        }
        public void PairHand(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower, int call, TextBox textboxPot, ref double Raise, ref bool raising, double rounds)
        {
            Random rPair = new Random();
            int rCall = rPair.Next(10, 16);
            int rRaise = rPair.Next(10, 13);
            if (botPower <= 199 && botPower >= 140)
            {
                type.PH(ref sChips, ref sTurn, ref sFTurn, sStatus, rCall, 6, rRaise, call, textboxPot, ref Raise, ref raising, rounds);
            }
            if (botPower <= 139 && botPower >= 128)
            {
                type.PH(ref sChips, ref sTurn, ref sFTurn, sStatus, rCall, 7, rRaise, call, textboxPot, ref Raise, ref raising, rounds);
            }
            if (botPower < 128 && botPower >= 101)
            {
                type.PH(ref sChips, ref sTurn, ref sFTurn, sStatus, rCall, 9, rRaise, call, textboxPot, ref Raise, ref raising, rounds);
            }
        }
        public void TwoPair(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower, int call, TextBox textboxPot, ref double Raise, ref bool raising, double rounds)
        {
            Random rPair = new Random();
            int rCall = rPair.Next(6, 11);
            int rRaise = rPair.Next(6, 11);
            if (botPower <= 290 && botPower >= 246)
            {
                type.PH(ref sChips, ref sTurn, ref sFTurn, sStatus, rCall, 3, rRaise, call, textboxPot, ref Raise, ref raising, rounds);
            }
            if (botPower <= 244 && botPower >= 234)
            {
                type.PH(ref sChips, ref sTurn, ref sFTurn, sStatus, rCall, 4, rRaise, call, textboxPot, ref Raise, ref raising, rounds);
            }
            if (botPower < 234 && botPower >= 201)
            {
                type.PH(ref sChips, ref sTurn, ref sFTurn, sStatus, rCall, 4, rRaise, call, textboxPot, ref Raise, ref raising, rounds);
            }
        }
        public void ThreeOfAKind(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower, int call, TextBox textboxPot, ref double Raise, ref bool raising, ref double rounds)
        {
            Random tk = new Random();
            int tCall = tk.Next(3, 7);
            int tRaise = tk.Next(4, 8);
            if (botPower <= 390 && botPower >= 330)
            {
                type.Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, tCall, tRaise, call, textboxPot, ref Raise, ref raising, ref rounds);
            }
            if (botPower <= 327 && botPower >= 321)//10  8
            {
                type.Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, tCall, tRaise, call, textboxPot, ref Raise, ref raising, ref rounds);
            }
            if (botPower < 321 && botPower >= 303)//7 2
            {
                type.Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, tCall, tRaise, call, textboxPot, ref Raise, ref raising, ref rounds);
            }
        }
        public void Straight(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower, int call, TextBox textboxPot, ref double Raise, ref bool raising, ref double rounds)
        {
            Random str = new Random();
            int sCall = str.Next(3, 6);
            int sRaise = str.Next(3, 8);
            if (botPower <= 480 && botPower >= 410)
            {
                type.Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, sCall, sRaise, call, textboxPot, ref Raise, ref raising, ref rounds);
            }
            if (botPower <= 409 && botPower >= 407)//10  8
            {
                type.Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, sCall, sRaise, call, textboxPot, ref Raise, ref raising, ref rounds);
            }
            if (botPower < 407 && botPower >= 404)
            {
                type.Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, sCall, sRaise, call, textboxPot, ref Raise, ref raising, ref rounds);
            }
        }
        public void Flush(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower, int call, TextBox textboxPot, ref double Raise, ref bool raising, ref double rounds)
        {
            Random fsh = new Random();
            int fCall = fsh.Next(2, 6);
            int fRaise = fsh.Next(3, 7);
            type.Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, fCall, fRaise, call, textboxPot, ref Raise, ref raising, ref rounds);
        }
        public void FullHouse(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower, int call, TextBox textboxPot, ref double Raise, ref bool raising, ref double rounds)
        {
            Random flh = new Random();
            int fhCall = flh.Next(1, 5);
            int fhRaise = flh.Next(2, 6);
            if (botPower <= 626 && botPower >= 620)
            {
                type.Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, fhCall, fhRaise, call, textboxPot, ref Raise, ref raising, ref rounds);
            }
            if (botPower < 620 && botPower >= 602)
            {
                type.Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, fhCall, fhRaise, call, textboxPot, ref Raise, ref raising, ref rounds);
            }
        }
        public void FourOfAKind(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower, int call, TextBox textboxPot, ref double Raise, ref bool raising, ref double rounds)
        {
            Random fk = new Random();
            int fkCall = fk.Next(1, 4);
            int fkRaise = fk.Next(2, 5);
            if (botPower <= 752 && botPower >= 704)
            {
                type.Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, fkCall, fkRaise, call, textboxPot, ref Raise, ref raising, ref rounds);
            }
        }
        public void StraightFlush(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower, int call, TextBox textboxPot, ref double Raise, ref bool raising, ref double rounds)
        {
            Random sf = new Random();
            int sfCall = sf.Next(1, 3);
            int sfRaise = sf.Next(1, 3);
            if (botPower <= 913 && botPower >= 804)
            {
                type.Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, sfCall, sfRaise, call, textboxPot, ref Raise, ref raising, ref rounds);
            }
        }
    }
}
