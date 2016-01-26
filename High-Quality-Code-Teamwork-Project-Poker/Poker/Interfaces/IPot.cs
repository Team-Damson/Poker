// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPot.cs" company="">
//   
// </copyright>
// <summary>
//   The Pot interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Poker.Interfaces
{
    using System.Windows.Forms;

    /// <summary>
    /// The Pot interface.
    /// </summary>
    public interface IPot
    {
        /// <summary>
        /// Gets the text box.
        /// </summary>
        /// <value>
        /// The text box.
        /// </value>
        TextBox TextBox { get; }

        /// <summary>
        /// Gets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        int Amount { get; }

        /// <summary>
        /// Set the pot.
        /// </summary>
        /// <param name="amount">
        /// The amount.
        /// </param>
        void Set(int amount);

        /// <summary>
        /// Add to the pot.
        /// </summary>
        /// <param name="amount">
        /// The amount.
        /// </param>
        void Add(int amount);

        /// <summary>
        /// Clear pot.
        /// </summary>
        void Clear();
    }
}