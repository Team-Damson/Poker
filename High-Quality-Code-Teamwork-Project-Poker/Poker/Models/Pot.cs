using System;

namespace Poker.Models
{
    using System.Windows.Forms;
    using Poker.Interfaces;

    public class Pot : IPot
    {
        public Pot(TextBox textBox)
        {
            this.TextBox = textBox;
            this.TextBox.Enabled = false;
            this.Amount = 0;
            this.UpdateTextBox();
        }

        public TextBox TextBox { get; private set; }

        public int Amount { get; private set; }

        public void Set(int amount)
        {
            this.Amount = amount;
            this.UpdateTextBox();
        }

        public void Add(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("The added amount cannot be negative");
            }

            this.Amount += amount;
            this.UpdateTextBox();
        }

        public void Clear()
        {
            this.Amount = 0;
            this.UpdateTextBox();
        }

        private void UpdateTextBox()
        {
            this.TextBox.Text = this.Amount.ToString();
        }
    }
}
