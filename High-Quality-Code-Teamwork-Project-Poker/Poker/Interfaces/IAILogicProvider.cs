using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poker.Enums;

namespace Poker.Interfaces
{
    public interface IAILogicProvider
    {
        void HandleTurn(IPlayer player, int call, IPot Pot, ref double raise, ref bool isAnyPlayerRaise,
            CommunityCardBoard currentRound);
    }
}
