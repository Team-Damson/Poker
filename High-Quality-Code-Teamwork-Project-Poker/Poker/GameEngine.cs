using Poker.Models;

namespace Poker
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Poker.Enums;
    using Poker.Events;
    using Poker.Interfaces;

    public class GameEngine : IGameEngine
    {
        private IHandTypeHandler handTypeHandler;
        private IPlayer human;
        private IDealer dealer;
        private IDeck deck;
        private double raise;
        private IList<IAIPlayer> enemies;
        private bool changed;
        private int raisedTurn = 1;
        private bool isAnyPlayerRaise;
        private int turnCount = 0;

        public event GameEngineStateEvent GameEngineStateEvent;

        public bool IsAnyPlayerRaise
        {
            get { return this.isAnyPlayerRaise; }
            set { this.isAnyPlayerRaise = value; }
        }

        public int BigBlind { get; set; }

        public int SmallBlind { get; set; }

        public IPot Pot { get; private set; }

        public int Call { get; set; }

        public double Raise
        {
            get { return this.raise; }
            set { this.raise = value; }
        }

        public IMessageWriter MessageWriter { get; set; }


        public GameEngine(IPlayer human, ICollection<IAIPlayer> enemies, IPot pot, IDealer dealer, IDeck deck, IMessageWriter messageWriter, IHandTypeHandler handTypeHandler)
        {
            this.human = human;
            this.enemies = new List<IAIPlayer>(enemies);
            this.Pot = pot;
            this.dealer = dealer;
            this.deck = deck;
            this.MessageWriter = messageWriter;
            this.handTypeHandler = handTypeHandler;
            this.BigBlind = AppSettigns.DefaultMinBigBlind;
            this.SmallBlind = AppSettigns.DefaultMinSmallBlind;
            this.SetDefaultCall();
            this.Raise = 0;
            this.IsAnyPlayerRaise = false;
        }

        public IPlayer GetHumanPlayer()
        {
            return this.human;
        }

        public async void Run()
        {
            await this.Shuffle();
        }

        private void InvokeGameEngineStateEvent(GameEngineEventArgs args)
        {
            if (this.GameEngineStateEvent != null)
            {
                this.GameEngineStateEvent(this, args);
            }
        }

        public IList<IPlayer> GetAllPlayers()
        {
            IList<IPlayer> allPlayers = new List<IPlayer>();
            allPlayers.Add(this.human);
            foreach (IPlayer enemy in this.enemies)
            {
                allPlayers.Add(enemy);
            }

            return allPlayers;
        }

        private async Task Shuffle()
        {
            this.InvokeGameEngineStateEvent(new GameEngineEventArgs(GameEngineState.BeginShuffling));

            await this.deck.SetCards(this.GetAllPlayers(), this.dealer);
            
            if (this.enemies.Count(e => !e.CanPlay()) == 5)
            {
                this.ShowPlayAgainDialog();
            }

            foreach (var player in this.GetAllPlayers())
            {
                this.handTypeHandler.Rules(player, this.dealer, this.deck);    
            }

            this.InvokeGameEngineStateEvent(new GameEngineEventArgs(GameEngineState.EndShuffling));
        }

        private void ShowPlayAgainDialog()
        {
            DialogResult dialogResult = this.MessageWriter.ShowDialog(Messages.PlayAgainMessage, Messages.WinningMessage, MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Application.Restart();
            }
            else if (dialogResult == DialogResult.No)
            {
                Application.Exit();
            }
        }

        private void SetDefaultCall()
        {
            this.Call = this.BigBlind;
        }

        private async Task HandleAITurn(IAIPlayer currentAI, IPlayer nextAI)
        {
            if (!currentAI.FoldedTurn)
            {
                if (currentAI.IsInTurn)
                {
                    this.FixCall(currentAI);//, 1);
                    //this.FixCall(currentAI, 2);
                    this.MessageWriter.Write(string.Format(Messages.PlayerTurn, currentAI.Name));
                    currentAI.ProccessNextTurn(Call, this.Pot, ref raise, ref isAnyPlayerRaise, this.dealer.CurrentRound);
                    this.turnCount++;
                    currentAI.IsInTurn = false;
                    nextAI.IsInTurn = true;
                }
            }

            if (currentAI.FoldedTurn && !currentAI.HasFolded)
            {
                currentAI.HasFolded = true;
            }

            if (currentAI.FoldedTurn || !currentAI.IsInTurn)
            {
                await this.CheckRaise(currentAI.Id + 1); // , currentAI.Id + 1);
                nextAI.IsInTurn = true;
            }
        }

        public async Task Turns()
        {
            if (!this.human.FoldedTurn)
            {
                if (this.human.IsInTurn)
                {
                    this.FixCall(this.human);//, 1);
                    this.InvokeGameEngineStateEvent(new GameEngineEventArgs(GameEngineState.HumanTurn));
                    this.turnCount++;
                    //this.FixCall(this.human, 2);
                }
            }

            if (this.human.FoldedTurn || !this.human.IsInTurn)
            {
                await this.AllIn();
                await this.CheckRaise(0);//, 0);
                this.InvokeGameEngineStateEvent(new GameEngineEventArgs(GameEngineState.AITurn));
                this.enemies.First().IsInTurn = true;
                for (int i = 0; i < this.enemies.Count; i++)
                {
                    if (this.enemies.Count - 1 == i)
                    {
                        await this.HandleAITurn(this.enemies[i], this.human);
                    }
                    else
                    {
                        await this.HandleAITurn(this.enemies[i], this.enemies[i + 1]);
                    }
                }

                await this.AllIn();
                await this.Turns();
            }
        }

        private int GetFoldedPlayersCount(ICollection<IPlayer> players)
        {
            return players.Count(p => p.HasFolded == true);
        }

        private ICollection<IPlayer> GetWinners(ICollection<IPlayer> players)
        {
            ICollection<IPlayer> winners = new List<IPlayer>();
            Type highestHand = this.handTypeHandler.GetHighestNotFoldedHand(this.GetAllPlayers());
            foreach (var player in players)
            {
                if (!player.HasFolded)
                {
                    if ((player.Type.Current == highestHand.Current && player.Type.Power == highestHand.Power) || this.GetNotFoldedPlayersCount(this.GetAllPlayers()) == 1)
                    {
                        winners.Add(player);
                    }
                }
            }

            return winners;
        }

        private void ShowWinnersMessages(ICollection<IPlayer> winners)
        {
            foreach (var player in winners)
            {
                if (player.Type.Current == PokerHand.HighCard)
                {
                    this.MessageWriter.Write(player.Name + " High Card ");
                }

                if (player.Type.Current == PokerHand.PairTable || player.Type.Current == PokerHand.PairFromHand)
                {
                    this.MessageWriter.Write(player.Name + " Pair ");
                }

                if (player.Type.Current == PokerHand.TwoPair)
                {
                    this.MessageWriter.Write(player.Name + " Two Pair ");
                }

                if (player.Type.Current == PokerHand.ThreeOfAKind)
                {
                    this.MessageWriter.Write(player.Name + " Three of a Kind ");
                }

                if (player.Type.Current == PokerHand.Straigth)
                {
                    this.MessageWriter.Write(player.Name + " Straight ");
                }

                if (player.Type.Current == PokerHand.Flush || player.Type.Current == PokerHand.FlushWithAce)
                {
                    this.MessageWriter.Write(player.Name + " Flush ");
                }

                if (player.Type.Current == PokerHand.FullHouse)
                {
                    this.MessageWriter.Write(player.Name + " Full House ");
                }

                if (player.Type.Current == PokerHand.FourOfAKind)
                {
                    this.MessageWriter.Write(player.Name + " Four of a Kind ");
                }

                if (player.Type.Current == PokerHand.StraightFlush)
                {
                    this.MessageWriter.Write(player.Name + " Straight Flush ");
                }

                if (player.Type.Current == PokerHand.RoyalFlush)
                {
                    this.MessageWriter.Write(player.Name + " Royal Flush ! ");
                }
            }
        }

        private void SetWinnersChips(ICollection<IPlayer> players)
        {
            foreach (var player in players)
            {
                player.Chips += this.Pot.Amount / players.Count;
            }
        }

        private void CheckWinners(ICollection<IPlayer> players, IDealer dealer)
        {
            for (int i = 0; i < dealer.Cards.Count; i++)
            {
                dealer.RevealCardAtIndex(i);
            }

            foreach (var player in players)
            {
                for (int i = 0; i < player.Cards.Count; i++)
                {
                    player.RevealCardAtIndex(i);
                }
            }

            var winners = this.GetWinners(players);
            if (this.GetNotFoldedPlayersCount(this.GetAllPlayers()) != 1)
            {
                this.ShowWinnersMessages(winners);
            }
            
            this.SetWinnersChips(winners);
        }

        private int GetNotFoldedPlayersCount(ICollection<IPlayer> players)
        {
            return this.GetAllPlayers().Count - this.GetFoldedPlayersCount(players);
        }

        private void ResetCall(ICollection<IPlayer> players)
        {
            foreach (var player in players)
            {
                player.CallAmount = 0;
            }
        }

        private void ResetRaise(ICollection<IPlayer> players)
        {
            foreach (var player in players)
            {
                player.RaiseAmount = 0;
            }
        }

        private async Task CheckRaise(int currentTurn)//, int raiseTurn)
        {
            if (this.IsAnyPlayerRaise)
            {
                this.turnCount = 0;
                this.IsAnyPlayerRaise = false;
                this.raisedTurn = currentTurn;
                this.changed = true;
            }
            else
            {
                if (this.turnCount >= this.GetNotFoldedPlayersCount(this.GetAllPlayers()) - 1 || !this.changed && this.turnCount == this.GetNotFoldedPlayersCount(this.GetAllPlayers()))
                {
                    if (currentTurn == this.raisedTurn - 1 || !this.changed && this.turnCount == this.GetNotFoldedPlayersCount(this.GetAllPlayers()) || this.raisedTurn == 0 && currentTurn == 5)
                    {
                        this.changed = false;
                        this.turnCount = 0;
                        this.Raise = 0;
                        this.Call = 0;
                        this.raisedTurn = 123;
                        this.dealer.CurrentRound++;
                        //currentRound++;
                        foreach (var player in this.GetAllPlayers())
                        {
                            if (!player.FoldedTurn)
                            {
                                player.StatusLabel.Text = string.Empty;
                            }
                        }
                    }
                }
            }

            if (this.dealer.CurrentRound == CommunityCardBoard.Flop)
            {
                for (int j = 0; j <= 2; j++)
                {
                    if (this.dealer.PictureBoxHolder[j].Image != this.dealer.Cards.ElementAt(j).Image)
                    {
                        this.dealer.RevealCardAtIndex(j);
                        this.ResetCall(this.GetAllPlayers());
                        this.ResetRaise(this.GetAllPlayers());
                    }
                }
            }

            if (this.dealer.CurrentRound == CommunityCardBoard.Turn)
            {
                for (int j = 2; j <= 3; j++)
                {
                    if (this.dealer.PictureBoxHolder[j].Image != this.dealer.Cards.ElementAt(j).Image)
                    {
                        this.dealer.RevealCardAtIndex(j);
                        this.ResetCall(this.GetAllPlayers());
                        this.ResetRaise(this.GetAllPlayers());
                    }
                }
            }

            if (this.dealer.CurrentRound == CommunityCardBoard.River)
            {
                for (int j = 3; j <= 4; j++)
                {
                    if (this.dealer.PictureBoxHolder[j].Image != this.dealer.Cards.ElementAt(j).Image)
                    {
                        this.dealer.RevealCardAtIndex(j);
                        this.ResetCall(this.GetAllPlayers());
                        this.ResetRaise(this.GetAllPlayers());
                    }
                }
            }

            if (this.dealer.CurrentRound == CommunityCardBoard.End && this.GetNotFoldedPlayersCount(this.GetAllPlayers()) == 6)
            {
                await this.Finish();
                await this.Turns();
            }
        }

        public void AddChips(ICollection<IPlayer> players, int amount)
        {
            foreach (var player in players)
            {
                player.Chips += amount;
            }
        }

        private void ClearCards(ICollection<ICardHolder> cardHolders)
        {
            foreach (var cardHolder in cardHolders)
            {
                foreach (var pictureBox in cardHolder.PictureBoxHolder)
                {
                    pictureBox.Image = null;
                    pictureBox.Invalidate();
                    pictureBox.Visible = false;
                }

                cardHolder.Cards.Clear();
            }
        }

        private void FixCall(IPlayer player)
        {
            if (this.dealer.CurrentRound != CommunityCardBoard.End)
            {
                if (player.StatusLabel.Text.Contains("Raise"))
                {
                    var changeRaise = player.StatusLabel.Text.Substring(6);
                    player.RaiseAmount = int.Parse(changeRaise);
                }

                if (player.StatusLabel.Text.Contains("Call"))
                {
                    var changeCall = player.StatusLabel.Text.Substring(5);
                    player.CallAmount = int.Parse(changeCall);
                }

                if (player.StatusLabel.Text.Contains("Check"))
                {
                    this.ResetCall(new List<IPlayer>() { player });
                    this.ResetRaise(new List<IPlayer>() { player });
                }

                if (player.RaiseAmount < this.Raise)
                {
                    Call = Convert.ToInt32(Raise) - player.RaiseAmount;
                }

                if (player.CallAmount < Call)
                {
                    Call = Call - player.CallAmount;
                }

                if (player.RaiseAmount == Raise && Raise > 0)
                {
                    Call = 0;
                }
            }
        }

        private async Task AllIn()
        {
            var notFoldedPlayersCount = this.GetNotFoldedPlayersCount(this.GetAllPlayers());

            #region LastManStanding
            if (notFoldedPlayersCount == 1)
            {
                IPlayer notFoldedPlayer = this.GetAllPlayers().FirstOrDefault(p => p.HasFolded == false);
                this.MessageWriter.Write(string.Format(Messages.PlayerWinHand, notFoldedPlayer.Name));
                this.HideCardsPictureBoxes(this.GetCardHolders());
                await Finish();
            }
            
            #endregion

            #region FiveOrLessLeft
            if (notFoldedPlayersCount < 6 && notFoldedPlayersCount > 1 && this.dealer.CurrentRound >= CommunityCardBoard.End)
            {
                await Finish();
            }
            #endregion
        }

        private void HideCardsPictureBoxes(ICollection<ICardHolder> cardHolders)
        {
            foreach (var cardHolder in cardHolders)
            {
                foreach (var pictureBox in cardHolder.PictureBoxHolder)
                {
                    pictureBox.Visible = false;
                }
            }
        }

        private void ResetForNextGame(IPlayer human, ICollection<IAIPlayer> enemies)
        {
            IList<IPlayer> allPlayers = new List<IPlayer>(enemies);
            allPlayers.Add(human);

            foreach (var player in allPlayers)
            {
                player.Panel.Visible = false;
                player.Type.Power = 0;
                player.Type.Current = -1;
                player.FoldedTurn = false;
                player.HasFolded = false;
                player.IsInTurn = false;
                player.StatusLabel.Text = string.Empty;
            }

            this.ResetCall(allPlayers);
            this.ResetRaise(allPlayers);

            human.IsInTurn = true;
        }

        private async Task Finish()
        {
            this.CheckWinners(this.GetAllPlayers(), this.dealer);
            this.ResetForNextGame(this.human, this.enemies);
            this.CheckPlayerChipsAmount(this.human);
            this.ResetGameVariables();
            this.ClearCards(this.GetCardHolders());
            await this.Shuffle();
        }

        private ICollection<ICardHolder> GetCardHolders()
        {
            ICollection<ICardHolder> cardHolders = new List<ICardHolder>(this.GetAllPlayers());
            cardHolders.Add(this.dealer);

            return cardHolders;
        } 

        private void CheckPlayerChipsAmount(IPlayer player)
        {
            if (player.Chips <= 0)
            {
                AddChips addChipsDialog = new AddChips();
                addChipsDialog.ShowDialog();
                if (addChipsDialog.Amount != 0)
                {
                    this.AddChips(this.GetAllPlayers(), addChipsDialog.Amount);
                    player.FoldedTurn = false;
                    player.IsInTurn = true;
                }
            }
        }

        private void ResetGameVariables()
        {
            this.Pot.Clear();
            this.SetDefaultCall();
            this.dealer.CurrentRound = 0;
            this.Raise = 0;
            this.IsAnyPlayerRaise = false;
            this.raisedTurn = 1;
            this.turnCount = 0;
        }
    }
}
