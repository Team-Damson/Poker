using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Poker.Interfaces;

namespace Poker.Models
{
    public class Deck
    {
        private IList<Card> cards;
        private static Deck instance;

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

        public void SetCards(IList<IPlayer> players, Dealer dealer)
        {
            this.Shuffle();
            int playersCount;
            for (playersCount = 0; playersCount < players.Count; playersCount++)
            {
                this.SetCardToPlayers(players[playersCount], playersCount, 2);
            }

            this.SetCardToDealer(dealer, 5, playersCount * 2);
        }

        private  void SetCardToPlayers(ICardHolder cardHandler, int cardHandlerIndex, int cardsCountToSet)
        {
            IList<Card> cards = new List<Card>();
            for (int i = 0; i < cardsCountToSet; i++)
            {
                //await Task.Delay(200);
                //cardHandler.SetCard(this.cards[cardHandlerIndex * cardsCountToSet + i]);
                cards.Add(this.cards[cardHandlerIndex * cardsCountToSet + i]);
            }

            cardHandler.SetCards(cards);
        }

        private  void SetCardToDealer(ICardHolder cardHandler, int cardsCountToSet, int allPlayersCardsCount)
        {
            IList<Card> cards = new List<Card>();
            for (int i = 0; i < cardsCountToSet; i++)
            {
                //await Task.Delay(200); 
                cards.Add(this.cards[allPlayersCardsCount + i]);
            }

            cardHandler.SetCards(cards);
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
