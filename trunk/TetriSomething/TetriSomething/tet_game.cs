using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetriSomething
{
    class tet_game
    {
       
        static private bool gameOver = false;
        static private int gameState;

        public void callGameOver()
        {
            gameOver = true;
        }

        public void gameLoop()
        { 
            do //I AGREE
            {
                tet_constants game = new tet_constants();

                switch (game.getState())
                {
                    case tet_constants.STATE_TITLE:
                        //titleScreen.Draw();
                        break;

                    case tet_constants.STATE_PLAY:
                        //playScreen.Draw();
                        break;

                    case tet_constants.STATE_OPTIONS:
                        //DrawSettings();
                        break;

                    case tet_constants.STATE_GAMEOVER:
                        callGameOver();
                        break;

                    case tet_constants.STATE_PLAYING:
                        //do{
                            //grab a random shape and place it above the grid
                            //while (shape can drop to next line)
                            //{
			                //drop the shape down a line
			                //  draw the grid and shape
			                //  delay the timer and check for input
                                //if imput is 
                                    //change game state to other game state
		                    //}
		                    //Add the shape to the grid permanantly
                        //}while(The shape is not outside the grid)
                        break;
                }
                //refresh window

            } while (!gameOver);
        }

    }
}

