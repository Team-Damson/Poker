namespace Poker.Interfaces
{
    using Poker.Enums;

    public interface ICard
    {
        Face CardFace { get; }

        Suit CardSuit { get; }
    }
}
