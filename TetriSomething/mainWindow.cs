using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;


namespace TetriSomething
{
    public partial class mainWindow : Form
    {
        const int _DOWN = 001;
        const int _RIGHT = 002;
        const int _LEFT = 003;
        const int WINHEIGHT = 768;
        const int WINWIDTH = 1240;

        public bool isConnectedAsClient = false;
        public bool isConnectedAsServer = false;
        public bool isMultiplayer = false;

        public tet_blocks blockLogic;
        char[] currentBlocks, futureBlocks = new char[10];
        
        tet_random random = new tet_random();
        tet_constants constants = new tet_constants();
        tet_graphics colors = new tet_graphics();
        tet_game TheGame = new tet_game();
        public tet_network_c client;
        public tet_network_s server;

        public bool isGameStarted = false;

        public System.Drawing.Graphics myGraphics;
        public System.Drawing.Graphics myGraphicsBackup;
        public System.Drawing.Graphics hisGraphics;
        public System.Drawing.Graphics hisGraphicsBackup;

        Pen myPen = new Pen(System.Drawing.Color.Black, 3);
        System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);

        public mainWindow()
        {
            this.KeyDown += new KeyEventHandler(keyListener);
            InitializeComponent();
            blockLogic = new tet_blocks(this);

            currentBlocks = blockLogic.generateNextBlocks();            
            futureBlocks = blockLogic.generateNextBlocks();

            myGraphics = this.CreateGraphics();
            myGraphicsBackup = this.CreateGraphics();
            hisGraphics = this.CreateGraphics();
            hisGraphicsBackup = this.CreateGraphics();
        }

        /// <summary>
        ///  ***
        /// </summary>
        /// <param name="Matrix"></param>
        public void drawMyMatrix(System.Drawing.Graphics graphicsObj)
        {
            tet_graphics pieceColor = new tet_graphics();
            Image _png;           

            for (int row = 0; row < 20; row++)
                for (int column = 0; column < 10; column++)
                {
                    _png = Image.FromFile(colors.getPieceColor(tet_constants.colorMatrix[row, column]));
                    try
                    {
                        graphicsObj.DrawImage(_png, new Rectangle(260 + column * 30, 100 + row * 30, 30, 30));
                    }
                    catch 
                    {
                        break;
                    }
                }


            //try
            //{
            //    myBrush.Color = Color.White;
            //    graphicsObj.FillRectangle(myBrush, new Rectangle(400, 90, 200, 80));

            //    myBrush.Color = Color.Black;
            //    graphicsObj.DrawString("Used shapes : " + blockLogic.usedShapesNr, new Font("Arial", 16), myBrush, new Point(400, 90));
            //    graphicsObj.DrawString("Cleared lines : " + blockLogic.clearedLines, new Font("Arial", 16), myBrush, new Point(400, 110));
            //    graphicsObj.DrawString("Score : " + blockLogic.myScore.getScore(), new Font("Arial", 16), myBrush, new Point(400, 130));
            //    graphicsObj.DrawString("Multiplier : " + blockLogic.myScore.getScoreMultiplier(), new Font("Arial", 16), myBrush, new Point(400, 150));
            //}
            //catch
            //{
            //    return;
            //}
        }

        public void drawHisMatrix(System.Drawing.Graphics graphicsObj, char[,] hisMatrix)
        {
            tet_graphics pieceColor = new tet_graphics();
            Image _png;

            for (int row = 0; row < 20; row++)
                for (int column = 0; column < 10; column++)
                {
                    _png = Image.FromFile(colors.getPieceColor(hisMatrix[row, column]));
                    try
                    {
                        graphicsObj.DrawImage(_png, new Rectangle(680 + column * 30, 100 + row * 30, 30, 30));
                    }
                    catch
                    {
                        break;
                    }
                }


            //try
            //{
            //    myBrush.Color = Color.White;
            //    graphicsObj.FillRectangle(myBrush, new Rectangle(400, 90, 200, 80));

            //    myBrush.Color = Color.Black;
            //    graphicsObj.DrawString("Used shapes : " + blockLogic.usedShapesNr, new Font("Arial", 16), myBrush, new Point(400, 90));
            //    graphicsObj.DrawString("Cleared lines : " + blockLogic.clearedLines, new Font("Arial", 16), myBrush, new Point(400, 110));
            //    graphicsObj.DrawString("Score : " + blockLogic.myScore.getScore(), new Font("Arial", 16), myBrush, new Point(400, 130));
            //    graphicsObj.DrawString("Multiplier : " + blockLogic.myScore.getScoreMultiplier(), new Font("Arial", 16), myBrush, new Point(400, 150));
            //}
            //catch
            //{
            //    return;
            //}
        }
      
