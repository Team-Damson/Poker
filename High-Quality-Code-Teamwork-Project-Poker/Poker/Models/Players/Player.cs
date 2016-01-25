using Poker.Models.Players;

namespace Poker.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Poker.Interfaces;

    public abstract class Player : CardHolder, IPlayer
    {
        private int chips;

        protected Player(int id, string name, Label statusLabel, TextBox chipsTextBoxTextBox, /*int[] cardIndexes,*/ int chips, IList<PictureBox> pictureBoxHolder, Panel panel)
        {
            this.Id = id;
            this.Name = name;
            this.StatusLabel = statusLabel;
            this.ChipsTextBox = chipsTextBoxTextBox;
            this.Chips = chips;
            //this.CardIndexes = cardIndexes;
            this.PictureBoxHolder = pictureBoxHolder ?? new List<PictureBox>();
            this.Panel = panel ?? new Panel();
            this.Type = new Type();
            this.Cards = new List<Card>();
        }

        public string Name { get; set; }

        public int Chips
        {
            get
            {
                return this.chips;
            }
            set
            {
                this.chips = value < 0 ? 0 : value;
                this.UpdateChipsTetxBox(this.chips);
                if (this.chips == 0)
                {
                    this.IsInTurn = false;
                    this.FoldedTurn = true;
                }
            }
        }

        //public int[] CardIndexes { get; set; }

        public Label StatusLabel { get; set; }

        public TextBox ChipsTextBox { get; set; }

        public int CallAmount { get; set; }

        public int RaiseAmount { get; set; }

        //public Hand Hand { get; set; }

        public bool HasFolded { get; set; }

        public bool IsInTurn { get; set; }

        public Panel Panel { get; set; }

        public Type Type { get; set; }

        public bool FoldedTurn { get; set; }

        public int Id { get; set; }

        public override async Task SetCards(IList<Card> cards)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                await Task.Delay(200);
                this.Cards.Add(cards[i]);
                this.PictureBoxHolder[i].Tag = cards[i].Power;
                this.SetCardImage(cards[i], this.PictureBoxHolder[i]);
                if (this.CanPlay())
                {
                    this.FoldedTurn = false;
                    this.PictureBoxHolder[i].Visible = true;
                }
                else
                {
                    this.FoldedTurn = true;
                    this.PictureBoxHolder[i].Visible = false;
                }
            }
        }
        public bool CanPlay()
        {
            return this.Chips > 0;
        }


        public void Raise(int amount)
        {
            this.StatusLabel.Text = "Raise " + amount;
            this.Chips -= amount;
            this.RaiseAmount = amount;
            this.IsInTurn = false;
        }

        public void Call(int amount)
        {
            this.IsInTurn = false;
            this.Chips -= amount;
            this.CallAmount = amount;
            this.StatusLabel.Text = "Call " + amount;
        }

        public void Fold()
        {
            this.StatusLabel.Text = "Fold";
            this.IsInTurn = false;
            this.FoldedTurn = true;
            this.HasFolded = true;
        }

        public void Check()
        {
            this.StatusLabel.Text = "Check";
            this.IsInTurn = false;
        }

        public void AllIn()
        {
            this.StatusLabel.Text = "All in " + this.Chips;
            this.CallAmount = this.Chips;
            this.Chips = 0;
            this.IsInTurn = false;
        }

        private void UpdateChipsTetxBox(int value)
        {
            this.ChipsTextBox.Text = AppSettigns.PlayerChipsTextBoxText + value.ToString();
        }
    }
}
