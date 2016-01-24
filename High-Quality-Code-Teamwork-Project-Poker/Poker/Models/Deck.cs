namespace Poker.Models
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Threading.Tasks;
    using Poker.Interfaces;

    public class Deck
    {
        private static Deck instance;
        private readonly IList<Card> cards;

        private Deck()
        {
            this.cards = new List<Card>();
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
                this.cards.Add(currentCard);
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

        public IList<Card> GetCards()
        {
            return this.cards;
        }

        public Card GetCardAtIndex(int index)
        {
            return this.cards[index];
        }

        public async Task SetCards(IList<IPlayer> players, Dealer dealer)
        {
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
            IList<Card> cards = new List<Card>();
            for (int i = 0; i < cardsCountToSet; i++)
            {
                //await Task.Delay(200);
                //cardHandler.SetCard(this.cards[cardHandlerIndex * cardsCountToSet + i]);
                cards.Add(this.cards[cardHandlerIndex * cardsCountToSet + i]);
            }

            await cardHandler.SetCards(cards);
        }

        private async Task SetCardToDealer(ICardHolder cardHandler, int cardsCountToSet, int allPlayersCardsCount)
        {
            IList<Card> cards = new List<Card>();
            for (int i = 0; i < cardsCountToSet; i++)
            {
                //await Task.Delay(200); 
                cards.Add(this.cards[allPlayersCardsCount + i]);
            }

            await cardHandler.SetCards(cards);
        }

        private void Shuffle()
        {
            Random random = new Random();
            for (int i = this.cards.Count; i > 0; i--)
            {
                int j = random.Next(i);
                var k = this.cards[j];
                this.cards[j] = this.cards[i - 1];
                this.cards[i - 1] = k;
            }
        }
    }
}
