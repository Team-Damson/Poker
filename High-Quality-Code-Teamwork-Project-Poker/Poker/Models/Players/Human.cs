using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Poker.Models.Players
{
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
