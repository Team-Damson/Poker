namespace Poker.Models
{
    using System.Collections.Generic;
    using Poker.Enums;
    using Poker.Interfaces;

    public class Hand
    {
        public Hand(PokerHand power, IList<ICard> cards)
        {
            this.Power = power;
            this.Cards = cards;
        }

        public PokerHand Power { get; set; }

        public IList<ICard> Cards { get; set; }
    }
}
