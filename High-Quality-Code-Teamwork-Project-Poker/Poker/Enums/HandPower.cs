namespace Poker.Enums
{
    public struct HandPower
    {
        public const double HighCard = -1;

        public const double Undefined = 0;

        public const double PairFromHand = 1;

        public const double Pair = 2;

        public const double ThreeOfAKind = 3;

        public const double Straigth = 4;

        public const double Flush = 5;

        public const double FlushWithAce = 5.5;

        public const double FullHouse = 6;

        public const double FourOfAKind = 7;

        public const double StraightFlush = 8;

        public const double RoyalFlush = 9;
    }
}
