namespace Poker.Interfaces
{
    using Poker.Enums;

    public interface IDealer : ICardHolder
    {
        CommunityCardBoard CurrentRound { get; set; }
    }
}
