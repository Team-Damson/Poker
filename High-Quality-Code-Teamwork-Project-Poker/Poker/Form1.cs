using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Poker.Events;
using Poker.Interfaces;
using Poker.Models;

namespace Poker
{
    using Poker.Enums;

    public partial class Form1 : Form
    {
        private const int DefaultBigBlind = 500;
        private const int DefaultSmallBlind = 250;

        HandTypes handType = new HandTypes();
        CheckHandType checkHandType = new CheckHandType();

        public event GameEngineStateEvent GameEngineStateEvent;

        private IPot pot;
        #region Variables

        private IPlayer human;

        private IDealer Dealer;
        private IDeck Deck;

        ProgressBar asd = new ProgressBar();
        private int call;
        double raise = 0;

        //CommunityCardBoard currentRound = CommunityCardBoard.Undeal;
        bool changed;
        //private int winners = 0;
        int raisedTurn = 1;

        List<Type> strongestHands = new List<Type>();
        //List<IPlayer> Winners = new List<IPlayer>();
        bool /*restart = false,*/ isAnyPlayerRaise = false;

        Poker.Type winningHand;
        Timer timer = new Timer();
        Timer Updates = new Timer();
        int secondsForHumanToPlay = 60,/* i, up = 10000000,*/ turnCount = 0;

        private int bigBlind;
        private int smallBlind;

        private IList<IPlayer> enemies;
        #endregion

        public Form1()
        {
            InitializeComponent();

            this.Deck = Models.Deck.Instance;

            this.winningHand = new Type();

            this.InitPlayers();

            this.InitDealer();

            this.bigBlind = DefaultBigBlind;
            this.smallBlind = DefaultSmallBlind;
            this.SetDefaultCall();

            this.pot = new Pot(this.textboxPot);

            MaximizeBox = false;
            MinimizeBox = false;
            Updates.Start();
            
            this.Shuffle();

            //this.UpdatePlayersChipsTextBoxes(this.GetAllPlayers());

            timer.Interval = (1000);
            timer.Tick += this.TimerTick;
            Updates.Interval = (100);
            Updates.Tick += this.UpdateTick;

            this.textboxBigBlind.Visible = false;
            this.textboxSmallBlind.Visible = false;
            this.buttonBigBlind.Visible = false;
            this.buttonSmallBlind.Visible = false;

            this.textboxRaise.Text = (this.bigBlind * 2).ToString();
        }

        /*private void UpdatePlayersChipsTextBoxes(ICollection<IPlayer> players)
        {
            foreach (var player in players)
            {
                player.ChipsTextBox.Enabled = false;
                player.ChipsTextBox.Text = AppSettigns.PlayerChipsTextBoxText + player.Chips.ToString();
            }
        }*/

        private void InitDealer()
        {
            this.Dealer = new Dealer(AppSettigns.DealerPictureBoxX, AppSettigns.DealerPictureBoxY);
            this.AddCardsPictureBox(this.Dealer);
        }

        private void InitPlayers()
        {
            this.InitHuman();
            this.InitEnemies();
        }

        private void InitHuman()
        {
            this.human = PlayerFactory.Create(PlayerType.Human, AppSettigns.FirstPlayerName, AppSettigns.DefaultChipsCount, this.labelPlayerStatus, this.textboxChipsAmount, AppSettigns.FirstPlayerAnchorStyles, AppSettigns.FirstPlayerPictureBoxX, AppSettigns.FirstPlayerPictureBoxY);
            this.AddPlayerUIComponents(human);
        }

