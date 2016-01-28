namespace Poker.Models.Players
{
    using Poker.Interfaces;
    using System.Collections.Generic;
    using System.Windows.Forms;

    public class Human : Player
    {
        public Human(
                  int id,
                  string name,
                  Label statusLabel,
                  TextBox chipsTextBoxTextBox,
                  int chips,
                  IList<PictureBox> pictureBoxHolder,
                  Panel panel)
            : base(id, name, statusLabel, chipsTextBoxTextBox, chips, pictureBoxHolder, panel)
        {
        }

        protected override void SetCardImage(ICard card, PictureBox pictureBox)
        {
            pictureBox.Image = card.Image;
        }
    }
}
