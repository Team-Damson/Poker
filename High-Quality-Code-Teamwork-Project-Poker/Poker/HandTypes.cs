using Poker.Interfaces;
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

        public void HighCard(IPlayer player, int call, TextBox textboxPot, ref double Raise, ref bool raising)
        {
            type.HP(player, 20, 25, call, textboxPot, ref Raise, ref raising);
        }
        public void PairTable(IPlayer player, int call, TextBox textboxPot, ref double Raise, ref bool raising)
        {
            type.HP(player, 16, 25, call, textboxPot, ref Raise, ref raising);
        }
        public void PairHand(IPlayer player, int call, TextBox textboxPot, ref double Raise, ref bool raising, double rounds)
        {
            Random rPair = new Random();
            int rCall = rPair.Next(10, 16);
            int rRaise = rPair.Next(10, 13);
            if (player.Type.Power <= 199 && player.Type.Power >= 140)
            {
                type.PH(player, rCall, 6, rRaise, call, textboxPot, ref Raise, ref raising, rounds);
            }
            if (player.Type.Power <= 139 && player.Type.Power >= 128)
            {
                type.PH(player, rCall, 7, rRaise, call, textboxPot, ref Raise, ref raising, rounds);
            }
            if (player.Type.Power < 128 && player.Type.Power >= 101)
            {
                type.PH(player, rCall, 9, rRaise, call, textboxPot, ref Raise, ref raising, rounds);
            }
        }
        public void TwoPair(IPlayer player, int call, TextBox textboxPot, ref double Raise, ref bool raising, double rounds)
        {
            Random rPair = new Random();
            int rCall = rPair.Next(6, 11);
            int rRaise = rPair.Next(6, 11);
            if (player.Type.Power <= 290 && player.Type.Power >= 246)
            {
                type.PH(player, rCall, 3, rRaise, call, textboxPot, ref Raise, ref raising, rounds);
            }
            if (player.Type.Power <= 244 && player.Type.Power >= 234)
            {
                type.PH(player, rCall, 4, rRaise, call, textboxPot, ref Raise, ref raising, rounds);
            }
            if (player.Type.Power < 234 && player.Type.Power >= 201)
            {
                type.PH(player, rCall, 4, rRaise, call, textboxPot, ref Raise, ref raising, rounds);
            }
        }
        public void ThreeOfAKind(IPlayer player, int call, TextBox textboxPot, ref double Raise, ref bool raising, ref double rounds)
        {
            Random tk = new Random();
            int tCall = tk.Next(3, 7);
            int tRaise = tk.Next(4, 8);
            if (player.Type.Power <= 390 && player.Type.Power >= 330)
            {
                type.Smooth(player, tCall, tRaise, call, textboxPot, ref Raise, ref raising, ref rounds);
            }
            if (player.Type.Power <= 327 && player.Type.Power >= 321)//10  8
            {
                type.Smooth(player, tCall, tRaise, call, textboxPot, ref Raise, ref raising, ref rounds);
            }
            if (player.Type.Power < 321 && player.Type.Power >= 303)//7 2
            {
                type.Smooth(player, tCall, tRaise, call, textboxPot, ref Raise, ref raising, ref rounds);
            }
        }
        public void Straight(IPlayer player, int call, TextBox textboxPot, ref double Raise, ref bool raising, ref double rounds)
        {
            Random str = new Random();
            int sCall = str.Next(3, 6);
            int sRaise = str.Next(3, 8);
            if (player.Type.Power <= 480 && player.Type.Power >= 410)
            {
                type.Smooth(player, sCall, sRaise, call, textboxPot, ref Raise, ref raising, ref rounds);
            }
            if (player.Type.Power <= 409 && player.Type.Power >= 407)//10  8
            {
                type.Smooth(player, sCall, sRaise, call, textboxPot, ref Raise, ref raising, ref rounds);
            }
            if (player.Type.Power < 407 && player.Type.Power >= 404)
            {
                type.Smooth(player, sCall, sRaise, call, textboxPot, ref Raise, ref raising, ref rounds);
            }
        }
        public void Flush(IPlayer player, int call, TextBox textboxPot, ref double Raise, ref bool raising, ref double rounds)
        {
            Random fsh = new Random();
            int fCall = fsh.Next(2, 6);
            int fRaise = fsh.Next(3, 7);
            type.Smooth(player, fCall, fRaise, call, textboxPot, ref Raise, ref raising, ref rounds);
        }
        public void FullHouse(IPlayer player, int call, TextBox textboxPot, ref double Raise, ref bool raising, ref double rounds)
        {
            Random flh = new Random();
            int fhCall = flh.Next(1, 5);
            int fhRaise = flh.Next(2, 6);
            if (player.Type.Power <= 626 && player.Type.Power >= 620)
            {
                type.Smooth(player, fhCall, fhRaise, call, textboxPot, ref Raise, ref raising, ref rounds);
            }
            if (player.Type.Power < 620 && player.Type.Power >= 602)
            {
                type.Smooth(player, fhCall, fhRaise, call, textboxPot, ref Raise, ref raising, ref rounds);
            }
        }
        public void FourOfAKind(IPlayer player, int call, TextBox textboxPot, ref double Raise, ref bool raising, ref double rounds)
        {
            Random fk = new Random();
            int fkCall = fk.Next(1, 4);
            int fkRaise = fk.Next(2, 5);
            if (player.Type.Power <= 752 && player.Type.Power >= 704)
            {
                type.Smooth(player, fkCall, fkRaise, call, textboxPot, ref Raise, ref raising, ref rounds);
            }
        }
        public void StraightFlush(IPlayer player, int call, TextBox textboxPot, ref double Raise, ref bool raising, ref double rounds)
        {
            Random sf = new Random();
            int sfCall = sf.Next(1, 3);
            int sfRaise = sf.Next(1, 3);
            if (player.Type.Power <= 913 && player.Type.Power >= 804)
            {
                type.Smooth(player, sfCall, sfRaise, call, textboxPot, ref Raise, ref raising, ref rounds);
            }
        }
    }
}