        private void InitEnemies()
        {
            IPlayer AI1 = PlayerFactory.Create(PlayerType.AI, AppSettigns.SecondPlayerName, AppSettigns.DefaultChipsCount, this.labelBot1Status, this.textboxBot1Chips, AppSettigns.SecondPlayerAnchorStyles, AppSettigns.SecondPlayerPictureBoxX, AppSettigns.SecondPlayerPictureBoxY);
            IPlayer AI2 = PlayerFactory.Create(PlayerType.AI, AppSettigns.ThirdPlayerName, AppSettigns.DefaultChipsCount, this.labelBot2Status, this.textboxBot2Chips, AppSettigns.ThirdPlayerAnchorStyles, AppSettigns.ThirdPlayerPictureBoxX, AppSettigns.ThirdPlayerPictureBoxY);
            IPlayer AI3 = PlayerFactory.Create(PlayerType.AI, AppSettigns.FourthPlayerName, AppSettigns.DefaultChipsCount, this.labelBot3Status, this.textboxBot3Chips, AppSettigns.FourthPlayerAnchorStyles, AppSettigns.FourthPlayerPictureBoxX, AppSettigns.FourthPlayerPictureBoxY);
            IPlayer AI4 = PlayerFactory.Create(PlayerType.AI, AppSettigns.FifthPlayerName, AppSettigns.DefaultChipsCount, this.labelBot4Status, this.textboxBot4Chips, AppSettigns.FifthPlayerAnchorStyles, AppSettigns.FifthPlayerPictureBoxX, AppSettigns.FifthPlayerPictureBoxY);
            IPlayer AI5 = PlayerFactory.Create(PlayerType.AI, AppSettigns.SixthPlayerName, AppSettigns.DefaultChipsCount, this.labelBot5Status, this.textboxBot5Chips, AppSettigns.SixthPlayerAnchorStyles, AppSettigns.SixthPlayerPictureBoxX, AppSettigns.SixthPlayerPictureBoxY);

            this.enemies = new List<IPlayer>() { AI1, AI2, AI3, AI4, AI5 };
            foreach (var enemy in this.enemies)
            {
                this.AddPlayerUIComponents(enemy);
            }
        }

        private void AddPlayerUIComponents(IPlayer player)
        {
            this.Controls.Add(player.Panel);
            this.AddCardsPictureBox(player);
        }

        private void AddCardsPictureBox(ICardHolder cardHolder)
        {
            foreach (PictureBox pictureBox in cardHolder.PictureBoxHolder)
            {
                this.Controls.Add(pictureBox);
            }
        }

        private void InvokeGameEngineStateEvent(GameEngineEventArgs args)
        {
            if (this.GameEngineStateEvent != null)
            {
                this.GameEngineStateEvent(this, args);
            }
        }

        private IList<IPlayer> GetAllPlayers()
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
            this.buttonCall.Enabled = false;
            this.buttonRaise.Enabled = false;
            this.buttonFold.Enabled = false;
            this.buttonCheck.Enabled = false;
            MaximizeBox = false;
            MinimizeBox = false;

            this.InvokeGameEngineStateEvent(new GameEngineEventArgs(GameEngineState.BeginShuffling));

            await this.Deck.SetCards(this.GetAllPlayers(), this.Dealer);

            
            if (this.enemies.Count(e => !e.CanPlay()) == 5)
            {
                DialogResult dialogResult = MessageBox.Show(AppSettigns.PlayAgainMessage, AppSettigns.WinningMessage, MessageBoxButtons.YesNo);
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
            this.Run();
            this.buttonRaise.Enabled = true;
            this.buttonCall.Enabled = true;
            this.buttonFold.Enabled = true;
        }

        private void SetDefaultCall()
        {
            this.call = this.bigBlind;
        }

        private void Run()
        {
            //if (!restart)
            //{
            MaximizeBox = true;
            MinimizeBox = true;
            //}
            timer.Start();
        }

