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

namespace Poker
{

    public partial class Form1 : Form
    {
        HandTypes handType = new HandTypes();
        CheckHandType chHandType = new CheckHandType();

        #region Variables
        ProgressBar asd = new ProgressBar();
        public int Nm;
        Panel pPanel = new Panel(); Panel b1Panel = new Panel(); Panel b2Panel = new Panel(); Panel b3Panel = new Panel();
        Panel b4Panel = new Panel(); Panel b5Panel = new Panel();
        int call = 500, foldedPlayers = 5;
        public int Chips = 10000, bot1Chips = 10000, bot2Chips = 10000, bot3Chips = 10000, bot4Chips = 10000, bot5Chips = 10000;
        double type, rounds = 0, b1Power, b2Power, b3Power, b4Power, b5Power, pPower = 0, pType = -1, Raise = 0,
        b1Type = -1, b2Type = -1, b3Type = -1, b4Type = -1, b5Type = -1;
        bool B1turn = false, B2turn = false, B3turn = false, B4turn = false, B5turn = false;
        bool B1Fturn = false, B2Fturn = false, B3Fturn = false, B4Fturn = false, B5Fturn = false;
        bool pFolded, b1Folded, b2Folded, b3Folded, b4Folded, b5Folded, intsadded, changed;
        int pCall = 0, b1Call = 0, b2Call = 0, b3Call = 0, b4Call = 0, b5Call = 0, pRaise = 0, b1Raise = 0, b2Raise = 0, b3Raise = 0, b4Raise = 0, b5Raise = 0;
        int height, width, winners = 0, Flop = 1, Turn = 2, River = 3, End = 4, maxLeft = 6;
        int last = 123, raisedTurn = 1;
        List<bool?> bools = new List<bool?>();
        List<Type> Win = new List<Type>();
        List<string> CheckWinners = new List<string>();
        List<int> ints = new List<int>();
        bool PFturn = false, Pturn = true, restart = false, raising = false;
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

