namespace Poker.Interfaces
{
    using Poker.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDeck
    {
        IList<Card> GetCards();

        Card GetCardAtIndex(int index);

        Task SetCards(IList<IPlayer> players, IDealer dealer);
    }
}
