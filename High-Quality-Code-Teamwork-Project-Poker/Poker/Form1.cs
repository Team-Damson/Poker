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
using Poker.Interfaces;
using Poker.Models;

namespace Poker
{
    using Poker.Enums;

    public partial class Form1 : Form
    {
        HandTypes handType = new HandTypes();
        CheckHandType checkHandType = new CheckHandType();

        #region Variables

        private IPlayer human;
        //private IPlayer AI1;
        //private IPlayer AI2;
        //private IPlayer AI3;
        //private IPlayer AI4;
        //private IPlayer AI5;

        private Dealer Dealer;
        private Deck Deck;

        ProgressBar asd = new ProgressBar();
        //public int Nm;
        //Panel pPanel = new Panel(); Panel b1Panel = new Panel(); Panel b2Panel = new Panel(); Panel b3Panel = new Panel();
        //Panel b4Panel = new Panel(); Panel b5Panel = new Panel();
        private int call = 500;//, foldedPlayers = 5;
        //public int Chips = 10000, bot1Chips = 10000, bot2Chips = 10000, bot3Chips = 10000, bot4Chips = 10000, bot5Chips = 10000;
        //private double type;
        double  /*rounds = 0, b1Power, b2Power, b3Power, b4Power, b5Power, pPower = 0, pType = -1,*/ Raise = 0;

        CommunityCardBoard rounds = CommunityCardBoard.Undeal;
        // b1Type = -1, b2Type = -1, b3Type = -1, b4Type = -1, b5Type = -1;
        //bool B1turn = false, B2turn = false, B3turn = false, B4turn = false, B5turn = false;
        //bool B1Fturn = false, B2Fturn = false, B3Fturn = false, B4Fturn = false, B5Fturn = false;
        bool /*pFolded, b1Folded, b2Folded, b3Folded, b4Folded, b5Folded, intsadded,*/ changed;
        //int pCall = 0, b1Call = 0, b2Call = 0, b3Call = 0, b4Call = 0, b5Call = 0, pRaise = 0, b1Raise = 0, b2Raise = 0, b3Raise = 0, b4Raise = 0, b5Raise = 0;
        private int /*height, width, Flop = 1, Turn = 2, River = 3, End = 4,*/ winners = 0; //maxLeft = 6;
        int /*last = 123,*/ raisedTurn = 1;

        //List<bool?> FoldedPlayers = new List<bool?>();
        List<Type> strongestHands = new List<Type>();
        List<IPlayer> Winners = new List<IPlayer>();
        //List<int> ints = new List<int>();
        bool /*PFturn = false, Pturn = true,*/ restart = false, raising = false;

        Poker.Type winningHand;
        //string[] ImgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
        /*string[] ImgLocation ={
                   "Assets\\Cards\\33.png","Assets\\Cards\\22.png",
                    "Assets\\Cards\\29.png","Assets\\Cards\\21.png",
                    "Assets\\Cards\\36.png","Assets\\Cards\\17.png",
                    "Assets\\Cards\\40.png","Assets\\Cards\\16.png",
                    "Assets\\Cards\\5.png","Assets\\Cards\\47.png",
                    "Assets\\Cards\\37.png","Assets\\Cards\\13.png",
                    
                    "Assets\\Cards\\12.png",
                    "Assets\\Cards\\8.png","Assets\\Cards\\18.png",
                    "Assets\\Cards\\15.png","Assets\\Cards\\27.png"};*/
        //int[] Reserve = new int[17];
        //Image[] Deck = new Image[52];
        //PictureBox[] Holder = new PictureBox[52];
        Timer timer = new Timer();
        Timer Updates = new Timer();
        int t = 60, i, up = 10000000, turnCount = 0;

        private int bigBlind = 500;
        private int smallBlind = 250;

        private IList<IPlayer> enemies;
        #endregion

        public Form1()
        {
            InitializeComponent();

            this.Deck = Models.Deck.Instance;

            this.winningHand = new Type();


            //FoldedPlayers.Add(PFturn); FoldedPlayers.Add(B1Fturn); FoldedPlayers.Add(B2Fturn); FoldedPlayers.Add(B3Fturn); FoldedPlayers.Add(B4Fturn); FoldedPlayers.Add(B5Fturn);
            this.InitPlayers();

            this.InitDealer();

            call = this.bigBlind;
            MaximizeBox = false;
            MinimizeBox = false;
            Updates.Start();
            
            //width = this.Width;
            //height = this.Height;
            this.Shuffle();

            this.textboxPot.Enabled = false;

            this.UpdatePlayersChipsTextBoxes(this.GetAllPlayers());

            timer.Interval = (1 * 1 * 1000);
            timer.Tick += this.TimerTick;
            Updates.Interval = (1 * 1 * 100);
            Updates.Tick += this.UpdateTick;

            this.textboxBigBlind.Visible = false;
            this.textboxSmallBlind.Visible = false;
            this.buttonBigBlind.Visible = false;
            this.buttonSmallBlind.Visible = false;

            this.textboxRaise.Text = (this.bigBlind * 2).ToString();
        }

        private void UpdatePlayersChipsTextBoxes(ICollection<IPlayer> players)
        {
            foreach (var player in players)
            {
                player.ChipsTextBox.Enabled = false;
                player.ChipsTextBox.Text = AppSettigns.PlayerChipsTextBoxText + player.Chips.ToString();
            }
        }

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
            this.human = PlayerFactory.Create(PlayerType.Human, "Player", 10000, this.labelPlayerStatus, this.textboxPlayerChips, AppSettigns.FirstPlayerAnchorStyles, AppSettigns.FirstPlayerPictureBoxX, AppSettigns.FirstPlayerPictureBoxY);
            this.AddPlayerUIComponents(human);
        }

