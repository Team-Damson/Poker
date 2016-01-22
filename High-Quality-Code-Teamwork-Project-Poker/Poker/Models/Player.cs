using System.Windows.Forms;
using Poker.Interfaces;

namespace Poker.Models
{
    public class Player : IPlayer
    {
        public Player(int id, string name, Label statusLabel, TextBox chipsTextBox, int[] cardIndexes, int chips, int call, int raise, bool hasFolded)
        {
            this.Id = id;
            this.Name = name;
            this.StatusLabel = statusLabel;
            this.PlayerChips = chipsTextBox;
            this.Chips = chips;
            this.CardIndexes = cardIndexes;
            this.Call = call;
            this.Raise = raise;
            this.HasFolded = hasFolded;
            this.Panel = new Panel();
            this.Type = new Type();
        }

        public string Name { get; set; }

        public int Chips { get; set; }

        public int[] CardIndexes { get; set; }

        public Label StatusLabel { get; set; }

        public TextBox PlayerChips { get; set; }

        public int Call { get; set; }

        public int Raise { get; set; }

        public Hand Hand { get; set; }

        public bool HasFolded { get; set; }

        public bool IsInTurn { get; set; }

        public System.Windows.Forms.Panel Panel { get; set; }

        public Type Type { get; set; }

        public bool FoldedTurn { get; set; }

        public int Id { get; set; }
    }
}
