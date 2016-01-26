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

    public partial class Form1 : Form
    {
        private readonly Timer timer = new Timer();
        private readonly Timer updates = new Timer();
        private readonly GameEngine engine;
        private readonly IMessageWriter messageWriter;
        private int secondsForHumanToPlay;

        public Form1()
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
            this.textboxRaise.Text = (GameEngine.DefaultBigBlind * 2).ToString();

            IPlayer human = this.GetHumanPlayer();
            ICollection<IPlayer> enemies = this.GetEnemies();
            IPot pot = new Pot(this.textboxPot);
            IDealer dealer = this.GetDealer();
            IDeck deck = Deck.Instance;
            this.messageWriter = new MessageBoxWriter();

            this.engine = new GameEngine(human, enemies, pot, dealer, deck, this.messageWriter);
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
            this.MaximizeBox = false;
            this.MinimizeBox = false;
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
            IPlayer human = PlayerFactory.Create(PlayerType.Human, AppSettigns.FirstPlayerName, AppSettigns.DefaultChipsCount, this.labelPlayerStatus, this.textboxChipsAmount, AppSettigns.FirstPlayerAnchorStyles, AppSettigns.FirstPlayerPictureBoxX, AppSettigns.FirstPlayerPictureBoxY);
            this.AddPlayerUIComponents(human);

            return human;
        }

        private ICollection<IPlayer> GetEnemies()
        {
            IPlayer AI1 = PlayerFactory.Create(PlayerType.AI, AppSettigns.SecondPlayerName, AppSettigns.DefaultChipsCount, this.labelBot1Status, this.textboxBot1Chips, AppSettigns.SecondPlayerAnchorStyles, AppSettigns.SecondPlayerPictureBoxX, AppSettigns.SecondPlayerPictureBoxY);
            IPlayer AI2 = PlayerFactory.Create(PlayerType.AI, AppSettigns.ThirdPlayerName, AppSettigns.DefaultChipsCount, this.labelBot2Status, this.textboxBot2Chips, AppSettigns.ThirdPlayerAnchorStyles, AppSettigns.ThirdPlayerPictureBoxX, AppSettigns.ThirdPlayerPictureBoxY);
            IPlayer AI3 = PlayerFactory.Create(PlayerType.AI, AppSettigns.FourthPlayerName, AppSettigns.DefaultChipsCount, this.labelBot3Status, this.textboxBot3Chips, AppSettigns.FourthPlayerAnchorStyles, AppSettigns.FourthPlayerPictureBoxX, AppSettigns.FourthPlayerPictureBoxY);
            IPlayer AI4 = PlayerFactory.Create(PlayerType.AI, AppSettigns.FifthPlayerName, AppSettigns.DefaultChipsCount, this.labelBot4Status, this.textboxBot4Chips, AppSettigns.FifthPlayerAnchorStyles, AppSettigns.FifthPlayerPictureBoxX, AppSettigns.FifthPlayerPictureBoxY);
            IPlayer AI5 = PlayerFactory.Create(PlayerType.AI, AppSettigns.SixthPlayerName, AppSettigns.DefaultChipsCount, this.labelBot5Status, this.textboxBot5Chips, AppSettigns.SixthPlayerAnchorStyles, AppSettigns.SixthPlayerPictureBoxX, AppSettigns.SixthPlayerPictureBoxY);

            ICollection<IPlayer> enemies = new List<IPlayer>() { AI1, AI2, AI3, AI4, AI5 };
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
            //this.UpdatePlayersChipsTextBoxes(this.GetAllPlayers());

            if (this.engine.GetHumanPlayer().Chips <= 0)
            {
                this.engine.GetHumanPlayer().IsInTurn = false;
                this.engine.GetHumanPlayer().FoldedTurn = true;
                this.buttonCall.Enabled = false;
                this.buttonRaise.Enabled = false;
                this.buttonFold.Enabled = false;
                this.buttonCheck.Enabled = false;
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

            if (this.engine.GetHumanPlayer().Chips <= 0)
            {
                this.buttonRaise.Enabled = false;
            }

            int parsedValue;

            if (this.textboxRaise.Text != "" && int.TryParse(this.textboxRaise.Text, out parsedValue))
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

            if (this.engine.GetHumanPlayer().Chips < this.engine.Call)
            {
                this.buttonRaise.Enabled = false;
            }
        }

        private async void OnFoldClick(object sender, EventArgs e)
        {
            this.engine.GetHumanPlayer().Fold();
            //this.human.StatusLabel.Text = "Fold";
            //this.human.IsInTurn = false;
            //this.human.FoldedTurn = true;
            await this.engine.Turns();
        }

        private async void OnCheckClick(object sender, EventArgs e)
        {
            if (this.engine.Call <= 0)
            {
                this.engine.GetHumanPlayer().Check();
                //this.human.IsInTurn = false;
                //this.human.StatusLabel.Text = "Check";
            }
            else
            {
                //labelPlayerStatus.Text = "All in " + Chips;

                this.buttonCheck.Enabled = false;
            }

            await this.engine.Turns();
        }

        private async void OnCallClick(object sender, EventArgs e)
        {
            this.engine.Rules(this.engine.GetHumanPlayer());

            if (this.engine.GetHumanPlayer().Chips >= this.engine.Call)
            {
                this.engine.GetHumanPlayer().Call(this.engine.Call);
                //this.human.Chips -= Call;
                //this.textboxChipsAmount.Text = AppSettigns.PlayerChipsTextBoxText + this.human.Chips.ToString();
                this.engine.Pot.Add(this.engine.Call);
                //this.human.IsInTurn = false;
                //this.human.StatusLabel.Text = "Call " + Call;
                //this.human.CallAmount = Call;
            }
            else if (this.engine.GetHumanPlayer().Chips <= this.engine.Call && this.engine.Call > 0)
            {
                this.engine.Pot.Add(this.engine.GetHumanPlayer().Chips);
                this.engine.GetHumanPlayer().AllIn();
                //this.human.StatusLabel.Text = "All in " + this.human.Chips;
                //this.human.Chips = 0;
                //this.textboxChipsAmount.Text = AppSettigns.PlayerChipsTextBoxText + this.human.Chips.ToString();
                //this.human.IsInTurn = false;
                this.buttonFold.Enabled = false;
                //this.human.CallAmount = this.human.Chips;
            }

            await this.engine.Turns();
        }

        private async void OnRaiseClick(object sender, EventArgs e)
        {
            this.engine.Rules(this.engine.GetHumanPlayer());
            int parsedValue;

            if (this.textboxRaise.Text != string.Empty && int.TryParse(this.textboxRaise.Text, out parsedValue))
            {
                if (this.engine.GetHumanPlayer().Chips > this.engine.Call)
                {
                    if (this.engine.Raise * 2 > int.Parse(this.textboxRaise.Text))
                    {
                        this.textboxRaise.Text = (this.engine.Raise * 2).ToString();
                        this.messageWriter.Write("You must raise atleast twice as the current raise !");
                        return;
                    }
                    else
                    {
                        if (this.engine.GetHumanPlayer().Chips >= int.Parse(this.textboxRaise.Text))
                        {
                            this.engine.Call = int.Parse(this.textboxRaise.Text);
                            this.engine.Raise = int.Parse(this.textboxRaise.Text);
                            //this.human.StatusLabel.Text = "Raise " + Call;
                            this.engine.Pot.Add(this.engine.Call);
                            this.buttonCall.Text = "Call";
                            this.engine.GetHumanPlayer().Raise((int)this.engine.Raise);
                            //this.human.Chips -= int.Parse(this.textboxRaise.Text);
                            this.engine.IsAnyPlayerRaise = true;
                            //this.human.Raise = Convert.ToInt32(Raise);
                        }
                        else
                        {
                            this.engine.Call = this.engine.GetHumanPlayer().Chips;
                            this.engine.Raise = this.engine.GetHumanPlayer().Chips;
                            this.engine.Pot.Add(this.engine.GetHumanPlayer().Chips);
                            this.engine.GetHumanPlayer().Raise(this.engine.GetHumanPlayer().Chips);
                            //this.human.StatusLabel.Text = "Raise " + Call;
                            //this.human.Chips = 0;
                            this.engine.IsAnyPlayerRaise = true;
                            //this.human.Raise = Convert.ToInt32(Raise);
                        }
                    }
                }
            }
            else
            {
                this.messageWriter.Write("This is a number only field");
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

                //this.UpdatePlayersChipsTextBoxes(this.GetAllPlayers());
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
            var minSmallBlind = 250;
            var maxSmallBlind = 100000;
            int parsedValue;

            if (this.textboxSmallBlind.Text.Contains(",") || this.textboxSmallBlind.Text.Contains("."))
            {
                var message = "The Small Blind can be only round number !";
                this.messageWriter.Write(message);
                this.textboxSmallBlind.Text = this.engine.SmallBlind.ToString();
                return;
            }

            if (!int.TryParse(this.textboxSmallBlind.Text, out parsedValue))
            {
                var message = "This is a number only field";
                this.messageWriter.Write(message);
                this.textboxSmallBlind.Text = this.engine.SmallBlind.ToString();
                return;
            }

            if (int.Parse(this.textboxSmallBlind.Text) > maxSmallBlind)
            {
                var message = "The maximum of the Small Blind is 100 000 $";
                this.messageWriter.Write(message);
                this.textboxSmallBlind.Text = this.engine.SmallBlind.ToString();
            }

            if (int.Parse(this.textboxSmallBlind.Text) < minSmallBlind)
            {
                var message = "The minimum of the Small Blind is 250 $";
                this.messageWriter.Write(message);
            }

            if (int.Parse(this.textboxSmallBlind.Text) >= minSmallBlind && int.Parse(this.textboxSmallBlind.Text) <= maxSmallBlind)
            {
                this.engine.SmallBlind = int.Parse(this.textboxSmallBlind.Text);
                var message = "The changes have been saved ! They will become available the next hand you play.";
                this.messageWriter.Write(message);
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
                this.messageWriter.Write(message);
                this.textboxBigBlind.Text = this.engine.BigBlind.ToString();
                return;
            }

            if (!int.TryParse(this.textboxSmallBlind.Text, out parsedValue))
            {
                var message = "This is a number only field";
                this.messageWriter.Write(message);
                this.textboxSmallBlind.Text = this.engine.BigBlind.ToString();
                return;
            }

            if (int.Parse(this.textboxBigBlind.Text) > maxBigBlind)
            {
                var message = "The maximum of the Big Blind is 200 000";
                this.messageWriter.Write(message);
                this.textboxBigBlind.Text = this.engine.BigBlind.ToString();
            }

            if (int.Parse(this.textboxBigBlind.Text) < minBigBlind)
            {
                var message = "The minimum of the Big Blind is 500 $";
                this.messageWriter.Write(message);
            }

            if (int.Parse(this.textboxBigBlind.Text) >= minBigBlind && int.Parse(this.textboxBigBlind.Text) <= maxBigBlind)
            {
                this.engine.BigBlind = int.Parse(this.textboxBigBlind.Text);
                var message = "The changes have been saved ! They will become available the next hand you play.";
                this.messageWriter.Write(message);
            }
        }
        #endregion
    }
}