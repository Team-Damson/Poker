using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Poker.Models;

namespace Poker.Interfaces
{
    public interface IPlayer
    {
        int Id { get; set; }

        string Name { get; set; }

        Panel Panel { get; set; }

        Type Type { get; set; }

        int[] CardIndexes { get; set; }

        Label StatusLabel { get; set; }

        TextBox PlayerChips { get; set; }

        int Chips { get; set; }

        int Call { get; set; }

        int Raise { get; set; }

        Hand Hand { get; set; }

        bool HasFolded { get; set; }

        bool IsInTurn { get; set; }

        bool FoldedTurn { get; set; }
    }
}
