using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetriSomething
{
    public class tet_constants
    {
        public const int CHANCE_POWERUP_STAR = 10; //as in 1 of X
        public const int DROP_TIME = 1000; // drop time in milliseconds
        public const int POWER_UP_OCCURED = 0; //when the random gen hits this a power up occurs

        public const int STATE_TITLE =    00001; // a set of constants to be used by the game
        public const int STATE_PLAY =     00002;
        public const int STATE_OPTIONS =  00003;
        public const int STATE_CONTROLS = 00004;
        public const int STATE_HELP =     00005;
        public const int STATE_SETTINGS = 00006;
        public const int STATE_PLAYING =  00007;
        public const int STATE_GAMEOVER = 00008;
        
        private int gameState = STATE_TITLE;

        public static int[,] gameMatrix = new int[20, 10];
        public static char[,] colorMatrix = new char[20, 10];
        public static char[,] hisColorMatrix = new char[20, 10];
        public static int[,] nextPieceMatrix = new int[3, 4];

        /// <summary>
        /// sets the game state
        /// </summary>
        /// <param name="state">an int to set the state</param>
        public void setState(int state)
        {
            gameState = state;
        }

        /// <summary>
        /// gets the game state
        /// </summary>
        /// <returns>game state</returns>
        public int getState()
        {
            return gameState;
        }
    }

}
