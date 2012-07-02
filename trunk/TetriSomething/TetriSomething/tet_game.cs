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
        public Timer autodrop = new Timer(tet_constants.DROP_TIME);

        /// <summary>
        /// does the game loop
        /// </summary>
        /// <param name="mainWindow">Handle to the main window</param>
        /// <param name="blockLogic">Handle to the block Logic</param>
        public void gameLoop(mainWindow mainWindow, tet_blocks blockLogic)
        {
            this.mainWindow = mainWindow;
            this.blockLogic = blockLogic;

            autodrop.Elapsed += new ElapsedEventHandler(autodrop_initiated);
            autodrop.Enabled = true;
           
        }

        /// <summary>
        /// When the timer ticks the autodrop happens
        /// </summary>
        void autodrop_initiated(object sender, ElapsedEventArgs e)
        {            
            bool isValid = blockLogic.moveCurrentShapeDown();
            //mainWindow.appendMatrixToDebug();
            if (mainWindow.isConnectedAsClient) mainWindow.client.sendMsgToClient();
            if (mainWindow.isConnectedAsServer) mainWindow.server.sendMsgToClient();
            if (isValid) { }//mainWindow.redrawMatrix(mainWindow.graphicsObj2);
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