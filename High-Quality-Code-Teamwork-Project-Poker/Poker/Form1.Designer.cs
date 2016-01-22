namespace Poker
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    partial class Form1
    {
        private Button buttonFold;

        private Button buttonCheck;

        private Button buttonCall;

        private Button buttonRaise;
        private TextBox textboxRaise;

        private ProgressBar progressbarTimer;

        private Button buttonAddChips;
        private TextBox textboxChipsAmount;

        private Label labelPlayerStatus;
        private TextBox textboxPlayerChips;
        private Label labelBot1Status;
        private TextBox textboxBot1Chips;
        private Label labelBot2Status;
        private TextBox textboxBot2Chips;
        private Label labelBot3Status;
        private TextBox textboxBot3Chips;
        private Label labelBot4Status;
        private TextBox textboxBot4Chips;
        private Label labelBot5Status;
        private TextBox textboxBot5Chips;

        private Label labelPot;
        private TextBox textboxPot;

        private Button buttonBlindOptions;
        private Button buttonBigBlind;
        private TextBox textboxBigBlind;
        private Button buttonSmallBlind;
        private TextBox textboxSmallBlind;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonFold = new Button();
            this.buttonCheck = new Button();
            this.buttonCall = new Button();
            this.buttonRaise = new Button();
            this.textboxRaise = new TextBox();
            this.progressbarTimer = new ProgressBar();
            this.buttonAddChips = new Button();
            this.textboxChipsAmount = new TextBox();
            this.labelPlayerStatus = new Label();
            this.textboxPlayerChips = new TextBox();
            this.labelBot1Status = new Label();
            this.textboxBot1Chips = new TextBox();
            this.labelBot2Status = new Label();
            this.textboxBot2Chips = new TextBox();
            this.labelBot3Status = new Label();
            this.textboxBot3Chips = new TextBox();
            this.labelBot4Status = new Label();
            this.textboxBot4Chips = new TextBox();
            this.labelBot5Status = new Label();
            this.textboxBot5Chips = new TextBox();
            this.labelPot = new Label();
            this.textboxPot = new TextBox();
            this.buttonBlindOptions = new Button();
            this.buttonBigBlind = new Button();
            this.textboxBigBlind = new TextBox();
            this.buttonSmallBlind = new Button();
            this.textboxSmallBlind = new TextBox();
            this.SuspendLayout();

            // 
            // buttonFold
            // 
            this.buttonFold.Anchor = AnchorStyles.Bottom;
            this.buttonFold.Font = new Font("Microsoft Sans Serif", 17F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            this.buttonFold.Location = new Point(335, 660);
            this.buttonFold.Name = "buttonFold";
            this.buttonFold.Size = new Size(130, 62);
            this.buttonFold.TabIndex = 0;
            this.buttonFold.Text = "Fold";
            this.buttonFold.UseVisualStyleBackColor = true;
            this.buttonFold.Click += new EventHandler(this.OnFoldClick);

            // 
            // buttonCheck
            // 
            this.buttonCheck.Anchor = AnchorStyles.Bottom;
            this.buttonCheck.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            this.buttonCheck.Location = new Point(494, 660);
            this.buttonCheck.Name = "buttonCheck";
            this.buttonCheck.Size = new Size(134, 62);
            this.buttonCheck.TabIndex = 2;
            this.buttonCheck.Text = "Check";
            this.buttonCheck.UseVisualStyleBackColor = true;
            this.buttonCheck.Click += new EventHandler(this.OnCheckClick);

            // 
            // buttonCall
            // 
            this.buttonCall.Anchor = AnchorStyles.Bottom;
            this.buttonCall.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            this.buttonCall.Location = new Point(667, 661);
            this.buttonCall.Name = "buttonCall";
            this.buttonCall.Size = new Size(126, 62);
            this.buttonCall.TabIndex = 3;
            this.buttonCall.Text = "Call";
            this.buttonCall.UseVisualStyleBackColor = true;
            this.buttonCall.Click += new EventHandler(this.OnCallClick);

            // 
            // buttonRaise
            // 
            this.buttonRaise.Anchor = AnchorStyles.Bottom;
            this.buttonRaise.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            this.buttonRaise.Location = new Point(835, 661);
            this.buttonRaise.Name = "buttonRaise";
            this.buttonRaise.Size = new Size(124, 62);
            this.buttonRaise.TabIndex = 4;
            this.buttonRaise.Text = "Raise";
            this.buttonRaise.UseVisualStyleBackColor = true;
            this.buttonRaise.Click += new EventHandler(this.OnRaiseClick);

            // 
            // progressbarTimer
            // 
            this.progressbarTimer.Anchor = AnchorStyles.Bottom;
            this.progressbarTimer.BackColor = SystemColors.Control;
            this.progressbarTimer.Location = new Point(335, 631);
            this.progressbarTimer.Maximum = 1000;
            this.progressbarTimer.Name = "progressbarTimer";
            this.progressbarTimer.Size = new Size(667, 23);
            this.progressbarTimer.TabIndex = 5;
            this.progressbarTimer.Value = 1000;

            // 
            // textboxChipsAmount
            // 
            this.textboxChipsAmount.Anchor = AnchorStyles.Bottom;
            this.textboxChipsAmount.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            this.textboxChipsAmount.Location = new Point(755, 553);
            this.textboxChipsAmount.Name = "textboxChips";
            this.textboxChipsAmount.Size = new Size(163, 23);
            this.textboxChipsAmount.TabIndex = 6;
            this.textboxChipsAmount.Text = "Chips : 0";

            // 
            // buttonAddChips
            // 
            this.buttonAddChips.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Left)));
            this.buttonAddChips.Location = new Point(12, 697);
            this.buttonAddChips.Name = "buttonAddChips";
            this.buttonAddChips.Size = new Size(75, 25);
            this.buttonAddChips.TabIndex = 7;
            this.buttonAddChips.Text = "AddChips";
            this.buttonAddChips.UseVisualStyleBackColor = true;
            this.buttonAddChips.Click += new EventHandler(this.OnAddClick);

            // 
            // textboxPlayerChips
            // 
            this.textboxPlayerChips.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Left)));
            this.textboxPlayerChips.Location = new Point(93, 700);
            this.textboxPlayerChips.Name = "textboxPlayerChips";
            this.textboxPlayerChips.Size = new Size(125, 20);
            this.textboxPlayerChips.TabIndex = 8;

            // 
            // textboxBot5Chips
            // 
            this.textboxBot5Chips.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.textboxBot5Chips.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            this.textboxBot5Chips.Location = new Point(1012, 553);
            this.textboxBot5Chips.Name = "textboxBot5Chips";
            this.textboxBot5Chips.Size = new Size(152, 23);
            this.textboxBot5Chips.TabIndex = 9;
            this.textboxBot5Chips.Text = "Chips : 0";

            // 
            // textboxBot4Chips
            // 
            this.textboxBot4Chips.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
            this.textboxBot4Chips.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            this.textboxBot4Chips.Location = new Point(970, 81);
            this.textboxBot4Chips.Name = "textboxBot4Chips";
            this.textboxBot4Chips.Size = new Size(123, 23);
            this.textboxBot4Chips.TabIndex = 10;
            this.textboxBot4Chips.Text = "Chips : 0";

            // 
            // textboxBot3Chips
            // 
            this.textboxBot3Chips.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
            this.textboxBot3Chips.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            this.textboxBot3Chips.Location = new Point(755, 81);
            this.textboxBot3Chips.Name = "textboxBot3Chips";
            this.textboxBot3Chips.Size = new Size(125, 23);
            this.textboxBot3Chips.TabIndex = 11;
            this.textboxBot3Chips.Text = "Chips : 0";

            // 
            // textboxBot2Chips
            // 
            this.textboxBot2Chips.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            this.textboxBot2Chips.Location = new Point(276, 81);
            this.textboxBot2Chips.Name = "textboxBot2Chips";
            this.textboxBot2Chips.Size = new Size(133, 23);
            this.textboxBot2Chips.TabIndex = 12;
            this.textboxBot2Chips.Text = "Chips : 0";

            // 
            // textboxBot1Chips
            // 
            this.textboxBot1Chips.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Left)));
            this.textboxBot1Chips.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            this.textboxBot1Chips.Location = new Point(181, 553);
            this.textboxBot1Chips.Name = "textboxBot1Chips";
            this.textboxBot1Chips.Size = new Size(142, 23);
            this.textboxBot1Chips.TabIndex = 13;
            this.textboxBot1Chips.Text = "Chips : 0";

            // 
            // textboxPot
            // 
            this.textboxPot.Anchor = AnchorStyles.None;
            this.textboxPot.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            this.textboxPot.Location = new Point(606, 212);
            this.textboxPot.Name = "textboxPot";
            this.textboxPot.Size = new Size(125, 23);
            this.textboxPot.TabIndex = 14;
            this.textboxPot.Text = "0";

            // 
            // buttonBlindOptions
            // 
            this.buttonBlindOptions.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            this.buttonBlindOptions.Location = new Point(12, 12);
            this.buttonBlindOptions.Name = "buttonBlindOptions";
            this.buttonBlindOptions.Size = new Size(75, 36);
            this.buttonBlindOptions.TabIndex = 15;
            this.buttonBlindOptions.Text = "BB/SB";
            this.buttonBlindOptions.UseVisualStyleBackColor = true;
            this.buttonBlindOptions.Click += new EventHandler(this.OnBlindOptionsClick);

            // 
            // buttonBigBlind
            // 
            this.buttonBigBlind.Location = new Point(12, 254);
            this.buttonBigBlind.Name = "buttonBigBlind";
            this.buttonBigBlind.Size = new Size(75, 23);
            this.buttonBigBlind.TabIndex = 16;
            this.buttonBigBlind.Text = "Big Blind";
            this.buttonBigBlind.UseVisualStyleBackColor = true;
            this.buttonBigBlind.Click += new EventHandler(this.OnBigBlindClick);

            // 
            // textboxSmallBlind
            // 
            this.textboxSmallBlind.Location = new Point(12, 228);
            this.textboxSmallBlind.Name = "textboxSmallBlind";
            this.textboxSmallBlind.Size = new Size(75, 20);
            this.textboxSmallBlind.TabIndex = 17;
            this.textboxSmallBlind.Text = "250";

            // 
            // buttonSmallBlind
            // 
            this.buttonSmallBlind.Location = new Point(12, 199);
            this.buttonSmallBlind.Name = "buttonSmallBlind";
            this.buttonSmallBlind.Size = new Size(75, 23);
            this.buttonSmallBlind.TabIndex = 18;
            this.buttonSmallBlind.Text = "Small Blind";
            this.buttonSmallBlind.UseVisualStyleBackColor = true;
            this.buttonSmallBlind.Click += new EventHandler(this.OnSmallBlindClick);

            // 
            // textboxBigBlind
            // 
            this.textboxBigBlind.Location = new Point(12, 283);
            this.textboxBigBlind.Name = "textboxBigBlind";
            this.textboxBigBlind.Size = new Size(75, 20);
            this.textboxBigBlind.TabIndex = 19;
            this.textboxBigBlind.Text = "500";

            // 
            // labelBot5Status
            // 
            this.labelBot5Status.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.labelBot5Status.Location = new Point(1012, 579);
            this.labelBot5Status.Name = "labelBot5Status";
            this.labelBot5Status.Size = new Size(152, 32);
            this.labelBot5Status.TabIndex = 26;

            // 
            // labelBot4Status
            // 
            this.labelBot4Status.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
            this.labelBot4Status.Location = new Point(970, 107);
            this.labelBot4Status.Name = "labelBot4Status";
            this.labelBot4Status.Size = new Size(123, 32);
            this.labelBot4Status.TabIndex = 27;

            // 
            // labelBot3Status
            // 
            this.labelBot3Status.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
            this.labelBot3Status.Location = new Point(755, 107);
            this.labelBot3Status.Name = "labelBot3Status";
            this.labelBot3Status.Size = new Size(125, 32);
            this.labelBot3Status.TabIndex = 28;

            // 
            // labelBot1Status
            // 
            this.labelBot1Status.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Left)));
            this.labelBot1Status.Location = new Point(181, 579);
            this.labelBot1Status.Name = "labelBot1Status";
            this.labelBot1Status.Size = new Size(142, 32);
            this.labelBot1Status.TabIndex = 29;

            // 
            // labelPlayerStatus
            // 
            this.labelPlayerStatus.Anchor = AnchorStyles.Bottom;
            this.labelPlayerStatus.Location = new Point(755, 579);
            this.labelPlayerStatus.Name = "labelPlayerStatus";
            this.labelPlayerStatus.Size = new Size(163, 32);
            this.labelPlayerStatus.TabIndex = 30;

            // 
            // labelBot2Status
            // 
            this.labelBot2Status.Location = new Point(276, 107);
            this.labelBot2Status.Name = "labelBot2Status";
            this.labelBot2Status.Size = new Size(133, 32);
            this.labelBot2Status.TabIndex = 31;

            // 
            // labelPot
            // 
            this.labelPot.Anchor = AnchorStyles.None;
            this.labelPot.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            this.labelPot.Location = new Point(654, 188);
            this.labelPot.Name = "labelPot";
            this.labelPot.Size = new Size(31, 21);
            this.labelPot.TabIndex = 0;
            this.labelPot.Text = "Pot";

            // 
            // textboxRaise
            // 
            this.textboxRaise.Anchor = AnchorStyles.Bottom;
            this.textboxRaise.Location = new Point(965, 703);
            this.textboxRaise.Name = "textboxRaise";
            this.textboxRaise.Size = new Size(108, 20);
            this.textboxRaise.TabIndex = 0;

            // 
            // Form1
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Poker.Properties.Resources.poker_table___Copy;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.ClientSize = new Size(1350, 729);
            this.Controls.Add(this.textboxRaise);
            this.Controls.Add(this.labelPot);
            this.Controls.Add(this.labelBot2Status);
            this.Controls.Add(this.labelPlayerStatus);
            this.Controls.Add(this.labelBot1Status);
            this.Controls.Add(this.labelBot3Status);
            this.Controls.Add(this.labelBot4Status);
            this.Controls.Add(this.labelBot5Status);
            this.Controls.Add(this.textboxBigBlind);
            this.Controls.Add(this.buttonSmallBlind);
            this.Controls.Add(this.textboxSmallBlind);
            this.Controls.Add(this.buttonBigBlind);
            this.Controls.Add(this.buttonBlindOptions);
            this.Controls.Add(this.textboxPot);
            this.Controls.Add(this.textboxBot1Chips);
            this.Controls.Add(this.textboxBot2Chips);
            this.Controls.Add(this.textboxBot3Chips);
            this.Controls.Add(this.textboxBot4Chips);
            this.Controls.Add(this.textboxBot5Chips);
            this.Controls.Add(this.textboxPlayerChips);
            this.Controls.Add(this.buttonAddChips);
            this.Controls.Add(this.textboxChipsAmount);
            this.Controls.Add(this.progressbarTimer);
            this.Controls.Add(this.buttonRaise);
            this.Controls.Add(this.buttonCall);
            this.Controls.Add(this.buttonCheck);
            this.Controls.Add(this.buttonFold);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "GLS Texas Poker";
            this.Layout += new LayoutEventHandler(this.LayoutChange);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}