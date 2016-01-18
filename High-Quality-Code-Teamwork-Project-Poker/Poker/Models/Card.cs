using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Models
{
    using Poker.Enums;
    using Poker.Interfaces;

    public class Card : ICard
    {
        public Card(Face cardFace, Suit cardSuit)
        {
            this.CardFace = cardFace;
            this.CardSuit = cardSuit;
        }

        public Face CardFace { get; private set; }

        public Suit CardSuit { get; private set; }
    }
}
