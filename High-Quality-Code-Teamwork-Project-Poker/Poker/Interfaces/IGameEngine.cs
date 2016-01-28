using Poker.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Interfaces
{
    public interface IGameEngine
    {
        int Call { get; set; }

        double Raise { get; set; }

        bool IsAnyPlayerRaise { get; set; }

        int SmallBlind { get; set; }

        int BigBlind { get; set; }

        IPot Pot { get; }

        event GameEngineStateEvent GameEngineStateEvent;

        void Run();

        IPlayer GetHumanPlayer();

        Task Turns();

        //void Rules(IPlayer player);

        void AddChips(ICollection<IPlayer> players, int amount);

        IList<IPlayer> GetAllPlayers();
    }
}
