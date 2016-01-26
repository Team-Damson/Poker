using Poker.Interfaces;

namespace Poker.Models.Players
{
    using System.Collections.Generic;
    using System.Windows.Forms;

    public class AI : Player, IAIPlayer
    {
        private IAILogicProvider aiLogicProvider;
        public AI(
                  int id,
                  string name,
                  Label statusLabel,
                  TextBox chipsTextBoxTextBox,
                  //int[] cardIndexes,
                  int chips,
                  IList<PictureBox> pictureBoxHolder,
                  Panel panel,
                  IAILogicProvider logicProvider)
            : base(id, name, statusLabel, chipsTextBoxTextBox, /*cardIndexes,*/ chips, pictureBoxHolder, panel)
        {
            this.aiLogicProvider = logicProvider;
        }

        protected override void SetCardImage(Card card, PictureBox pictureBox)
        {
            pictureBox.Image = Card.BackImage;
        }

        public void ProccessNextTurn(int call, IPot Pot, ref double raise, ref bool isAnyPlayerRaise, Enums.CommunityCardBoard currentRound)
        {
            this.aiLogicProvider.HandleTurn(this, call, Pot, ref raise, ref isAnyPlayerRaise, currentRound);
        }
    }
}
