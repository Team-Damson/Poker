using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.Interfaces;
using Poker.Models;
using Poker.Models.Players;
using System.Windows.Forms;
using System.Drawing;
using System.Threading.Tasks;

namespace PokerUnitTests
{
    [TestClass]
    public class WinningHandScenarioTests
    {
        private static IList<IPlayer> players;
        private static IDeck deck;
        private static IDealer dealer;
        private static HandTypeHandler handHandler;

        [ClassInitialize]
        public static void InitGameObjectss(TestContext testContext)
        {
            players = new List<IPlayer>();
            
            IPlayer human = PlayerFactory.CreateHuman("human", 10000, new Label(), new TextBox(), AnchorStyles.Bottom, 100, 100);
            players.Add(human);

            IAILogicProvider logicProvider = new AILogicProvider();
            IAIPlayer AI1 = PlayerFactory.CreateAI(logicProvider, "AI1", 10000, new Label(), new TextBox(), AnchorStyles.Bottom, 100, 100);
            players.Add(AI1);
            IAIPlayer AI2 = PlayerFactory.CreateAI(logicProvider, "AI2", 10000, new Label(), new TextBox(), AnchorStyles.Bottom, 100, 100);
            players.Add(AI2);
            IAIPlayer AI3 = PlayerFactory.CreateAI(logicProvider, "AI3", 10000, new Label(), new TextBox(), AnchorStyles.Bottom, 100, 100);
            players.Add(AI3);
            IAIPlayer AI4 = PlayerFactory.CreateAI(logicProvider, "AI4", 10000, new Label(), new TextBox(), AnchorStyles.Bottom, 100, 100);
            players.Add(AI4);
            IAIPlayer AI5 = PlayerFactory.CreateAI(logicProvider, "AI5", 10000, new Label(), new TextBox(), AnchorStyles.Bottom, 100, 100);
            players.Add(AI5);

            dealer = new Dealer(100, 100);

            deck = new MockDeck();

            handHandler = new HandTypeHandler();
        }

        [TestCleanup]
        public void CleanUp()
        {
            foreach (var player in players)
            {
                player.CleanCard();
            }

            dealer.CleanCard();
        }
        [TestMethod]
        public async Task AllWithoutFirsPlayerHavePair_SecondPlayerShouldWinWithHigherCard()
        {
            IList<ICard> cards = new List<ICard>();

            cards.Add(new Card(2, Card.BackImage));
            cards.Add(new Card(39, Card.BackImage));

            cards.Add(new Card(3, Card.BackImage));
            cards.Add(new Card(1, Card.BackImage));

            cards.Add(new Card(23, Card.BackImage));
            cards.Add(new Card(27, Card.BackImage));

            cards.Add(new Card(25, Card.BackImage));
            cards.Add(new Card(40, Card.BackImage));

            cards.Add(new Card(42, Card.BackImage));
            cards.Add(new Card(50, Card.BackImage));

            cards.Add(new Card(21, Card.BackImage));
            cards.Add(new Card(31, Card.BackImage));

            cards.Add(new Card(22, Card.BackImage));
            cards.Add(new Card(26, Card.BackImage));
            cards.Add(new Card(13, Card.BackImage));
            cards.Add(new Card(28, Card.BackImage));
            cards.Add(new Card(41, Card.BackImage));

            deck.Cards = cards;
            await deck.SetCards(players.ToList(), dealer);
            foreach (var player in players)
            {
                handHandler.Rules(player, dealer, deck);
            }

            Poker.Type winnerHandtype = handHandler.GetHighestNotFoldedHand(players.ToList());

            Assert.AreEqual(players[1].Type.Current, winnerHandtype.Current);
            Assert.AreEqual(players[1].Type.Power, winnerHandtype.Power);
        }
        [TestMethod]
        public async Task FirstPlayersHasFlush_SecondPlayerHasStreight_FirstPlayerShouldWin()
        {
            IList<ICard> cards = new List<ICard>();
            
            cards.Add(new Card(2, Card.BackImage));
            cards.Add(new Card(6, Card.BackImage));

            cards.Add(new Card(34, Card.BackImage));
            cards.Add(new Card(39, Card.BackImage));

            cards.Add(new Card(12, Card.BackImage));
            cards.Add(new Card(27, Card.BackImage));

            cards.Add(new Card(40, Card.BackImage));
            cards.Add(new Card(11, Card.BackImage));

            cards.Add(new Card(48, Card.BackImage));
            cards.Add(new Card(23, Card.BackImage));

            cards.Add(new Card(21, Card.BackImage));
            cards.Add(new Card(31, Card.BackImage));

            cards.Add(new Card(10, Card.BackImage));
            cards.Add(new Card(14, Card.BackImage));
            cards.Add(new Card(50, Card.BackImage));
            cards.Add(new Card(47, Card.BackImage));
            cards.Add(new Card(41, Card.BackImage));

            deck.Cards = cards;
            await deck.SetCards(players.ToList(), dealer);
            foreach (var player in players)
            {
                handHandler.Rules(player, dealer, deck);
            }

            Poker.Type winnerHandtype = handHandler.GetHighestNotFoldedHand(players.ToList());

            Assert.AreEqual(players.First().Type.Current, winnerHandtype.Current);
            Assert.AreEqual(players.First().Type.Power, winnerHandtype.Power);
        }