        private void Form1_Shown(object sender, EventArgs e)
        {
            //myFlashController.Stop();
            myFlashController.LoadMovie(0, Application.StartupPath + @"\" + tet_graphics.FLASH_DOUBLE);
            myFlashController.LoadMovie(0, Application.StartupPath + @"\" + tet_graphics.FLASH_TRIPLE);
            myFlashController.Loop = false;  

            Image background;

            background = Image.FromFile("png/background2.png");

            myGraphics.DrawImage(background, new Rectangle(0, 0, WINWIDTH, WINHEIGHT));
           
        }       

        private void keyListener(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                if (isGameStarted)
                {
                    //TheGame.autodrop.Enabled = false;
                    bool isValid = blockLogic.rotateCurrentShape();
                    //appendMatrixToDebug();
                    if (isConnectedAsClient) client.sendMsgToClient(blockLogic.objectToSend);
                    if (isConnectedAsServer) server.sendMsgToClient(blockLogic.objectToSend);
                    //TheGame.autodrop.Enabled = true;
                    if (isValid) { } // redrawMatrix(graphicsObj1);
                }
            }
            if (e.KeyCode == Keys.Left)
            {
                if (isGameStarted)
                {
                    //TheGame.autodrop.Enabled = false;
                    bool isValid = blockLogic.moveCurrentShape(-1);
                    //appendMatrixToDebug();
                    if (isConnectedAsClient) client.sendMsgToClient(blockLogic.objectToSend);
                    if (isConnectedAsServer) server.sendMsgToClient(blockLogic.objectToSend);
                    //TheGame.autodrop.Enabled = true;
                    if (isValid) { } // redrawMatrix(graphicsObj1);
                }
            }
            if (e.KeyCode == Keys.Right)
            {
                if (isGameStarted)
                {
                    //TheGame.autodrop.Enabled = false;
                    bool isValid = blockLogic.moveCurrentShape(1);
                    //appendMatrixToDebug();
                    if (isConnectedAsClient) client.sendMsgToClient(blockLogic.objectToSend);
                    if (isConnectedAsServer) server.sendMsgToClient(blockLogic.objectToSend);
                    //TheGame.autodrop.Enabled = true;
                    if (isValid) { } // redrawMatrix(graphicsObj1);
                }
            }
            if (e.KeyCode == Keys.Down)
            {
                //TheGame.autodrop.Enabled = false;
                bool isValid = blockLogic.moveCurrentShapeDown("STEP");
                //appendMatrixToDebug();
                if (isConnectedAsClient) client.sendMsgToClient(blockLogic.objectToSend);
                if (isConnectedAsServer) server.sendMsgToClient(blockLogic.objectToSend);
                //TheGame.autodrop.Enabled = true;
                if (isValid) { } // redrawMatrix(graphicsObj1);
            }
            if (e.KeyCode == Keys.NumPad0)
            {
                if (isGameStarted)
                {
                    TheGame.autodrop.Enabled = false;
                    bool isValid = blockLogic.moveCurrentShapeDown("SNAP");
                    //appendMatrixToDebug();
                    if (isConnectedAsClient) client.sendMsgToClient(blockLogic.objectToSend);
                    if (isConnectedAsServer) server.sendMsgToClient(blockLogic.objectToSend);
                    TheGame.autodrop.Enabled = true;
                    if (isValid) { } //redrawMatrix(graphicsObj1);
                    else
                    {
                        isGameStarted = false;
                        TheGame.autodrop.Enabled = false;

                        drawMyMatrix(myGraphics);
                        //myGraphics.DrawString("Game over, press ENTER to play again", new Font("Arial", 22), myBrush, new Point(10, WINHEIGHT / 2));

                    }
                   
                }
            }            
            if (e.KeyCode == Keys.R)
            {
                TheGame.autodrop.Enabled = false;
                reloadGame();
            }
            if (e.KeyCode == Keys.Escape)
                Application.Exit();
        }

        private void promptWaitingForPlayer()
        {
            Image background;

            background = Image.FromFile("png/background2.png");

            myGraphics.DrawImage(background, new Rectangle(0, 0, WINWIDTH, WINHEIGHT));

            myGraphics.DrawString("Waiting for another player to connect.", new Font("Arial", 22), myBrush, new Point(30, WINHEIGHT / 2));
        }

        public void reloadGame()
        {
            //myFlashController.Stop();
            myFlashController.Loop = false;
            myFlashController.Visible = true;

            Image background;

            background = Image.FromFile("png/background.png");

            myGraphics.DrawImage(background, new Rectangle(0, 0, WINWIDTH, WINHEIGHT));

            TheGame = null;

            TheGame = new tet_game();
            TheGame.gameLoop(this, blockLogic);

            colors.initColorMatrix();
            blockLogic.initGameMatrix();

            blockLogic.usedShapesNr = 0;
            blockLogic.clearedLines = 0;
            blockLogic.myScore.resetScore();
            blockLogic.initializePieces();

            blockLogic.oldReceivedObject = null;

            blockLogic.objectToSend.enemyColorMatrix = tet_constants.colorMatrix;
            blockLogic.objectToSend.enemyClearedLines = blockLogic.clearedLines;
            blockLogic.objectToSend.enemyUsedShapes = blockLogic.usedShapesNr;
            blockLogic.objectToSend.enemyScore = blockLogic.myScore.getScore();
            blockLogic.objectToSend.enemyNextShape = blockLogic.currentShape;
            
            blockLogic.pushNewPiece();

            drawMyMatrix(myGraphics);
            drawHisMatrix(hisGraphics, tet_constants.hisColorMatrix);

            //Image image = Image.FromFile("png/ForeverAlone.png");
            //graphicsObj2.DrawImage(image, new Rectangle(680, 100, 300, 600));

            drawMyScore(myGraphics);

            isGameStarted = true;
            
        }

        public void drawString()
        {
            myGraphics.DrawString("Game over, press ENTER to play again", new Font("Arial", 22), myBrush, new Point(10, WINHEIGHT / 2));
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aboutWindow about = new aboutWindow();
            about.Show();
        }

        public void drawMoveDown(Graphics graphics, int[,] shape, int currentAnchorRow, int currentAnchorColumn, char currentShape)
        {
            try
            {
                Image image = Image.FromFile(tet_graphics.WHITE);
                graphics.DrawImage(image, new Rectangle(260 + currentAnchorColumn * 30, 100 + (currentAnchorRow - 1) * 30, 30, 30));
                graphics.DrawImage(image, new Rectangle(260 + (currentAnchorColumn + shape[0, 1]) * 30, 100 + (currentAnchorRow - 1 + shape[0, 0]) * 30, 30, 30));
                graphics.DrawImage(image, new Rectangle(260 + (currentAnchorColumn + shape[1, 1]) * 30, 100 + (currentAnchorRow - 1 + shape[1, 0]) * 30, 30, 30));
                graphics.DrawImage(image, new Rectangle(260 + (currentAnchorColumn + shape[2, 1]) * 30, 100 + (currentAnchorRow - 1 + shape[2, 0]) * 30, 30, 30));

                image = Image.FromFile(colors.getPieceColor(currentShape));
                graphics.DrawImage(image, new Rectangle(260 + currentAnchorColumn * 30, 100 + currentAnchorRow * 30, 30, 30));
                graphics.DrawImage(image, new Rectangle(260 + (currentAnchorColumn + shape[0, 1]) * 30, 100 + (currentAnchorRow + shape[0, 0]) * 30, 30, 30));
                graphics.DrawImage(image, new Rectangle(260 + (currentAnchorColumn + shape[1, 1]) * 30, 100 + (currentAnchorRow + shape[1, 0]) * 30, 30, 30));
                graphics.DrawImage(image, new Rectangle(260 + (currentAnchorColumn + shape[2, 1]) * 30, 100 + (currentAnchorRow + shape[2, 0]) * 30, 30, 30));
            }
            catch
            {
                drawMyMatrix(myGraphicsBackup);
            }
        }

        public void drawMoveRight(Graphics graphics, int[,] shape, int currentAnchorRow, int currentAnchorColumn, char currentShape)
        {
            try
            {
                Image image = Image.FromFile(tet_graphics.WHITE);
                graphics.DrawImage(image, new Rectangle(260 + (currentAnchorColumn - 1) * 30, 100 + currentAnchorRow * 30, 30, 30));
                graphics.DrawImage(image, new Rectangle(260 + (currentAnchorColumn - 1 + shape[0, 1]) * 30, 100 + (currentAnchorRow + shape[0, 0]) * 30, 30, 30));
                graphics.DrawImage(image, new Rectangle(260 + (currentAnchorColumn - 1 + shape[1, 1]) * 30, 100 + (currentAnchorRow + shape[1, 0]) * 30, 30, 30));
                graphics.DrawImage(image, new Rectangle(260 + (currentAnchorColumn - 1 + shape[2, 1]) * 30, 100 + (currentAnchorRow + shape[2, 0]) * 30, 30, 30));

                image = Image.FromFile(colors.getPieceColor(currentShape));
                graphics.DrawImage(image, new Rectangle(260 + currentAnchorColumn * 30, 100 + currentAnchorRow * 30, 30, 30));
                graphics.DrawImage(image, new Rectangle(260 + (currentAnchorColumn + shape[0, 1]) * 30, 100 + (currentAnchorRow + shape[0, 0]) * 30, 30, 30));
                graphics.DrawImage(image, new Rectangle(260 + (currentAnchorColumn + shape[1, 1]) * 30, 100 + (currentAnchorRow + shape[1, 0]) * 30, 30, 30));
                graphics.DrawImage(image, new Rectangle(260 + (currentAnchorColumn + shape[2, 1]) * 30, 100 + (currentAnchorRow + shape[2, 0]) * 30, 30, 30));
            }
            catch
            {
                drawMyMatrix(myGraphicsBackup);
            }
        }

        public void drawMoveLeft(Graphics graphics, int[,] shape, int currentAnchorRow, int currentAnchorColumn, char currentShape)
        {
            try
            {
                Image image = Image.FromFile(tet_graphics.WHITE);
                graphics.DrawImage(image, new Rectangle(260 + (currentAnchorColumn + 1) * 30, 100 + currentAnchorRow * 30, 30, 30));
                graphics.DrawImage(image, new Rectangle(260 + (currentAnchorColumn + 1 + shape[0, 1]) * 30, 100 + (currentAnchorRow + shape[0, 0]) * 30, 30, 30));
                graphics.DrawImage(image, new Rectangle(260 + (currentAnchorColumn + 1 + shape[1, 1]) * 30, 100 + (currentAnchorRow + shape[1, 0]) * 30, 30, 30));
                graphics.DrawImage(image, new Rectangle(260 + (currentAnchorColumn + 1 + shape[2, 1]) * 30, 100 + (currentAnchorRow + shape[2, 0]) * 30, 30, 30));

                image = Image.FromFile(colors.getPieceColor(currentShape));
                graphics.DrawImage(image, new Rectangle(260 + currentAnchorColumn * 30, 100 + currentAnchorRow * 30, 30, 30));
                graphics.DrawImage(image, new Rectangle(260 + (currentAnchorColumn + shape[0, 1]) * 30, 100 + (currentAnchorRow + shape[0, 0]) * 30, 30, 30));
                graphics.DrawImage(image, new Rectangle(260 + (currentAnchorColumn + shape[1, 1]) * 30, 100 + (currentAnchorRow + shape[1, 0]) * 30, 30, 30));
                graphics.DrawImage(image, new Rectangle(260 + (currentAnchorColumn + shape[2, 1]) * 30, 100 + (currentAnchorRow + shape[2, 0]) * 30, 30, 30));
            }
            catch
            {
                drawMyMatrix(myGraphicsBackup);
            }
        }

        public void drawRotation(Graphics graphics, int[,] oldPosition, int[,] newPosition, int currentAnchorRow, int currentAnchorColumn, char currentShape)
        {
            try
            {
                Image image = Image.FromFile(tet_graphics.WHITE);
                graphics.DrawImage(image, new Rectangle(260 + (currentAnchorColumn + oldPosition[0, 1]) * 30, 100 + (currentAnchorRow + oldPosition[0, 0]) * 30, 30, 30));
                graphics.DrawImage(image, new Rectangle(260 + (currentAnchorColumn + oldPosition[1, 1]) * 30, 100 + (currentAnchorRow + oldPosition[1, 0]) * 30, 30, 30));
                graphics.DrawImage(image, new Rectangle(260 + (currentAnchorColumn + oldPosition[2, 1]) * 30, 100 + (currentAnchorRow + oldPosition[2, 0]) * 30, 30, 30));

                image = Image.FromFile(colors.getPieceColor(currentShape));
                graphics.DrawImage(image, new Rectangle(260 + currentAnchorColumn * 30, 100 + currentAnchorRow * 30, 30, 30));
                graphics.DrawImage(image, new Rectangle(260 + (currentAnchorColumn + newPosition[0, 1]) * 30, 100 + (currentAnchorRow + newPosition[0, 0]) * 30, 30, 30));
                graphics.DrawImage(image, new Rectangle(260 + (currentAnchorColumn + newPosition[1, 1]) * 30, 100 + (currentAnchorRow + newPosition[1, 0]) * 30, 30, 30));
                graphics.DrawImage(image, new Rectangle(260 + (currentAnchorColumn + newPosition[2, 1]) * 30, 100 + (currentAnchorRow + newPosition[2, 0]) * 30, 30, 30));
            }
            catch
            {
                drawMyMatrix(myGraphicsBackup);
            }
        }

        public void drawNewPiece(Graphics graphics, int[,] shape, char currentShape, int currentAnchorRow, int currentAnchorColumn)
        {
            try
            {
                Image image = Image.FromFile(colors.getPieceColor(currentShape));
                graphics.DrawImage(image, new Rectangle(260 + currentAnchorColumn * 30, 100 + currentAnchorRow * 30, 30, 30));
                graphics.DrawImage(image, new Rectangle(260 + (currentAnchorColumn + shape[0, 1]) * 30, 100 + (currentAnchorRow + shape[0, 0]) * 30, 30, 30));
                graphics.DrawImage(image, new Rectangle(260 + (currentAnchorColumn + shape[1, 1]) * 30, 100 + (currentAnchorRow + shape[1, 0]) * 30, 30, 30));
                graphics.DrawImage(image, new Rectangle(260 + (currentAnchorColumn + shape[2, 1]) * 30, 100 + (currentAnchorRow + shape[2, 0]) * 30, 30, 30));
            }
            catch
            {
                drawNewPiece(myGraphicsBackup, shape, currentShape, currentAnchorRow, currentAnchorColumn);
            }
        }

        public void drawMyScore(Graphics graphicsObj)
        {
            try
            {
                myBrush.Color = Color.FromArgb(67, 131, 204);
                graphicsObj.FillRectangle(myBrush, new Rectangle(40, 120, 180, 55));

                myBrush.Color = Color.Black;
                //graphicsObj.DrawString("Score : " + blockLogic.myScore.getScore(), new Font("Arial", 16), myBrush, new Point(40, 90));

                long tempScore = blockLogic.myScore.getScore();

                int[] scoreVector = new int[7];
                int index = 0;

                while (tempScore != 0)
                {
                    scoreVector[index] = (int)tempScore % 10;
                    tempScore /= 10;
                    index++;
                }

                Image scoreNr;

                for (int i = 0; i < 7; i++)
                {
                    scoreNr = Image.FromFile(colors.getScoreNr(scoreVector[i]));
                    graphicsObj.DrawImage(scoreNr, new Rectangle(193 - i * 25, 125, 24, 40));
                }

                //graphicsObj.DrawString("Cleared lines : " + blockLogic.clearedLines, new Font("Arial", 16), myBrush, new Point(40, 110));
                //graphicsObj.DrawString("Multiplier : " + blockLogic.myScore.getScoreMultiplier(), new Font("Arial", 16), myBrush, new Point(40, 130));
                //graphicsObj.DrawString("Used shapes : " + blockLogic.usedShapesNr, new Font("Arial", 16), myBrush, new Point(40, 150));
            }
            catch
            {
                drawMyScore(myGraphicsBackup);
            }
        }

        public void drawHisScore(Graphics graphicsObj, long hisScore)
        {
            try
            {
                myBrush.Color = Color.FromArgb(204, 67, 67);
                graphicsObj.FillRectangle(myBrush, new Rectangle(1020, 120, 180, 55));

                myBrush.Color = Color.Black;
                //graphicsObj.DrawString("Score : " + blockLogic.myScore.getScore(), new Font("Arial", 16), myBrush, new Point(40, 90));

                long tempScore = hisScore;

                int[] scoreVector = new int[7];
                int index = 0;

                while (tempScore != 0)
                {
                    scoreVector[index] = (int)tempScore % 10;
                    tempScore /= 10;
                    index++;
                }

                Image scoreNr;

                for (int i = 0; i < 7; i++)
                {
                    scoreNr = Image.FromFile(colors.getScoreNr(scoreVector[i]));
                    graphicsObj.DrawImage(scoreNr, new Rectangle(1173 - i * 25, 125, 24, 40));
                }
            }
            catch
            {
                drawHisScore(hisGraphicsBackup, hisScore);
            }
        }

        public void drawMyNextShape(Graphics graphicsObj, char p)
        {
            try
            {
                myBrush.Color = Color.FromArgb(67, 131, 204);
                //myBrush.Color = Color.LightPink;
                myGraphics.FillRectangle(myBrush, new Rectangle(40, 250, 180, 90));

                Image png = Image.FromFile(colors.getNextPiecePng(p));
                myGraphics.DrawImage(png, new Rectangle(40, 250, 180, 90));
            }
            catch
            {
                drawMyNextShape(myGraphicsBackup, p);
            }
        }

        public void drawHisNexShape(Graphics graphics, char p)
        {
            try
            {
                myBrush.Color = Color.FromArgb(204, 67, 67);
                //myBrush.Color = Color.LightPink;
                graphics.FillRectangle(myBrush, new Rectangle(1020, 250, 180, 90));

                Image png = Image.FromFile(colors.getNextPiecePng(p));
                graphics.DrawImage(png, new Rectangle(1020, 250, 180, 90));
            }
            catch
            {
                drawHisNexShape(hisGraphicsBackup, p);
            }
        }

        public void playFlashAnimation(int clearedLinesThisDrop)
        {
            if (clearedLinesThisDrop == 2)
                myFlashController.Movie = Application.StartupPath + @"\" + tet_graphics.FLASH_DOUBLE;
            else
                myFlashController.Movie = Application.StartupPath + @"\" + tet_graphics.FLASH_TRIPLE;
            //myFlashController.LoadMovie(0, tet_graphics.FLASH_DOUBLE);
            myFlashController.Loop = false;
            myFlashController.Play();
        }

        private void soloBTN_Click(object sender, EventArgs e)
        {
            soloBTN.Visible = false;
            hostBTN.Visible = false;
            connectBTN.Visible = false;
            optionsBTN.Visible = false;
            aboutBTN.Visible = false;
            quitBTN.Visible = false;

            reloadGame();
        }

        private void hostBTN_Click(object sender, EventArgs e)
        {
            soloBTN.Visible = false;
            hostBTN.Visible = false;
            connectBTN.Visible = false;
            optionsBTN.Visible = false;
            aboutBTN.Visible = false;
            quitBTN.Visible = false;

            server = new tet_network_s(this);
            promptWaitingForPlayer();
            isConnectedAsServer = true;
        }

        private void connectBTN_Click(object sender, EventArgs e)
        {
            soloBTN.Visible = false;
            hostBTN.Visible = false;
            connectBTN.Visible = false;
            optionsBTN.Visible = false;
            aboutBTN.Visible = false;
            quitBTN.Visible = false;

            try { client = new tet_network_c(this); }
            catch
            {
                MessageBox.Show("Host not found, application will restart (dirty fix, needs a more elegant one) ", "Battle Tetrix", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                Application.Restart();
            }
            isConnectedAsClient = true;
        }

        private void quitBTN_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
