namespace Poker
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    partial class AddChips
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;
        private Label labelRunOutOfChips;
        private Button buttonAddChips;
        private Button buttonExit;
        private TextBox textBoxChipsAmmount;

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
            this.labelRunOutOfChips = new Label();
            this.buttonAddChips = new Button();
            this.buttonExit = new Button();
            this.textBoxChipsAmmount = new TextBox();
            this.SuspendLayout();
            // 
            // labelRunOutOfChips
            // 
            this.labelRunOutOfChips.Font = new Font("Microsoft Sans Serif", 12.5F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            this.labelRunOutOfChips.Location = new Point(48, 49);
            this.labelRunOutOfChips.Name = "labelRunOutOfChips";
            this.labelRunOutOfChips.Size = new Size(176, 23);
            this.labelRunOutOfChips.TabIndex = 0;
            this.labelRunOutOfChips.Text = "You ran out of chips !";
            this.labelRunOutOfChips.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // buttonAddChips
            // 
            this.buttonAddChips.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            this.buttonAddChips.Location = new Point(12, 226);
            this.buttonAddChips.Name = "buttonAddChips";
            this.buttonAddChips.Size = new Size(75, 23);
            this.buttonAddChips.TabIndex = 1;
            this.buttonAddChips.Text = "Add Chips";
            this.buttonAddChips.UseVisualStyleBackColor = true;
            this.buttonAddChips.Click += new EventHandler(this.AddChipsClick);
            // 
            // buttonExit
            // 
            this.buttonExit.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            this.buttonExit.Location = new Point(197, 226);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new Size(75, 23);
            this.buttonExit.TabIndex = 2;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new EventHandler(this.ExitClick);
            // 
            // textBoxChipsAmmount
            // 
            this.textBoxChipsAmmount.Location = new Point(91, 229);
            this.textBoxChipsAmmount.Name = "textBoxChipsAmmount";
            this.textBoxChipsAmmount.Size = new Size(100, 20);
            this.textBoxChipsAmmount.TabIndex = 3;
            // 
            // AddChips
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new Size(284, 261);
            this.Controls.Add(this.textBoxChipsAmmount);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonAddChips);
            this.Controls.Add(this.labelRunOutOfChips);
            this.Name = "AddChips";
            this.Text = "You Ran Out Of Chips";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}