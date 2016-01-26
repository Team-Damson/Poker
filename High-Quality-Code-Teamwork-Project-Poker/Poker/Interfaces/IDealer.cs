// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDealer.cs" company="">
//   
// </copyright>
// <summary>
//   The Dealer interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Poker.Interfaces
{
    using Poker.Enums;

    /// <summary>
    /// The Dealer interface.
    /// </summary>
    public interface IDealer : ICardHolder
    {
        /// <summary>
        /// Gets or sets the current round.
        /// </summary>
        /// <value>
        /// The current round.
        /// </value>
        CommunityCardBoard CurrentRound { get; set; }
    }
}