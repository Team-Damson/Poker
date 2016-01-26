// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICard.cs" company="">
//   
// </copyright>
// <summary>
//   The Card interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Poker.Interfaces
{
    using System.Drawing;

    /// <summary>
    /// The Card interface.
    /// </summary>
    public interface ICard
    {
        /// <summary>
        /// Gets the power of card.
        /// </summary>
        /// <value>
        /// The power.
        /// </value>
        int Power { get; }

        /// <summary>
        /// Gets the image of card.
        /// </summary>
        /// <value>
        /// The image.
        /// </value>
        Image Image { get; }
    }
}