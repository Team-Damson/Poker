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

        Type Type { get; set; }

        //int[] CardIndexes { get; set; }

        Label StatusLabel { get; set; }

        TextBox ChipsTextBox { get; set; }

        int Chips { get; set; }

        int CallAmount { get; set; }

        int RaiseAmount { get; set; }

        //Hand Hand { get; set; }

        bool HasFolded { get; set; }

        bool IsInTurn { get; set; }

        bool FoldedTurn { get; set; }

        bool CanPlay();

        void Raise(int amount);
        void Call(int amount);
        void Fold();
        void Check();
        void AllIn();
    }
}
