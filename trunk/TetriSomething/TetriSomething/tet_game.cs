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
            while (!gameOver)
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
                   
                    case tet_constants.STATE_PLAYING:
                       //game.gameDevice.Clear(Color.Black); MIGHT CAUSE LAG??
                       // doStuff();
                       
                      //print info
                      break;
                } 
                //refresh window

            }
            }

        }
    }