        [TestMethod]
        public async Task FirstPlayerHasThreeOfAKind_SecondPlayerHasTwoPair_FirstPlayerShouldWin()
        {
            IList<ICard> cards = new List<ICard>();
          
            cards.Add(new Card(2, Card.BackImage));
            cards.Add(new Card(39, Card.BackImage));

            cards.Add(new Card(3, Card.BackImage));
            cards.Add(new Card(1, Card.BackImage));

            cards.Add(new Card(22, Card.BackImage));
            cards.Add(new Card(27, Card.BackImage));

            cards.Add(new Card(25, Card.BackImage));
            cards.Add(new Card(4, Card.BackImage));

            cards.Add(new Card(42, Card.BackImage));
            cards.Add(new Card(50, Card.BackImage));

            cards.Add(new Card(21, Card.BackImage));
            cards.Add(new Card(31, Card.BackImage));

            cards.Add(new Card(52, Card.BackImage));
            cards.Add(new Card(51, Card.BackImage));
            cards.Add(new Card(36, Card.BackImage));
            cards.Add(new Card(38, Card.BackImage));
            cards.Add(new Card(41, Card.BackImage));

            deck.Cards = cards;
            await deck.SetCards(players.ToList(), dealer);
            foreach (var player in players)
            {
                handHandler.Rules(player, dealer, deck);
            }

            Poker.Type winnerHandtype = handHandler.GetHighestNotFoldedHand(players.ToList());

            Assert.AreEqual(players[0].Type.Current, winnerHandtype.Current);
            Assert.AreEqual(players[0].Type.Power, winnerHandtype.Power);
        }

        [TestMethod]
        public async Task FirstPlayersHasFlush_SecondPlayerHasStreight_FourthPlayerHasFullHouse_FourthPlayerShouldWin()
        {
            IList<ICard> cards = new List<ICard>();
          
            cards.Add(new Card(2, Card.BackImage));
            cards.Add(new Card(6, Card.BackImage));

            cards.Add(new Card(34, Card.BackImage));
            cards.Add(new Card(39, Card.BackImage));

            cards.Add(new Card(12, Card.BackImage));
            cards.Add(new Card(27, Card.BackImage));

            cards.Add(new Card(47, Card.BackImage));
            cards.Add(new Card(48, Card.BackImage));

            cards.Add(new Card(49, Card.BackImage));
            cards.Add(new Card(23, Card.BackImage));

            cards.Add(new Card(21, Card.BackImage));
            cards.Add(new Card(31, Card.BackImage));

            cards.Add(new Card(52, Card.BackImage));
            cards.Add(new Card(51, Card.BackImage));
            cards.Add(new Card(50, Card.BackImage));
            cards.Add(new Card(47, Card.BackImage));
            cards.Add(new Card(41, Card.BackImage));

            deck.Cards = cards;
            await deck.SetCards(players.ToList(), dealer);
            foreach (var player in players)
            {
                handHandler.Rules(player, dealer, deck);
            }

            Poker.Type winnerHandtype = handHandler.GetHighestNotFoldedHand(players.ToList());

            Assert.AreEqual(players[3].Type.Current, winnerHandtype.Current);
            Assert.AreEqual(players[3].Type.Power, winnerHandtype.Power);
        }