        #endregion
        public Form1()
        {
            //bools.Add(PFturn); bools.Add(B1Fturn); bools.Add(B2Fturn); bools.Add(B3Fturn); bools.Add(B4Fturn); bools.Add(B5Fturn);
            call = this.bigBlind;
            MaximizeBox = false;
            MinimizeBox = false;
            Updates.Start();
            InitializeComponent();
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
            this.textboxChipsAmount.Text = "Chips : " + Chips.ToString();
            this.textboxBot1Chips.Text = "Chips : " + bot1Chips.ToString();
            this.textboxBot2Chips.Text = "Chips : " + bot2Chips.ToString();
            this.textboxBot3Chips.Text = "Chips : " + bot3Chips.ToString();
            this.textboxBot4Chips.Text = "Chips : " + bot4Chips.ToString();
            this.textboxBot5Chips.Text = "Chips : " + bot5Chips.ToString();
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
            bools.Add(PFturn); bools.Add(B1Fturn); bools.Add(B2Fturn); bools.Add(B3Fturn); bools.Add(B4Fturn); bools.Add(B5Fturn);
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
                    this.Controls.Add(pPanel);
                    pPanel.Location = new Point(Holder[0].Left - 10, Holder[0].Top - 10);
                    pPanel.BackColor = Color.DarkBlue;
                    pPanel.Height = 150;
                    pPanel.Width = 180;
                    pPanel.Visible = false;
                }
                if (bot1Chips > 0)
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
                        this.Controls.Add(b1Panel);
                        b1Panel.Location = new Point(Holder[2].Left - 10, Holder[2].Top - 10);
                        b1Panel.BackColor = Color.DarkBlue;
                        b1Panel.Height = 150;
                        b1Panel.Width = 180;
                        b1Panel.Visible = false;
                        if (i == 3)
                        {
                            check = false;
                        }
                    }
                }
                if (bot2Chips > 0)
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
                        this.Controls.Add(b2Panel);
                        b2Panel.Location = new Point(Holder[4].Left - 10, Holder[4].Top - 10);
                        b2Panel.BackColor = Color.DarkBlue;
                        b2Panel.Height = 150;
                        b2Panel.Width = 180;
                        b2Panel.Visible = false;
                        if (i == 5)
                        {
                            check = false;
                        }
                    }
                }
                if (bot3Chips > 0)
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
                        this.Controls.Add(b3Panel);
                        b3Panel.Location = new Point(Holder[6].Left - 10, Holder[6].Top - 10);
                        b3Panel.BackColor = Color.DarkBlue;
                        b3Panel.Height = 150;
                        b3Panel.Width = 180;
                        b3Panel.Visible = false;
                        if (i == 7)
                        {
                            check = false;
                        }
                    }
                }
                if (bot4Chips > 0)
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
                        this.Controls.Add(b4Panel);
                        b4Panel.Location = new Point(Holder[8].Left - 10, Holder[8].Top - 10);
                        b4Panel.BackColor = Color.DarkBlue;
                        b4Panel.Height = 150;
                        b4Panel.Width = 180;
                        b4Panel.Visible = false;
                        if (i == 9)
                        {
                            check = false;
                        }
                    }
                }
                if (bot5Chips > 0)
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
                        this.Controls.Add(b5Panel);
                        b5Panel.Location = new Point(Holder[10].Left - 10, Holder[10].Top - 10);
                        b5Panel.BackColor = Color.DarkBlue;
                        b5Panel.Height = 150;
                        b5Panel.Width = 180;
                        b5Panel.Visible = false;
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
                if (bot1Chips <= 0)
                {
                    B1Fturn = true;
                    Holder[2].Visible = false;
                    Holder[3].Visible = false;
                }
                else
                {
                    B1Fturn = false;
                    if (i == 3)
                    {
                        if (Holder[3] != null)
                        {
                            Holder[2].Visible = true;
                            Holder[3].Visible = true;
                        }
                    }
                }
                if (bot2Chips <= 0)
                {
                    B2Fturn = true;
                    Holder[4].Visible = false;
                    Holder[5].Visible = false;
                }
                else
                {
                    B2Fturn = false;
                    if (i == 5)
                    {
                        if (Holder[5] != null)
                        {
                            Holder[4].Visible = true;
                            Holder[5].Visible = true;
                        }
                    }
                }
                if (bot3Chips <= 0)
                {
                    B3Fturn = true;
                    Holder[6].Visible = false;
                    Holder[7].Visible = false;
                }
                else
                {
                    B3Fturn = false;
                    if (i == 7)
                    {
                        if (Holder[7] != null)
                        {
                            Holder[6].Visible = true;
                            Holder[7].Visible = true;
                        }
                    }
                }
                if (bot4Chips <= 0)
                {
                    B4Fturn = true;
                    Holder[8].Visible = false;
                    Holder[9].Visible = false;
                }
                else
                {
                    B4Fturn = false;
                    if (i == 9)
                    {
                        if (Holder[9] != null)
                        {
                            Holder[8].Visible = true;
                            Holder[9].Visible = true;
                        }
                    }
                }
                if (bot5Chips <= 0)
                {
                    B5Fturn = true;
                    Holder[10].Visible = false;
                    Holder[11].Visible = false;
                }
                else
                {
                    B5Fturn = false;
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
        async Task Turns()
        {
            #region Rotating
            if (!PFturn)
            {
                if (Pturn)
                {
                    FixCall(this.labelPlayerStatus, ref pCall, ref pRaise, 1);
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
                    FixCall(this.labelPlayerStatus, ref pCall, ref pRaise, 2);
                }
            }
            if (PFturn || !Pturn)
            {
                await AllIn();
                if (PFturn && !pFolded)
                {
                    if (this.buttonCall.Text.Contains("All in") == false || this.buttonRaise.Text.Contains("All in") == false)
                    {
                        bools.RemoveAt(0);
                        bools.Insert(0, null);
                        maxLeft--;
                        pFolded = true;
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
                B1turn = true;
                if (!B1Fturn)
                {
                    if (B1turn)
                    {
                        FixCall(this.labelBot1Status, ref b1Call, ref b1Raise, 1);
                        FixCall(this.labelBot1Status, ref b1Call, ref b1Raise, 2);
                        Rules(2, 3, "Bot 1", ref b1Type, ref b1Power, B1Fturn);
                        MessageBox.Show("Bot 1's Turn");
                        AI(2, 3, ref bot1Chips, ref B1turn, ref  B1Fturn, this.labelBot1Status, 0, b1Power, b1Type);
                        turnCount++;
                        last = 1;
                        B1turn = false;
                        B2turn = true;
                    }
                }
                if (B1Fturn && !b1Folded)
                {
                    bools.RemoveAt(1);
                    bools.Insert(1, null);
                    maxLeft--;
                    b1Folded = true;
                }
                if (B1Fturn || !B1turn)
                {
                    await CheckRaise(1, 1);
                    B2turn = true;
                }
                if (!B2Fturn)
                {
                    if (B2turn)
                    {
                        FixCall(this.labelBot2Status, ref b2Call, ref b2Raise, 1);
                        FixCall(this.labelBot2Status, ref b2Call, ref b2Raise, 2);
                        Rules(4, 5, "Bot 2", ref b2Type, ref b2Power, B2Fturn);
                        MessageBox.Show("Bot 2's Turn");
                        AI(4, 5, ref bot2Chips, ref B2turn, ref  B2Fturn, this.labelBot2Status, 1, b2Power, b2Type);
                        turnCount++;
                        last = 2;
                        B2turn = false;
                        B3turn = true;
                    }
                }
                if (B2Fturn && !b2Folded)
                {
                    bools.RemoveAt(2);
                    bools.Insert(2, null);
                    maxLeft--;
                    b2Folded = true;
                }
                if (B2Fturn || !B2turn)
                {
                    await CheckRaise(2, 2);
                    B3turn = true;
                }
                if (!B3Fturn)
                {
                    if (B3turn)
                    {
                        FixCall(this.labelBot3Status, ref b3Call, ref b3Raise, 1);
                        FixCall(this.labelBot3Status, ref b3Call, ref b3Raise, 2);
                        Rules(6, 7, "Bot 3", ref b3Type, ref b3Power, B3Fturn);
                        MessageBox.Show("Bot 3's Turn");
                        AI(6, 7, ref bot3Chips, ref B3turn, ref  B3Fturn, this.labelBot3Status, 2, b3Power, b3Type);
                        turnCount++;
                        last = 3;
                        B3turn = false;
                        B4turn = true;
                    }
                }
                if (B3Fturn && !b3Folded)
                {
                    bools.RemoveAt(3);
                    bools.Insert(3, null);
                    maxLeft--;
                    b3Folded = true;
                }
                if (B3Fturn || !B3turn)
                {
                    await CheckRaise(3, 3);
                    B4turn = true;
                }
                if (!B4Fturn)
                {
                    if (B4turn)
                    {
                        FixCall(this.labelBot4Status, ref b4Call, ref b4Raise, 1);
                        FixCall(this.labelBot4Status, ref b4Call, ref b4Raise, 2);
                        Rules(8, 9, "Bot 4", ref b4Type, ref b4Power, B4Fturn);
                        MessageBox.Show("Bot 4's Turn");
                        AI(8, 9, ref bot4Chips, ref B4turn, ref  B4Fturn, this.labelBot4Status, 3, b4Power, b4Type);
                        turnCount++;
                        last = 4;
                        B4turn = false;
                        B5turn = true;
                    }
                }
                if (B4Fturn && !b4Folded)
                {
                    bools.RemoveAt(4);
                    bools.Insert(4, null);
                    maxLeft--;
                    b4Folded = true;
                }
                if (B4Fturn || !B4turn)
                {
                    await CheckRaise(4, 4);
                    B5turn = true;
                }
                if (!B5Fturn)
                {
                    if (B5turn)
                    {
                        FixCall(this.labelBot5Status, ref b5Call, ref b5Raise, 1);
                        FixCall(this.labelBot5Status, ref b5Call, ref b5Raise, 2);
                        Rules(10, 11, "Bot 5", ref b5Type, ref b5Power, B5Fturn);
                        MessageBox.Show("Bot 5's Turn");
                        AI(10, 11, ref bot5Chips, ref B5turn, ref  B5Fturn, this.labelBot5Status, 4, b5Power, b5Type);
                        turnCount++;
                        last = 5;
                        B5turn = false;
                    }
                }
                if (B5Fturn && !b5Folded)
                {
                    bools.RemoveAt(5);
                    bools.Insert(5, null);
                    maxLeft--;
                    b5Folded = true;
                }
                if (B5Fturn || !B5turn)
                {
                    await CheckRaise(5, 5);
                    Pturn = true;
                }
                if (PFturn && !pFolded)
                {
                    if (this.buttonCall.Text.Contains("All in") == false || this.buttonRaise.Text.Contains("All in") == false)
                    {
                        bools.RemoveAt(0);
                        bools.Insert(0, null);
                        maxLeft--;
                        pFolded = true;
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

        void Rules(int c1, int c2, string currentText, ref double current, ref double Power, bool foldedTurn)
        {
            if (c1 == 0 && c2 == 1)
            {
            }
            if (!foldedTurn || c1 == 0 && c2 == 1 && this.labelPlayerStatus.Text.Contains("Fold") == false)
            {
                #region Variables
                bool done = false, vf = false;
                int[] Straight1 = new int[5];
                int[] Straight = new int[7];
                Straight[0] = Reserve[c1];
                Straight[1] = Reserve[c2];
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
                    if (Reserve[i] == int.Parse(Holder[c1].Tag.ToString()) && Reserve[i + 1] == int.Parse(Holder[c2].Tag.ToString()))
                    {
                        //Pair from Hand current = 1

                        chHandType.rPairFromHand(ref current, ref Power, ref Win, ref sorted, ref Reserve, i);

                        #region Pair or Two Pair from Table current = 2 || 0
                        chHandType.rPairTwoPair(ref current, ref Power, ref Win, ref sorted, ref Reserve, i);
                        #endregion

                        #region Two Pair current = 2
                        chHandType.rTwoPair(ref current, ref Power, ref Win, ref sorted, ref Reserve, i);
                        #endregion

                        #region Three of a kind current = 3
                        chHandType.rThreeOfAKind(ref current, ref Power, Straight, ref Win, ref sorted);
                        #endregion

                        #region Straight current = 4
                        chHandType.rStraight(ref current, ref Power, Straight, ref Win, ref sorted);
                        #endregion

                        #region Flush current = 5 || 5.5
                        chHandType.rFlush(ref current, ref Power, ref vf, Straight1, ref Win, ref sorted, ref Reserve, i);
                        #endregion

                        #region Full House current = 6
                        chHandType.rFullHouse(ref current, ref Power, ref done, Straight, ref Win, ref sorted, ref type);
                        #endregion

                        #region Four of a Kind current = 7
                        chHandType.rFourOfAKind(ref current, ref Power, Straight, ref Win, ref sorted);
                        #endregion

                        #region Straight Flush current = 8 || 9
                        chHandType.rStraightFlush(ref current, ref Power, st1, st2, st3, st4, ref Win, ref sorted);
                        #endregion

                        #region High Card current = -1
                        chHandType.rHighCard(ref current, ref Power, ref Win, ref sorted, ref Reserve, i);
                        #endregion
                    }
                }
            }
        }
        

        void Winner(double current, double Power, string currentText, int chips, string lastly)
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
            if (current == sorted.Current)
            {
                if (Power == sorted.Power)
                {
                    winners++;
                    CheckWinners.Add(currentText);
                    if (current == -1)
                    {
                        MessageBox.Show(currentText + " High Card ");
                    }
                    if (current == 1 || current == 0)
                    {
                        MessageBox.Show(currentText + " Pair ");
                    }
                    if (current == 2)
                    {
                        MessageBox.Show(currentText + " Two Pair ");
                    }
                    if (current == 3)
                    {
                        MessageBox.Show(currentText + " Three of a Kind ");
                    }
                    if (current == 4)
                    {
                        MessageBox.Show(currentText + " Straight ");
                    }
                    if (current == 5 || current == 5.5)
                    {
                        MessageBox.Show(currentText + " Flush ");
                    }
                    if (current == 6)
                    {
                        MessageBox.Show(currentText + " Full House ");
                    }
                    if (current == 7)
                    {
                        MessageBox.Show(currentText + " Four of a Kind ");
                    }
                    if (current == 8)
                    {
                        MessageBox.Show(currentText + " Straight Flush ");
                    }
                    if (current == 9)
                    {
                        MessageBox.Show(currentText + " Royal Flush ! ");
                    }
                }
            }
            if (currentText == lastly)//lastfixed
            {
                if (winners > 1)
                {
                    if (CheckWinners.Contains("Player"))
                    {
                        Chips += int.Parse(this.textboxPot.Text) / winners;
                        this.textboxChipsAmount.Text = Chips.ToString();
                        //pPanel.Visible = true;

                    }
                    if (CheckWinners.Contains("Bot 1"))
                    {
                        bot1Chips += int.Parse(this.textboxPot.Text) / winners;
                        this.textboxBot1Chips.Text = bot1Chips.ToString();
                        //b1Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 2"))
                    {
                        bot2Chips += int.Parse(this.textboxPot.Text) / winners;
                        this.textboxBot2Chips.Text = bot2Chips.ToString();
                        //b2Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 3"))
                    {
                        bot3Chips += int.Parse(this.textboxPot.Text) / winners;
                        this.textboxBot3Chips.Text = bot3Chips.ToString();
                        //b3Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 4"))
                    {
                        bot4Chips += int.Parse(this.textboxPot.Text) / winners;
                        this.textboxBot4Chips.Text = bot4Chips.ToString();
                        //b4Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 5"))
                    {
                        bot5Chips += int.Parse(this.textboxPot.Text) / winners;
                        this.textboxBot5Chips.Text = bot5Chips.ToString();
                        //b5Panel.Visible = true;
                    }
                    //await Finish(1);
                }
                if (winners == 1)
                {
                    if (CheckWinners.Contains("Player"))
                    {
                        Chips += int.Parse(this.textboxPot.Text);
                        //await Finish(1);
                        //pPanel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 1"))
                    {
                        bot1Chips += int.Parse(this.textboxPot.Text);
                        //await Finish(1);
                        //b1Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 2"))
                    {
                        bot2Chips += int.Parse(this.textboxPot.Text);
                        //await Finish(1);
                        //b2Panel.Visible = true;

                    }
                    if (CheckWinners.Contains("Bot 3"))
                    {
                        bot3Chips += int.Parse(this.textboxPot.Text);
                        //await Finish(1);
                        //b3Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 4"))
                    {
                        bot4Chips += int.Parse(this.textboxPot.Text);
                        //await Finish(1);
                        //b4Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 5"))
                    {
                        bot5Chips += int.Parse(this.textboxPot.Text);
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
                        if (!PFturn)
                            this.labelPlayerStatus.Text = "";
                        if (!B1Fturn)
                            this.labelBot1Status.Text = "";
                        if (!B2Fturn)
                            this.labelBot2Status.Text = "";
                        if (!B3Fturn)
                            this.labelBot3Status.Text = "";
                        if (!B4Fturn)
                            this.labelBot4Status.Text = "";
                        if (!B5Fturn)
                            this.labelBot5Status.Text = "";
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
                        pCall = 0; pRaise = 0;
                        b1Call = 0; b1Raise = 0;
                        b2Call = 0; b2Raise = 0;
                        b3Call = 0; b3Raise = 0;
                        b4Call = 0; b4Raise = 0;
                        b5Call = 0; b5Raise = 0;
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
                        pCall = 0; pRaise = 0;
                        b1Call = 0; b1Raise = 0;
                        b2Call = 0; b2Raise = 0;
                        b3Call = 0; b3Raise = 0;
                        b4Call = 0; b4Raise = 0;
                        b5Call = 0; b5Raise = 0;
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
                        pCall = 0; pRaise = 0;
                        b1Call = 0; b1Raise = 0;
                        b2Call = 0; b2Raise = 0;
                        b3Call = 0; b3Raise = 0;
                        b4Call = 0; b4Raise = 0;
                        b5Call = 0; b5Raise = 0;
                    }
                }
            }
            if (rounds == End && maxLeft == 6)
            {
                string fixedLast = "qwerty";
                if (!this.labelPlayerStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Player";
                    Rules(0, 1, "Player", ref pType, ref pPower, PFturn);
                }
                if (!this.labelBot1Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 1";
                    Rules(2, 3, "Bot 1", ref b1Type, ref b1Power, B1Fturn);
                }
                if (!this.labelBot2Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 2";
                    Rules(4, 5, "Bot 2", ref b2Type, ref b2Power, B2Fturn);
                }
                if (!this.labelBot3Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 3";
                    Rules(6, 7, "Bot 3", ref b3Type, ref b3Power, B3Fturn);
                }
                if (!this.labelBot4Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 4";
                    Rules(8, 9, "Bot 4", ref b4Type, ref b4Power, B4Fturn);
                }
                if (!this.labelBot5Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 5";
                    Rules(10, 11, "Bot 5", ref b5Type, ref b5Power, B5Fturn);
                }
                Winner(pType, pPower, "Player", Chips, fixedLast);
                Winner(b1Type, b1Power, "Bot 1", bot1Chips, fixedLast);
                Winner(b2Type, b2Power, "Bot 2", bot2Chips, fixedLast);
                Winner(b3Type, b3Power, "Bot 3", bot3Chips, fixedLast);
                Winner(b4Type, b4Power, "Bot 4", bot4Chips, fixedLast);
                Winner(b5Type, b5Power, "Bot 5", bot5Chips, fixedLast);
                restart = true;
                Pturn = true;
                PFturn = false;
                B1Fturn = false;
                B2Fturn = false;
                B3Fturn = false;
                B4Fturn = false;
                B5Fturn = false;
                if (Chips <= 0)
                {
                    AddChips f2 = new AddChips();
                    f2.ShowDialog();
                    if (f2.Amount != 0)
                    {
                        Chips = f2.Amount;
                        bot1Chips += f2.Amount;
                        bot2Chips += f2.Amount;
                        bot3Chips += f2.Amount;
                        bot4Chips += f2.Amount;
                        bot5Chips += f2.Amount;
                        PFturn = false;
                        Pturn = true;
                        this.buttonRaise.Enabled = true;
                        this.buttonFold.Enabled = true;
                        this.buttonCheck.Enabled = true;
                        this.buttonRaise.Text = "Raise";
                    }
                }
                pPanel.Visible = false; b1Panel.Visible = false; b2Panel.Visible = false; b3Panel.Visible = false; b4Panel.Visible = false; b5Panel.Visible = false;
                pCall = 0; pRaise = 0;
                b1Call = 0; b1Raise = 0;
                b2Call = 0; b2Raise = 0;
                b3Call = 0; b3Raise = 0;
                b4Call = 0; b4Raise = 0;
                b5Call = 0; b5Raise = 0;
                last = 0;
                call = this.bigBlind;
                Raise = 0;
                ImgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
                bools.Clear();
                rounds = 0;
                pPower = 0; pType = -1;
                type = 0; b1Power = 0; b2Power = 0; b3Power = 0; b4Power = 0; b5Power = 0;
                b1Type = -1; b2Type = -1; b3Type = -1; b4Type = -1; b5Type = -1;
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
                this.labelPlayerStatus.Text = "";
                await Shuffle();
                await Turns();
            }
        }
        void FixCall(Label status, ref int cCall, ref int cRaise, int options)
        {
            if (rounds != 4)
            {
                if (options == 1)
                {
                    if (status.Text.Contains("Raise"))
                    {
                        var changeRaise = status.Text.Substring(6);
                        cRaise = int.Parse(changeRaise);
                    }
                    if (status.Text.Contains("Call"))
                    {
                        var changeCall = status.Text.Substring(5);
                        cCall = int.Parse(changeCall);
                    }
                    if (status.Text.Contains("Check"))
                    {
                        cRaise = 0;
                        cCall = 0;
                    }
                }
                if (options == 2)
                {
                    if (cRaise != Raise && cRaise <= Raise)
                    {
                        call = Convert.ToInt32(Raise) - cRaise;
                    }
                    if (cCall != call || cCall <= call)
                    {
                        call = call - cCall;
                    }
                    if (cRaise == Raise && Raise > 0)
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
            if (Chips <= 0 && !intsadded)
            {
                if (this.labelPlayerStatus.Text.Contains("Raise"))
                {
                    ints.Add(Chips);
                    intsadded = true;
                }
                if (this.labelPlayerStatus.Text.Contains("Call"))
                {
                    ints.Add(Chips);
                    intsadded = true;
                }
            }
            intsadded = false;
            if (bot1Chips <= 0 && !B1Fturn)
            {
                if (!intsadded)
                {
                    ints.Add(bot1Chips);
                    intsadded = true;
                }
                intsadded = false;
            }
            if (bot2Chips <= 0 && !B2Fturn)
            {
                if (!intsadded)
                {
                    ints.Add(bot2Chips);
                    intsadded = true;
                }
                intsadded = false;
            }
            if (bot3Chips <= 0 && !B3Fturn)
            {
                if (!intsadded)
                {
                    ints.Add(bot3Chips);
                    intsadded = true;
                }
                intsadded = false;
            }
            if (bot4Chips <= 0 && !B4Fturn)
            {
                if (!intsadded)
                {
                    ints.Add(bot4Chips);
                    intsadded = true;
                }
                intsadded = false;
            }
            if (bot5Chips <= 0 && !B5Fturn)
            {
                if (!intsadded)
                {
                    ints.Add(bot5Chips);
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
                    Chips += int.Parse(this.textboxPot.Text);
                    this.textboxChipsAmount.Text = Chips.ToString();
                    pPanel.Visible = true;
                    MessageBox.Show("Player Wins");
                }
                if (index == 1)
                {
                    bot1Chips += int.Parse(this.textboxPot.Text);
                    this.textboxChipsAmount.Text = bot1Chips.ToString();
                    b1Panel.Visible = true;
                    MessageBox.Show("Bot 1 Wins");
                }
                if (index == 2)
                {
                    bot2Chips += int.Parse(this.textboxPot.Text);
                    this.textboxChipsAmount.Text = bot2Chips.ToString();
                    b2Panel.Visible = true;
                    MessageBox.Show("Bot 2 Wins");
                }
                if (index == 3)
                {
                    bot3Chips += int.Parse(this.textboxPot.Text);
                    this.textboxChipsAmount.Text = bot3Chips.ToString();
                    b3Panel.Visible = true;
                    MessageBox.Show("Bot 3 Wins");
                }
                if (index == 4)
                {
                    bot4Chips += int.Parse(this.textboxPot.Text);
                    this.textboxChipsAmount.Text = bot4Chips.ToString();
                    b4Panel.Visible = true;
                    MessageBox.Show("Bot 4 Wins");
                }
                if (index == 5)
                {
                    bot5Chips += int.Parse(this.textboxPot.Text);
                    this.textboxChipsAmount.Text = bot5Chips.ToString();
                    b5Panel.Visible = true;
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
            pPanel.Visible = false; b1Panel.Visible = false; b2Panel.Visible = false; b3Panel.Visible = false; b4Panel.Visible = false; b5Panel.Visible = false;
            call = this.bigBlind; Raise = 0;
            foldedPlayers = 5;
            type = 0; rounds = 0; b1Power = 0; b2Power = 0; b3Power = 0; b4Power = 0; b5Power = 0; pPower = 0; pType = -1; Raise = 0;
            b1Type = -1; b2Type = -1; b3Type = -1; b4Type = -1; b5Type = -1;
            B1turn = false; B2turn = false; B3turn = false; B4turn = false; B5turn = false;
            B1Fturn = false; B2Fturn = false; B3Fturn = false; B4Fturn = false; B5Fturn = false;
            pFolded = false; b1Folded = false; b2Folded = false; b3Folded = false; b4Folded = false; b5Folded = false;
            PFturn = false; Pturn = true; restart = false; raising = false;
            pCall = 0; b1Call = 0; b2Call = 0; b3Call = 0; b4Call = 0; b5Call = 0; pRaise = 0; b1Raise = 0; b2Raise = 0; b3Raise = 0; b4Raise = 0; b5Raise = 0;
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
            this.labelPlayerStatus.Text = "";
            this.labelBot1Status.Text = "";
            this.labelBot2Status.Text = "";
            this.labelBot3Status.Text = "";
            this.labelBot4Status.Text = "";
            this.labelBot5Status.Text = "";
            if (Chips <= 0)
            {
                AddChips f2 = new AddChips();
                f2.ShowDialog();
                if (f2.Amount != 0)
                {
                    Chips = f2.Amount;
                    bot1Chips += f2.Amount;
                    bot2Chips += f2.Amount;
                    bot3Chips += f2.Amount;
                    bot4Chips += f2.Amount;
                    bot5Chips += f2.Amount;
                    PFturn = false;
                    Pturn = true;
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
            if (!this.labelPlayerStatus.Text.Contains("Fold"))
            {
                fixedLast = "Player";
                Rules(0, 1, "Player", ref pType, ref pPower, PFturn);
            }
            if (!this.labelBot1Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 1";
                Rules(2, 3, "Bot 1", ref b1Type, ref b1Power, B1Fturn);
            }
            if (!this.labelBot2Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 2";
                Rules(4, 5, "Bot 2", ref b2Type, ref b2Power, B2Fturn);
            }
            if (!this.labelBot3Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 3";
                Rules(6, 7, "Bot 3", ref b3Type, ref b3Power, B3Fturn);
            }
            if (!this.labelBot4Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 4";
                Rules(8, 9, "Bot 4", ref b4Type, ref b4Power, B4Fturn);
            }
            if (!this.labelBot5Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 5";
                Rules(10, 11, "Bot 5", ref b5Type, ref b5Power, B5Fturn);
            }
            Winner(pType, pPower, "Player", Chips, fixedLast);
            Winner(b1Type, b1Power, "Bot 1", bot1Chips, fixedLast);
            Winner(b2Type, b2Power, "Bot 2", bot2Chips, fixedLast);
            Winner(b3Type, b3Power, "Bot 3", bot3Chips, fixedLast);
            Winner(b4Type, b4Power, "Bot 4", bot4Chips, fixedLast);
            Winner(b5Type, b5Power, "Bot 5", bot5Chips, fixedLast);
        }
        void AI(int c1, int c2, ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower, double botCurrent)
        {
            if (!sFTurn)
            {
                if (botCurrent == -1)
                {
                    handType.HighCard(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower, call, textboxPot, ref Raise, ref raising);
                }
                if (botCurrent == 0)
                {
                    handType.PairTable(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower, call, textboxPot, ref Raise, ref raising);
                }
                if (botCurrent == 1)
                {
                    handType.PairHand(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower, call, textboxPot, ref Raise, ref raising, rounds);
                }
                if (botCurrent == 2)
                {
                    handType.TwoPair(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower, call, textboxPot, ref Raise, ref raising, rounds);
                }
                if (botCurrent == 3)
                {
                    handType.ThreeOfAKind(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower, call, textboxPot, ref Raise, ref raising, ref rounds);
                }
                if (botCurrent == 4)
                {
                    handType.Straight(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower, call, textboxPot, ref Raise, ref raising, ref rounds);
                }
                if (botCurrent == 5 || botCurrent == 5.5)
                {
                    handType.Flush(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower, call, textboxPot, ref Raise, ref raising, ref rounds);
                }
                if (botCurrent == 6)
                {
                    handType.FullHouse(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower, call, textboxPot, ref Raise, ref raising, ref rounds);
                }
                if (botCurrent == 7)
                {
                    handType.FourOfAKind(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower, call, textboxPot, ref Raise, ref raising, ref rounds);
                }
                if (botCurrent == 8 || botCurrent == 9)
                {
                    handType.StraightFlush(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower, call, textboxPot, ref Raise, ref raising, ref rounds);
                }
            }
            if (sFTurn)
            {
                Holder[c1].Visible = false;
                Holder[c2].Visible = false;
            }
        }
     

      
             
        
     

        

        #region UI
        private async void timer_Tick(object sender, object e)
        {
            if (this.progressbarTimer.Value <= 0)
            {
                PFturn = true;
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
            if (Chips <= 0)
            {
                this.textboxChipsAmount.Text = "Chips : 0";
            }
            if (bot1Chips <= 0)
            {
                this.textboxBot1Chips.Text = "Chips : 0";
            }
            if (bot2Chips <= 0)
            {
                this.textboxBot2Chips.Text = "Chips : 0";
            }
            if (bot3Chips <= 0)
            {
                this.textboxBot3Chips.Text = "Chips : 0";
            }
            if (bot4Chips <= 0)
            {
                this.textboxBot4Chips.Text = "Chips : 0";
            }
            if (bot5Chips <= 0)
            {
                this.textboxBot5Chips.Text = "Chips : 0";
            }
            this.textboxChipsAmount.Text = "Chips : " + Chips.ToString();
            this.textboxBot1Chips.Text = "Chips : " + bot1Chips.ToString();
            this.textboxBot2Chips.Text = "Chips : " + bot2Chips.ToString();
            this.textboxBot3Chips.Text = "Chips : " + bot3Chips.ToString();
            this.textboxBot4Chips.Text = "Chips : " + bot4Chips.ToString();
            this.textboxBot5Chips.Text = "Chips : " + bot5Chips.ToString();
            if (Chips <= 0)
            {
                Pturn = false;
                PFturn = true;
                this.buttonCall.Enabled = false;
                this.buttonRaise.Enabled = false;
                this.buttonFold.Enabled = false;
                this.buttonCheck.Enabled = false;
            }
            if (up > 0)
            {
                up--;
            }
            if (Chips >= call)
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
            if (Chips <= 0)
            {
                this.buttonRaise.Enabled = false;
            }
            int parsedValue;

            if (this.textboxRaise.Text != "" && int.TryParse(this.textboxRaise.Text, out parsedValue))
            {
                if (Chips <= int.Parse(this.textboxRaise.Text))
                {
                    this.buttonRaise.Text = "All in";
                }
                else
                {
                    this.buttonRaise.Text = "Raise";
                }
            }
            if (Chips < call)
            {
                this.buttonRaise.Enabled = false;
            }
        }
        private async void bFold_Click(object sender, EventArgs e)
        {
            this.labelPlayerStatus.Text = "Fold";
            Pturn = false;
            PFturn = true;
            await Turns();
        }
        private async void bCheck_Click(object sender, EventArgs e)
        {
            if (call <= 0)
            {
                Pturn = false;
                this.labelPlayerStatus.Text = "Check";
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
            Rules(0, 1, "Player", ref pType, ref pPower, PFturn);
            if (Chips >= call)
            {
                Chips -= call;
                this.textboxChipsAmount.Text = "Chips : " + Chips.ToString();
                if (this.textboxPot.Text != "")
                {
                    this.textboxPot.Text = (int.Parse(this.textboxPot.Text) + call).ToString();
                }
                else
                {
                    this.textboxPot.Text = call.ToString();
                }
                Pturn = false;
                this.labelPlayerStatus.Text = "Call " + call;
                pCall = call;
            }
            else if (Chips <= call && call > 0)
            {
                this.textboxPot.Text = (int.Parse(this.textboxPot.Text) + Chips).ToString();
                this.labelPlayerStatus.Text = "All in " + Chips;
                Chips = 0;
                this.textboxChipsAmount.Text = "Chips : " + Chips.ToString();
                Pturn = false;
                this.buttonFold.Enabled = false;
                pCall = Chips;
            }
            await Turns();
        }
        private async void bRaise_Click(object sender, EventArgs e)
        {
            Rules(0, 1, "Player", ref pType, ref pPower, PFturn);
            int parsedValue;
            if (this.textboxRaise.Text != "" && int.TryParse(this.textboxRaise.Text, out parsedValue))
            {
                if (Chips > call)
                {
                    if (Raise * 2 > int.Parse(this.textboxRaise.Text))
                    {
                        this.textboxRaise.Text = (Raise * 2).ToString();
                        MessageBox.Show("You must raise atleast twice as the current raise !");
                        return;
                    }
                    else
                    {
                        if (Chips >= int.Parse(this.textboxRaise.Text))
                        {
                            call = int.Parse(this.textboxRaise.Text);
                            Raise = int.Parse(this.textboxRaise.Text);
                            this.labelPlayerStatus.Text = "Raise " + call.ToString();
                            this.textboxPot.Text = (int.Parse(this.textboxPot.Text) + call).ToString();
                            this.buttonCall.Text = "Call";
                            Chips -= int.Parse(this.textboxRaise.Text);
                            raising = true;
                            last = 0;
                            pRaise = Convert.ToInt32(Raise);
                        }
                        else
                        {
                            call = Chips;
                            Raise = Chips;
                            this.textboxPot.Text = (int.Parse(this.textboxPot.Text) + Chips).ToString();
                            this.labelPlayerStatus.Text = "Raise " + call.ToString();
                            Chips = 0;
                            raising = true;
                            last = 0;
                            pRaise = Convert.ToInt32(Raise);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("This is a number only field");
                return;
            }
            Pturn = false;
            await Turns();
        }
        private void bAdd_Click(object sender, EventArgs e)
        {
            if (this.textboxPlayerChips.Text == "") { }
            else
            {
                Chips += int.Parse(this.textboxPlayerChips.Text);
                bot1Chips += int.Parse(this.textboxPlayerChips.Text);
                bot2Chips += int.Parse(this.textboxPlayerChips.Text);
                bot3Chips += int.Parse(this.textboxPlayerChips.Text);
                bot4Chips += int.Parse(this.textboxPlayerChips.Text);
                bot5Chips += int.Parse(this.textboxPlayerChips.Text);
            }
            this.textboxChipsAmount.Text = "Chips : " + Chips.ToString();
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