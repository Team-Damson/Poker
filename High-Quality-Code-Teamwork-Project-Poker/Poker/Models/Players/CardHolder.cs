using Poker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Poker.Models.Players
{
    public abstract class CardHolder : ICardHolder
    {
        public ICollection<Card> Cards { get; set; }

        public IList<PictureBox> PictureBoxHolder { get; set; }

        public virtual async Task SetCards(IList<Card> cards)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                await Task.Delay(200);
                this.Cards.Add(cards[i]);
                this.PictureBoxHolder[i].Tag = cards[i].Power;
                this.SetCardImage(cards[i], this.PictureBoxHolder[i]);
                //this.PictureBoxHolder[i].Visible = true;
            }
        }

        public void RevealCardAtIndex(int index)
        {
            this.PictureBoxHolder[index].Image = this.Cards.ElementAt(index).Image;
        }

        protected abstract void SetCardImage(Card card, PictureBox pictureBox);
    }
}
