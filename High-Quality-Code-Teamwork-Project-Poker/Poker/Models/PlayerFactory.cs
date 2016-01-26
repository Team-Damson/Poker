namespace Poker.Models
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using Poker.Enums;
    using Poker.Interfaces;
    using Poker.Models.Players;

    public class PlayerFactory
    {
        private static int currentPlayerId = 0;
        private static int cardHoldersCount = 0;

        public static IPlayer CreateHuman(
            string name,
            int chips, 
            Label statusLabel, 
            TextBox chipsTextBox,
            AnchorStyles cardHoldersPictureBoxesAnchorStyles,
            int cardHoldersPictureBoxesX,
            int cardHoldersPictureBoxesY)
        {
            IList<PictureBox> cardHolders = GetCardHoldersPictureBoxes(cardHoldersPictureBoxesAnchorStyles,
                cardHoldersPictureBoxesX, cardHoldersPictureBoxesY);

            Panel panel = GetPlayerPanel(cardHolders);

            chipsTextBox.Enabled = false;

            return new Human(
                currentPlayerId,
                name,
                statusLabel,
                chipsTextBox,
                chips,
                cardHolders,
                panel);
        }

        public static IAIPlayer CreateAI(
            IAILogicProvider logicProvider,
            string name,
            int chips,
            Label statusLabel,
            TextBox chipsTextBox,
            AnchorStyles cardHoldersPictureBoxesAnchorStyles,
            int cardHoldersPictureBoxesX,
            int cardHoldersPictureBoxesY)
        {
            IList<PictureBox> cardHolders = GetCardHoldersPictureBoxes(cardHoldersPictureBoxesAnchorStyles,
                cardHoldersPictureBoxesX, cardHoldersPictureBoxesY);

            Panel panel = GetPlayerPanel(cardHolders);

            chipsTextBox.Enabled = false;

            return new AI(
                currentPlayerId,
                name,
                statusLabel,
                chipsTextBox,
                chips,
                cardHolders,
                panel,
                logicProvider);
        }

        private static Panel GetPlayerPanel(IList<PictureBox> cardHolders)
        {
            Panel panel = new Panel();
            panel.Location = new Point(cardHolders.First().Left - 10, cardHolders.Last().Top - 10);
            panel.BackColor = Color.DarkBlue;
            panel.Height = 150;
            panel.Width = 180;
            panel.Visible = false;

            return panel;
        }

        private static IList<PictureBox> GetCardHoldersPictureBoxes(
            AnchorStyles cardHoldersPictureBoxesAnchorStyles,
            int cardHoldersPictureBoxesX,
            int cardHoldersPictureBoxesY)
        {
            IList<PictureBox> cardHolders = new List<PictureBox>();
            cardHolders.Add(CreatePictureBox(cardHoldersPictureBoxesAnchorStyles, cardHoldersPictureBoxesX, cardHoldersPictureBoxesY));
            cardHoldersPictureBoxesX += cardHolders.First().Width;
            cardHolders.Add(CreatePictureBox(cardHoldersPictureBoxesAnchorStyles, cardHoldersPictureBoxesX, cardHoldersPictureBoxesY));

            return cardHolders;
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
