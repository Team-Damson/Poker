namespace Poker.Models.Players
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Poker.Interfaces;

    public abstract class CardHolder : ICardHolder
    {
        public IList<ICard> Cards { get; set; }

        public IList<PictureBox> PictureBoxHolder { get; set; }

        public abstract Task SetCards(IList<ICard> cards);

        public void RevealCardAtIndex(int index)
        {
            this.PictureBoxHolder[index].Image = this.Cards.ElementAt(index).Image;
        }

        protected abstract void SetCardImage(ICard card, PictureBox pictureBox);


        public void AddCard(ICard card)
        {
            this.Cards.Add(card);
        }


        public void CleanCard()
        {
            this.Cards.Clear();
        }
    }
}
