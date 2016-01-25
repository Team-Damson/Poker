using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poker.Enums;

namespace Poker.Events
{
    public delegate void GameEngineStateEvent(object sender, GameEngineEventArgs args);

    public class GameEngineEventArgs
    {
        public GameEngineEventArgs(GameEngineState gameState)
        {
            this.GameState = gameState;
        }

        public GameEngineState GameState { get; set; }
    }
}
