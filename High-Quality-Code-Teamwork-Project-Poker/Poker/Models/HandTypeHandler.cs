using Poker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Models
{
    public class HandTypeHandler : IHandTypeHandler
    {
        private CheckHandType checkHandType;

        public HandTypeHandler()
        {
            this.checkHandType = new CheckHandType();
        }

        public void Rules(IPlayer player, IDealer dealer, IDeck deck)
        {
            if (!player.FoldedTurn)
            {
                #region Variables

                bool done = false;
                bool vf = false;
                int[] Straight1 = new int[5];
                int[] Straight = new int[7];
                Straight[0] = player.Cards.First().Power;
                Straight[1] = player.Cards.Last().Power;
                Straight1[0] = Straight[2] = dealer.Cards.ElementAt(0).Power;
                Straight1[1] = Straight[3] = dealer.Cards.ElementAt(1).Power;
                Straight1[2] = Straight[4] = dealer.Cards.ElementAt(2).Power;
                Straight1[3] = Straight[5] = dealer.Cards.ElementAt(3).Power;
                Straight1[4] = Straight[6] = dealer.Cards.ElementAt(4).Power;
                var a = Straight.Where(o => o % 4 == 0).ToArray();
                var b = Straight.Where(o => o % 4 == 1).ToArray();
                var c = Straight.Where(o => o % 4 == 2).ToArray();
                var d = Straight.Where(o => o % 4 == 3).ToArray();
                var spades = a.Select(o => o / 4).Distinct().ToArray();
                var hearts = b.Select(o => o / 4).Distinct().ToArray();
                var diamonds = c.Select(o => o / 4).Distinct().ToArray();
                var clubs = d.Select(o => o / 4).Distinct().ToArray();
                Array.Sort(Straight);
                Array.Sort(spades);
                Array.Sort(hearts);
                Array.Sort(diamonds);
                Array.Sort(clubs);

                #endregion

                for (int i = 0; i < 16; i++)
                {
                    if (deck.GetCardAtIndex(i).Power == player.Cards.First().Power && deck.GetCardAtIndex(i + 1).Power == player.Cards.Last().Power)
                    {
                        // High Card
                        this.checkHandType.CheckHighCard(player, deck.Cards, i);

                        // TwoPair from Hand
                        this.checkHandType.CheckPairFromHand(player, deck.Cards, i);

                        // TwoPair or Two TwoPair from Table
                        this.checkHandType.CheckPairTwoPair(player, deck.Cards, i);

                        // Three of a kind
                        this.checkHandType.CheckThreeOfAKind(player, Straight); 

                        // Straight
                        this.checkHandType.CheckStraight(player, Straight); 

                        // Flush current
                        this.checkHandType.CheckFlush(player, ref vf, Straight1, deck.Cards, i);

                        // Full House
                        this.checkHandType.CheckFullHouse(player, ref done, Straight); 

                        // Four of a Kind
                        this.checkHandType.CheckFourOfAKind(player, Straight);

                        // Straight Flush
                        this.checkHandType.CheckStraightFlush(player, spades, hearts, diamonds, clubs); 
                    }
                }
            }
        }

        public Type GetHighestNotFoldedHand(IList<IPlayer> players)
        {
            Type winner =
                players.Where(p => !p.HasFolded)
                    .OrderByDescending(p => p.Type.Current)
                    .ThenByDescending(p => p.Type.Power)
                    .Select(p => p.Type)
                    .First();

            return winner;
        }
    }
}
