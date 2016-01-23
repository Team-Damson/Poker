using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Poker.Models
{
    public class AI : Player
    {
        public AI(int id, string name, Label statusLabel, TextBox chipsTextBox, int[] cardIndexes, int chips,
            IList<PictureBox> pictureBoxHolder, Panel panel)
            : base(id, name, statusLabel, chipsTextBox, cardIndexes, chips,
            pictureBoxHolder, panel)
        {
        }

        protected override void SetCardImage(Card card, PictureBox pictureBox)
        {
            pictureBox.Image = new Bitmap("Assets\\Back\\Back.png");
        }
    }
}
