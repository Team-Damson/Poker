using System.Windows.Forms;
namespace Poker.Interfaces
{
    public interface IMessageWriter
    {
        void Write(string message);
        DialogResult ShowDialog(string message, string caption, MessageBoxButtons messageBoxButtons);
    }
}
