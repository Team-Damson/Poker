﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDeck.cs" company="">
//   
// </copyright>
// <summary>
//   The Deck interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Poker.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Poker.Models;

    /// <summary>
    /// The Deck interface.
    /// </summary>
    public interface IDeck
    {
        IList<ICard> Cards { get; set; }

        /// <summary>
        /// Get card at index.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <returns>
        /// The <see cref="Card"/>.
        /// </returns>
        ICard GetCardAtIndex(int index);

        /// <summary>
        /// Set cards.
        /// </summary>
        /// <param name="players">
        /// The players.
        /// </param>
        /// <param name="dealer">
        /// The dealer.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task SetCards(IList<IPlayer> players, IDealer dealer);
    }
}