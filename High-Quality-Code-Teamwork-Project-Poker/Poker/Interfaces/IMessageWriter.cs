// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMessageWriter.cs" company="">
//   
// </copyright>
// <summary>
//   The MessageWriter interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Poker.Interfaces
{
    using System.Windows.Forms;

    /// <summary>
    /// The MessageWriter interface.
    /// </summary>
    public interface IMessageWriter
    {
        /// <summary>
        /// The write.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        void Write(string message);

        /// <summary>
        /// The show dialog.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="caption">
        /// The caption.
        /// </param>
        /// <param name="messageBoxButtons">
        /// The message box buttons.
        /// </param>
        /// <returns>
        /// The <see cref="DialogResult"/>.
        /// </returns>
        DialogResult ShowDialog(string message, string caption, MessageBoxButtons messageBoxButtons);
    }
}