        private async Task HandleAITurn(IPlayer currentAI, IPlayer nextAI)
        {
            if (!currentAI.FoldedTurn)
            {
                if (currentAI.IsInTurn)
                {
                    FixCall(currentAI, 1);
                    FixCall(currentAI, 2);
                    Rules(currentAI);
                    MessageBox.Show(currentAI.Name + AppSettigns.PlayerTurnMessage);
                    AI(currentAI);
                    turnCount++;
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
                await CheckRaise(currentAI.Id + 1);//, currentAI.Id + 1);
                nextAI.IsInTurn = true;
            }
        }

        async Task Turns()
        {
            #region Rotating
            if (!human.FoldedTurn)
            {
                if (human.IsInTurn)
                {
                    FixCall(human, 1);
                    this.InvokeGameEngineStateEvent(new GameEngineEventArgs(GameEngineState.HumanTurn));
                    this.progressbarTimer.Visible = true;
                    this.progressbarTimer.Value = 1000;
                    secondsForHumanToPlay = 60;
                    //up = 10000000;
                    timer.Start();
                    this.buttonRaise.Enabled = true;
                    this.buttonCall.Enabled = true;
                    this.buttonRaise.Enabled = true;
                    this.buttonRaise.Enabled = true;
                    this.buttonFold.Enabled = true;
                    turnCount++;
                    FixCall(human, 2);
                }
            }
            if (human.FoldedTurn || !human.IsInTurn)
            {
                await AllIn();
                if (human.FoldedTurn && !human.HasFolded)
                {
                    if (this.buttonCall.Text.Contains("All in") == false || this.buttonRaise.Text.Contains("All in") == false)
                    {
                        //human.HasFolded = true;
                    }
                }
                await CheckRaise(0);//, 0);
                this.InvokeGameEngineStateEvent(new GameEngineEventArgs(GameEngineState.AITurn));
                this.progressbarTimer.Visible = false;
                this.buttonRaise.Enabled = false;
                this.buttonCall.Enabled = false;
                this.buttonRaise.Enabled = false;
                this.buttonRaise.Enabled = false;
                this.buttonFold.Enabled = false;
                timer.Stop();
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

                if (human.FoldedTurn && !human.HasFolded)
                {
                    if (this.buttonCall.Text.Contains("All in") == false || this.buttonRaise.Text.Contains("All in") == false)
                    {
                        //human.HasFolded = true;
                    }
                }
            #endregion

                await AllIn();
                //if (!restart)
                ////{
                await Turns();
                //}
                //restart = false;
            }
        }

        private int GetFoldedPlayersCount(ICollection<IPlayer> players)
        {
            return players.Count(p => p.HasFolded == true);
        }

        void Rules(IPlayer player)
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
                Straight1[0] = Straight[2] = this.Dealer.Cards.ElementAt(0).Power;
                Straight1[1] = Straight[3] = this.Dealer.Cards.ElementAt(1).Power;
                Straight1[2] = Straight[4] = this.Dealer.Cards.ElementAt(2).Power;
                Straight1[3] = Straight[5] = this.Dealer.Cards.ElementAt(3).Power;
                Straight1[4] = Straight[6] = this.Dealer.Cards.ElementAt(4).Power;
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
                    if (this.Deck.GetCardAtIndex(i).Power == player.Cards.First().Power && this.Deck.GetCardAtIndex(i + 1).Power == player.Cards.Last().Power)
                    {
                        //TwoPair from Hand current = 1

                        this.checkHandType.CheckPairFromHand(player, ref this.strongestHands, ref this.winningHand, this.Deck.GetCards(), i);

                        #region TwoPair or Two TwoPair from Table current = 2 || 0
                        this.checkHandType.CheckPairTwoPair(player, ref this.strongestHands, ref this.winningHand, this.Deck.GetCards(), i);

                        #endregion

                        #region Two TwoPair current = 2
                        #endregion

                        #region Three of a kind current = 3
                        this.checkHandType.CheckThreeOfAKind(player, Straight, ref this.strongestHands, ref this.winningHand);
                        #endregion

                        #region Straight current = 4
                        this.checkHandType.CheckStraight(player, Straight, ref this.strongestHands, ref this.winningHand);
                        #endregion

                        #region Flush current = 5 || 5.5
                        this.checkHandType.CheckFlush(player, ref vf, Straight1, ref this.strongestHands, ref this.winningHand, this.Deck.GetCards(), i);
                        #endregion

                        #region Full House current = 6
                        this.checkHandType.CheckFullHouse(player, ref done, Straight, ref this.strongestHands, ref this.winningHand);
                        #endregion

                        #region Four of a Kind current = 7
                        this.checkHandType.CheckFourOfAKind(player, Straight, ref this.strongestHands, ref this.winningHand);
                        #endregion

                        #region Straight Flush current = 8 || 9
                        this.checkHandType.CheckStraightFlush(player, st1, st2, st3, st4, ref this.strongestHands, ref this.winningHand);
                        #endregion

                        #region High Card current = -1
                        this.checkHandType.CheckHighCard(player, ref this.strongestHands, ref this.winningHand, this.Deck.GetCards(), i);

                        #endregion
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
                if (player.Type.Current == -1)
                {
                    MessageBox.Show(player.Name + " High Card ");
                }
                if (player.Type.Current == 1 || player.Type.Current == 0)
                {
                    MessageBox.Show(player.Name + " TwoPair ");
                }
                if (player.Type.Current == 2)
                {
                    MessageBox.Show(player.Name + " Two TwoPair ");
                }
                if (player.Type.Current == 3)
                {
                    MessageBox.Show(player.Name + " Three of a Kind ");
                }
                if (player.Type.Current == 4)
                {
                    MessageBox.Show(player.Name + " Straight ");
                }
                if (player.Type.Current == 5 || player.Type.Current == 5.5)
                {
                    MessageBox.Show(player.Name + " Flush ");
                }
                if (player.Type.Current == 6)
                {
                    MessageBox.Show(player.Name + " Full House ");
                }
                if (player.Type.Current == 7)
                {
                    MessageBox.Show(player.Name + " Four of a Kind ");
                }
                if (player.Type.Current == 8)
                {
                    MessageBox.Show(player.Name + " Straight Flush ");
                }
                if (player.Type.Current == 9)
                {
                    MessageBox.Show(player.Name + " Royal Flush ! ");
                }
            }
        }

