namespace Poker.Models
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Poker.Enums;
    using Poker.Interfaces;
    using Poker.Models.Players;

    public class Dealer : CardHolder, IDealer
    {
        private const int DealedCards = 5;

        public Dealer(int horizontal, int vertical)
        {
            this.PictureBoxHolder = new List<PictureBox>();
            this.Cards = new List<ICard>();

            for (int i = 0; i < DealedCards; i++)
            {
                PictureBox cardHolder = new PictureBox();
                cardHolder.Anchor = AnchorStyles.None;
                cardHolder.SizeMode = PictureBoxSizeMode.StretchImage;
                cardHolder.Height = 130;
                cardHolder.Width = 80;
                cardHolder.Visible = false;
                cardHolder.Location = new Point(horizontal, vertical);
                horizontal += 110;
                this.PictureBoxHolder.Add(cardHolder);
            }
        }

        public CommunityCardBoard CurrentRound { get; set; }

        public override async Task SetCards(IList<ICard> cards)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                await Task.Delay(200);
                this.Cards.Add(cards[i]);
                this.PictureBoxHolder[i].Tag = cards[i].Power;
                this.SetCardImage(cards[i], this.PictureBoxHolder[i]);
                this.PictureBoxHolder[i].Visible = true;
            }
        }
        
        protected override void SetCardImage(ICard card, PictureBox pictureBox)
        {
            pictureBox.Image = Card.BackImage;
        }
    }
}
