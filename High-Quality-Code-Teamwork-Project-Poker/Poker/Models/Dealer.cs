namespace Poker.Models
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Poker.Interfaces;

    public class Dealer : ICardHolder
    {
        private const int DealedCards = 5;

        public Dealer(int horizontal, int vertical)
        {
            this.PictureBoxHolder = new List<PictureBox>();
            this.Cards = new List<Card>();

            for (int i = 0; i < DealedCards; i++)
            {
                PictureBox cardHolder = new PictureBox();
                cardHolder.Anchor = AnchorStyles.None;
                //Holder[i].Image = Deck[i];
                cardHolder.SizeMode = PictureBoxSizeMode.StretchImage;
                cardHolder.Height = 130;
                cardHolder.Width = 80;
                cardHolder.Visible = false;
                cardHolder.Location = new Point(horizontal, vertical);
                horizontal += 110;
                this.PictureBoxHolder.Add(cardHolder);
            }
        }

        public ICollection<Card> Cards { get; set; }

        public IList<PictureBox> PictureBoxHolder { get; set; }

        public async Task SetCards(IList<Card> cards)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                await Task.Delay(200);
                this.Cards.Add(cards[i]);
                this.PictureBoxHolder[i].Tag = cards[i].Power;
                this.PictureBoxHolder[i].Image = new Bitmap("Assets\\Back\\Back.png");
                this.PictureBoxHolder[i].Visible = true;
            }
        }

        public void SetCard(Card card)
        {
            throw new NotImplementedException();
        }
        
        public void RevealCardAtIndex(int index)
        {
            this.PictureBoxHolder[index].Image = this.Cards.ElementAt(index).Image;
        }
    }
}
