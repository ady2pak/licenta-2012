using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace TetriSomething
{
    class tet_game
    {

        //static private bool gameOver = false;
        //static private int gameState;
        mainWindow mainWindow;
        tet_blocks blockLogic;
        tet_constants game = new tet_constants();
        public Timer autodrop = new Timer(1000);

        //public void callGameOver()
        //{
        //    gameOver = true;
        //}


        public void gameLoop(mainWindow mainWindow, tet_blocks blockLogic)
        {
            this.mainWindow = mainWindow;
            this.blockLogic = blockLogic;
            tet_time time = new tet_time();

            autodrop.Elapsed += new ElapsedEventHandler(autodrop_initiated);
            autodrop.Enabled = true;

            /*do //I AGREE
            {
                

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
                            
                            //Console.WriteLine("i;m functional");
                            //blockLogic.pushNewPiece();
                            //mainWindow.redrawMatrix(tet_constants.gameMatrix);

                            //current = time.getCurrentTime();
                            //last = time.getLastTime();

                            //if (DateTime.Compare(current, last.AddSeconds(2)) >= 0)
                            //{
                            //    tet_blocks blocks = new tet_blocks();
                            //    blocks.moveCurrentShapeDown();
                            //    time.setCurrentTime();
                            //}
                        }


                        //if imput is 
                        //change game state to other game state
                        //if "ESC" - chage state to State Gameover
                        //}
                        //Add the shape to the grid permanantly
                        //}while(The shape is not outside the grid)


                        break;
                    default:
                        Console.WriteLine("bad");
                        break;
                }
                //refresh window

            } while (!gameOver);*/
        }

        void autodrop_initiated(object sender, ElapsedEventArgs e)
        {            
            bool isValid = blockLogic.moveCurrentShapeDown();
            if (isValid) mainWindow.redrawMatrix(mainWindow.graphicsObj2);
            else
            {
                mainWindow.isGameStarted = false;
                autodrop.Enabled = false;
                mainWindow.redrawMatrix(mainWindow.graphicsObj2);
                mainWindow.drawString();
                    
            }            
        }
    }
}