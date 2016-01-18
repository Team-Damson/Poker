namespace Poker.Models
{
    public class Player
    {
        protected Player(int chips, int call, int raise, Hand hand, bool hasFolded)
        {
            this.Chips = chips;
            this.Call = call;
            this.Raise = raise;
            this.Hand = hand;
            this.HasFolded = hasFolded;
        }

        public int Chips { get; set; }

        public int Call { get; set; }

        public int Raise { get; set; }

        public Hand Hand { get; set; }

        public bool HasFolded { get; set; }

        public bool IsInTurn { get; set; }
    }
}