        private void InitEnemies()
        {
            IPlayer AI1 = PlayerFactory.Create(PlayerType.AI, "Bot 1", 10000, this.labelBot1Status, this.textboxBot1Chips, AppSettigns.SecondPlayerAnchorStyles, AppSettigns.SecondPlayerPictureBoxX, AppSettigns.SecondPlayerPictureBoxY);
            IPlayer AI2 = PlayerFactory.Create(PlayerType.AI, "Bot 2", 10000, this.labelBot2Status, this.textboxBot2Chips, AppSettigns.ThirdPlayerAnchorStyles, AppSettigns.ThirdPlayerPictureBoxX, AppSettigns.ThirdPlayerPictureBoxY);
            IPlayer AI3 = PlayerFactory.Create(PlayerType.AI, "Bot 3", 10000, this.labelBot3Status, this.textboxBot3Chips, AppSettigns.FourthPlayerAnchorStyles, AppSettigns.FourthPlayerPictureBoxX, AppSettigns.FourthPlayerPictureBoxY);
            IPlayer AI4 = PlayerFactory.Create(PlayerType.AI, "Bot 4", 10000, this.labelBot4Status, this.textboxBot4Chips, AppSettigns.FifthPlayerAnchorStyles, AppSettigns.FifthPlayerPictureBoxX, AppSettigns.FifthPlayerPictureBoxY);
            IPlayer AI5 = PlayerFactory.Create(PlayerType.AI, "Bot 5", 10000, this.labelBot5Status, this.textboxBot5Chips, AppSettigns.SixthPlayerAnchorStyles, AppSettigns.SixthPlayerPictureBoxX, AppSettigns.SixthPlayerPictureBoxY);

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
            //FoldedPlayers.Add(human.FoldedTurn); FoldedPlayers.Add(AI1.FoldedTurn); FoldedPlayers.Add(AI2.FoldedTurn); FoldedPlayers.Add(AI3.FoldedTurn); FoldedPlayers.Add(AI4.FoldedTurn); FoldedPlayers.Add(AI5.FoldedTurn);

            this.buttonCall.Enabled = false;
            this.buttonRaise.Enabled = false;
            this.buttonFold.Enabled = false;
            this.buttonCheck.Enabled = false;
            MaximizeBox = false;
            MinimizeBox = false;

            await this.Deck.SetCards(this.GetAllPlayers(), this.Dealer);

            this.Run();
            if (this.enemies.Count(e => !e.CanPlay()) == 5)
            {
                DialogResult dialogResult = MessageBox.Show("Would You Like To Play Again ?", "You Won , Congratulations ! ", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Application.Restart();
                }
                else if (dialogResult == DialogResult.No)
                {
                    Application.Exit();
                }
            }
            this.buttonRaise.Enabled = true;
            this.buttonCall.Enabled = true;
            this.buttonFold.Enabled = true;
        }
        /*
        async Task Shuffle2()
        {
            FoldedPlayers.Add(human.FoldedTurn); FoldedPlayers.Add(AI1.FoldedTurn); FoldedPlayers.Add(AI2.FoldedTurn); FoldedPlayers.Add(AI3.FoldedTurn); FoldedPlayers.Add(AI4.FoldedTurn); FoldedPlayers.Add(AI5.FoldedTurn);
            this.buttonCall.Enabled = false;
            this.buttonRaise.Enabled = false;
            this.buttonFold.Enabled = false;
            this.buttonCheck.Enabled = false;
            MaximizeBox = false;
            MinimizeBox = false;
            bool check = false;
            Bitmap backImage = new Bitmap("Assets\\Back\\Back.png");
            int horizontal = 580, vertical = 480;
            Random r = new Random();

            // shuffle deck
            for (i = ImgLocation.Length; i > 0; i--)
            {
                int j = r.Next(i);
                var k = ImgLocation[j];
                ImgLocation[j] = ImgLocation[i - 1];
                ImgLocation[i - 1] = k;
            }

            for (i = 0; i < 17; i++)
            {

                Deck[i] = Image.FromFile(ImgLocation[i]);
                var charsToRemove = new string[] { "Assets\\Cards\\", ".png" };
                foreach (var c in charsToRemove)
                {
                    ImgLocation[i] = ImgLocation[i].Replace(c, string.Empty);
                }
                Reserve[i] = int.Parse(ImgLocation[i]) - 1;

                Holder[i] = new PictureBox();
                Holder[i].SizeMode = PictureBoxSizeMode.StretchImage;
                Holder[i].Height = 130;
                Holder[i].Width = 80;

                this.Controls.Add(Holder[i]);
                Holder[i].Name = "pb" + i.ToString();


                await Task.Delay(200);
                #region Throwing Cards
                if (i < 2)
                {
                    if (Holder[0].Tag != null)
                    {
                        Holder[1].Tag = Reserve[1];
                    }
                    Holder[0].Tag = Reserve[0];
                    Holder[i].Image = Deck[i];
                    Holder[i].Anchor = (AnchorStyles.Bottom);
                    //Holder[i].Dock = DockStyle.Top;
                    Holder[i].Location = new Point(horizontal, vertical);
                    horizontal += Holder[i].Width;
                    this.Controls.Add(human.Panel);
                    human.Panel.Location = new Point(Holder[0].Left - 10, Holder[0].Top - 10);
                    human.Panel.BackColor = Color.DarkBlue;
                    human.Panel.Height = 150;
                    human.Panel.Width = 180;
                    human.Panel.Visible = false;
                }
                if (AI1.Chips > 0)
                {
                    foldedPlayers--;
                    if (i >= 2 && i < 4)
                    {
                        if (Holder[2].Tag != null)
                        {
                            Holder[3].Tag = Reserve[3];
                        }
                        Holder[2].Tag = Reserve[2];
                        if (!check)
                        {
                            horizontal = 15;
                            vertical = 420;
                        }
                        check = true;
                        Holder[i].Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
                        Holder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];
                        Holder[i].Location = new Point(horizontal, vertical);
                        horizontal += Holder[i].Width;
                        Holder[i].Visible = true;
                        this.Controls.Add(AI1.Panel);
                        AI1.Panel.Location = new Point(Holder[2].Left - 10, Holder[2].Top - 10);
                        AI1.Panel.BackColor = Color.DarkBlue;
                        AI1.Panel.Height = 150;
                        AI1.Panel.Width = 180;
                        AI1.Panel.Visible = false;
                        if (i == 3)
                        {
                            check = false;
                        }
                    }
                }
                if (AI2.Chips > 0)
                {
                    foldedPlayers--;
                    if (i >= 4 && i < 6)
                    {
                        if (Holder[4].Tag != null)
                        {
                            Holder[5].Tag = Reserve[5];
                        }
                        Holder[4].Tag = Reserve[4];
                        if (!check)
                        {
                            horizontal = 75;
                            vertical = 65;
                        }
                        check = true;
                        Holder[i].Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                        Holder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];
                        Holder[i].Location = new Point(horizontal, vertical);
                        horizontal += Holder[i].Width;
                        Holder[i].Visible = true;
                        this.Controls.Add(AI2.Panel);
                        AI2.Panel.Location = new Point(Holder[4].Left - 10, Holder[4].Top - 10);
                        AI2.Panel.BackColor = Color.DarkBlue;
                        AI2.Panel.Height = 150;
                        AI2.Panel.Width = 180;
                        AI2.Panel.Visible = false;
                        if (i == 5)
                        {
                            check = false;
                        }
                    }
                }
                if (AI3.Chips > 0)
                {
                    foldedPlayers--;
                    if (i >= 6 && i < 8)
                    {
                        if (Holder[6].Tag != null)
                        {
                            Holder[7].Tag = Reserve[7];
                        }
                        Holder[6].Tag = Reserve[6];
                        if (!check)
                        {
                            horizontal = 590;
                            vertical = 25;
                        }
                        check = true;
                        Holder[i].Anchor = (AnchorStyles.Top);
                        Holder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];
                        Holder[i].Location = new Point(horizontal, vertical);
                        horizontal += Holder[i].Width;
                        Holder[i].Visible = true;
                        this.Controls.Add(AI2.Panel);
                        AI2.Panel.Location = new Point(Holder[6].Left - 10, Holder[6].Top - 10);
                        AI2.Panel.BackColor = Color.DarkBlue;
                        AI2.Panel.Height = 150;
                        AI2.Panel.Width = 180;
                        AI2.Panel.Visible = false;
                        if (i == 7)
                        {
                            check = false;
                        }
                    }
                }
                if (AI4.Chips > 0)
                {
                    foldedPlayers--;
                    if (i >= 8 && i < 10)
                    {
                        if (Holder[8].Tag != null)
                        {
                            Holder[9].Tag = Reserve[9];
                        }
                        Holder[8].Tag = Reserve[8];
                        if (!check)
                        {
                            horizontal = 1115;
                            vertical = 65;
                        }
                        check = true;
                        Holder[i].Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                        Holder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];
                        Holder[i].Location = new Point(horizontal, vertical);
                        horizontal += Holder[i].Width;
                        Holder[i].Visible = true;
                        this.Controls.Add(AI4.Panel);
                        AI4.Panel.Location = new Point(Holder[8].Left - 10, Holder[8].Top - 10);
                        AI4.Panel.BackColor = Color.DarkBlue;
                        AI4.Panel.Height = 150;
                        AI4.Panel.Width = 180;
                        AI4.Panel.Visible = false;
                        if (i == 9)
                        {
                            check = false;
                        }
                    }
                }
                if (AI5.Chips > 0)
                {
                    foldedPlayers--;
                    if (i >= 10 && i < 12)
                    {
                        if (Holder[10].Tag != null)
                        {
                            Holder[11].Tag = Reserve[11];
                        }
                        Holder[10].Tag = Reserve[10];
                        if (!check)
                        {
                            horizontal = 1160;
                            vertical = 420;
                        }
                        check = true;
                        Holder[i].Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
                        Holder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];
                        Holder[i].Location = new Point(horizontal, vertical);
                        horizontal += Holder[i].Width;
                        Holder[i].Visible = true;
                        this.Controls.Add(AI5.Panel);
                        AI5.Panel.Location = new Point(Holder[10].Left - 10, Holder[10].Top - 10);
                        AI5.Panel.BackColor = Color.DarkBlue;
                        AI5.Panel.Height = 150;
                        AI5.Panel.Width = 180;
                        AI5.Panel.Visible = false;
                        if (i == 11)
                        {
                            check = false;
                        }
                    }
                }
                if (i >= 12)
                {
                    Holder[12].Tag = Reserve[12];
                    if (i > 12) Holder[13].Tag = Reserve[13];
                    if (i > 13) Holder[14].Tag = Reserve[14];
                    if (i > 14) Holder[15].Tag = Reserve[15];
                    if (i > 15)
                    {
                        Holder[16].Tag = Reserve[16];

                    }
                    if (!check)
                    {
                        horizontal = 410;
                        vertical = 265;
                    }
                    check = true;
                    if (Holder[i] != null)
                    {
                        Holder[i].Anchor = AnchorStyles.None;
                        Holder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];
                        Holder[i].Location = new Point(horizontal, vertical);
                        horizontal += 110;
                    }
                }
                #endregion
                if (AI1.Chips <= 0)
                {
                    AI1.FoldedTurn = true;
                    Holder[2].Visible = false;
                    Holder[3].Visible = false;
                }
                else
                {
                    AI1.FoldedTurn = false;
                    if (i == 3)
                    {
                        if (Holder[3] != null)
                        {
                            Holder[2].Visible = true;
                            Holder[3].Visible = true;
                        }
                    }
                }
                if (AI2.Chips <= 0)
                {
                    AI2.FoldedTurn = true;
                    Holder[4].Visible = false;
                    Holder[5].Visible = false;
                }
                else
                {
                    AI2.FoldedTurn = false;
                    if (i == 5)
                    {
                        if (Holder[5] != null)
                        {
                            Holder[4].Visible = true;
                            Holder[5].Visible = true;
                        }
                    }
                }
                if (AI3.Chips <= 0)
                {
                    AI3.FoldedTurn = true;
                    Holder[6].Visible = false;
                    Holder[7].Visible = false;
                }
                else
                {
                    AI3.FoldedTurn = false;
                    if (i == 7)
                    {
                        if (Holder[7] != null)
                        {
                            Holder[6].Visible = true;
                            Holder[7].Visible = true;
                        }
                    }
                }
                if (AI4.Chips <= 0)
                {
                    AI4.FoldedTurn = true;
                    Holder[8].Visible = false;
                    Holder[9].Visible = false;
                }
                else
                {
                    AI4.FoldedTurn = false;
                    if (i == 9)
                    {
                        if (Holder[9] != null)
                        {
                            Holder[8].Visible = true;
                            Holder[9].Visible = true;
                        }
                    }
                }
                if (AI5.Chips <= 0)
                {
                    AI5.FoldedTurn = true;
                    Holder[10].Visible = false;
                    Holder[11].Visible = false;
                }
                else
                {
                    AI5.FoldedTurn = false;
                    if (i == 11)
                    {
                        if (Holder[11] != null)
                        {
                            Holder[10].Visible = true;
                            Holder[11].Visible = true;
                        }
                    }
                }
                if (i == 16)
                {
                    if (!restart)
                    {
                        MaximizeBox = true;
                        MinimizeBox = true;
                    }
                    timer.Start();
                }
            }
            if (foldedPlayers == 5)
            {
                DialogResult dialogResult = MessageBox.Show("Would You Like To Play Again ?", "You Won , Congratulations ! ", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Application.Restart();
                }
                else if (dialogResult == DialogResult.No)
                {
                    Application.Exit();
                }
            }
            else
            {
                foldedPlayers = 5;
            }
            if (i == 17)
            {
                this.buttonRaise.Enabled = true;
                this.buttonCall.Enabled = true;
                this.buttonRaise.Enabled = true;
                this.buttonRaise.Enabled = true;
                this.buttonFold.Enabled = true;
            }
        }
        */
        private void Run()
        {
            if (!restart)
            {
                MaximizeBox = true;
                MinimizeBox = true;
            }
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
                    MessageBox.Show(currentAI.Name + "'s Turn");
                    AI(currentAI);
                    turnCount++;
                    //last = 1;
                    currentAI.IsInTurn = false;
                    nextAI.IsInTurn = true;
                }
            }
            if (currentAI.FoldedTurn && !currentAI.HasFolded)
            {
                //FoldedPlayers.RemoveAt(currentAI.Id + 1);
                //FoldedPlayers.Insert(currentAI.Id + 1, null);
                //maxLeft--;
                currentAI.HasFolded = true;
            }
            if (currentAI.FoldedTurn || !currentAI.IsInTurn)
            {
                await CheckRaise(currentAI.Id + 1, currentAI.Id + 1);
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
                    //MessageBox.Show("Player's Turn");
                    this.progressbarTimer.Visible = true;
                    this.progressbarTimer.Value = 1000;
                    t = 60;
                    up = 10000000;
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
                        //FoldedPlayers.RemoveAt(0);
                        //FoldedPlayers.Insert(0, null);
                        //maxLeft--;
                        human.HasFolded = true;
                    }
                }
                await CheckRaise(0, 0);
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
                        //FoldedPlayers.RemoveAt(0);
                        //FoldedPlayers.Insert(0, null);
                        //maxLeft--;
                        human.HasFolded = true;
                    }
                }
            #endregion

                await AllIn();
                if (!restart)
                {
                    await Turns();
                }
                restart = false;
            }
        }

        private int GetFoldedPlayersCount(ICollection<IPlayer> players)
        {
            return players.Count(p => p.HasFolded == true);
        }

        void Rules(IPlayer player)
        {
            if (!player.FoldedTurn || player.CardIndexes.First() == 0 && player.CardIndexes.Last() == 1 && this.labelPlayerStatus.Text.Contains("Fold") == false)
            {
                #region Variables

                bool done = false;
                bool vf = false;
                int[] Straight1 = new int[5];
                int[] Straight = new int[7];
                Straight[0] = player.Cards.First().Power;//Reserve[player.CardIndexes.First()];
                Straight[1] = player.Cards.Last().Power;//Reserve[player.CardIndexes.Last()];
                Straight1[0] = Straight[2] = this.Dealer.Cards.ElementAt(0).Power;//Reserve[12];
                Straight1[1] = Straight[3] = this.Dealer.Cards.ElementAt(1).Power;//Reserve[13];
                Straight1[2] = Straight[4] = this.Dealer.Cards.ElementAt(2).Power;//Reserve[14];
                Straight1[3] = Straight[5] = this.Dealer.Cards.ElementAt(3).Power;//Reserve[15];
                Straight1[4] = Straight[6] = this.Dealer.Cards.ElementAt(4).Power;//Reserve[16];
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

                for (i = 0; i < 16; i++)
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

        private void CheckWinner(IPlayer player)
        {
            if (player.Type.Current == this.winningHand.Current)
            {
                if (player.Type.Power == this.winningHand.Power)
                {
                    Winners.Add(player);
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
        }

        private void SetWinnersChips(ICollection<IPlayer> players)
        {
            foreach (var player in players)
            {
                player.Chips += int.Parse(this.textboxPot.Text) / players.Count;
                player.ChipsTextBox.Text = player.Chips.ToString();
            }
        }

        private void CheckWinners(ICollection<IPlayer> players, Dealer dealer)
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

                this.CheckWinner(player);
                //this.Winner(player);
            }

            this.SetWinnersChips(this.Winners);
        }

        /*
        void Winner(IPlayer player)//, string lastly)
        {
            //if (lastly == " ")
            //{
            //    lastly = "Bot 5";
            //}
            /*
            foreach (var enemy in this.enemies)
            {
                if (enemy.PictureBoxHolder.Count(p => p.Visible) == 2)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        enemy.PictureBoxHolder[i].Image = enemy.Cards.ElementAt(i).Image;
                    }
                }
            }

            for (int j = 0; j < this.Dealer.PictureBoxHolder.Count; j++)
            {
                this.Dealer.PictureBoxHolder[j].Image = this.Dealer.Cards.ElementAt(j).Image;
                //await Task.Delay(5);
                //if (Holder[j].Visible)
                //    Holder[j].Image = Deck[j];
            }*/

            //this.CheckWinner(player);
            /*
            if (player.Type.Current == winningHand.Current)
            {
                if (player.Type.Power == winningHand.Power)
                {
                    winners++;
                    Winners.Add(player);
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
            if (player.Name == lastly)//lastfixed
            {
                if (winners > 1)
                {
                    if (Winners.Contains(player))
                    {
                        human.Chips += int.Parse(this.textboxPot.Text) / winners;
                        this.human.ChipsTextBox.Text = human.Chips.ToString();
                        //pPanel.Visible = true;

                    }
                    if (Winners.Contains(player))
                    {
                        AI1.Chips += int.Parse(this.textboxPot.Text) / winners;
                        this.AI1.ChipsTextBox.Text = AI1.Chips.ToString();
                        //b1Panel.Visible = true;
                    }
                    if (Winners.Contains(player))
                    {
                        AI2.Chips += int.Parse(this.textboxPot.Text) / winners;
                        this.AI2.ChipsTextBox.Text = AI2.Chips.ToString();
                        //b2Panel.Visible = true;
                    }
                    if (Winners.Contains(player))
                    {
                        AI3.Chips += int.Parse(this.textboxPot.Text) / winners;
                        this.AI3.ChipsTextBox.Text = AI3.Chips.ToString();
                        //b3Panel.Visible = true;
                    }
                    if (Winners.Contains(player))
                    {
                        AI4.Chips += int.Parse(this.textboxPot.Text) / winners;
                        this.AI4.ChipsTextBox.Text = AI4.Chips.ToString();
                        //b4Panel.Visible = true;
                    }
                    if (Winners.Contains(player))
                    {
                        AI5.Chips += int.Parse(this.textboxPot.Text) / winners;
                        this.AI5.ChipsTextBox.Text = AI5.Chips.ToString();
                        //b5Panel.Visible = true;
                    }
                    //await Finish(1);
                }
                if (winners == 1)
                {
                    if (Winners.Contains(player))
                    {
                        human.Chips += int.Parse(this.textboxPot.Text);
                        //await Finish(1);
                        //pPanel.Visible = true;
                    }
                    if (Winners.Contains(player))
                    {
                        AI1.Chips += int.Parse(this.textboxPot.Text);
                        //await Finish(1);
                        //b1Panel.Visible = true;
                    }
                    if (Winners.Contains(player))
                    {
                        AI2.Chips += int.Parse(this.textboxPot.Text);
                        //await Finish(1);
                        //b2Panel.Visible = true;

                    }
                    if (Winners.Contains(player))
                    {
                        AI3.Chips += int.Parse(this.textboxPot.Text);
                        //await Finish(1);
                        //b3Panel.Visible = true;
                    }
                    if (Winners.Contains(player))
                    {
                        AI4.Chips += int.Parse(this.textboxPot.Text);
                        //await Finish(1);
                        //b4Panel.Visible = true;
                    }
                    if (Winners.Contains(player))
                    {
                        AI5.Chips += int.Parse(this.textboxPot.Text);
                        //await Finish(1);
                        //b5Panel.Visible = true;
                    }
                }
            }
        }
        */

        private int GetNotFoldedPlayersCount(ICollection<IPlayer> players)
        {
            return 6 - this.GetFoldedPlayersCount(players);
        }

        private void ResetCall(ICollection<IPlayer> players)
        {
            foreach (var player in players)
            {
                player.Call = 0;
            }
        }

        private void ResetRaise(ICollection<IPlayer> players)
        {
            foreach (var player in players)
            {
                player.Raise = 0;
            }
        }

        async Task CheckRaise(int currentTurn, int raiseTurn)
        {
            if (raising)
            {
                turnCount = 0;
                raising = false;
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
                        Raise = 0;
                        call = 0;
                        raisedTurn = 123;
                        rounds++;
                        foreach (var player in this.GetAllPlayers())
                        {
                            if (!player.FoldedTurn)
                            {
                                player.StatusLabel.Text = string.Empty;
                            }
                        }
                        /*if (!human.FoldedTurn)
                            this.human.StatusLabel.Text = "";
                        if (!AI1.FoldedTurn)
                            this.AI1.StatusLabel.Text = "";
                        if (!AI2.FoldedTurn)
                            this.AI2.StatusLabel.Text = "";
                        if (!AI3.FoldedTurn)
                            this.AI3.StatusLabel.Text = "";
                        if (!AI4.FoldedTurn)
                            this.AI4.StatusLabel.Text = "";
                        if (!AI5.FoldedTurn)
                            this.AI5.StatusLabel.Text = "";*/
                    }
                }
            }

            if (rounds == CommunityCardBoard.Flop)
            {
                for (int j = 0; j <= 2; j++)
                {
                    if (this.Dealer.PictureBoxHolder[j].Image != this.Dealer.Cards.ElementAt(j).Image)
                    {
                        this.Dealer.PictureBoxHolder[j].Image = this.Dealer.Cards.ElementAt(j).Image;
                        this.ResetCall(this.GetAllPlayers());
                        this.ResetRaise(this.GetAllPlayers());
                    }
                }
            }
            if (rounds == CommunityCardBoard.Turn)
            {
                for (int j = 2; j <= 3; j++)
                {
                    if (this.Dealer.PictureBoxHolder[j].Image != this.Dealer.Cards.ElementAt(j).Image)
                    {
                        this.Dealer.PictureBoxHolder[j].Image = this.Dealer.Cards.ElementAt(j).Image;
                        this.ResetCall(this.GetAllPlayers());
                        this.ResetRaise(this.GetAllPlayers());
                    }
                }
            }
            if (rounds == CommunityCardBoard.River)
            {
                for (int j = 3; j <= 4; j++)
                {
                    if (this.Dealer.PictureBoxHolder[j].Image != this.Dealer.Cards.ElementAt(j).Image)
                    {
                        this.Dealer.PictureBoxHolder[j].Image = this.Dealer.Cards.ElementAt(j).Image;
                        this.ResetCall(this.GetAllPlayers());
                        this.ResetRaise(this.GetAllPlayers());
                    }
                }
            }
            if (rounds == CommunityCardBoard.End && this.GetNotFoldedPlayersCount(this.GetAllPlayers()) == 6)
            {
                /*string fixedLast = "qwerty";
                if (!this.human.StatusLabel.Text.Contains("Fold"))
                {
                    fixedLast = "Player";
                    Rules(human);
                }
                if (!this.AI1.StatusLabel.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 1";
                    Rules(AI1);
                }
                if (!this.AI2.StatusLabel.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 2";
                    Rules(AI2);
                }
                if (!this.AI3.StatusLabel.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 3";
                    Rules(AI3);
                }
                if (!this.AI4.StatusLabel.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 4";
                    Rules(AI4);
                }
                if (!this.AI5.StatusLabel.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 5";
                    Rules(AI5);
                }*/
                foreach (var player in this.GetAllPlayers())
                {
                    Rules(player);
                }
                this.CheckWinners(this.GetAllPlayers(), this.Dealer);
                restart = true;
                //human.IsInTurn = true;
                //human.FoldedTurn = false;
                //AI1.FoldedTurn = false;
                //AI2.FoldedTurn = false;
                //AI3.FoldedTurn = false;
                //AI4.FoldedTurn = false;
                //AI5.FoldedTurn = false;
                /*foreach (var enemy in this.enemies)
                {
                    enemy.FoldedTurn = false;
                    enemy.Panel.Visible = false;
                    enemy.Call = 0;
                    enemy.Raise = 0;
                    enemy.Type.Power = 0;
                    enemy.Type.Current = -1;
                }*/
                this.ResetForNextGame(this.human, this.enemies);
                if (human.Chips <= 0)
                {
                    AddChips addChipsDialog = new AddChips();
                    addChipsDialog.ShowDialog();
                    if (addChipsDialog.Amount != 0)
                    {
                        this.AddChips(this.GetAllPlayers(), addChipsDialog.Amount);
                        human.FoldedTurn = false;
                        human.IsInTurn = true;
                        this.buttonRaise.Enabled = true;
                        this.buttonFold.Enabled = true;
                        this.buttonCheck.Enabled = true;
                        this.buttonRaise.Text = "Raise";
                    }
                }
                //human.Panel.Visible = false;
                //AI1.Panel.Visible = false;
                //AI2.Panel.Visible = false;
                //AI3.Panel.Visible = false;
                //AI4.Panel.Visible = false;
                //AI5.Panel.Visible = false;
                //human.Call = 0; human.Raise = 0;
                //AI1.Call = 0; AI1.Raise = 0;
                //AI2.Call = 0; AI2.Raise = 0;
                //AI3.Call = 0; AI3.Raise = 0;
                //AI4.Call = 0; AI4.Raise = 0;
                //AI5.Call = 0; AI5.Raise = 0;
                //last = 0;
                call = this.bigBlind;
                Raise = 0;
                //ImgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
                //FoldedPlayers.Clear();
                rounds = 0;
                //human.Type.Power = 0; human.Type.Current = -1;
                //type = 0; 
                //AI1.Type.Power = 0; AI2.Type.Power = 0; AI3.Type.Power = 0; AI4.Type.Power = 0; AI5.Type.Power = 0;
                //AI1.Type.Current = -1; AI2.Type.Current = -1; AI3.Type.Current = -1; AI4.Type.Current = -1; AI5.Type.Current = -1;
                //ints.Clear();
                Winners.Clear();
                winners = 0;
                this.strongestHands.Clear();
                this.winningHand.Current = 0;
                this.winningHand.Power = 0;
                this.ClearCards();
                this.textboxPot.Text = "0";
                this.human.StatusLabel.Text = "";
                await Shuffle();
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

        private void ClearCards()
        {
            foreach (var pictureBox in this.human.PictureBoxHolder)
            {
                pictureBox.Image = null;
                pictureBox.Invalidate();
                pictureBox.Visible = false;
            }

            this.human.Cards = new List<Card>();
            foreach (var player in this.enemies)
            {
                foreach (var pictureBox in player.PictureBoxHolder)
                {
                    pictureBox.Image = null;
                    pictureBox.Invalidate();
                    pictureBox.Visible = false;
                }

                player.Cards = new List<Card>();
            }

            foreach (var pictureBox in this.Dealer.PictureBoxHolder)
            {
                pictureBox.Image = null;
                pictureBox.Invalidate();
                pictureBox.Visible = false;
            }

            this.Dealer.Cards = new List<Card>();
        }

        void FixCall(IPlayer player, int options)
        {
            if (rounds != CommunityCardBoard.End)
            {
                if (options == 1)
                {
                    if (player.StatusLabel.Text.Contains("Raise"))
                    {
                        var changeRaise = player.StatusLabel.Text.Substring(6);
                        player.Raise = int.Parse(changeRaise);
                    }
                    if (player.StatusLabel.Text.Contains("Call"))
                    {
                        var changeCall = player.StatusLabel.Text.Substring(5);
                        player.Call = int.Parse(changeCall);
                    }
                    if (player.StatusLabel.Text.Contains("Check"))
                    {
                        player.Raise = 0;
                        player.Call = 0;
                    }
                }

                if (options == 2)
                {
                    if (player.Raise != Raise && player.Raise <= Raise)
                    {
                        call = Convert.ToInt32(Raise) - player.Raise;
                    }

                    if (player.Call != call || player.Call <= call)
                    {
                        call = call - player.Call;
                    }

                    if (player.Raise == Raise && Raise > 0)
                    {
                        call = 0;
                        this.buttonCall.Enabled = false;
                        this.buttonCall.Text = "Callisfuckedup";
                    }
                }
            }
        }

        async Task AllIn()
        {
            #region All in
            int allInPlayersCount = 0;
            if (human.Chips <= 0)// && !intsadded)
            {
                if (this.human.StatusLabel.Text.Contains("Raise") || this.human.StatusLabel.Text.Contains("Call"))
                {
                    allInPlayersCount++;
                    //intsadded = true;
                }
            }

            foreach (var enemy in this.enemies)
            {
                if (enemy.Chips <= 0 && !enemy.FoldedTurn)
                {
                    allInPlayersCount++;
                }
            }
            //intsadded = false;
            /*if (AI1.Chips <= 0 && !AI1.FoldedTurn)
            {
                if (!intsadded)
                {
                    ints.Add(this.AI1.Chips);
                    intsadded = true;
                }

                intsadded = false;

            }

            if (this.AI2.Chips <= 0 && !this.AI2.FoldedTurn)
            {
                if (!intsadded)
                {
                    ints.Add(this.AI2.Chips);
                    intsadded = true;
                }

                intsadded = false;
            }

            if (this.AI3.Chips <= 0 && !this.AI3.FoldedTurn)
            {
                if (!intsadded)
                {
                    ints.Add(this.AI3.Chips);
                    intsadded = true;
                }

                intsadded = false;
            }

            if (this.AI4.Chips <= 0 && !this.AI4.FoldedTurn)
            {
                if (!intsadded)
                {
                    ints.Add(this.AI4.Chips);
                    intsadded = true;
                }

                intsadded = false;
            }

            if (this.AI5.Chips <= 0 && !this.AI5.FoldedTurn)
            {
                if (!intsadded)
                {
                    ints.Add(this.AI5.Chips);
                    intsadded = true;
                }
            }*/
            if (allInPlayersCount == this.GetNotFoldedPlayersCount(this.GetAllPlayers()))
            {
                await Finish(2);
            }
            #endregion

            var notFoldedPlayersCount = this.GetNotFoldedPlayersCount(this.GetAllPlayers());// FoldedPlayers.Count(x => x == false);

            #region LastManStanding
            if (notFoldedPlayersCount == 1)
            {
                IPlayer notFoldedPlayer = this.GetAllPlayers().FirstOrDefault(p => p.HasFolded == false);// FoldedPlayers.IndexOf(false);
                notFoldedPlayer.Chips += int.Parse(this.textboxPot.Text);
                notFoldedPlayer.ChipsTextBox.Text = notFoldedPlayer.Chips.ToString();
                notFoldedPlayer.Panel.Visible = false;
                MessageBox.Show(notFoldedPlayer.Name + " Wins");
                /*if (index == 0)
                {
                    human.Chips += int.Parse(this.textboxPot.Text);
                    this.textboxChipsAmount.Text = human.Chips.ToString();
                    human.Panel.Visible = true;
                    MessageBox.Show("Player Wins");
                }
                if (index == 1)
                {
                    AI1.Chips += int.Parse(this.textboxPot.Text);
                    this.textboxChipsAmount.Text = AI1.Chips.ToString();
                    AI1.Panel.Visible = true;
                    MessageBox.Show("Bot 1 Wins");
                }
                if (index == 2)
                {
                    AI2.Chips += int.Parse(this.textboxPot.Text);
                    this.textboxChipsAmount.Text = AI2.Chips.ToString();
                    AI2.Panel.Visible = true;
                    MessageBox.Show("Bot 2 Wins");
                }
                if (index == 3)
                {
                    AI3.Chips += int.Parse(this.textboxPot.Text);
                    this.textboxChipsAmount.Text = AI3.Chips.ToString();
                    AI3.Panel.Visible = true;
                    MessageBox.Show("Bot 3 Wins");
                }
                if (index == 4)
                {
                    AI4.Chips += int.Parse(this.textboxPot.Text);
                    this.textboxChipsAmount.Text = AI4.Chips.ToString();
                    AI4.Panel.Visible = true;
                    MessageBox.Show("Bot 4 Wins");
                }
                if (index == 5)
                {
                    AI5.Chips += int.Parse(this.textboxPot.Text);
                    this.textboxChipsAmount.Text = AI5.Chips.ToString();
                    AI5.Panel.Visible = true;
                    MessageBox.Show("Bot 5 Wins");
                }*/
                //for (int j = 0; j <= 16; j++)
                //{
                foreach (var pictureBox in this.human.PictureBoxHolder)
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
                }
                //}
                await Finish(1);
            }
            //intsadded = false;
            #endregion

            #region FiveOrLessLeft
            if (notFoldedPlayersCount < 6 && notFoldedPlayersCount > 1 && rounds >= CommunityCardBoard.End)
            {
                await Finish(2);
            }
            #endregion
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
                player.Call = 0;
                player.Raise = 0;
                player.FoldedTurn = false;
                player.HasFolded = false;
                player.IsInTurn = false;
            }

            human.IsInTurn = true;
        }
        async Task Finish(int n)
        {
            if (n == 2)
            {
                FixWinners();
            }

            this.ResetForNextGame(this.human, this.enemies);

            call = this.bigBlind; Raise = 0;
            //foldedPlayers = 5;
            //type = 0; 
            rounds = 0;
            
            Raise = 0;

            restart = false; raising = false;
            //height = 0; width = 0; 
            winners = 0;
            //Flop = 1; Turn = 2; River = 3; End = 4;
            //maxLeft = 6;
            //last = 123;
            raisedTurn = 1;
            //FoldedPlayers.Clear();
            Winners.Clear();
            //ints.Clear();
            this.strongestHands.Clear();
            this.winningHand.Current = 0;
            this.winningHand.Power = 0;
            this.textboxPot.Text = "0";
            t = 60; up = 10000000; turnCount = 0;

            foreach (var player in this.GetAllPlayers())
            {
                player.StatusLabel.Text = string.Empty;
            }

            if (human.Chips <= 0)
            {
                AddChips f2 = new AddChips();
                f2.ShowDialog();
                if (f2.Amount != 0)
                {
                    foreach (var player in this.GetAllPlayers())
                    {
                        player.Chips += f2.Amount;
                    }
                    human.FoldedTurn = false;
                    human.IsInTurn = true;
                    this.buttonRaise.Enabled = true;
                    this.buttonFold.Enabled = true;
                    this.buttonCheck.Enabled = true;
                    this.buttonRaise.Text = "Raise";
                }
            }
            //ImgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
            this.ClearCards();
            await Shuffle();
            //await Turns();
        }

        void FixWinners()
        {
            this.strongestHands.Clear();
            this.winningHand.Current = 0;
            this.winningHand.Power = 0;
            /*string fixedLast = "qwerty";
            if (!this.human.StatusLabel.Text.Contains("Fold"))
            {
                fixedLast = "Player";
                Rules(human);
            }
            if (!this.AI1.StatusLabel.Text.Contains("Fold"))
            {
                fixedLast = "Bot 1";
                Rules(AI1);
            }
            if (!this.AI2.StatusLabel.Text.Contains("Fold"))
            {
                fixedLast = "Bot 2";
                Rules(AI2);
            }
            if (!this.AI3.StatusLabel.Text.Contains("Fold"))
            {
                fixedLast = "Bot 3";
                Rules(AI3);
            }
            if (!this.AI4.StatusLabel.Text.Contains("Fold"))
            {
                fixedLast = "Bot 4";
                Rules(AI4);
            }
            if (!this.AI5.StatusLabel.Text.Contains("Fold"))
            {
                fixedLast = "Bot 5";
                Rules(AI5);
            }*/
            foreach (var player in this.GetAllPlayers())
            {
                Rules(player);
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
                //        handType.HighCard(player, call, textboxPot, ref Raise, ref raising);
                //        break;
                //    case PokerHand.PairTable:
                //        handType.PairTable(player, call, textboxPot, ref Raise, ref raising);
                //        break;
                //    case PokerHand.PairFromHand:
                //        handType.PairHand(player, call, textboxPot, ref Raise, ref raising, rounds);
                //        break;
                //    case PokerHand.TwoPair:
                //        handType.TwoPair(player, call, textboxPot, ref Raise, ref raising, rounds);
                //        break;
                //    case PokerHand.ThreeOfAKind:
                //        handType.ThreeOfAKind(player, call, textboxPot, ref Raise, ref raising);
                //        break;
                //    case PokerHand.Straigth:
                //        handType.Straight(player, call, textboxPot, ref Raise, ref raising);
                //        break;
                //    case PokerHand.Flush:
                //    case PokerHand.FlushWithAce:
                //        handType.Flush(player, call, textboxPot, ref Raise, ref raising);
                //        break;
                //    case PokerHand.FullHouse:
                //        handType.FullHouse(player, call, textboxPot, ref Raise, ref raising);
                //        break;
                //    case PokerHand.FourOfAKind:
                //        handType.FourOfAKind(player, call, textboxPot, ref Raise, ref raising);
                //        break;
                //    case PokerHand.StraightFlush:
                //    case PokerHand.RoyalFlush:
                //        handType.StraightFlush(player, call, textboxPot, ref Raise, ref raising);
                //        break;
                //    default:
                //        throw new InvalidOperationException("Invalid Pocker Hand");
                //}


                if (player.Type.Current == PokerHand.HighCard)
                {
                    handType.HighCard(player, call, textboxPot, ref Raise, ref raising);
                }

                if (player.Type.Current == PokerHand.PairTable)
                {
                    handType.PairTable(player, call, textboxPot, ref Raise, ref raising);
                }

                if (player.Type.Current == PokerHand.PairFromHand)
                {
                    handType.PairHand(player, call, textboxPot, ref Raise, ref raising, rounds);
                }

                if (player.Type.Current == PokerHand.TwoPair)
                {
                    handType.TwoPair(player, call, textboxPot, ref Raise, ref raising, rounds);
                }

                if (player.Type.Current == PokerHand.ThreeOfAKind)
                {
                    handType.ThreeOfAKind(player, call, textboxPot, ref Raise, ref raising);
                }

                if (player.Type.Current == PokerHand.Straigth)
                {
                    handType.Straight(player, call, textboxPot, ref Raise, ref raising);
                }

                if (player.Type.Current == PokerHand.Flush || player.Type.Current == PokerHand.FlushWithAce)
                {
                    handType.Flush(player, call, textboxPot, ref Raise, ref raising);
                }

                if (player.Type.Current == PokerHand.FullHouse)
                {
                    handType.FullHouse(player, call, textboxPot, ref Raise, ref raising);
                }

                if (player.Type.Current == PokerHand.FourOfAKind)
                {
                    handType.FourOfAKind(player, call, textboxPot, ref Raise, ref raising);
                }

                if (player.Type.Current == PokerHand.StraightFlush || player.Type.Current == PokerHand.RoyalFlush)
                {
                    handType.StraightFlush(player, call, textboxPot, ref Raise, ref raising);
                }
            }
            
            if (player.FoldedTurn)
            {
                player.PictureBoxHolder[0].Visible = false;
                player.PictureBoxHolder[1].Visible = false;
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

            if (t > 0)
            {
                t--;
                this.progressbarTimer.Value = (t / 6) * 100;
            }
        }

        private void UpdateTick(object sender, object e)
        {
            this.UpdatePlayersChipsTextBoxes(this.GetAllPlayers());

            if (this.human.Chips <= 0)
            {
                this.human.IsInTurn = false;
                this.human.FoldedTurn = true;
                this.buttonCall.Enabled = false;
                this.buttonRaise.Enabled = false;
                this.buttonFold.Enabled = false;
                this.buttonCheck.Enabled = false;
            }

            if (up > 0)
            {
                up--;
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
            this.human.StatusLabel.Text = "Fold";
            this.human.IsInTurn = false;
            this.human.FoldedTurn = true;
            await this.Turns();
        }

        private async void OnCheckClick(object sender, EventArgs e)
        {
            if (call <= 0)
            {
                this.human.IsInTurn = false;
                this.human.StatusLabel.Text = "Check";
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
                this.human.Chips -= call;
                this.textboxChipsAmount.Text = "Chips : " + this.human.Chips.ToString();
                if (this.textboxPot.Text != "")
                {
                    this.textboxPot.Text = (int.Parse(this.textboxPot.Text) + call).ToString();
                }
                else
                {
                    this.textboxPot.Text = call.ToString();
                }

                this.human.IsInTurn = false;
                this.human.StatusLabel.Text = "Call " + call;
                this.human.Call = call;
            }
            else if (this.human.Chips <= call && call > 0)
            {
                this.textboxPot.Text = (int.Parse(this.textboxPot.Text) + this.human.Chips).ToString();
                this.human.StatusLabel.Text = "All in " + this.human.Chips;
                this.human.Chips = 0;
                this.textboxChipsAmount.Text = "Chips : " + this.human.Chips.ToString();
                this.human.IsInTurn = false;
                this.buttonFold.Enabled = false;
                this.human.Call = this.human.Chips;
            }

            await this.Turns();
        }

        private async void OnRaiseClick(object sender, EventArgs e)
        {
            this.Rules(this.human);
            int parsedValue;

            if (this.textboxRaise.Text != "" && int.TryParse(this.textboxRaise.Text, out parsedValue))
            {
                if (this.human.Chips > call)
                {
                    if (Raise * 2 > int.Parse(this.textboxRaise.Text))
                    {
                        this.textboxRaise.Text = (Raise * 2).ToString();
                        MessageBox.Show("You must raise atleast twice as the current raise !");
                        return;
                    }
                    else
                    {
                        if (this.human.Chips >= int.Parse(this.textboxRaise.Text))
                        {
                            call = int.Parse(this.textboxRaise.Text);
                            Raise = int.Parse(this.textboxRaise.Text);
                            this.human.StatusLabel.Text = "Raise " + call;
                            this.textboxPot.Text = (int.Parse(this.textboxPot.Text) + call).ToString();
                            this.buttonCall.Text = "Call";
                            this.human.Chips -= int.Parse(this.textboxRaise.Text);
                            raising = true;
                            //last = 0;
                            this.human.Raise = Convert.ToInt32(Raise);
                        }
                        else
                        {
                            call = this.human.Chips;
                            Raise = this.human.Chips;
                            this.textboxPot.Text = (int.Parse(this.textboxPot.Text) + this.human.Chips).ToString();
                            this.human.StatusLabel.Text = "Raise " + call;
                            this.human.Chips = 0;
                            raising = true;
                            //last = 0;
                            this.human.Raise = Convert.ToInt32(Raise);
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
            if (this.human.ChipsTextBox.Text != string.Empty)
            {
                int chipsToAdd;
                int.TryParse(this.human.ChipsTextBox.Text, out chipsToAdd);

                var allPlayers = this.GetAllPlayers();
                foreach (var player in allPlayers)
                {
                    player.Chips += chipsToAdd;
                }

                this.UpdatePlayersChipsTextBoxes(allPlayers);
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

        private void LayoutChange(object sender, LayoutEventArgs e)
        {
            // width = this.Width;
            // height = this.Height;
        }

        #endregion
    }
}