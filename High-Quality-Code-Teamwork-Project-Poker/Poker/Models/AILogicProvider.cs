using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poker.Interfaces;
using Poker.Enums;

namespace Poker.Models
{
    public class AILogicProvider : IAILogicProvider
    {
        private HandTypes handType;

        // TODO: Add IHandTypes dependancy injection

        public AILogicProvider()
        {
            this.handType = new HandTypes();
        }

        public void HandleTurn(IPlayer player, int call, IPot Pot, ref double raise, ref bool isAnyPlayerRaise, CommunityCardBoard currentRound)
        {
            if (!player.FoldedTurn)
            {
                //switch (player.Type.Current)
                //{
                //    case PokerHand.HighCard:
                //        handType.HighCard(player, Call, textboxPot, ref RaiseAmount, ref IsAnyPlayerRaise);
                //        break;
                //    case PokerHand.PairTable:
                //        handType.PairTable(player, Call, textboxPot, ref RaiseAmount, ref IsAnyPlayerRaise);
                //        break;
                //    case PokerHand.PairFromHand:
                //        handType.PairHand(player, Call, textboxPot, ref RaiseAmount, ref IsAnyPlayerRaise, currentRound);
                //        break;
                //    case PokerHand.TwoPair:
                //        handType.TwoPair(player, Call, textboxPot, ref RaiseAmount, ref IsAnyPlayerRaise, currentRound);
                //        break;
                //    case PokerHand.ThreeOfAKind:
                //        handType.ThreeOfAKind(player, Call, textboxPot, ref RaiseAmount, ref IsAnyPlayerRaise);
                //        break;
                //    case PokerHand.Straigth:
                //        handType.Straight(player, Call, textboxPot, ref RaiseAmount, ref IsAnyPlayerRaise);
                //        break;
                //    case PokerHand.Flush:
                //    case PokerHand.FlushWithAce:
                //        handType.Flush(player, Call, textboxPot, ref RaiseAmount, ref IsAnyPlayerRaise);
                //        break;
                //    case PokerHand.FullHouse:
                //        handType.FullHouse(player, Call, textboxPot, ref RaiseAmount, ref IsAnyPlayerRaise);
                //        break;
                //    case PokerHand.FourOfAKind:
                //        handType.FourOfAKind(player, Call, textboxPot, ref RaiseAmount, ref IsAnyPlayerRaise);
                //        break;
                //    case PokerHand.StraightFlush:
                //    case PokerHand.RoyalFlush:
                //        handType.StraightFlush(player, Call, textboxPot, ref RaiseAmount, ref IsAnyPlayerRaise);
                //        break;
                //    default:
                //        throw new InvalidOperationException("Invalid Pocker Hand");
                //}

                if (player.Type.Current == PokerHand.HighCard)
                {
                    this.handType.HighCard(player, call, Pot, ref raise, ref isAnyPlayerRaise);
                }

                if (player.Type.Current == PokerHand.PairTable)
                {
                    this.handType.PairTable(player, call, Pot, ref raise, ref isAnyPlayerRaise);
                }

                if (player.Type.Current == PokerHand.PairFromHand)
                {
                    this.handType.PairHand(player, call, Pot, ref raise, ref isAnyPlayerRaise, currentRound);
                }

                if (player.Type.Current == PokerHand.TwoPair)
                {
                    this.handType.TwoPair(player, call, Pot, ref raise, ref isAnyPlayerRaise, currentRound);
                }

                if (player.Type.Current == PokerHand.ThreeOfAKind)
                {
                    this.handType.ThreeOfAKind(player, call, Pot, ref raise, ref isAnyPlayerRaise);
                }

                if (player.Type.Current == PokerHand.Straigth)
                {
                    this.handType.Straight(player, call, Pot, ref raise, ref isAnyPlayerRaise);
                }

                if (player.Type.Current == PokerHand.Flush || player.Type.Current == PokerHand.FlushWithAce)
                {
                    this.handType.Flush(player, call, Pot, ref raise, ref isAnyPlayerRaise);
                }

                if (player.Type.Current == PokerHand.FullHouse)
                {
                    this.handType.FullHouse(player, call, Pot, ref raise, ref isAnyPlayerRaise);
                }

                if (player.Type.Current == PokerHand.FourOfAKind)
                {
                    this.handType.FourOfAKind(player, call, Pot, ref raise, ref isAnyPlayerRaise);
                }

                if (player.Type.Current == PokerHand.StraightFlush || player.Type.Current == PokerHand.RoyalFlush)
                {
                    this.handType.StraightFlush(player, call, Pot, ref raise, ref isAnyPlayerRaise);
                }
            }
            
            if (player.FoldedTurn)
            {
                foreach (var pictureBox in player.PictureBoxHolder)
                {
                    pictureBox.Visible = false;
                }
                //player.PictureBoxHolder[0].Visible = false;
                //player.PictureBoxHolder[1].Visible = false;
            }
        }
    }
}
