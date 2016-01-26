// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICardHolder.cs" company="">
//   
// </copyright>
// <summary>
//   The CardHolder interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Poker.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Poker.Models;

    /// <summary>
    /// The CardHolder interface.
    /// </summary>
    public interface ICardHolder
    {
        /// <summary>
        /// Gets or sets the cards.
        /// </summary>
        /// <value>
        /// The cards.
        /// </value>
        ICollection<Card> Cards { get; set; }

        /// <summary>
        /// Gets or sets the picture box holder.
        /// </summary>
        /// <value>
        /// The picture box holder.
        /// </value>
        IList<PictureBox> PictureBoxHolder { get; set; }

        /// <summary>
        /// Set cards.
        /// </summary>
        /// <param name="cards">
        /// The cards.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task SetCards(IList<Card> cards);

        // void SetCard(Card card);

        /// <summary>
        /// The reveal card at index.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        void RevealCardAtIndex(int index);
    }
}