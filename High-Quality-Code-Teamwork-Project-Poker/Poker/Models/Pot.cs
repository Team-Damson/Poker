using Poker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Poker.Models
{
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