        private void SetWinnersChips(ICollection<IPlayer> players)
        {
            foreach (var player in players)
            {
                player.Chips += this.pot.Amount / players.Count;
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

            //if (this.GetNotFoldedPlayersCount(this.GetAllPlayers()) != 1)
            //{
            //    this.ShowWinnersMessages(winners);
            //}

            this.ShowWinnersMessages(winners);
            
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
            if (isAnyPlayerRaise)
            {
                turnCount = 0;
                isAnyPlayerRaise = false;
                raisedTurn = currentTurn;
                changed = true;
            }
            else
            {
                if (turnCount >= this.GetNotFoldedPlayersCount(this.GetAllPlayers()) - 1 || !changed && turnCount == this.GetNotFoldedPlayersCount(this.GetAllPlayers()))
                {
                    if (currentTurn == raisedTurn - 1 || !changed && turnCount == this.GetNotFoldedPlayersCount(this.GetAllPlayers()) || raisedTurn == 0 && currentTurn == 5)
                    {
                        changed = false;
                        turnCount = 0;
                        raise = 0;
                        call = 0;
                        raisedTurn = 123;
                        this.Dealer.CurrentRound++;
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

            if (this.Dealer.CurrentRound == CommunityCardBoard.Flop)
            {
                for (int j = 0; j <= 2; j++)
                {
                    if (this.Dealer.PictureBoxHolder[j].Image != this.Dealer.Cards.ElementAt(j).Image)
                    {
                        this.Dealer.RevealCardAtIndex(j);
                        this.ResetCall(this.GetAllPlayers());
                        this.ResetRaise(this.GetAllPlayers());
                    }
                }
            }
            if (this.Dealer.CurrentRound == CommunityCardBoard.Turn)
            {
                for (int j = 2; j <= 3; j++)
                {
                    if (this.Dealer.PictureBoxHolder[j].Image != this.Dealer.Cards.ElementAt(j).Image)
                    {
                        this.Dealer.RevealCardAtIndex(j);
                        this.ResetCall(this.GetAllPlayers());
                        this.ResetRaise(this.GetAllPlayers());
                    }
                }
            }
            if (this.Dealer.CurrentRound == CommunityCardBoard.River)
            {
                for (int j = 3; j <= 4; j++)
                {
                    if (this.Dealer.PictureBoxHolder[j].Image != this.Dealer.Cards.ElementAt(j).Image)
                    {
                        this.Dealer.RevealCardAtIndex(j);
                        this.ResetCall(this.GetAllPlayers());
                        this.ResetRaise(this.GetAllPlayers());
                    }
                }
            }
            if (this.Dealer.CurrentRound == CommunityCardBoard.End && this.GetNotFoldedPlayersCount(this.GetAllPlayers()) == 6)
            {
                /*foreach (var player in this.GetAllPlayers())
                {
                    Rules(player);
                }
                this.CheckWinners(this.GetAllPlayers(), this.Dealer);
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
                this.pot.Clear();
                this.human.StatusLabel.Text = string.Empty;
                await Shuffle();*/
                await this.Finish(2);
                await Turns();
            }
        }

        private void AddChips(ICollection<IPlayer> players, int amount)
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
            if (this.Dealer.CurrentRound != CommunityCardBoard.End)
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
                    if (player.RaiseAmount < this.raise)//!= RaiseAmount && player.RaiseAmount <= RaiseAmount)
                    {
                        //player.CallAmount = Convert.ToInt32(RaiseAmount) - player.RaiseAmount;
                        call = Convert.ToInt32(raise) - player.RaiseAmount;
                    }

                    if (player.CallAmount < call)//!= call || player.CallAmount <= call)
                    {
                        call = call - player.CallAmount;
                    }

                    if (player.RaiseAmount == raise && raise > 0)
                    {
                        //call = (int)RaiseAmount;
                        call = 0;
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
            if (human.Chips <= 0)
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
                //notFoldedPlayer.Chips += this.pot.Amount;
                //notFoldedPlayer.ChipsTextBox.Text = notFoldedPlayer.Chips.ToString();
                //notFoldedPlayer.Panel.Visible = false;
                MessageBox.Show(notFoldedPlayer.Name + " Wins");
                
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

                foreach (var pictureBox in this.Dealer.PictureBoxHolder)
                {
                    pictureBox.Visible = false;
                }*/
                this.HideCardsPictureBoxes(this.GetCardHolders());
                await Finish(1);
            }
            
            #endregion

            #region FiveOrLessLeft
            if (notFoldedPlayersCount < 6 && notFoldedPlayersCount > 1 && this.Dealer.CurrentRound >= CommunityCardBoard.End)
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

        private void ResetForNextGame(IPlayer human, ICollection<IPlayer> enemies)
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

            //restart = false; isAnyPlayerRaise = false;
            //winners = 0;
            //raisedTurn = 1;
            //Winners.Clear();
            //this.strongestHands.Clear();
            //this.winningHand.Current = 0;
            //this.winningHand.Power = 0;
            //this.pot.Clear();
            //secondsForHumanToPlay = 60; up = 10000000; turnCount = 0;

            //foreach (var player in this.GetAllPlayers())
            //{
            //    player.StatusLabel.Text = string.Empty;
            //}

            this.CheckPlayerChipsAmount(this.human);
            this.ResetGameVariables();
            this.ClearCards(this.GetCardHolders());
            await Shuffle();
            //await Turns();
        }

        private ICollection<ICardHolder> GetCardHolders()
        {
            ICollection<ICardHolder> cardHolders = new List<ICardHolder>(this.GetAllPlayers());
            cardHolders.Add(this.Dealer);

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
                    this.buttonRaise.Enabled = true;
                    this.buttonFold.Enabled = true;
                    this.buttonCheck.Enabled = true;
                    this.buttonRaise.Text = "Raise";
                }
            }
        }

        private void ResetGameVariables()
        {
            this.strongestHands.Clear();
            this.winningHand.Current = 0;
            this.winningHand.Power = 0;
            this.pot.Clear();
            this.SetDefaultCall();
            this.Dealer.CurrentRound = 0;
            raise = 0;
            //restart = false;
            isAnyPlayerRaise = false;
            //winners = 0;
            raisedTurn = 1;
            //Winners.Clear();

            secondsForHumanToPlay = 60; 
            //up = 10000000; 
            turnCount = 0;
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
                    Rules(player);
                }
            }

