using System;
using Poker.Enums;

namespace Poker.Models
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using Poker.Interfaces;
    using Poker.Models.Players;

    public class PlayerFactory
    {
        private static int cardsIndexCounter = 0;
        private static int currentPlayerId = 0;
        private static int cardHoldersCount = 0;

        public static IPlayer Create(
            PlayerType playerType,
            string name,
            int chips, 
            Label statusLabel, 
            TextBox chipsTextBox,
            AnchorStyles cardHoldersPictureBoxesAnchorStyles,
            int cardHoldersPictureBoxesX,
            int cardHoldersPictureBoxesY)
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

            chipsTextBox.Enabled = false;

            switch (playerType)
            {
                case(PlayerType.Human):
                    return new Human(
                        currentPlayerId,
                        name,
                        statusLabel,
                        chipsTextBox,
                        new int[] { cardsIndexCounter++, cardsIndexCounter++ },
                        chips,
                        cardHolders,
                        panel);
                    break;
                case (PlayerType.AI):
                    return new AI(
                        currentPlayerId,
                        name,
                        statusLabel,
                        chipsTextBox,
                        new int[] { cardsIndexCounter++, cardsIndexCounter++ },
                        chips,
                        cardHolders,
                        panel);
                    break;
                default:
                    throw new NotImplementedException("This player type is not implemented.");
            }
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
