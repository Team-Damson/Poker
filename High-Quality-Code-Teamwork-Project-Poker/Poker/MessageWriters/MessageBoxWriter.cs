namespace Poker.MessageWriters
{
    using System.Windows.Forms;
    using Poker.Interfaces;

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