            this.CheckWinners(this.GetAllPlayers(), this.Dealer);
        }

        void AI(IPlayer player)
        {
            if (!player.FoldedTurn)
            {
                //switch (player.Type.Current)
                //{
                //    case PokerHand.HighCard:
                //        handType.HighCard(player, call, textboxPot, ref RaiseAmount, ref isAnyPlayerRaise);
                //        break;
                //    case PokerHand.PairTable:
                //        handType.PairTable(player, call, textboxPot, ref RaiseAmount, ref isAnyPlayerRaise);
                //        break;
                //    case PokerHand.PairFromHand:
                //        handType.PairHand(player, call, textboxPot, ref RaiseAmount, ref isAnyPlayerRaise, currentRound);
                //        break;
                //    case PokerHand.TwoPair:
                //        handType.TwoPair(player, call, textboxPot, ref RaiseAmount, ref isAnyPlayerRaise, currentRound);
                //        break;
                //    case PokerHand.ThreeOfAKind:
                //        handType.ThreeOfAKind(player, call, textboxPot, ref RaiseAmount, ref isAnyPlayerRaise);
                //        break;
                //    case PokerHand.Straigth:
                //        handType.Straight(player, call, textboxPot, ref RaiseAmount, ref isAnyPlayerRaise);
                //        break;
                //    case PokerHand.Flush:
                //    case PokerHand.FlushWithAce:
                //        handType.Flush(player, call, textboxPot, ref RaiseAmount, ref isAnyPlayerRaise);
                //        break;
                //    case PokerHand.FullHouse:
                //        handType.FullHouse(player, call, textboxPot, ref RaiseAmount, ref isAnyPlayerRaise);
                //        break;
                //    case PokerHand.FourOfAKind:
                //        handType.FourOfAKind(player, call, textboxPot, ref RaiseAmount, ref isAnyPlayerRaise);
                //        break;
                //    case PokerHand.StraightFlush:
                //    case PokerHand.RoyalFlush:
                //        handType.StraightFlush(player, call, textboxPot, ref RaiseAmount, ref isAnyPlayerRaise);
                //        break;
                //    default:
                //        throw new InvalidOperationException("Invalid Pocker Hand");
                //}


                if (player.Type.Current == PokerHand.HighCard)
                {
                    handType.HighCard(player, call, this.pot, ref raise, ref isAnyPlayerRaise);
                }

                if (player.Type.Current == PokerHand.PairTable)
                {
                    handType.PairTable(player, call, this.pot, ref raise, ref isAnyPlayerRaise);
                }

                if (player.Type.Current == PokerHand.PairFromHand)
                {
                    handType.PairHand(player, call, this.pot, ref raise, ref isAnyPlayerRaise, this.Dealer.CurrentRound);
                }

                if (player.Type.Current == PokerHand.TwoPair)
                {
                    handType.TwoPair(player, call, this.pot, ref raise, ref isAnyPlayerRaise, this.Dealer.CurrentRound);
                }

                if (player.Type.Current == PokerHand.ThreeOfAKind)
                {
                    handType.ThreeOfAKind(player, call, this.pot, ref raise, ref isAnyPlayerRaise);
                }

                if (player.Type.Current == PokerHand.Straigth)
                {
                    handType.Straight(player, call, this.pot, ref raise, ref isAnyPlayerRaise);
                }

                if (player.Type.Current == PokerHand.Flush || player.Type.Current == PokerHand.FlushWithAce)
                {
                    handType.Flush(player, call, this.pot, ref raise, ref isAnyPlayerRaise);
                }

                if (player.Type.Current == PokerHand.FullHouse)
                {
                    handType.FullHouse(player, call, this.pot, ref raise, ref isAnyPlayerRaise);
                }

                if (player.Type.Current == PokerHand.FourOfAKind)
                {
                    handType.FourOfAKind(player, call, this.pot, ref raise, ref isAnyPlayerRaise);
                }

                if (player.Type.Current == PokerHand.StraightFlush || player.Type.Current == PokerHand.RoyalFlush)
                {
                    handType.StraightFlush(player, call, this.pot, ref raise, ref isAnyPlayerRaise);
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
  

        #region UI
        private async void TimerTick(object sender, object e)
        {
            if (this.progressbarTimer.Value <= 0)
            {
                this.human.FoldedTurn = true;
                await this.Turns();
            }

            if (secondsForHumanToPlay > 0)
            {
                secondsForHumanToPlay--;
                this.progressbarTimer.Value = (secondsForHumanToPlay / 6) * 100;
            }
        }

        private void UpdateTick(object sender, object e)
        {
            //this.UpdatePlayersChipsTextBoxes(this.GetAllPlayers());

            if (this.human.Chips <= 0)
            {
                this.human.IsInTurn = false;
                this.human.FoldedTurn = true;
                this.buttonCall.Enabled = false;
                this.buttonRaise.Enabled = false;
                this.buttonFold.Enabled = false;
                this.buttonCheck.Enabled = false;
            }

            if (this.human.Chips >= call)
            {
                this.buttonCall.Text = "Call " + call;
            }
            else
            {
                this.buttonCall.Text = "All in";
                this.buttonRaise.Enabled = false;
            }

            if (call > 0)
            {
                this.buttonCheck.Enabled = false;
            }

            if (call <= 0)
            {
                this.buttonCheck.Enabled = true;
                this.buttonCall.Text = "Call";
                this.buttonCall.Enabled = false;
            }

            if (this.human.Chips <= 0)
            {
                this.buttonRaise.Enabled = false;
            }

            int parsedValue;

            if (this.textboxRaise.Text != "" && int.TryParse(this.textboxRaise.Text, out parsedValue))
            {
                if (this.human.Chips <= int.Parse(this.textboxRaise.Text))
                {
                    this.buttonRaise.Text = "All in";
                }
                else
                {
                    this.buttonRaise.Text = "Raise";
                }
            }

            if (this.human.Chips < call)
            {
                this.buttonRaise.Enabled = false;
            }
        }

        private async void OnFoldClick(object sender, EventArgs e)
        {
            this.human.Fold();
            //this.human.StatusLabel.Text = "Fold";
            //this.human.IsInTurn = false;
            //this.human.FoldedTurn = true;
            await this.Turns();
        }

        private async void OnCheckClick(object sender, EventArgs e)
        {
            if (call <= 0)
            {
                this.human.Check();
                //this.human.IsInTurn = false;
                //this.human.StatusLabel.Text = "Check";
            }
            else
            {
                //labelPlayerStatus.Text = "All in " + Chips;

                this.buttonCheck.Enabled = false;
            }

            await this.Turns();
        }

        private async void OnCallClick(object sender, EventArgs e)
        {
            this.Rules(this.human);

            if (this.human.Chips >= call)
            {
                this.human.Call(call);
                //this.human.Chips -= call;
                //this.textboxChipsAmount.Text = AppSettigns.PlayerChipsTextBoxText + this.human.Chips.ToString();
                this.pot.Add(call);
                //this.human.IsInTurn = false;
                //this.human.StatusLabel.Text = "Call " + call;
                //this.human.CallAmount = call;
            }
            else if (this.human.Chips <= call && call > 0)
            {
                this.pot.Add(this.human.Chips);
                this.human.AllIn();
                //this.human.StatusLabel.Text = "All in " + this.human.Chips;
                //this.human.Chips = 0;
                //this.textboxChipsAmount.Text = AppSettigns.PlayerChipsTextBoxText + this.human.Chips.ToString();
                //this.human.IsInTurn = false;
                this.buttonFold.Enabled = false;
                //this.human.CallAmount = this.human.Chips;
            }

            await this.Turns();
        }

        private async void OnRaiseClick(object sender, EventArgs e)
        {
            this.Rules(this.human);
            int parsedValue;

            if (this.textboxRaise.Text != string.Empty && int.TryParse(this.textboxRaise.Text, out parsedValue))
            {
                if (this.human.Chips > call)
                {
                    if (raise * 2 > int.Parse(this.textboxRaise.Text))
                    {
                        this.textboxRaise.Text = (raise * 2).ToString();
                        MessageBox.Show("You must raise atleast twice as the current raise !");
                        return;
                    }
                    else
                    {
                        if (this.human.Chips >= int.Parse(this.textboxRaise.Text))
                        {
                            call = int.Parse(this.textboxRaise.Text);
                            raise = int.Parse(this.textboxRaise.Text);
                            //this.human.StatusLabel.Text = "Raise " + call;
                            this.pot.Add(call);
                            this.buttonCall.Text = "Call";
                            this.human.Raise((int)raise);
                            //this.human.Chips -= int.Parse(this.textboxRaise.Text);
                            isAnyPlayerRaise = true;
                            //this.human.Raise = Convert.ToInt32(Raise);
                        }
                        else
                        {
                            call = this.human.Chips;
                            raise = this.human.Chips;
                            this.pot.Add(this.human.Chips);
                            this.human.Raise(this.human.Chips);
                            //this.human.StatusLabel.Text = "Raise " + call;
                            //this.human.Chips = 0;
                            isAnyPlayerRaise = true;
                            //this.human.Raise = Convert.ToInt32(Raise);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("This is a number only field");
                return;
            }

            this.human.IsInTurn = false;
            await this.Turns();
        }

        private void OnAddClick(object sender, EventArgs e)
        {
            if (this.textboxPlayerChips.Text != string.Empty)
            {
                int chipsToAdd;
                int.TryParse(this.textboxPlayerChips.Text, out chipsToAdd);

                this.AddChips(this.GetAllPlayers(), chipsToAdd);

                //this.UpdatePlayersChipsTextBoxes(this.GetAllPlayers());
            }
        }

        private void OnBlindOptionsClick(object sender, EventArgs e)
        {
            this.textboxBigBlind.Text = this.bigBlind.ToString();
            this.textboxSmallBlind.Text = this.smallBlind.ToString();

            if (this.textboxBigBlind.Visible == false)
            {
                this.textboxBigBlind.Visible = true;
                this.textboxSmallBlind.Visible = true;
                this.buttonBigBlind.Visible = true;
                this.buttonSmallBlind.Visible = true;
            }
            else
            {
                this.textboxBigBlind.Visible = false;
                this.textboxSmallBlind.Visible = false;
                this.buttonBigBlind.Visible = false;
                this.buttonSmallBlind.Visible = false;
            }
        }

        private void OnSmallBlindClick(object sender, EventArgs e)
        {
            var minSmallBlind = 250;
            var maxSmallBlind = 100000;
            int parsedValue;

            if (this.textboxSmallBlind.Text.Contains(",") || this.textboxSmallBlind.Text.Contains("."))
            {
                var message = "The Small Blind can be only round number !";
                MessageBox.Show(message);
                this.textboxSmallBlind.Text = this.smallBlind.ToString();
                return;
            }

            if (!int.TryParse(this.textboxSmallBlind.Text, out parsedValue))
            {
                var message = "This is a number only field";
                MessageBox.Show(message);
                this.textboxSmallBlind.Text = this.smallBlind.ToString();
                return;
            }

            if (int.Parse(this.textboxSmallBlind.Text) > maxSmallBlind)
            {
                var message = "The maximum of the Small Blind is 100 000 $";
                MessageBox.Show(message);
                this.textboxSmallBlind.Text = this.smallBlind.ToString();
            }

            if (int.Parse(this.textboxSmallBlind.Text) < minSmallBlind)
            {
                var message = "The minimum of the Small Blind is 250 $";
                MessageBox.Show(message);
            }

            if (int.Parse(this.textboxSmallBlind.Text) >= minSmallBlind && int.Parse(this.textboxSmallBlind.Text) <= maxSmallBlind)
            {
                this.smallBlind = int.Parse(this.textboxSmallBlind.Text);
                var message = "The changes have been saved ! They will become available the next hand you play.";
                MessageBox.Show(message);
            }
        }

        private void OnBigBlindClick(object sender, EventArgs e)
        {
            var minBigBlind = 500;
            var maxBigBlind = 200000;
            int parsedValue;

            if (this.textboxBigBlind.Text.Contains(",") || this.textboxBigBlind.Text.Contains("."))
            {
                var message = "The Big Blind can be only round number!";
                MessageBox.Show(message);
                this.textboxBigBlind.Text = this.bigBlind.ToString();
                return;
            }

            if (!int.TryParse(this.textboxSmallBlind.Text, out parsedValue))
            {
                var message = "This is a number only field";
                MessageBox.Show(message);
                this.textboxSmallBlind.Text = this.bigBlind.ToString();
                return;
            }

            if (int.Parse(this.textboxBigBlind.Text) > maxBigBlind)
            {
                var message = "The maximum of the Big Blind is 200 000";
                MessageBox.Show(message);
                this.textboxBigBlind.Text = this.bigBlind.ToString();
            }

            if (int.Parse(this.textboxBigBlind.Text) < minBigBlind)
            {
                var message = "The minimum of the Big Blind is 500 $";
                MessageBox.Show(message);
            }

            if (int.Parse(this.textboxBigBlind.Text) >= minBigBlind && int.Parse(this.textboxBigBlind.Text) <= maxBigBlind)
            {
                this.bigBlind = int.Parse(this.textboxBigBlind.Text);
                var message = "The changes have been saved ! They will become available the next hand you play.";
                MessageBox.Show(message);
            }
        }
        #endregion
    }
}