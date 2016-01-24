namespace Poker
{
    using System;
    using System.Windows.Forms;
    using Poker.Interfaces;

    public class HandTypes
    {
        private readonly Dunno type = new Dunno();
        private readonly Random rnd = new Random();

        public void HighCard(IPlayer player, int call, TextBox textboxPot, ref double Raise, ref bool raising)
        {
            this.type.HP(player, 20, 25, call, textboxPot, ref Raise, ref raising);
        }

        public void PairTable(IPlayer player, int call, TextBox textboxPot, ref double Raise, ref bool raising)
        {
            this.type.HP(player, 16, 25, call, textboxPot, ref Raise, ref raising);
        }

        public void PairHand(IPlayer player, int call, TextBox textboxPot, ref double Raise, ref bool raising, double rounds)
        {
            int randomCall = this.rnd.Next(10, 16);
            int randomRaise = this.rnd.Next(10, 13);

            if (player.Type.Power <= 199 && player.Type.Power >= 140)
            {
                this.type.PH(player, randomCall, 6, randomRaise, call, textboxPot, ref Raise, ref raising, rounds);
            }

            if (player.Type.Power <= 139 && player.Type.Power >= 128)
            {
                this.type.PH(player, randomCall, 7, randomRaise, call, textboxPot, ref Raise, ref raising, rounds);
            }

            if (player.Type.Power < 128 && player.Type.Power >= 101)
            {
                this.type.PH(player, randomCall, 9, randomRaise, call, textboxPot, ref Raise, ref raising, rounds);
            }
        }

        public void TwoPair(IPlayer player, int call, TextBox textboxPot, ref double Raise, ref bool raising, double rounds)
        {
            int randomCall = this.rnd.Next(6, 11);
            int randomRaise = this.rnd.Next(6, 11);

            if (player.Type.Power <= 290 && player.Type.Power >= 246)
            {
                this.type.PH(player, randomCall, 3, randomRaise, call, textboxPot, ref Raise, ref raising, rounds);
            }

            if (player.Type.Power <= 244 && player.Type.Power >= 234)
            {
                this.type.PH(player, randomCall, 4, randomRaise, call, textboxPot, ref Raise, ref raising, rounds);
            }

            if (player.Type.Power < 234 && player.Type.Power >= 201)
            {
                this.type.PH(player, randomCall, 4, randomRaise, call, textboxPot, ref Raise, ref raising, rounds);
            }
        }

        public void ThreeOfAKind(IPlayer player, int call, TextBox textboxPot, ref double Raise, ref bool raising, ref double rounds)
        {
            int randomCall = this.rnd.Next(3, 7);
            int randomRaise = this.rnd.Next(4, 8);

            if (player.Type.Power <= 390 && player.Type.Power >= 330)
            {
                this.type.Smooth(player, randomCall, randomRaise, call, textboxPot, ref Raise, ref raising, ref rounds);
            }

            if (player.Type.Power <= 327 && player.Type.Power >= 321)
            {
                this.type.Smooth(player, randomCall, randomRaise, call, textboxPot, ref Raise, ref raising, ref rounds);
            }

            if (player.Type.Power < 321 && player.Type.Power >= 303)
            {
                this.type.Smooth(player, randomCall, randomRaise, call, textboxPot, ref Raise, ref raising, ref rounds);
            }
        }

        public void Straight(IPlayer player, int call, TextBox textboxPot, ref double Raise, ref bool raising, ref double rounds)
        {
            int randomCall = this.rnd.Next(3, 6);
            int randomRaise = this.rnd.Next(3, 8);

            if (player.Type.Power <= 480 && player.Type.Power >= 410)
            {
                this.type.Smooth(player, randomCall, randomRaise, call, textboxPot, ref Raise, ref raising, ref rounds);
            }

            if (player.Type.Power <= 409 && player.Type.Power >= 407)
            {
                this.type.Smooth(player, randomCall, randomRaise, call, textboxPot, ref Raise, ref raising, ref rounds);
            }

            if (player.Type.Power < 407 && player.Type.Power >= 404)
            {
                this.type.Smooth(player, randomCall, randomRaise, call, textboxPot, ref Raise, ref raising, ref rounds);
            }
        }

        public void Flush(IPlayer player, int call, TextBox textboxPot, ref double Raise, ref bool raising, ref double rounds)
        {
            int randomCall = this.rnd.Next(2, 6);
            int randomRaise = this.rnd.Next(3, 7);

            this.type.Smooth(player, randomCall, randomRaise, call, textboxPot, ref Raise, ref raising, ref rounds);
        }

        public void FullHouse(IPlayer player, int call, TextBox textboxPot, ref double Raise, ref bool raising, ref double rounds)
        {
            int randomhCall = this.rnd.Next(1, 5);
            int randomRaise = this.rnd.Next(2, 6);

            if (player.Type.Power <= 626 && player.Type.Power >= 620)
            {
                this.type.Smooth(player, randomhCall, randomRaise, call, textboxPot, ref Raise, ref raising, ref rounds);
            }

            if (player.Type.Power < 620 && player.Type.Power >= 602)
            {
                this.type.Smooth(player, randomhCall, randomRaise, call, textboxPot, ref Raise, ref raising, ref rounds);
            }
        }

        public void FourOfAKind(IPlayer player, int call, TextBox textboxPot, ref double Raise, ref bool raising, ref double rounds)
        {
            int randomCall = this.rnd.Next(1, 4);
            int randomRaise = this.rnd.Next(2, 5);

            if (player.Type.Power <= 752 && player.Type.Power >= 704)
            {
                this.type.Smooth(player, randomCall, randomRaise, call, textboxPot, ref Raise, ref raising, ref rounds);
            }
        }

        public void StraightFlush(IPlayer player, int call, TextBox textboxPot, ref double Raise, ref bool raising, ref double rounds)
        {
            int randomCall = this.rnd.Next(1, 3);
            int randomRaise = this.rnd.Next(1, 3);

            if (player.Type.Power <= 913 && player.Type.Power >= 804)
            {
                this.type.Smooth(player, randomCall, randomRaise, call, textboxPot, ref Raise, ref raising, ref rounds);
            }
        }
    }
}
