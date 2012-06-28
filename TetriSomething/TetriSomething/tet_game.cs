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
        mainWindow mainWindow;
        tet_blocks blockLogic = new tet_blocks();
  
        public void callGameOver()
        {
            gameOver = true;
        }


        public void gameLoop()
        {
            tet_time time = new tet_time();
            DateTime current;
            DateTime last;

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
                        {
                            blockLogic.pushNewPiece();
                            mainWindow.redrawMatrix(tet_constants.gameMatrix);

                            current = time.getCurrentTime();
                            last = time.getLastTime();

                            if (DateTime.Compare(current, last.AddSeconds(2)) >= 0)
                            {
                                tet_blocks blocks = new tet_blocks();
                                blocks.moveCurrentShapeDown();
                                time.setCurrentTime();
                            }
                        }


                        //if imput is 
                        //change game state to other game state
                        //if "ESC" - chage state to State Gameover
                        //}
                        //Add the shape to the grid permanantly
                        //}while(The shape is not outside the grid)


                        break;
                }
                //refresh window

            } while (!gameOver);
        }


        internal void gameLoop(mainWindow mainWindow)
        {
           this.mainWindow = mainWindow;
        }
    }
}

