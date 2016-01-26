namespace Poker
{
    using System.Windows.Forms;

    public static class AppSettigns
    {
        // First player PictureBox settings
        public const string FirstPlayerName = "Player";
        public const AnchorStyles FirstPlayerAnchorStyles = AnchorStyles.Bottom;
        public const int FirstPlayerPictureBoxX = 580;
        public const int FirstPlayerPictureBoxY = 480;

        // Second player PictureBox settings
        public const string SecondPlayerName = "Bot 1";
        public const AnchorStyles SecondPlayerAnchorStyles = AnchorStyles.Bottom | AnchorStyles.Left;
        public const int SecondPlayerPictureBoxX = 15;
        public const int SecondPlayerPictureBoxY = 420;

        // Third player PictureBox settings
        public const string ThirdPlayerName = "Bot 2";
        public const AnchorStyles ThirdPlayerAnchorStyles = AnchorStyles.Top | AnchorStyles.Left;
        public const int ThirdPlayerPictureBoxX = 75;
        public const int ThirdPlayerPictureBoxY = 65;

        // Fourth player PictureBox settings
        public const string FourthPlayerName = "Bot 3";
        public const AnchorStyles FourthPlayerAnchorStyles = AnchorStyles.Top;
        public const int FourthPlayerPictureBoxX = 590;
        public const int FourthPlayerPictureBoxY = 25;

        // Fifth player PictureBox settings
        public const string FifthPlayerName = "Bot 4";
        public const AnchorStyles FifthPlayerAnchorStyles = AnchorStyles.Top | AnchorStyles.Right;
        public const int FifthPlayerPictureBoxX = 1115;
        public const int FifthPlayerPictureBoxY = 65;

        // Sixth player PictureBox settings
        public const string SixthPlayerName = "Bot 5";
        public const AnchorStyles SixthPlayerAnchorStyles = AnchorStyles.Bottom | AnchorStyles.Right;
        public const int SixthPlayerPictureBoxX = 1160;
        public const int SixthPlayerPictureBoxY = 420;

        // Dealer settings
        public const int DealerPictureBoxX = 410;
        public const int DealerPictureBoxY = 265;

        // Chips
        public const int DefaultChipsCount = 10000;

        // Blinds
        public const int DefaultMinBigBlind = 500;
        public const int DefaultMaxBigBlind = 200000;
        public const int DefaultMinSmallBlind = 250;
        public const int DefaultMaxSmallBlind = 100000;

        //public const string PlayerChipsTextBoxText = "Chips : ";
        //public const string WinningMessage = "You Won , Congratulations ! ";
        //public const string PlayAgainMessage = "Would You Like To Play Again ?";
        //public const string PlayerTurnMessage = "'s Turn";
    }
}
