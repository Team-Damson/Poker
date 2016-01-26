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
        //public const int DefaultBigBlind = 500;
        //public const int DefaultSmallBlind = 250;

        //private HandTypes handType = new HandTypes();
        private CheckHandType checkHandType = new CheckHandType();
        private IPlayer human;
        private IDealer dealer;
        private IDeck deck;
        private double raise;
        private IList<IAIPlayer> enemies;
        private bool changed;
        private int raisedTurn = 1;
        private List<Type> strongestHands = new List<Type>();
        private bool isAnyPlayerRaise;
        private Type winningHand;
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


        public GameEngine(IPlayer human, ICollection<IAIPlayer> enemies, IPot pot, IDealer dealer, IDeck deck, IMessageWriter messageWriter)
        {
            this.human = human;
            this.enemies = new List<IAIPlayer>(enemies);
            this.Pot = pot;
            this.dealer = dealer;
            this.deck = deck;
            this.MessageWriter = messageWriter;
            this.winningHand = new Type();
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

        async Task Shuffle()
        {
            this.InvokeGameEngineStateEvent(new GameEngineEventArgs(GameEngineState.BeginShuffling));

            await this.deck.SetCards(this.GetAllPlayers(), this.dealer);
            
            if (this.enemies.Count(e => !e.CanPlay()) == 5)
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

            this.InvokeGameEngineStateEvent(new GameEngineEventArgs(GameEngineState.EndShuffling));
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
                    this.FixCall(currentAI, 1);
                    this.FixCall(currentAI, 2);
                    this.Rules(currentAI);
                    this.MessageWriter.Write(string.Format(Messages.PlayerTurn, currentAI.Name));
                    currentAI.ProccessNextTurn(Call, this.Pot, ref raise, ref isAnyPlayerRaise, this.dealer.CurrentRound);
                    //this.AI(currentAI);
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
            #region Rotating
            if (!this.human.FoldedTurn)
            {
                if (this.human.IsInTurn)
                {
                    this.FixCall(this.human, 1);
                    this.InvokeGameEngineStateEvent(new GameEngineEventArgs(GameEngineState.HumanTurn));
                    this.turnCount++;
                    this.FixCall(this.human, 2);
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
            #endregion

                await this.AllIn();
                //if (!restart)
                //{
                await this.Turns();
                //}
                //restart = false;
            }
        }

        private int GetFoldedPlayersCount(ICollection<IPlayer> players)
        {
            return players.Count(p => p.HasFolded == true);
        }

        public void Rules(IPlayer player)
        {
            if (!player.FoldedTurn)// || player.CardIndexes.First() == 0 && player.CardIndexes.Last() == 1 && this.labelPlayerStatus.Text.Contains("Fold") == false)
            {
                #region Variables

                bool done = false;
                bool vf = false;
                int[] Straight1 = new int[5];
                int[] Straight = new int[7];
                Straight[0] = player.Cards.First().Power;
                Straight[1] = player.Cards.Last().Power;
                Straight1[0] = Straight[2] = this.dealer.Cards.ElementAt(0).Power;
                Straight1[1] = Straight[3] = this.dealer.Cards.ElementAt(1).Power;
                Straight1[2] = Straight[4] = this.dealer.Cards.ElementAt(2).Power;
                Straight1[3] = Straight[5] = this.dealer.Cards.ElementAt(3).Power;
                Straight1[4] = Straight[6] = this.dealer.Cards.ElementAt(4).Power;
                var a = Straight.Where(o => o % 4 == 0).ToArray();
                var b = Straight.Where(o => o % 4 == 1).ToArray();
                var c = Straight.Where(o => o % 4 == 2).ToArray();
                var d = Straight.Where(o => o % 4 == 3).ToArray();
                var st1 = a.Select(o => o / 4).Distinct().ToArray();
                var st2 = b.Select(o => o / 4).Distinct().ToArray();
                var st3 = c.Select(o => o / 4).Distinct().ToArray();
                var st4 = d.Select(o => o / 4).Distinct().ToArray();
                Array.Sort(Straight);
                Array.Sort(st1);
                Array.Sort(st2);
                Array.Sort(st3);
                Array.Sort(st4);

                #endregion

                for (int i = 0; i < 16; i++)
                {
                    if (this.deck.GetCardAtIndex(i).Power == player.Cards.First().Power && this.deck.GetCardAtIndex(i + 1).Power == player.Cards.Last().Power)
                    {
                        // TwoPair from Hand
                        this.checkHandType.CheckPairFromHand(player, ref this.strongestHands, ref this.winningHand, this.deck.GetCards(), i);

                        // TwoPair or Two TwoPair from Table
                        this.checkHandType.CheckPairTwoPair(player, ref this.strongestHands, ref this.winningHand, this.deck.GetCards(), i);
                        
                        // Three of a kind
                        this.checkHandType.CheckThreeOfAKind(player, Straight, ref this.strongestHands, ref this.winningHand);
                        
                        // Straight
                        this.checkHandType.CheckStraight(player, Straight, ref this.strongestHands, ref this.winningHand);
                        
                        // Flush current
                        this.checkHandType.CheckFlush(player, ref vf, Straight1, ref this.strongestHands, ref this.winningHand, this.deck.GetCards(), i);
                        
                        // Full House
                        this.checkHandType.CheckFullHouse(player, ref done, Straight, ref this.strongestHands, ref this.winningHand);
                        
                        // Four of a Kind
                        this.checkHandType.CheckFourOfAKind(player, Straight, ref this.strongestHands, ref this.winningHand);
                       
                        // Straight Flush
                        this.checkHandType.CheckStraightFlush(player, st1, st2, st3, st4, ref this.strongestHands, ref this.winningHand);
                        
                        // High Card
                        this.checkHandType.CheckHighCard(player, ref this.strongestHands, ref this.winningHand, this.deck.GetCards(), i);
                    }
                }
            }
        }

        private ICollection<IPlayer> GetWinners(ICollection<IPlayer> players)
        {
            ICollection<IPlayer> winners = new List<IPlayer>();

            foreach (var player in players)
            {
                if (!player.HasFolded)
                {
                    if ((player.Type.Current == this.winningHand.Current && player.Type.Power == this.winningHand.Power) || this.GetNotFoldedPlayersCount(this.GetAllPlayers()) == 1)
                    {
                        //if (player.Type.Power == this.winningHand.Power)
                        //{
                        winners.Add(player);
                        //}
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
                    this.MessageWriter.Write(player.Name + " TwoPair ");
                }

                if (player.Type.Current == PokerHand.TwoPair)
                {
                    this.MessageWriter.Write(player.Name + " Two TwoPair ");
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
                player.ChipsTextBox.Text = player.Chips.ToString();
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

                //this.CheckWinner(player);
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

        async Task CheckRaise(int currentTurn)//, int raiseTurn)
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
                /*foreach (var player in this.GetAllPlayers())
                {
                    Rules(player);
                }
                this.CheckWinners(this.GetAllPlayers(), this.dealer);
                restart = true;
                this.ResetForNextGame(this.human, this.enemies);
                this.CheckPlayerChipsAmount(this.human);
                this.SetDefaultCall();
                RaiseAmount = 0;
                currentRound = 0;
                //Winners.Clear();
                //winners = 0;
                this.strongestHands.Clear();
                this.winningHand.Current = 0;
                this.winningHand.Power = 0;
                this.ClearCards(this.GetCardHolders());
                this.Pot.Clear();
                this.human.StatusLabel.Text = string.Empty;
                await Shuffle();*/
                await this.Finish(2);
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

        void FixCall(IPlayer player, int options)
        {
            if (this.dealer.CurrentRound != CommunityCardBoard.End)
            {
                if (options == 1)
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
                        //player.RaiseAmount = 0;
                        this.ResetCall(new List<IPlayer>() { player });
                        this.ResetRaise(new List<IPlayer>() { player });
                        //player.CallAmount = 0;
                    }
                }

                if (options == 2)
                {
                    if (player.RaiseAmount < this.Raise)//!= RaiseAmount && player.RaiseAmount <= RaiseAmount)
                    {
                        //player.CallAmount = Convert.ToInt32(RaiseAmount) - player.RaiseAmount;
                        Call = Convert.ToInt32(Raise) - player.RaiseAmount;
                    }

                    if (player.CallAmount < Call)//!= Call || player.CallAmount <= Call)
                    {
                        Call = Call - player.CallAmount;
                    }

                    if (player.RaiseAmount == Raise && Raise > 0)
                    {
                        //Call = (int)RaiseAmount;
                        Call = 0;
                        //this.buttonCall.Enabled = false;
                        //this.buttonCall.Text = "Callisfuckedup";
                    }
                }
            }
        }

        async Task AllIn()
        {
            #region All in
            int allInPlayersCount = 0;
            if (this.human.Chips <= 0)
            {
                if (this.human.StatusLabel.Text.Contains("Raise") || this.human.StatusLabel.Text.Contains("Call"))
                {
                    allInPlayersCount++;
                }
            }

            foreach (var enemy in this.enemies)
            {
                if (enemy.Chips <= 0 && !enemy.FoldedTurn)
                {
                    allInPlayersCount++;
                }
            }
            
            if (allInPlayersCount == this.GetNotFoldedPlayersCount(this.GetAllPlayers()))
            {
                //await Finish(2);
            }
            #endregion

            var notFoldedPlayersCount = this.GetNotFoldedPlayersCount(this.GetAllPlayers());

            #region LastManStanding
            if (notFoldedPlayersCount == 1)
            {
                IPlayer notFoldedPlayer = this.GetAllPlayers().FirstOrDefault(p => p.HasFolded == false);
                //notFoldedPlayer.Chips += this.Pot.Amount;
                //notFoldedPlayer.ChipsTextBox.Text = notFoldedPlayer.Chips.ToString();
                //notFoldedPlayer.Panel.Visible = false;
                this.MessageWriter.Write(string.Format(Messages.PlayerWinHand, notFoldedPlayer.Name));
                
                /*foreach (var pictureBox in this.human.PictureBoxHolder)
                {
                    pictureBox.Visible = false;
                }

                foreach (var player in this.enemies)
                {
                    foreach (var pictureBox in player.PictureBoxHolder)
                    {
                        pictureBox.Visible = false;
                    }
                }

                foreach (var pictureBox in this.dealer.PictureBoxHolder)
                {
                    pictureBox.Visible = false;
                }*/
                this.HideCardsPictureBoxes(this.GetCardHolders());
                await Finish(1);
            }
            
            #endregion

            #region FiveOrLessLeft
            if (notFoldedPlayersCount < 6 && notFoldedPlayersCount > 1 && this.dealer.CurrentRound >= CommunityCardBoard.End)
            {
                await Finish(2);
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
                //player.CallAmount = 0;
                //player.RaiseAmount = 0;
                player.FoldedTurn = false;
                player.HasFolded = false;
                player.IsInTurn = false;
                player.StatusLabel.Text = string.Empty;
            }

            this.ResetCall(allPlayers);
            this.ResetRaise(allPlayers);

            human.IsInTurn = true;
        }

        async Task Finish(int n)
        {
            //if (n == 2)
            //{
            FixWinners();
            //}

            this.ResetForNextGame(this.human, this.enemies);

            //this.SetDefaultCall();
            //RaiseAmount = 0; 
            //currentRound = 0;
            
            //RaiseAmount = 0;

            //restart = false; IsAnyPlayerRaise = false;
            //winners = 0;
            //raisedTurn = 1;
            //Winners.Clear();
            //this.strongestHands.Clear();
            //this.winningHand.Current = 0;
            //this.winningHand.Power = 0;
            //this.Pot.Clear();
            //secondsForHumanToPlay = 60; up = 10000000; turnCount = 0;

            //foreach (var player in this.GetAllPlayers())
            //{
            //    player.StatusLabel.Text = string.Empty;
            //}

            this.CheckPlayerChipsAmount(this.human);
            this.ResetGameVariables();
            this.ClearCards(this.GetCardHolders());
            await this.Shuffle();
            //await Turns();
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
                    //this.buttonRaise.Enabled = true;
                    //this.buttonFold.Enabled = true;
                    //this.buttonCheck.Enabled = true;
                    //this.buttonRaise.Text = "Raise";
                }
            }
        }

        private void ResetGameVariables()
        {
            this.strongestHands.Clear();
            this.winningHand.Current = 0;
            this.winningHand.Power = 0;
            this.Pot.Clear();
            this.SetDefaultCall();
            this.dealer.CurrentRound = 0;
            this.Raise = 0;
            //restart = false;
            this.IsAnyPlayerRaise = false;
            //winners = 0;
            this.raisedTurn = 1;
            //Winners.Clear();

            //up = 10000000; 
            this.turnCount = 0;
        }

        void FixWinners()
        {
            //this.strongestHands.Clear();
            //this.winningHand.Current = 0;
            //this.winningHand.Power = 0;
            foreach (var player in this.GetAllPlayers())
            {
                if (!player.HasFolded)
                {
                    this.Rules(player);
                }
            }

            this.CheckWinners(this.GetAllPlayers(), this.dealer);
        }

        /*void AI(IPlayer player)
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
                    this.handType.HighCard(player, Call, this.Pot, ref raise, ref isAnyPlayerRaise);
                }

                if (player.Type.Current == PokerHand.PairTable)
                {
                    this.handType.PairTable(player, Call, this.Pot, ref raise, ref isAnyPlayerRaise);
                }

                if (player.Type.Current == PokerHand.PairFromHand)
                {
                    this.handType.PairHand(player, Call, this.Pot, ref raise, ref isAnyPlayerRaise, this.dealer.CurrentRound);
                }

                if (player.Type.Current == PokerHand.TwoPair)
                {
                    this.handType.TwoPair(player, Call, this.Pot, ref raise, ref isAnyPlayerRaise, this.dealer.CurrentRound);
                }

                if (player.Type.Current == PokerHand.ThreeOfAKind)
                {
                    this.handType.ThreeOfAKind(player, Call, this.Pot, ref raise, ref isAnyPlayerRaise);
                }

                if (player.Type.Current == PokerHand.Straigth)
                {
                    this.handType.Straight(player, Call, this.Pot, ref raise, ref isAnyPlayerRaise);
                }

                if (player.Type.Current == PokerHand.Flush || player.Type.Current == PokerHand.FlushWithAce)
                {
                    this.handType.Flush(player, Call, this.Pot, ref raise, ref isAnyPlayerRaise);
                }

                if (player.Type.Current == PokerHand.FullHouse)
                {
                    this.handType.FullHouse(player, Call, this.Pot, ref raise, ref isAnyPlayerRaise);
                }

                if (player.Type.Current == PokerHand.FourOfAKind)
                {
                    this.handType.FourOfAKind(player, Call, this.Pot, ref raise, ref isAnyPlayerRaise);
                }

                if (player.Type.Current == PokerHand.StraightFlush || player.Type.Current == PokerHand.RoyalFlush)
                {
                    this.handType.StraightFlush(player, Call, this.Pot, ref raise, ref isAnyPlayerRaise);
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
        }*/
    }
}
