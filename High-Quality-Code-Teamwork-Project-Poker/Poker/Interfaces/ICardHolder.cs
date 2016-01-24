namespace Poker.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Poker.Models;

    public interface ICardHolder
    {
        ICollection<Card> Cards { get; set; }

        IList<PictureBox> PictureBoxHolder { get; set; }

        Task SetCards(IList<Card> cards);

        void SetCard(Card card);

        void RevealCardAtIndex(int index);
    }
}
