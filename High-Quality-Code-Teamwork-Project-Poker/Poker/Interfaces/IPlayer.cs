// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPlayer.cs" company="">
//   
// </copyright>
// <summary>
//   The Player interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Poker.Enums;

namespace Poker.Interfaces
{
    using System.Windows.Forms;

    /// <summary>
    /// The Player interface.
    /// </summary>
    public interface IPlayer : ICardHolder
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        int Id { get; set; }

        PlayerState PlayerState { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the panel.
        /// </summary>
        /// <value>
        /// The panel.
        /// </value>
        Panel Panel { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        Type Type { get; set; }

        // int[] CardIndexes { get; set; }

        /// <summary>
        /// Gets or sets the status label.
        /// </summary>
        /// <value>
        /// The status label.
        /// </value>
        Label StatusLabel { get; set; }

        /// <summary>
        /// Gets or sets the chips text box.
        /// </summary>
        /// <value>
        /// The chips text box.
        /// </value>
        TextBox ChipsTextBox { get; set; }

        /// <summary>
        /// Gets or sets the chips.
        /// </summary>
        /// <value>
        /// The chips.
        /// </value>
        int Chips { get; set; }

        /// <summary>
        /// Gets or sets the call amount.
        /// </summary>
        /// <value>
        /// The call amount.
        /// </value>
        int CallAmount { get; set; }

        /// <summary>
        /// Gets or sets the raise amount.
        /// </summary>
        /// <value>
        /// The raise amount.
        /// </value>
        int RaiseAmount { get; set; }

        // Hand Hand { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether has folded.
        /// </summary>
        /// <value>
        /// The has folded.
        /// </value>
        bool HasFolded { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is in turn.
        /// </summary>
        /// <value>
        /// The is in turn.
        /// </value>
        bool IsInTurn { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether folded turn.
        /// </summary>
        /// <value>
        /// The folded turn.
        /// </value>
        bool FoldedTurn { get; set; }

        /// <summary>
        /// Show possiblity if player can play.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool CanPlay();

        /// <summary>
        /// The raise.
        /// </summary>
        /// <param name="amount">
        /// The amount.
        /// </param>
        void Raise(int amount);

        /// <summary>
        /// The call.
        /// </summary>
        /// <param name="amount">
        /// The amount.
        /// </param>
        void Call(int amount);

        /// <summary>
        /// The fold.
        /// </summary>
        void Fold();

        /// <summary>
        /// The check.
        /// </summary>
        void Check();

        /// <summary>
        /// The all in.
        /// </summary>
        void AllIn();
    }
}