namespace Poker.Models
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    public class AI : Player
    {
        public AI(
                  int id,
                  string name,
                  Label statusLabel,
                  TextBox chipsTextBoxTextBox,
                  int[] cardIndexes,
                  int chips,
                  IList<PictureBox> pictureBoxHolder,
                  Panel panel)
            : base(id, name, statusLabel, chipsTextBoxTextBox, cardIndexes, chips, pictureBoxHolder, panel)
        {
        }

        protected override void SetCardImage(Card card, PictureBox pictureBox)
        {
            pictureBox.Image = new Bitmap("Assets\\Back\\Back.png");
        }
    }
}
