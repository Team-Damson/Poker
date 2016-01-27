using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Enums
{
    public enum PlayerState
    {
        BeginGame,
        Check,
        Call,
        Raise,
        Fold,
        AllIn,
        OutOfChips
    }
}
