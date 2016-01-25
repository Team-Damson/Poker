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

        public abstract Task SetCards(IList<Card> cards);

        public void RevealCardAtIndex(int index)
        {
            this.PictureBoxHolder[index].Image = this.Cards.ElementAt(index).Image;
        }

        protected abstract void SetCardImage(Card card, PictureBox pictureBox);
    }
}
