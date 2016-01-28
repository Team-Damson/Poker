using Poker.Interfaces;

namespace Poker.Models.Players
{
    using Poker.Enums;
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
                  int chips,
                  IList<PictureBox> pictureBoxHolder,
                  Panel panel,
                  IAILogicProvider logicProvider)
            : base(id, name, statusLabel, chipsTextBoxTextBox, chips, pictureBoxHolder, panel)
        {
            this.aiLogicProvider = logicProvider;
        }

        protected override void SetCardImage(ICard card, PictureBox pictureBox)
        {
            pictureBox.Image = Card.BackImage;
        }

        public void ProccessNextTurn(int call, IPot pot, ref double raise, ref bool isAnyPlayerRaise, CommunityCardBoard currentRound)
        {
            this.aiLogicProvider.HandleTurn(this, call, pot, ref raise, ref isAnyPlayerRaise, currentRound);
        }
    }
}
