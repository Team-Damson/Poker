namespace Poker
{
    using System;
    //using System.Drawing;
    using System.Windows.Forms;

    public partial class AddChips : Form
    {
        private const int MaxAllowedChips = 100000000;

        private int amount;

        public AddChips()
        {
            //var fontFamily = new FontFamily("Arial");
            this.InitializeComponent();
            this.ControlBox = false;
            this.labelRunOutOfChips.BorderStyle = BorderStyle.FixedSingle;
        }

        public int Amount
        {
            get
            {
                return this.amount;
            }

            private set
            {
                if (value < 0 || value > MaxAllowedChips)
                {
                    throw new ArgumentOutOfRangeException(string.Format("Amount of chips should be in range [0...{0}].", MaxAllowedChips));
                }

                this.amount = value;
            }
        }

        public void AddChipsClick(object sender, EventArgs e)
        {
            int parsedValue;
            var isValidAmount = int.TryParse(this.textBoxChipsAmmount.Text, out parsedValue);

            if (!isValidAmount)
            {
                var message = "This is a number only field.";
                MessageBox.Show(message);
                return;
            }

            if (parsedValue < 0)
            {
                var message = "You cannot add negative amount.";
                MessageBox.Show(message);
            }
            else if (parsedValue > MaxAllowedChips)
            {
                MessageBox.Show(string.Format("The maximium chips you can add is {0}.", MaxAllowedChips));
            }
            else
            {
                this.Amount = parsedValue;
                this.Close();
            }
        }

        private void ExitClick(object sender, EventArgs e)
        {
            var message = "Are you sure?";
            var title = "Quit";
            var result = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            switch (result)
            {
                case DialogResult.No:
                    break;
                case DialogResult.Yes:
                    Application.Exit();
                    break;
            }
        }
    }
}