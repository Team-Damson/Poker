using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Poker.Interfaces;

namespace Poker.MessageWriters
{
    class MessageBoxWriter : IMessageWriter
    {
        public void Write(string message)
        {
            MessageBox.Show(message);
        }

        public DialogResult ShowDialog(string message, string caption, MessageBoxButtons messageBoxButtons)
        {
            return MessageBox.Show(message, caption, messageBoxButtons);
        }
    }
}