        [TestMethod]
        public async Task AllPlayersHaveThreeOfAKind_SecondPlayerHasStreight_SecondPlayerShouldWin()
        {
            IList<ICard> cards = new List<ICard>();
          
            cards.Add(new Card(2, Card.BackImage));
            cards.Add(new Card(39, Card.BackImage));

            cards.Add(new Card(34, Card.BackImage));
            cards.Add(new Card(39, Card.BackImage));

            cards.Add(new Card(22, Card.BackImage));
            cards.Add(new Card(27, Card.BackImage));

            cards.Add(new Card(47, Card.BackImage));
            cards.Add(new Card(40, Card.BackImage));

            cards.Add(new Card(42, Card.BackImage));
            cards.Add(new Card(23, Card.BackImage));

            cards.Add(new Card(21, Card.BackImage));
            cards.Add(new Card(31, Card.BackImage));

            cards.Add(new Card(52, Card.BackImage));
            cards.Add(new Card(51, Card.BackImage));
            cards.Add(new Card(50, Card.BackImage));
            cards.Add(new Card(47, Card.BackImage));
            cards.Add(new Card(41, Card.BackImage));

            deck.Cards = cards;
            await deck.SetCards(players.ToList(), dealer);
            foreach (var player in players)
            {
                handHandler.Rules(player, dealer, deck);
            }

            Poker.Type winnerHandtype = handHandler.GetHighestNotFoldedHand(players.ToList());

            Assert.AreEqual(players[1].Type.Current, winnerHandtype.Current);
            Assert.AreEqual(players[1].Type.Power, winnerHandtype.Power);
        }

        [TestMethod]
        public async Task AllPlayersHavePair_SecondPlayerHasStreightFlush_SecondPlayerShouldWin()
        {
            IList<ICard> cards = new List<ICard>();
          
            cards.Add(new Card(2, Card.BackImage));
            cards.Add(new Card(39, Card.BackImage));

            cards.Add(new Card(48, Card.BackImage));
            cards.Add(new Card(44, Card.BackImage));

            cards.Add(new Card(22, Card.BackImage));
            cards.Add(new Card(27, Card.BackImage));

            cards.Add(new Card(47, Card.BackImage));
            cards.Add(new Card(40, Card.BackImage));

            cards.Add(new Card(42, Card.BackImage));
            cards.Add(new Card(23, Card.BackImage));

            cards.Add(new Card(21, Card.BackImage));
            cards.Add(new Card(31, Card.BackImage));

            cards.Add(new Card(52, Card.BackImage));
            cards.Add(new Card(51, Card.BackImage));
            cards.Add(new Card(36, Card.BackImage));
            cards.Add(new Card(40, Card.BackImage));
            cards.Add(new Card(41, Card.BackImage));

            deck.Cards = cards;
            await deck.SetCards(players.ToList(), dealer);
            foreach (var player in players)
            {
                handHandler.Rules(player, dealer, deck);
            }

            Poker.Type winnerHandtype = handHandler.GetHighestNotFoldedHand(players.ToList());

            Assert.AreEqual(players[1].Type.Current, winnerHandtype.Current);
            Assert.AreEqual(players[1].Type.Power, winnerHandtype.Power);
        }

        [TestMethod]
        public async Task FifthPlayerHasThreeOfAKind_FourthPlayerHasStreight_FourthPlayerShouldWin()
        {
            IList<ICard> cards = new List<ICard>();
          
            cards.Add(new Card(2, Card.BackImage));
            cards.Add(new Card(39, Card.BackImage));

            cards.Add(new Card(3, Card.BackImage));
            cards.Add(new Card(1, Card.BackImage));

            cards.Add(new Card(22, Card.BackImage));
            cards.Add(new Card(27, Card.BackImage));

            cards.Add(new Card(47, Card.BackImage));
            cards.Add(new Card(40, Card.BackImage));

            cards.Add(new Card(42, Card.BackImage));
            cards.Add(new Card(50, Card.BackImage));

            cards.Add(new Card(21, Card.BackImage));
            cards.Add(new Card(31, Card.BackImage));

            cards.Add(new Card(52, Card.BackImage));
            cards.Add(new Card(51, Card.BackImage));
            cards.Add(new Card(36, Card.BackImage));
            cards.Add(new Card(38, Card.BackImage));
            cards.Add(new Card(41, Card.BackImage));

            deck.Cards = cards;
            await deck.SetCards(players.ToList(), dealer);
            foreach (var player in players)
            {
                handHandler.Rules(player, dealer, deck);
            }

            Poker.Type winnerHandtype = handHandler.GetHighestNotFoldedHand(players.ToList());

            Assert.AreEqual(players[3].Type.Current, winnerHandtype.Current);
            Assert.AreEqual(players[3].Type.Power, winnerHandtype.Power);
        }

