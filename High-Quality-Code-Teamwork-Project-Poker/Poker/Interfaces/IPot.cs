using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Poker.Interfaces
{
    public interface IPot
    {
        TextBox TextBox { get; }

        int Amount { get; }

        void Set(int amount);

        void Add(int amount);

        void Clear();
    }
}
