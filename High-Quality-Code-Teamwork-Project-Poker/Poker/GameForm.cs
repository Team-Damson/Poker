namespace Poker
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using Poker.Enums;
    using Poker.Events;
    using Poker.Interfaces;
    using Poker.MessageWriters;
    using Poker.Models;

    public partial class GameForm : Form
    {
        private readonly Timer timer = new Timer();
        private readonly Timer updates = new Timer();
        private readonly IGameEngine engine;
        private readonly IMessageWriter messageWriter;
        private int secondsForHumanToPlay;

        public GameForm()
        {
            this.InitializeComponent();

            this.timer.Interval = 1000;
            this.timer.Tick += this.TimerTick;
            this.updates.Interval = 100;
            this.updates.Tick += this.UpdateTick;

            this.textboxBigBlind.Visible = false;
            this.textboxSmallBlind.Visible = false;
            this.buttonBigBlind.Visible = false;
            this.buttonSmallBlind.Visible = false;
            this.textboxRaise.Text = (AppSettigns.DefaultMinBigBlind * 2).ToString();

            IPlayer human = this.GetHumanPlayer();
            IAILogicProvider logicProvider = new AILogicProvider();
            ICollection<IAIPlayer> enemies = this.GetEnemies(logicProvider);
            IPot pot = new Pot(this.textboxPot);
            IDealer dealer = this.GetDealer();
            IDeck deck = Deck.Instance;
            this.messageWriter = new MessageBoxWriter();
            IHandTypeHandler handTypeHandler = new HandTypeHandler();

            this.engine = new GameEngine(human, enemies, pot, dealer, deck, this.messageWriter, handTypeHandler);
            this.engine.GameEngineStateEvent += this.ChangeGameEngineStateHandler;
            this.updates.Start();
            this.engine.Run();
        }

        private void ChangeGameEngineStateHandler(object sender, GameEngineEventArgs args)
        {
            switch (args.GameState)
            {
                case GameEngineState.AITurn:
                case GameEngineState.BeginShuffling:
                    this.DisableButtons();
                    break;
                case GameEngineState.HumanTurn:
                case GameEngineState.EndShuffling:
                    this.EnableButtons();
                    break;
            }
        }

        private void DisableButtons()
        {
            this.timer.Stop();
            this.progressbarTimer.Visible = false;
            this.buttonCall.Enabled = false;
            this.buttonRaise.Enabled = false;
            this.buttonFold.Enabled = false;
            this.buttonCheck.Enabled = false;
            //this.MaximizeBox = false;
            //this.MinimizeBox = false;
        }

        private void EnableButtons()
        {
            this.buttonRaise.Enabled = true;
            this.buttonCall.Enabled = true;
            this.buttonFold.Enabled = true;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.progressbarTimer.Visible = true;
            this.progressbarTimer.Value = 1000;
            this.secondsForHumanToPlay = 60;
            this.timer.Start();
        }

        private IDealer GetDealer()
        {
            IDealer dealer = new Dealer(AppSettigns.DealerPictureBoxX, AppSettigns.DealerPictureBoxY);
            this.AddCardsPictureBox(dealer);

            return dealer;
        }

        private IPlayer GetHumanPlayer()
        {
            IPlayer human = PlayerFactory.CreateHuman(AppSettigns.FirstPlayerName, AppSettigns.DefaultChipsCount, this.labelPlayerStatus, this.textboxChipsAmount, AppSettigns.FirstPlayerAnchorStyles, AppSettigns.FirstPlayerPictureBoxX, AppSettigns.FirstPlayerPictureBoxY);
            this.AddPlayerUIComponents(human);

            return human;
        }

        private ICollection<IAIPlayer> GetEnemies(IAILogicProvider logicProvider)
        {
            IAIPlayer AI1 = PlayerFactory.CreateAI(logicProvider, AppSettigns.SecondPlayerName, AppSettigns.DefaultChipsCount, this.labelBot1Status, this.textboxBot1Chips, AppSettigns.SecondPlayerAnchorStyles, AppSettigns.SecondPlayerPictureBoxX, AppSettigns.SecondPlayerPictureBoxY);
            IAIPlayer AI2 = PlayerFactory.CreateAI(logicProvider, AppSettigns.ThirdPlayerName, AppSettigns.DefaultChipsCount, this.labelBot2Status, this.textboxBot2Chips, AppSettigns.ThirdPlayerAnchorStyles, AppSettigns.ThirdPlayerPictureBoxX, AppSettigns.ThirdPlayerPictureBoxY);
            IAIPlayer AI3 = PlayerFactory.CreateAI(logicProvider, AppSettigns.FourthPlayerName, AppSettigns.DefaultChipsCount, this.labelBot3Status, this.textboxBot3Chips, AppSettigns.FourthPlayerAnchorStyles, AppSettigns.FourthPlayerPictureBoxX, AppSettigns.FourthPlayerPictureBoxY);
            IAIPlayer AI4 = PlayerFactory.CreateAI(logicProvider, AppSettigns.FifthPlayerName, AppSettigns.DefaultChipsCount, this.labelBot4Status, this.textboxBot4Chips, AppSettigns.FifthPlayerAnchorStyles, AppSettigns.FifthPlayerPictureBoxX, AppSettigns.FifthPlayerPictureBoxY);
            IAIPlayer AI5 = PlayerFactory.CreateAI(logicProvider, AppSettigns.SixthPlayerName, AppSettigns.DefaultChipsCount, this.labelBot5Status, this.textboxBot5Chips, AppSettigns.SixthPlayerAnchorStyles, AppSettigns.SixthPlayerPictureBoxX, AppSettigns.SixthPlayerPictureBoxY);

            ICollection<IAIPlayer> enemies = new List<IAIPlayer>() { AI1, AI2, AI3, AI4, AI5 };
            foreach (var enemy in enemies)
            {
                this.AddPlayerUIComponents(enemy);
            }

            return enemies;
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

        #region UI
        private async void TimerTick(object sender, object e)
        {
            if (this.progressbarTimer.Value <= 0)
            {
                this.engine.GetHumanPlayer().FoldedTurn = true;
                await this.engine.Turns();
            }

            if (this.secondsForHumanToPlay > 0)
            {
                this.secondsForHumanToPlay--;
                this.progressbarTimer.Value = (this.secondsForHumanToPlay / 6) * 100;
            }
        }

        private void UpdateTick(object sender, object e)
        {
            if (this.engine.GetHumanPlayer().Chips <= 0)
            {
                this.DisableButtons();
            }

            if (this.engine.GetHumanPlayer().Chips >= this.engine.Call)
            {
                this.buttonCall.Text = "Call " + this.engine.Call;
            }
            else
            {
                this.buttonCall.Text = "All in";
                this.buttonRaise.Enabled = false;
            }

            if (this.engine.Call > 0)
            {
                this.buttonCheck.Enabled = false;
            }

            if (this.engine.Call <= 0)
            {
                this.buttonCheck.Enabled = true;
                this.buttonCall.Text = "Call";
                this.buttonCall.Enabled = false;
            }

            int parsedValue;

            if (this.textboxRaise.Text != string.Empty && int.TryParse(this.textboxRaise.Text, out parsedValue))
            {
                if (this.engine.GetHumanPlayer().Chips <= int.Parse(this.textboxRaise.Text))
                {
                    this.buttonRaise.Text = "All in";
                }
                else
                {
                    this.buttonRaise.Text = "Raise";
                }
            }
        }

        private async void OnFoldClick(object sender, EventArgs e)
        {
            this.engine.GetHumanPlayer().Fold();
            await this.engine.Turns();
        }

        private async void OnCheckClick(object sender, EventArgs e)
        {
            if (this.engine.Call <= 0)
            {
                this.engine.GetHumanPlayer().Check();
            }
            else
            {
                this.buttonCheck.Enabled = false;
            }

            await this.engine.Turns();
        }

        private async void OnCallClick(object sender, EventArgs e)
        {
            if (this.engine.GetHumanPlayer().Chips >= this.engine.Call)
            {
                this.engine.GetHumanPlayer().Call(this.engine.Call);
                this.engine.Pot.Add(this.engine.Call);
            }
            else if (this.engine.GetHumanPlayer().Chips <= this.engine.Call && this.engine.Call > 0)
            {
                this.engine.Pot.Add(this.engine.GetHumanPlayer().Chips);
                this.engine.GetHumanPlayer().AllIn();
                this.buttonFold.Enabled = false;
            }

            await this.engine.Turns();
        }

        private async void OnRaiseClick(object sender, EventArgs e)
        {
            int parsedValue;

            if (this.textboxRaise.Text != string.Empty && int.TryParse(this.textboxRaise.Text, out parsedValue))
            {
                if (this.engine.GetHumanPlayer().Chips > this.engine.Call)
                {
                    if (this.engine.Raise * 2 > int.Parse(this.textboxRaise.Text))
                    {
                        this.textboxRaise.Text = (this.engine.Raise * 2).ToString();
                        this.messageWriter.Write(Messages.RaiseAtleastTwice);
                        return;
                    }
                    else
                    {
                        if (this.engine.GetHumanPlayer().Chips >= int.Parse(this.textboxRaise.Text))
                        {
                            this.engine.Call = int.Parse(this.textboxRaise.Text);
                            this.engine.Raise = int.Parse(this.textboxRaise.Text);
                            this.engine.Pot.Add(this.engine.Call);
                            this.buttonCall.Text = "Call";
                            this.engine.GetHumanPlayer().Raise((int)this.engine.Raise);
                            this.engine.IsAnyPlayerRaise = true;
                        }
                        else
                        {
                            this.engine.Call = this.engine.GetHumanPlayer().Chips;
                            this.engine.Raise = this.engine.GetHumanPlayer().Chips;
                            this.engine.Pot.Add(this.engine.GetHumanPlayer().Chips);
                            this.engine.GetHumanPlayer().Raise(this.engine.GetHumanPlayer().Chips);
                            this.engine.IsAnyPlayerRaise = true;
                        }
                    }
                }
            }
            else
            {
                this.messageWriter.Write(Messages.NumberOnlyField);
                return;
            }

            this.engine.GetHumanPlayer().IsInTurn = false;
            await this.engine.Turns();
        }

        private void OnAddClick(object sender, EventArgs e)
        {
            if (this.textboxPlayerChips.Text != string.Empty)
            {
                int chipsToAdd;
                int.TryParse(this.textboxPlayerChips.Text, out chipsToAdd);

                this.engine.AddChips(this.engine.GetAllPlayers(), chipsToAdd);
            }
        }

        private void OnBlindOptionsClick(object sender, EventArgs e)
        {
            this.textboxBigBlind.Text = this.engine.BigBlind.ToString();
            this.textboxSmallBlind.Text = this.engine.SmallBlind.ToString();

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
            int parsedValue;

            if (this.textboxSmallBlind.Text.Contains(",") || this.textboxSmallBlind.Text.Contains("."))
            {
                this.messageWriter.Write(Messages.SmallBlindRoundNumber);
                this.textboxSmallBlind.Text = this.engine.SmallBlind.ToString();
                return;
            }

            if (!int.TryParse(this.textboxSmallBlind.Text, out parsedValue))
            {
                this.messageWriter.Write(Messages.NumberOnlyField);
                this.textboxSmallBlind.Text = this.engine.SmallBlind.ToString();
                return;
            }

            if (int.Parse(this.textboxSmallBlind.Text) > AppSettigns.DefaultMaxSmallBlind)
            {
                this.messageWriter.Write(string.Format(Messages.SmallBlindMaxValue, AppSettigns.DefaultMaxSmallBlind));
                this.textboxSmallBlind.Text = this.engine.SmallBlind.ToString();
            }

            if (int.Parse(this.textboxSmallBlind.Text) < AppSettigns.DefaultMinSmallBlind)
            {
                this.messageWriter.Write(string.Format(Messages.SmallBlindMinValue, AppSettigns.DefaultMinSmallBlind));
            }

            if (int.Parse(this.textboxSmallBlind.Text) >= AppSettigns.DefaultMinSmallBlind && int.Parse(this.textboxSmallBlind.Text) <= AppSettigns.DefaultMaxSmallBlind)
            {
                this.engine.SmallBlind = int.Parse(this.textboxSmallBlind.Text);
                this.messageWriter.Write(Messages.SaveChanges);
            }
        }

        private void OnBigBlindClick(object sender, EventArgs e)
        {
            int parsedValue;

            if (this.textboxBigBlind.Text.Contains(",") || this.textboxBigBlind.Text.Contains("."))
            {
                this.messageWriter.Write(Messages.BigBlindRoundNumber);
                this.textboxBigBlind.Text = this.engine.BigBlind.ToString();
                return;
            }

            if (!int.TryParse(this.textboxSmallBlind.Text, out parsedValue))
            {
                this.messageWriter.Write(Messages.NumberOnlyField);
                this.textboxSmallBlind.Text = this.engine.BigBlind.ToString();
                return;
            }

            if (int.Parse(this.textboxBigBlind.Text) > AppSettigns.DefaultMaxBigBlind)
            {
                this.messageWriter.Write(string.Format(Messages.BigBlindMaxValue, AppSettigns.DefaultMaxBigBlind));
                this.textboxBigBlind.Text = this.engine.BigBlind.ToString();
            }

            if (int.Parse(this.textboxBigBlind.Text) < AppSettigns.DefaultMinBigBlind)
            {
                this.messageWriter.Write(string.Format(Messages.BigBlindMinValue, AppSettigns.DefaultMinBigBlind));
            }

            if (int.Parse(this.textboxBigBlind.Text) >= AppSettigns.DefaultMinBigBlind && int.Parse(this.textboxBigBlind.Text) <= AppSettigns.DefaultMaxBigBlind)
            {
                this.engine.BigBlind = int.Parse(this.textboxBigBlind.Text);
                this.messageWriter.Write(Messages.SaveChanges);
            }
        }
        #endregion
    }
}