using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Poker.Models;

namespace Poker.Interfaces
{
    public interface ICardHolder
    {
        ICollection<Card> Cards { get; set; }

        IList<PictureBox> PictureBoxHolder { get; set; }

        void SetCards(IList<Card> cards);
        void SetCard(Card card);
    }
}
