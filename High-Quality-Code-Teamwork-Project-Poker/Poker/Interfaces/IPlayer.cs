namespace Poker.Interfaces
{
    using System.Collections.Generic;
    using System.Windows.Forms;
    using Poker.Models;

    public interface IPlayer : ICardHolder
    {
        int Id { get; set; }

        string Name { get; set; }

        Panel Panel { get; set; }

        bool WinCurrentHand { get; set; }

        Type Type { get; set; }

        ICollection<Card> Cards { get; set; }

        IList<PictureBox> PictureBoxHolder { get; set; }

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

        bool CanPlay();
    }
}
