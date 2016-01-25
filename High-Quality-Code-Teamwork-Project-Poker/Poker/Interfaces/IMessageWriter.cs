using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Poker.Interfaces
{
    public interface IMessageWriter
    {
        void Write(string message);
        DialogResult ShowDialog(string message, string caption, MessageBoxButtons messageBoxButtons);
    }
}
