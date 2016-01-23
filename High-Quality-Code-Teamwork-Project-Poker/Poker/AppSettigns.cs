using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Poker
{
    public static class AppSettigns
    {
        // First player PictureBox settings
        public const AnchorStyles FirstPlayerAnchorStyles = AnchorStyles.Bottom;
        public const int FirstPlayerPictureBoxX = 580;
        public const int FirstPlayerPictureBoxY = 480;

        // Second player PictureBox settings
        public const AnchorStyles SecondPlayerAnchorStyles = AnchorStyles.Bottom | AnchorStyles.Left;
        public const int SecondPlayerPictureBoxX = 15;
        public const int SecondPlayerPictureBoxY = 420;

        // Third player PictureBox settings
        public const AnchorStyles ThirdPlayerAnchorStyles = AnchorStyles.Top | AnchorStyles.Left;
        public const int ThirdPlayerPictureBoxX = 75;
        public const int ThirdPlayerPictureBoxY = 65;

        // Fourth player PictureBox settings
        public const AnchorStyles FourthPlayerAnchorStyles = AnchorStyles.Top;
        public const int FourthPlayerPictureBoxX = 590;
        public const int FourthPlayerPictureBoxY = 25;

        // Fifth player PictureBox settings
        public const AnchorStyles FifthPlayerAnchorStyles = AnchorStyles.Top | AnchorStyles.Right;
        public const int FifthPlayerPictureBoxX = 1115;
        public const int FifthPlayerPictureBoxY = 65;

        // Sixth player PictureBox settings
        public const AnchorStyles SixthPlayerAnchorStyles = AnchorStyles.Bottom | AnchorStyles.Right;
        public const int SixthPlayerPictureBoxX = 1160;
        public const int SixthPlayerPictureBoxY = 420;

        // Dealer settings
        public const int DealerPictureBoxX = 410;
        public const int DealerPictureBoxY = 265;
    }
}
