using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Poker.Interfaces;

namespace Poker.Models
{
    public class PlayerFactory
    {
        private static int cardsIndexCounter = 0;
        private static int currentPlayerId = 0;

        public static IPlayer Create(string name, int chips, Label statusLabel, TextBox chipsTextBox, int call, int raise)
        {
            IPlayer player = new Player(
                currentPlayerId,
                name, 
                statusLabel,
                chipsTextBox,
                new int[] {cardsIndexCounter++, cardsIndexCounter++}, 
                chips, 
                call, 
                raise,
                false);

            return player;
        }
    }
}
