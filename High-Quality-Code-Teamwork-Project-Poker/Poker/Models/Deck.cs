namespace Poker.Models
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Threading.Tasks;
    using Poker.Interfaces;

    public class Deck : IDeck
    {
        private static Deck instance;

        private Deck()
        {
            this.Cards = new List<ICard>();
            string[] imagesLocations = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < imagesLocations.Length; i++)
            {
                var charsToRemove = new string[] { "Assets\\Cards\\", ".png" };
                Image cardImage = Image.FromFile(imagesLocations[i]);
                foreach (var c in charsToRemove)
                {
                    imagesLocations[i] = imagesLocations[i].Replace(c, string.Empty);
                }

                int cardNumber = int.Parse(imagesLocations[i]) - 1;
                Card currentCard = new Card(cardNumber, cardImage);
                this.Cards.Add(currentCard);
            }
        }

        public static Deck Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Deck();
                }

                return instance;
            }
        }

        public IList<ICard> Cards { get; set; }

        public ICard GetCardAtIndex(int index)
        {
            if (index < 0 || index >= this.Cards.Count)
            {
                throw new IndexOutOfRangeException("The index cannot be negative or bigger than cards count");
            }

            return this.Cards[index];
        }

        public async Task SetCards(IList<IPlayer> players, IDealer dealer)
        {
            if (players == null || dealer == null)
            {
                throw new NullReferenceException("The players and dealer cannot be null");
            }

            this.Shuffle();
            int playersCount;
            for (playersCount = 0; playersCount < players.Count; playersCount++)
            {
                await this.SetCardToPlayers(players[playersCount], playersCount, 2);
            }

            await this.SetCardToDealer(dealer, 5, playersCount * 2);
        }

        private async Task SetCardToPlayers(ICardHolder cardHandler, int cardHandlerIndex, int cardsCountToSet)
        {
            IList<ICard> cards = new List<ICard>();
            for (int i = 0; i < cardsCountToSet; i++)
            {
                cards.Add(this.Cards[cardHandlerIndex * cardsCountToSet + i]);
            }

            await cardHandler.SetCards(cards);
        }

        private async Task SetCardToDealer(ICardHolder cardHandler, int cardsCountToSet, int allPlayersCardsCount)
        {
            IList<ICard> cards = new List<ICard>();
            for (int i = 0; i < cardsCountToSet; i++)
            {
                cards.Add(this.Cards[allPlayersCardsCount + i]);
            }

            await cardHandler.SetCards(cards);
        }

        private void Shuffle()
        {
            Random random = new Random();
            for (int i = this.Cards.Count; i > 0; i--)
            {
                int j = random.Next(i);
                var k = this.Cards[j];
                this.Cards[j] = this.Cards[i - 1];
                this.Cards[i - 1] = k;
            }
        }
    }
}
