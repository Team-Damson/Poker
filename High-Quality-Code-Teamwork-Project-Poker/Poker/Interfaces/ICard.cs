namespace Poker.Interfaces
{
    using Poker.Enums;
    using System.Drawing;
    using System.Windows.Forms;

    public interface ICard
    {
        int Power { get; }

        Image Image { get; }

        // CardFace { get; }

        //Suit CardSuit { get; }
    }
}
