using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Interfaces
{
    public interface IHandTypeHandler
    {
        void Rules(IPlayer player, IDealer dealer, IDeck deck);

        Type GetHighestNotFoldedHand(IList<IPlayer> players);
    }
}
