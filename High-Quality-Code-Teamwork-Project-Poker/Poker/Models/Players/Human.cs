namespace Poker.Models.Players
{
    using System.Collections.Generic;
    using System.Windows.Forms;

    public class Human : Player
    {
        public Human(
                  int id,
                  string name,
                  Label statusLabel,
                  TextBox chipsTextBoxTextBox,
                  //int[] cardIndexes,
                  int chips,
                  IList<PictureBox> pictureBoxHolder,
                  Panel panel)
            : base(id, name, statusLabel, chipsTextBoxTextBox, /*cardIndexes,*/ chips, pictureBoxHolder, panel)
        {
        }

        protected override void SetCardImage(Card card, PictureBox pictureBox)
        {
            pictureBox.Image = card.Image;
        }
    }
}
