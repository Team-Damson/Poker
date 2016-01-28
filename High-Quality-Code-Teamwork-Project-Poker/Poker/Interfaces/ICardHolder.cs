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
        IList<ICard> Cards { get; set; }

        /// <summary>
        /// Gets or sets the picture box holder.
        /// </summary>
        /// <value>
        /// The picture box holder.
        /// </value>
        IList<PictureBox> PictureBoxHolder { get; set; }

        void AddCard(ICard card);
        /// <summary>
        /// Set cards.
        /// </summary>
        /// <param name="cards">
        /// The cards.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task SetCards(IList<ICard> cards);

        // void SetCard(Card card);
        void CleanCard();
        /// <summary>
        /// The reveal card at index.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        void RevealCardAtIndex(int index);
    }
}