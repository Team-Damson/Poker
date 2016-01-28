using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poker.Interfaces;
using Poker.Models;

namespace PokerUnitTests
{
    public class MockDeck : IDeck
    {
        public IList<ICard> Cards { get; set; }

        public ICard GetCardAtIndex(int index)
        {
            return this.Cards[index];
        }

        public Task SetCards(IList<IPlayer> players, IDealer dealer)
        {
            int i = 0;
            foreach (var player in players)
            {
                for (int j = 0; j < 2; j++)
                {
                    player.AddCard(this.Cards[i]);
                    i++;
                }
            }

            for (int j = 0; j < 5; j++)
            {
                dealer.AddCard(this.Cards[i]);
                i++;
            }

            return Task.FromResult(0);
        }
    }
}
