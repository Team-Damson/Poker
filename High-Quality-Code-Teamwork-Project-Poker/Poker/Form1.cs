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

    public partial class Form1 : Form
    {
        HandTypes handType = new HandTypes();
        CheckHandType chHandType = new CheckHandType();

        #region Variables

        private IPlayer human;
        private IPlayer AI1;
        private IPlayer AI2;
        private IPlayer AI3;
        private IPlayer AI4;
        private IPlayer AI5;

        ProgressBar asd = new ProgressBar();
        public int Nm;
        //Panel pPanel = new Panel(); Panel b1Panel = new Panel(); Panel b2Panel = new Panel(); Panel b3Panel = new Panel();
        //Panel b4Panel = new Panel(); Panel b5Panel = new Panel();
        int call = 500, foldedPlayers = 5;
        //public int Chips = 10000, bot1Chips = 10000, bot2Chips = 10000, bot3Chips = 10000, bot4Chips = 10000, bot5Chips = 10000;
        private double type;
        double rounds = 0, /*b1Power, b2Power, b3Power, b4Power, b5Power, pPower = 0, pType = -1,*/ Raise = 0;
        // b1Type = -1, b2Type = -1, b3Type = -1, b4Type = -1, b5Type = -1;
        //bool B1turn = false, B2turn = false, B3turn = false, B4turn = false, B5turn = false;
        //bool B1Fturn = false, B2Fturn = false, B3Fturn = false, B4Fturn = false, B5Fturn = false;
        bool /*pFolded, b1Folded, b2Folded, b3Folded, b4Folded, b5Folded,*/ intsadded, changed;
        //int pCall = 0, b1Call = 0, b2Call = 0, b3Call = 0, b4Call = 0, b5Call = 0, pRaise = 0, b1Raise = 0, b2Raise = 0, b3Raise = 0, b4Raise = 0, b5Raise = 0;
        int height, width, winners = 0, Flop = 1, Turn = 2, River = 3, End = 4, maxLeft = 6;
        int last = 123, raisedTurn = 1;
        List<bool?> bools = new List<bool?>();
        List<Type> Win = new List<Type>();
        List<string> CheckWinners = new List<string>();
        List<int> ints = new List<int>();
        bool /*PFturn = false, Pturn = true,*/ restart = false, raising = false;
        Poker.Type sorted;
        string[] ImgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
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
        int[] Reserve = new int[17];
        Image[] Deck = new Image[52];
        PictureBox[] Holder = new PictureBox[52];
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

            //bools.Add(PFturn); bools.Add(B1Fturn); bools.Add(B2Fturn); bools.Add(B3Fturn); bools.Add(B4Fturn); bools.Add(B5Fturn);
            this.human = PlayerFactory.Create("Player", 10000, this.labelPlayerStatus, this.textboxPlayerChips, 0, 0);
            this.AI1 = PlayerFactory.Create("Bot 1", 10000, this.labelBot1Status, this.textboxBot1Chips, 0, 0);
            this.AI2 = PlayerFactory.Create("Bot 2", 10000, this.labelBot2Status, this.textboxBot2Chips, 0, 0);
            this.AI3 = PlayerFactory.Create("Bot 3", 10000, this.labelBot3Status, this.textboxBot3Chips, 0, 0);
            this.AI4 = PlayerFactory.Create("Bot 4", 10000, this.labelBot4Status, this.textboxBot4Chips, 0, 0);
            this.AI5 = PlayerFactory.Create("Bot 5", 10000, this.labelBot5Status, this.textboxBot5Chips, 0, 0);

            this.enemies = new List<IPlayer>() {AI1, AI2, AI3, AI4, AI5};

            call = this.bigBlind;
            MaximizeBox = false;
            MinimizeBox = false;
            Updates.Start();
            
            width = this.Width;
            height = this.Height;
            Shuffle();
            this.textboxPot.Enabled = false;
            this.textboxChipsAmount.Enabled = false;
            this.textboxBot1Chips.Enabled = false;
            this.textboxBot2Chips.Enabled = false;
            this.textboxBot3Chips.Enabled = false;
            this.textboxBot4Chips.Enabled = false;
            this.textboxBot5Chips.Enabled = false;
            this.textboxChipsAmount.Text = "Chips : " + human.Chips.ToString();
            this.textboxBot1Chips.Text = "Chips : " + AI1.Chips.ToString();
            this.textboxBot2Chips.Text = "Chips : " + AI2.Chips.ToString();
            this.textboxBot3Chips.Text = "Chips : " + AI3.Chips.ToString();
            this.textboxBot4Chips.Text = "Chips : " + AI4.Chips.ToString();
            this.textboxBot5Chips.Text = "Chips : " + AI5.Chips.ToString();
            timer.Interval = (1 * 1 * 1000);
            timer.Tick += timer_Tick;
            Updates.Interval = (1 * 1 * 100);
            Updates.Tick += Update_Tick;
            this.textboxBigBlind.Visible = true;
            this.textboxSmallBlind.Visible = true;
            this.buttonBigBlind.Visible = true;
            this.buttonSmallBlind.Visible = true;
            this.textboxBigBlind.Visible = true;
            this.textboxSmallBlind.Visible = true;
            this.buttonBigBlind.Visible = true;
            this.buttonSmallBlind.Visible = true;
            this.textboxBigBlind.Visible = false;
            this.textboxSmallBlind.Visible = false;
            this.buttonBigBlind.Visible = false;
            this.buttonSmallBlind.Visible = false;
            this.textboxRaise.Text = (this.bigBlind * 2).ToString();
        }
        async Task Shuffle()
        {
            bools.Add(human.FoldedTurn); bools.Add(AI1.FoldedTurn); bools.Add(AI2.FoldedTurn); bools.Add(AI3.FoldedTurn); bools.Add(AI4.FoldedTurn); bools.Add(AI5.FoldedTurn);
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
                    last = 1;
                    currentAI.IsInTurn = false;
                    nextAI.IsInTurn = true;
                }
            }
            if (currentAI.FoldedTurn && !currentAI.HasFolded)
            {
                bools.RemoveAt(currentAI.Id + 1);
                bools.Insert(currentAI.Id + 1, null);
                maxLeft--;
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
                        bools.RemoveAt(0);
                        bools.Insert(0, null);
                        maxLeft--;
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
                AI1.IsInTurn = true;
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
                        bools.RemoveAt(0);
                        bools.Insert(0, null);
                        maxLeft--;
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

        void Rules(IPlayer player)
        {
            if (!player.FoldedTurn || player.CardIndexes.First() == 0 && player.CardIndexes.Last() == 1 && this.labelPlayerStatus.Text.Contains("Fold") == false)
            {
                #region Variables
                bool done = false, vf = false;
                int[] Straight1 = new int[5];
                int[] Straight = new int[7];
                Straight[0] = Reserve[player.CardIndexes.First()];
                Straight[1] = Reserve[player.CardIndexes.Last()];
                Straight1[0] = Straight[2] = Reserve[12];
                Straight1[1] = Straight[3] = Reserve[13];
                Straight1[2] = Straight[4] = Reserve[14];
                Straight1[3] = Straight[5] = Reserve[15];
                Straight1[4] = Straight[6] = Reserve[16];
                var a = Straight.Where(o => o % 4 == 0).ToArray();
                var b = Straight.Where(o => o % 4 == 1).ToArray();
                var c = Straight.Where(o => o % 4 == 2).ToArray();
                var d = Straight.Where(o => o % 4 == 3).ToArray();
                var st1 = a.Select(o => o / 4).Distinct().ToArray();
                var st2 = b.Select(o => o / 4).Distinct().ToArray();
                var st3 = c.Select(o => o / 4).Distinct().ToArray();
                var st4 = d.Select(o => o / 4).Distinct().ToArray();
                Array.Sort(Straight); Array.Sort(st1); Array.Sort(st2); Array.Sort(st3); Array.Sort(st4);
                #endregion
                for (i = 0; i < 16; i++)
                {
                    if (Reserve[i] == int.Parse(Holder[player.CardIndexes.First()].Tag.ToString()) && Reserve[i + 1] == int.Parse(Holder[player.CardIndexes.Last()].Tag.ToString()))
                    {
                        //Pair from Hand current = 1

                        chHandType.rPairFromHand(player, ref Win, ref sorted, ref Reserve, i);

                        #region Pair or Two Pair from Table current = 2 || 0
                        chHandType.rPairTwoPair(player, ref Win, ref sorted, ref Reserve, i);
                        #endregion

                        #region Two Pair current = 2
                        chHandType.rTwoPair(player, ref Win, ref sorted, ref Reserve, i);
                        #endregion

                        #region Three of a kind current = 3
                        chHandType.rThreeOfAKind(player, Straight, ref Win, ref sorted);
                        #endregion

                        #region Straight current = 4
                        chHandType.rStraight(player, Straight, ref Win, ref sorted);
                        #endregion

                        #region Flush current = 5 || 5.5
                        chHandType.rFlush(player, ref vf, Straight1, ref Win, ref sorted, ref Reserve, i);
                        #endregion

                        #region Full House current = 6
                        chHandType.rFullHouse(player, ref done, Straight, ref Win, ref sorted, ref type);
                        #endregion

                        #region Four of a Kind current = 7
                        chHandType.rFourOfAKind(player, Straight, ref Win, ref sorted);
                        #endregion

                        #region Straight Flush current = 8 || 9
                        chHandType.rStraightFlush(player, st1, st2, st3, st4, ref Win, ref sorted);
                        #endregion

                        #region High Card current = -1
                        chHandType.rHighCard(player, ref Win, ref sorted, ref Reserve, i);
                        #endregion
                    }
                }
            }
        }
        

        void Winner(IPlayer player, string lastly)
        {
            if (lastly == " ")
            {
                lastly = "Bot 5";
            }
            for (int j = 0; j <= 16; j++)
            {
                //await Task.Delay(5);
                if (Holder[j].Visible)
                    Holder[j].Image = Deck[j];
            }
            if (player.Type.Current == sorted.Current)
            {
                if (player.Type.Power == sorted.Power)
                {
                    winners++;
                    CheckWinners.Add(player.Name);
                    if (player.Type.Current == -1)
                    {
                        MessageBox.Show(player.Name + " High Card ");
                    }
                    if (player.Type.Current == 1 || player.Type.Current == 0)
                    {
                        MessageBox.Show(player.Name + " Pair ");
                    }
                    if (player.Type.Current == 2)
                    {
                        MessageBox.Show(player.Name + " Two Pair ");
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
                    if (CheckWinners.Contains("Player"))
                    {
                        human.Chips += int.Parse(this.textboxPot.Text) / winners;
                        this.human.PlayerChips.Text = human.Chips.ToString();
                        //pPanel.Visible = true;

                    }
                    if (CheckWinners.Contains("Bot 1"))
                    {
                        AI1.Chips += int.Parse(this.textboxPot.Text) / winners;
                        this.AI1.PlayerChips.Text = AI1.Chips.ToString();
                        //b1Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 2"))
                    {
                        AI2.Chips += int.Parse(this.textboxPot.Text) / winners;
                        this.AI2.PlayerChips.Text = AI2.Chips.ToString();
                        //b2Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 3"))
                    {
                        AI3.Chips += int.Parse(this.textboxPot.Text) / winners;
                        this.AI3.PlayerChips.Text = AI3.Chips.ToString();
                        //b3Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 4"))
                    {
                        AI4.Chips += int.Parse(this.textboxPot.Text) / winners;
                        this.AI4.PlayerChips.Text = AI4.Chips.ToString();
                        //b4Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 5"))
                    {
                        AI5.Chips += int.Parse(this.textboxPot.Text) / winners;
                        this.AI5.PlayerChips.Text = AI5.Chips.ToString();
                        //b5Panel.Visible = true;
                    }
                    //await Finish(1);
                }
                if (winners == 1)
                {
                    if (CheckWinners.Contains("Player"))
                    {
                        human.Chips += int.Parse(this.textboxPot.Text);
                        //await Finish(1);
                        //pPanel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 1"))
                    {
                        AI1.Chips += int.Parse(this.textboxPot.Text);
                        //await Finish(1);
                        //b1Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 2"))
                    {
                        AI2.Chips += int.Parse(this.textboxPot.Text);
                        //await Finish(1);
                        //b2Panel.Visible = true;

                    }
                    if (CheckWinners.Contains("Bot 3"))
                    {
                        AI3.Chips += int.Parse(this.textboxPot.Text);
                        //await Finish(1);
                        //b3Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 4"))
                    {
                        AI4.Chips += int.Parse(this.textboxPot.Text);
                        //await Finish(1);
                        //b4Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 5"))
                    {
                        AI5.Chips += int.Parse(this.textboxPot.Text);
                        //await Finish(1);
                        //b5Panel.Visible = true;
                    }
                }
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
                if (turnCount >= maxLeft - 1 || !changed && turnCount == maxLeft)
                {
                    if (currentTurn == raisedTurn - 1 || !changed && turnCount == maxLeft || raisedTurn == 0 && currentTurn == 5)
                    {
                        changed = false;
                        turnCount = 0;
                        Raise = 0;
                        call = 0;
                        raisedTurn = 123;
                        rounds++;
                        if (!human.FoldedTurn)
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
                            this.AI5.StatusLabel.Text = "";
                    }
                }
            }
            if (rounds == Flop)
            {
                for (int j = 12; j <= 14; j++)
                {
                    if (Holder[j].Image != Deck[j])
                    {
                        Holder[j].Image = Deck[j];
                        human.Call = 0; human.Raise = 0;
                        AI1.Call = 0; AI1.Raise = 0;
                        AI2.Call = 0; AI2.Raise = 0;
                        AI3.Call = 0; AI3.Raise = 0;
                        AI4.Call = 0; AI4.Raise = 0;
                        AI5.Call = 0; AI5.Raise = 0;
                    }
                }
            }
            if (rounds == Turn)
            {
                for (int j = 14; j <= 15; j++)
                {
                    if (Holder[j].Image != Deck[j])
                    {
                        Holder[j].Image = Deck[j];
                        human.Call = 0; human.Raise = 0;
                        AI1.Call = 0; AI1.Raise = 0;
                        AI2.Call = 0; AI2.Raise = 0;
                        AI3.Call = 0; AI3.Raise = 0;
                        AI4.Call = 0; AI4.Raise = 0;
                        AI5.Call = 0; AI5.Raise = 0;
                    }
                }
            }
            if (rounds == River)
            {
                for (int j = 15; j <= 16; j++)
                {
                    if (Holder[j].Image != Deck[j])
                    {
                        Holder[j].Image = Deck[j];
                        human.Call = 0; human.Raise = 0;
                        AI1.Call = 0; AI1.Raise = 0;
                        AI2.Call = 0; AI2.Raise = 0;
                        AI3.Call = 0; AI3.Raise = 0;
                        AI4.Call = 0; AI4.Raise = 0;
                        AI5.Call = 0; AI5.Raise = 0;
                    }
                }
            }
            if (rounds == End && maxLeft == 6)
            {
                string fixedLast = "qwerty";
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
                }
                Winner(human, fixedLast);
                Winner(AI1, fixedLast);
                Winner(AI2, fixedLast);
                Winner(AI3, fixedLast);
                Winner(AI4, fixedLast);
                Winner(AI5, fixedLast);
                restart = true;
                human.IsInTurn = true;
                human.FoldedTurn = false;
                AI1.FoldedTurn = false;
                AI2.FoldedTurn = false;
                AI3.FoldedTurn = false;
                AI4.FoldedTurn = false;
                AI5.FoldedTurn = false;
                if (human.Chips <= 0)
                {
                    AddChips f2 = new AddChips();
                    f2.ShowDialog();
                    if (f2.Amount != 0)
                    {
                        human.Chips = f2.Amount;
                        AI1.Chips += f2.Amount;
                        AI2.Chips += f2.Amount;
                        AI3.Chips += f2.Amount;
                        AI4.Chips += f2.Amount;
                        AI5.Chips += f2.Amount;
                        human.FoldedTurn = false;
                        human.IsInTurn = true;
                        this.buttonRaise.Enabled = true;
                        this.buttonFold.Enabled = true;
                        this.buttonCheck.Enabled = true;
                        this.buttonRaise.Text = "Raise";
                    }
                }
                human.Panel.Visible = false;
                AI1.Panel.Visible = false;
                AI2.Panel.Visible = false;
                AI3.Panel.Visible = false;
                AI4.Panel.Visible = false;
                AI5.Panel.Visible = false;
                human.Call = 0; human.Raise = 0;
                AI1.Call = 0; AI1.Raise = 0;
                AI2.Call = 0; AI2.Raise = 0;
                AI3.Call = 0; AI3.Raise = 0;
                AI4.Call = 0; AI4.Raise = 0;
                AI5.Call = 0; AI5.Raise = 0;
                last = 0;
                call = this.bigBlind;
                Raise = 0;
                ImgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
                bools.Clear();
                rounds = 0;
                human.Type.Power = 0; human.Type.Current = -1;
                type = 0; AI1.Type.Power = 0; AI2.Type.Power = 0; AI3.Type.Power = 0; AI4.Type.Power = 0; AI5.Type.Power = 0;
                AI1.Type.Current = -1; AI2.Type.Current = -1; AI3.Type.Current = -1; AI4.Type.Current = -1; AI5.Type.Current = -1;
                ints.Clear();
                CheckWinners.Clear();
                winners = 0;
                Win.Clear();
                sorted.Current = 0;
                sorted.Power = 0;
                for (int os = 0; os < 17; os++)
                {
                    Holder[os].Image = null;
                    Holder[os].Invalidate();
                    Holder[os].Visible = false;
                }
                this.textboxPot.Text = "0";
                this.human.StatusLabel.Text = "";
                await Shuffle();
                await Turns();
            }
        }
        void FixCall(IPlayer player, int options)
        {
            if (rounds != 4)
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
            if (human.Chips <= 0 && !intsadded)
            {
                if (this.human.StatusLabel.Text.Contains("Raise"))
                {
                    ints.Add(human.Chips);
                    intsadded = true;
                }
                if (this.human.StatusLabel.Text.Contains("Call"))
                {
                    ints.Add(human.Chips);
                    intsadded = true;
                }
            }
            intsadded = false;
            if (AI1.Chips <= 0 && !AI1.FoldedTurn)
            {
                if (!intsadded)
                {
                    ints.Add(AI1.Chips);
                    intsadded = true;
                }
                intsadded = false;
            }
            if (AI2.Chips <= 0 && !AI2.FoldedTurn)
            {
                if (!intsadded)
                {
                    ints.Add(AI2.Chips);
                    intsadded = true;
                }
                intsadded = false;
            }
            if (AI3.Chips <= 0 && !AI3.FoldedTurn)
            {
                if (!intsadded)
                {
                    ints.Add(AI3.Chips);
                    intsadded = true;
                }
                intsadded = false;
            }
            if (AI4.Chips <= 0 && !AI4.FoldedTurn)
            {
                if (!intsadded)
                {
                    ints.Add(AI4.Chips);
                    intsadded = true;
                }
                intsadded = false;
            }
            if (AI5.Chips <= 0 && !AI5.FoldedTurn)
            {
                if (!intsadded)
                {
                    ints.Add(AI5.Chips);
                    intsadded = true;
                }
            }
            if (ints.ToArray().Length == maxLeft)
            {
                await Finish(2);
            }
            else
            {
                ints.Clear();
            }
            #endregion

            var abc = bools.Count(x => x == false);

            #region LastManStanding
            if (abc == 1)
            {
                int index = bools.IndexOf(false);
                if (index == 0)
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
                }
                for (int j = 0; j <= 16; j++)
                {
                    Holder[j].Visible = false;
                }
                await Finish(1);
            }
            intsadded = false;
            #endregion

            #region FiveOrLessLeft
            if (abc < 6 && abc > 1 && rounds >= End)
            {
                await Finish(2);
            }
            #endregion


        }
        async Task Finish(int n)
        {
            if (n == 2)
            {
                FixWinners();
            }
            human.Panel.Visible = false; AI1.Panel.Visible = false; AI2.Panel.Visible = false; AI3.Panel.Visible = false; AI4.Panel.Visible = false; AI5.Panel.Visible = false;
            call = this.bigBlind; Raise = 0;
            foldedPlayers = 5;
            type = 0; rounds = 0;
            AI1.Type.Power = 0; 
            AI2.Type.Power = 0; 
            AI3.Type.Power = 0; 
            AI4.Type.Power = 0; 
            AI5.Type.Power = 0;
            human.Type.Power = 0; 
            human.Type.Current = -1; 
            Raise = 0;
            AI1.Type.Current = -1; AI2.Type.Current = -1; AI3.Type.Current = -1; AI4.Type.Current = -1; AI5.Type.Current = -1;
            AI1.IsInTurn = false; AI2.IsInTurn = false; AI3.IsInTurn = false; AI4.IsInTurn = false; AI5.IsInTurn = false;
            AI1.FoldedTurn = false; AI2.FoldedTurn = false; AI3.FoldedTurn = false; AI4.FoldedTurn = false; AI5.FoldedTurn = false;
            human.HasFolded = false; AI1.HasFolded = false; AI2.HasFolded= false; AI3.HasFolded= false; AI4.HasFolded= false; AI5.HasFolded= false;
            human.FoldedTurn = false; human.IsInTurn = true; restart = false; raising = false;
            human.Call = 0; AI1.Call = 0; AI2.Call = 0; AI3.Call = 0; AI4.Call = 0; AI5.Call = 0; 
            human.Raise = 0; AI1.Raise = 0; AI2.Raise = 0; AI3.Raise = 0; AI4.Raise = 0; AI5.Raise = 0;
            height = 0; width = 0; winners = 0; Flop = 1; Turn = 2; River = 3; End = 4; maxLeft = 6;
            last = 123; raisedTurn = 1;
            bools.Clear();
            CheckWinners.Clear();
            ints.Clear();
            Win.Clear();
            sorted.Current = 0;
            sorted.Power = 0;
            this.textboxPot.Text = "0";
            t = 60; up = 10000000; turnCount = 0;
            this.human.StatusLabel.Text = "";
            this.AI1.StatusLabel.Text = "";
            this.AI2.StatusLabel.Text = "";
            this.AI3.StatusLabel.Text = "";
            this.AI4.StatusLabel.Text = "";
            this.AI5.StatusLabel.Text = "";
            if (human.Chips <= 0)
            {
                AddChips f2 = new AddChips();
                f2.ShowDialog();
                if (f2.Amount != 0)
                {
                    human.Chips = f2.Amount;
                    AI1.Chips += f2.Amount;
                    AI2.Chips += f2.Amount;
                    AI3.Chips += f2.Amount;
                    AI4.Chips += f2.Amount;
                    AI5.Chips += f2.Amount;
                    human.FoldedTurn = false;
                    human.IsInTurn = true;
                    this.buttonRaise.Enabled = true;
                    this.buttonFold.Enabled = true;
                    this.buttonCheck.Enabled = true;
                    this.buttonRaise.Text = "Raise";
                }
            }
            ImgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
            for (int os = 0; os < 17; os++)
            {
                Holder[os].Image = null;
                Holder[os].Invalidate();
                Holder[os].Visible = false;
            }
            await Shuffle();
            //await Turns();
        }
        void FixWinners()
        {
            Win.Clear();
            sorted.Current = 0;
            sorted.Power = 0;
            string fixedLast = "qwerty";
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
            }
            Winner(human, fixedLast);
            Winner(AI1, fixedLast);
            Winner(AI2, fixedLast);
            Winner(AI3, fixedLast);
            Winner(AI4, fixedLast);
            Winner(AI5, fixedLast);
        }
        void AI(IPlayer player)
        {
            if (!player.FoldedTurn)
            {
                if (player.Type.Current == -1)
                {
                    handType.HighCard(player, call, textboxPot, ref Raise, ref raising);
                }
                if (player.Type.Current == 0)
                {
                    handType.PairTable(player, call, textboxPot, ref Raise, ref raising);
                }
                if (player.Type.Current == 1)
                {
                    handType.PairHand(player, call, textboxPot, ref Raise, ref raising, rounds);
                }
                if (player.Type.Current == 2)
                {
                    handType.TwoPair(player, call, textboxPot, ref Raise, ref raising, rounds);
                }
                if (player.Type.Current == 3)
                {
                    handType.ThreeOfAKind(player, call, textboxPot, ref Raise, ref raising, ref rounds);
                }
                if (player.Type.Current == 4)
                {
                    handType.Straight(player, call, textboxPot, ref Raise, ref raising, ref rounds);
                }
                if (player.Type.Current == 5 || player.Type.Current == 5.5)
                {
                    handType.Flush(player, call, textboxPot, ref Raise, ref raising, ref rounds);
                }
                if (player.Type.Current == 6)
                {
                    handType.FullHouse(player, call, textboxPot, ref Raise, ref raising, ref rounds);
                }
                if (player.Type.Current == 7)
                {
                    handType.FourOfAKind(player, call, textboxPot, ref Raise, ref raising, ref rounds);
                }
                if (player.Type.Current == 8 || player.Type.Current == 9)
                {
                    handType.StraightFlush(player, call, textboxPot, ref Raise, ref raising, ref rounds);
                }
            }
            if (player.FoldedTurn)
            {
                Holder[player.CardIndexes.First()].Visible = false;
                Holder[player.CardIndexes.Last()].Visible = false;
            }
        }
     

      
             
        
     

        

        #region UI
        private async void timer_Tick(object sender, object e)
        {
            if (this.progressbarTimer.Value <= 0)
            {
                human.FoldedTurn = true;
                await Turns();
            }
            if (t > 0)
            {
                t--;
                this.progressbarTimer.Value = (t / 6) * 100;
            }
        }
        private void Update_Tick(object sender, object e)
        {
            if (human.Chips <= 0)
            {
                this.textboxChipsAmount.Text = "Chips : 0";
            }
            if (AI1.Chips <= 0)
            {
                this.textboxBot1Chips.Text = "Chips : 0";
            }
            if (AI2.Chips <= 0)
            {
                this.textboxBot2Chips.Text = "Chips : 0";
            }
            if (AI3.Chips <= 0)
            {
                this.textboxBot3Chips.Text = "Chips : 0";
            }
            if (AI4.Chips <= 0)
            {
                this.textboxBot4Chips.Text = "Chips : 0";
            }
            if (AI5.Chips <= 0)
            {
                this.textboxBot5Chips.Text = "Chips : 0";
            }
            this.textboxChipsAmount.Text = "Chips : " + human.Chips.ToString();
            this.textboxBot1Chips.Text = "Chips : " + AI1.Chips.ToString();
            this.textboxBot2Chips.Text = "Chips : " + AI2.Chips.ToString();
            this.textboxBot3Chips.Text = "Chips : " + AI3.Chips.ToString();
            this.textboxBot4Chips.Text = "Chips : " + AI4.Chips.ToString();
            this.textboxBot5Chips.Text = "Chips : " + AI5.Chips.ToString();
            if (human.Chips <= 0)
            {
                human.IsInTurn = false;
                human.FoldedTurn = true;
                this.buttonCall.Enabled = false;
                this.buttonRaise.Enabled = false;
                this.buttonFold.Enabled = false;
                this.buttonCheck.Enabled = false;
            }
            if (up > 0)
            {
                up--;
            }
            if (human.Chips >= call)
            {
                this.buttonCall.Text = "Call " + call.ToString();
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
            if (human.Chips <= 0)
            {
                this.buttonRaise.Enabled = false;
            }
            int parsedValue;

            if (this.textboxRaise.Text != "" && int.TryParse(this.textboxRaise.Text, out parsedValue))
            {
                if (human.Chips <= int.Parse(this.textboxRaise.Text))
                {
                    this.buttonRaise.Text = "All in";
                }
                else
                {
                    this.buttonRaise.Text = "Raise";
                }
            }
            if (human.Chips < call)
            {
                this.buttonRaise.Enabled = false;
            }
        }
        private async void bFold_Click(object sender, EventArgs e)
        {
            this.human.StatusLabel.Text = "Fold";
            human.IsInTurn = false;
            human.FoldedTurn = true;
            await Turns();
        }
        private async void bCheck_Click(object sender, EventArgs e)
        {
            if (call <= 0)
            {
                human.IsInTurn = false;
                this.human.StatusLabel.Text = "Check";
            }
            else
            {
                //labelPlayerStatus.Text = "All in " + Chips;

                this.buttonCheck.Enabled = false;
            }
            await Turns();
        }
        private async void bCall_Click(object sender, EventArgs e)
        {
            Rules(human);
            if (human.Chips >= call)
            {
                human.Chips -= call;
                this.textboxChipsAmount.Text = "Chips : " + human.Chips.ToString();
                if (this.textboxPot.Text != "")
                {
                    this.textboxPot.Text = (int.Parse(this.textboxPot.Text) + call).ToString();
                }
                else
                {
                    this.textboxPot.Text = call.ToString();
                }
                human.IsInTurn = false;
                this.human.StatusLabel.Text = "Call " + call;
                human.Call = call;
            }
            else if (human.Chips <= call && call > 0)
            {
                this.textboxPot.Text = (int.Parse(this.textboxPot.Text) + human.Chips).ToString();
                this.human.StatusLabel.Text = "All in " + human.Chips;
                human.Chips = 0;
                this.textboxChipsAmount.Text = "Chips : " + human.Chips.ToString();
                human.IsInTurn = false;
                this.buttonFold.Enabled = false;
                human.Call = human.Chips;
            }
            await Turns();
        }
        private async void bRaise_Click(object sender, EventArgs e)
        {
            Rules(human);
            int parsedValue;
            if (this.textboxRaise.Text != "" && int.TryParse(this.textboxRaise.Text, out parsedValue))
            {
                if (human.Chips > call)
                {
                    if (Raise * 2 > int.Parse(this.textboxRaise.Text))
                    {
                        this.textboxRaise.Text = (Raise * 2).ToString();
                        MessageBox.Show("You must raise atleast twice as the current raise !");
                        return;
                    }
                    else
                    {
                        if (human.Chips >= int.Parse(this.textboxRaise.Text))
                        {
                            call = int.Parse(this.textboxRaise.Text);
                            Raise = int.Parse(this.textboxRaise.Text);
                            this.human.StatusLabel.Text = "Raise " + call.ToString();
                            this.textboxPot.Text = (int.Parse(this.textboxPot.Text) + call).ToString();
                            this.buttonCall.Text = "Call";
                            human.Chips -= int.Parse(this.textboxRaise.Text);
                            raising = true;
                            last = 0;
                            human.Raise = Convert.ToInt32(Raise);
                        }
                        else
                        {
                            call = human.Chips;
                            Raise = human.Chips;
                            this.textboxPot.Text = (int.Parse(this.textboxPot.Text) + human.Chips).ToString();
                            this.human.StatusLabel.Text = "Raise " + call.ToString();
                            human.Chips = 0;
                            raising = true;
                            last = 0;
                            human.Raise = Convert.ToInt32(Raise);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("This is a number only field");
                return;
            }
            human.IsInTurn = false;
            await Turns();
        }
        private void bAdd_Click(object sender, EventArgs e)
        {
            if (this.human.PlayerChips.Text == "") { }
            else
            {
                human.Chips += int.Parse(this.human.PlayerChips.Text);
                AI1.Chips += int.Parse(this.human.PlayerChips.Text);
                AI2.Chips += int.Parse(this.human.PlayerChips.Text);
                AI3.Chips += int.Parse(this.human.PlayerChips.Text);
                AI4.Chips += int.Parse(this.human.PlayerChips.Text);
                AI5.Chips += int.Parse(this.human.PlayerChips.Text);
            }
            this.textboxChipsAmount.Text = "Chips : " + human.Chips.ToString();
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

        private void Layout_Change(object sender, LayoutEventArgs e)
        {
            width = this.Width;
            height = this.Height;
        }
        #endregion
    }
}