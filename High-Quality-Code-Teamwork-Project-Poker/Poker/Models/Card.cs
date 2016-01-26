namespace Poker.Models
{
    using System.Drawing;
    using Poker.Interfaces;
   
    public class Card : ICard
    {
        public static readonly Bitmap BackImage = new Bitmap("Assets\\Back\\Back.png");

        public Card(int power, Image image)//Face cardFace, Suit cardSuit)
        {
            this.Power = power;
            this.Image = image;
            //this.CardFace = cardFace;
            //this.CardSuit = cardSuit;
        }

        public int Power { get; private set; }

        public Image Image { get; private set; }

        //public Face CardFace { get; private set; }

        //public Suit CardSuit { get; private set; }
    }
}
