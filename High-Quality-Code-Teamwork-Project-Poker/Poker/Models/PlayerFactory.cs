namespace Poker.Models
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using Poker.Interfaces;

    public class PlayerFactory
    {
        private static int cardsIndexCounter = 0;
        private static int currentPlayerId = 0;
        private static int cardHoldersCount = 0;

        public static IPlayer Create(
            string name,
            int chips, 
            Label statusLabel, 
            TextBox chipsTextBox,
            AnchorStyles cardHoldersPictureBoxesAnchorStyles,
            int cardHoldersPictureBoxesX,
            int cardHoldersPictureBoxesY,
            bool isHuman)
        {
            IList<PictureBox> cardHolders = new List<PictureBox>();
            cardHolders.Add(CreatePictureBox(cardHoldersPictureBoxesAnchorStyles, cardHoldersPictureBoxesX, cardHoldersPictureBoxesY));
            cardHoldersPictureBoxesX += cardHolders.First().Width;
            cardHolders.Add(CreatePictureBox(cardHoldersPictureBoxesAnchorStyles, cardHoldersPictureBoxesX, cardHoldersPictureBoxesY));
            
            Panel panel = new Panel();
            panel.Location = new Point(cardHolders.First().Left - 10, cardHolders.Last().Top - 10);
            panel.BackColor = Color.DarkBlue;
            panel.Height = 150;
            panel.Width = 180;
            panel.Visible = false;
            IPlayer player;
            if (isHuman)
            {
                player = new Player(
                    currentPlayerId,
                    name,
                    statusLabel,
                    chipsTextBox,
                    new int[] { cardsIndexCounter++, cardsIndexCounter++ },
                    chips,
                    cardHolders,
                    panel);
            }
            else
            {
                player = new AI(
                    currentPlayerId,
                    name,
                    statusLabel,
                    chipsTextBox,
                    new int[] { cardsIndexCounter++, cardsIndexCounter++ },
                    chips,
                    cardHolders,
                    panel);
            }

            return player;
        }

        private static PictureBox CreatePictureBox(
            AnchorStyles cardHoldersPictureBoxesAnchorStyles,
            int cardHoldersPictureBoxesX,
            int cardHoldersPictureBoxesY)
        {
            PictureBox cardHolder = new PictureBox();
            cardHolder.SizeMode = PictureBoxSizeMode.StretchImage;
            cardHolder.Height = 130;
            cardHolder.Width = 80;
            cardHolder.Visible = false;
            cardHolder.Name = "pb" + PlayerFactory.cardHoldersCount++.ToString();
            cardHolder.Anchor = cardHoldersPictureBoxesAnchorStyles;
            cardHolder.Location = new Point(cardHoldersPictureBoxesX, cardHoldersPictureBoxesY);

            return cardHolder;
        }
    }
}
