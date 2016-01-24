namespace Poker.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Poker.Interfaces;

    public class Player : IPlayer
    {
        public Player(int id, string name, Label statusLabel, TextBox chipsTextBox, int[] cardIndexes, int chips, IList<PictureBox> pictureBoxHolder, Panel panel)
        {
            this.Id = id;
            this.Name = name;
            this.StatusLabel = statusLabel;
            this.PlayerChips = chipsTextBox;
            this.Chips = chips;
            this.CardIndexes = cardIndexes;
            this.PictureBoxHolder = pictureBoxHolder ?? new List<PictureBox>();
            this.Panel = panel ?? new Panel();
            this.Type = new Type();
            this.Cards = new List<Card>();
        }

        public ICollection<Card> Cards { get; set; }

        public IList<PictureBox> PictureBoxHolder { get; set; }

        public bool WinCurrentHand { get; set; }

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

        public Panel Panel { get; set; }

        public Type Type { get; set; }

        public bool FoldedTurn { get; set; }

        public int Id { get; set; }

        public void SetCard(Card card)
        {
            this.Cards.Add(card);
        }

        public async Task SetCards(IList<Card> cards)
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

        public void SetCardsVisible()
        {
        }

        public bool CanPlay()
        {
            return this.Chips > 0;
        }

        public void RevealCardAtIndex(int index)
        {
            this.PictureBoxHolder[index].Image = this.Cards.ElementAt(index).Image;
        }

        protected virtual void SetCardImage(Card card, PictureBox pictureBox)
        {
            pictureBox.Image = card.Image;
        }
    }
}
