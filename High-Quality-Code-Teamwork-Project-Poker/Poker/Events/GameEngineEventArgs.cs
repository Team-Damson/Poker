namespace Poker.Events
{
    using Poker.Enums;

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