        [TestMethod]
        public async Task FourthPlayerHasFourOfAKind_SecondPlayerHasTwoPair_FourthPlayerShouldWin()
        {
            IList<ICard> cards = new List<ICard>();
        
            cards.Add(new Card(2, Card.BackImage));
            cards.Add(new Card(39, Card.BackImage));

            cards.Add(new Card(3, Card.BackImage));
            cards.Add(new Card(1, Card.BackImage));

            cards.Add(new Card(23, Card.BackImage));
            cards.Add(new Card(27, Card.BackImage));

            cards.Add(new Card(25, Card.BackImage));
            cards.Add(new Card(40, Card.BackImage));

            cards.Add(new Card(42, Card.BackImage));
            cards.Add(new Card(50, Card.BackImage));

            cards.Add(new Card(21, Card.BackImage));
            cards.Add(new Card(31, Card.BackImage));

            cards.Add(new Card(22, Card.BackImage));
            cards.Add(new Card(26, Card.BackImage));
            cards.Add(new Card(27, Card.BackImage));
            cards.Add(new Card(28, Card.BackImage));
            cards.Add(new Card(41, Card.BackImage));

            deck.Cards = cards;
            await deck.SetCards(players.ToList(), dealer);
            foreach (var player in players)
            {
                handHandler.Rules(player, dealer, deck);
            }

            Poker.Type winnerHandtype = handHandler.GetHighestNotFoldedHand(players.ToList());

            Assert.AreEqual(players[3].Type.Current, winnerHandtype.Current);
            Assert.AreEqual(players[3].Type.Power, winnerHandtype.Power);
        }

        [TestMethod]
        public async Task FifthPlayerHasFlush_FirstAndSecondPlayerHasPair_FifthPlayerShouldWin()
        {
            IList<ICard> cards = new List<ICard>();
         
            cards.Add(new Card(2, Card.BackImage));
            cards.Add(new Card(39, Card.BackImage));

            cards.Add(new Card(3, Card.BackImage));
            cards.Add(new Card(1, Card.BackImage));

            cards.Add(new Card(23, Card.BackImage));
            cards.Add(new Card(27, Card.BackImage));

            cards.Add(new Card(25, Card.BackImage));
            cards.Add(new Card(40, Card.BackImage));

            cards.Add(new Card(42, Card.BackImage));
            cards.Add(new Card(50, Card.BackImage));

            cards.Add(new Card(21, Card.BackImage));
            cards.Add(new Card(31, Card.BackImage));

            cards.Add(new Card(22, Card.BackImage));
            cards.Add(new Card(26, Card.BackImage));
            cards.Add(new Card(14, Card.BackImage));
            cards.Add(new Card(28, Card.BackImage));
            cards.Add(new Card(41, Card.BackImage));

            deck.Cards = cards;
            await deck.SetCards(players.ToList(), dealer);
            foreach (var player in players)
            {
                handHandler.Rules(player, dealer, deck);
            }

            Poker.Type winnerHandtype = handHandler.GetHighestNotFoldedHand(players.ToList());

            Assert.AreEqual(players[4].Type.Current, winnerHandtype.Current);
            Assert.AreEqual(players[4].Type.Power, winnerHandtype.Power);
        }

        [TestMethod]
        public async Task FifthAndFourthPlayerHasFlush_ThirdPlayerHasThreeOfAKind_FifthAndFourthPlayerShouldWin()
        {
            IList<ICard> cards = new List<ICard>();

            cards.Add(new Card(2, Card.BackImage));
            cards.Add(new Card(39, Card.BackImage));

            cards.Add(new Card(3, Card.BackImage));
            cards.Add(new Card(11, Card.BackImage));

            cards.Add(new Card(23, Card.BackImage));
            cards.Add(new Card(27, Card.BackImage));

            cards.Add(new Card(25, Card.BackImage));
            cards.Add(new Card(40, Card.BackImage));

            cards.Add(new Card(42, Card.BackImage));
            cards.Add(new Card(17, Card.BackImage));

            cards.Add(new Card(36, Card.BackImage));
            cards.Add(new Card(38, Card.BackImage));

            cards.Add(new Card(45, Card.BackImage));
            cards.Add(new Card(46, Card.BackImage));
            cards.Add(new Card(49, Card.BackImage));
            cards.Add(new Card(41, Card.BackImage));
            cards.Add(new Card(5, Card.BackImage));

            deck.Cards = cards;
            await deck.SetCards(players.ToList(), dealer);
            foreach (var player in players)
            {
                handHandler.Rules(player, dealer, deck);
            }

            Poker.Type winnerHandtype = handHandler.GetHighestNotFoldedHand(players.ToList());

            Assert.AreEqual(players[4].Type.Current, winnerHandtype.Current);
            Assert.AreEqual(players[4].Type.Power, winnerHandtype.Power);
            Assert.AreEqual(players[3].Type.Current, winnerHandtype.Current);
            Assert.AreEqual(players[3].Type.Power, winnerHandtype.Power);
        }
    }
}
