using Poker.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Interfaces
{
    public interface IAIPlayer : IPlayer
    {
        void ProccessNextTurn(int call, IPot pot, ref double raise, ref bool isAnyPlayerRaise,
            CommunityCardBoard currentRound);
    }
}
