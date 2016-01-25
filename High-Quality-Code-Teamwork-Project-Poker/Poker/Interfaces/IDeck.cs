using Poker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Interfaces
{
    public interface IDeck
    {
        IList<Card> GetCards();

        Card GetCardAtIndex(int index);

        Task SetCards(IList<IPlayer> players, IDealer dealer);
    }
}
