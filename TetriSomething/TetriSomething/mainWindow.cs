using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


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

        tet_blocks blockLogic;
        char[] currentBlocks, futureBlocks = new char[10];
        
        tet_random random = new tet_random();
        tet_constants constants = new tet_constants();
        tet_colors colors = new tet_colors();
        tet_game TheGame = new tet_game();
        public tet_network_c client;
        public tet_network_s server;

        public bool isGameStarted = false;

        public System.Drawing.Graphics graphicsObj1;
        public System.Drawing.Graphics graphicsObj2;

        Pen myPen = new Pen(System.Drawing.Color.Black, 3);
        System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);

        public mainWindow()
        {
            this.KeyDown += new KeyEventHandler(keyListener);
            InitializeComponent();
            blockLogic = new tet_blocks(this);
            currentBlocks = blockLogic.generateNextBlocks();            
            futureBlocks = blockLogic.generateNextBlocks(); 
            graphicsObj1 = this.CreateGraphics();
            graphicsObj2 = this.CreateGraphics();
        }

        /// <summary>
        ///  ***
        /// </summary>
        /// <param name="Matrix"></param>
        public void drawMyMatrix(System.Drawing.Graphics graphicsObj)
        {
            tet_colors pieceColor = new tet_colors();
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
            tet_colors pieceColor = new tet_colors();
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
            Image background;

            background = Image.FromFile("png/background.png");

            graphicsObj1.DrawImage(background, new Rectangle(0, 0, WINWIDTH, WINHEIGHT));

            //graphicsObj1.DrawString("Press ENTER to play", new Font("Arial", 22), myBrush, new Point(10, WINHEIGHT / 2));

            graphicsObj1.DrawString("Press F1 to play SOLO.", new Font("Arial", 22), myBrush, new Point(10, WINHEIGHT / 2));
            graphicsObj1.DrawString("Press F2 to play MULTIPLAYER.", new Font("Arial", 22), myBrush, new Point(10, WINHEIGHT / 2 + 30));
        }       

        void keyListener(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                if (isGameStarted)
                {
                    //TheGame.autodrop.Enabled = false;
                    bool isValid = blockLogic.rotateCurrentShape();
                    //appendMatrixToDebug();
                    if (isConnectedAsClient) client.sendMsgToClient();
                    if (isConnectedAsServer) server.sendMsgToClient();
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
                    if (isConnectedAsClient) client.sendMsgToClient();
                    if (isConnectedAsServer) server.sendMsgToClient();
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
                    if (isConnectedAsClient) client.sendMsgToClient();
                    if (isConnectedAsServer) server.sendMsgToClient();
                    //TheGame.autodrop.Enabled = true;
                    if (isValid) { } // redrawMatrix(graphicsObj1);
                }
            }
            if (e.KeyCode == Keys.Down)
            {
                if (isGameStarted)
                {
                    TheGame.autodrop.Enabled = false;
                    bool isValid = blockLogic.moveCurrentShapeDown("SNAP");
                    //appendMatrixToDebug();
                    if (isConnectedAsClient) client.sendMsgToClient();
                    if (isConnectedAsServer) server.sendMsgToClient();
                    TheGame.autodrop.Enabled = true;
                    if (isValid) { } //redrawMatrix(graphicsObj1);
                    else
                    {
                        isGameStarted = false;
                        TheGame.autodrop.Enabled = false;

                        drawMyMatrix(graphicsObj1);
                        graphicsObj1.DrawString("Game over, press ENTER to play again", new Font("Arial", 22), myBrush, new Point(10, WINHEIGHT / 2));

                    }
                   
                }
            }
            if (e.KeyCode == Keys.F1)
            {
                if (!isGameStarted)
                    reloadGame();
            }

            if (e.KeyCode == Keys.F2)
            {
                if (!isGameStarted)
                {
                    isMultiplayer = true;
                    promptMultiplayer();
                }
            }
            if (e.KeyCode == Keys.F3)
            {
                if (!isGameStarted && isMultiplayer)
                {
                    server = new tet_network_s(this);
                    promptWaitingForPlayer();
                    isConnectedAsServer = true;
                }
            }
            if (e.KeyCode == Keys.F4)
            {
                if (!isGameStarted && isMultiplayer)
                {
                    client = new tet_network_c(this);
                    isConnectedAsClient = true;
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

            background = Image.FromFile("png/background.png");

            graphicsObj1.DrawImage(background, new Rectangle(0, 0, WINWIDTH, WINHEIGHT));

            graphicsObj1.DrawString("Waiting for another player to connect.", new Font("Arial", 22), myBrush, new Point(10, WINHEIGHT / 2));
        }

        private void promptMultiplayer()
        {
            Image background;

            background = Image.FromFile("png/background.png");

            graphicsObj1.DrawImage(background, new Rectangle(0, 0, WINWIDTH, WINHEIGHT));            

            graphicsObj1.DrawString("Press F3 to HOST game.", new Font("Arial", 22), myBrush, new Point(10, WINHEIGHT / 2));
            graphicsObj1.DrawString("Press F4 to CONNECT to another player.", new Font("Arial", 22), myBrush, new Point(10, WINHEIGHT / 2 + 30));
        }

        public void reloadGame()
        {
            Image background;

            background = Image.FromFile("png/background.png");

            graphicsObj1.DrawImage(background, new Rectangle(0, 0, WINWIDTH, WINHEIGHT));

            TheGame = null;

            TheGame = new tet_game();
            TheGame.gameLoop(this, blockLogic);

            colors.initColorMatrix();
            blockLogic.initGameMatrix();

            blockLogic.usedShapesNr = 0;
            blockLogic.clearedLines = 0;
            blockLogic.myScore.resetScore();
            blockLogic.initializePieces();
            blockLogic.pushNewPiece();

            drawMyMatrix(graphicsObj1);
            drawHisMatrix(graphicsObj2, tet_constants.hisColorMatrix);
            drawMyScore(graphicsObj1);

            isGameStarted = true;
            
        }

        public void drawString()
        {
            graphicsObj2.DrawString("Game over, press ENTER to play again", new Font("Arial", 22), myBrush, new Point(10, WINHEIGHT / 2));
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
                Image image = Image.FromFile(tet_colors.WHITE);
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
                drawMyMatrix(graphicsObj2);
            }
        }

        public void drawMoveRight(Graphics graphics, int[,] shape, int currentAnchorRow, int currentAnchorColumn, char currentShape)
        {
            try
            {
                Image image = Image.FromFile(tet_colors.WHITE);
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
                drawMyMatrix(graphicsObj2);
            }
        }

        public void drawMoveLeft(Graphics graphics, int[,] shape, int currentAnchorRow, int currentAnchorColumn, char currentShape)
        {
            try
            {
                Image image = Image.FromFile(tet_colors.WHITE);
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
                drawMyMatrix(graphicsObj2);
            }
        }

        public void drawRotation(Graphics graphics, int[,] oldPosition, int[,] newPosition, int currentAnchorRow, int currentAnchorColumn, char currentShape)
        {
            try
            {
                Image image = Image.FromFile(tet_colors.WHITE);
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
                drawMyMatrix(graphicsObj2);
            }
        }

        public void drawNewPiece(Graphics graphics, int[,] shape, char currentShape, int currentAnchorRow, int currentAnchorColumn)
        {
            Image image = Image.FromFile(colors.getPieceColor(currentShape));
            graphics.DrawImage(image, new Rectangle(260 + currentAnchorColumn * 30, 100 + currentAnchorRow * 30, 30, 30));
            graphics.DrawImage(image, new Rectangle(260 + (currentAnchorColumn + shape[0, 1]) * 30, 100 + (currentAnchorRow + shape[0, 0]) * 30, 30, 30));
            graphics.DrawImage(image, new Rectangle(260 + (currentAnchorColumn + shape[1, 1]) * 30, 100 + (currentAnchorRow + shape[1, 0]) * 30, 30, 30));
            graphics.DrawImage(image, new Rectangle(260 + (currentAnchorColumn + shape[2, 1]) * 30, 100 + (currentAnchorRow + shape[2, 0]) * 30, 30, 30));
        }

        public void drawMyScore(Graphics graphicsObj)
        {
            myBrush.Color = Color.White;
            graphicsObj.FillRectangle(myBrush, new Rectangle(40, 100, 180, 120));

            myBrush.Color = Color.Black;
            graphicsObj.DrawString("Score : " + blockLogic.myScore.getScore(), new Font("Arial", 16), myBrush, new Point(40, 100));
            graphicsObj.DrawString("Cleared lines : " + blockLogic.clearedLines, new Font("Arial", 16), myBrush, new Point(40, 120));
            graphicsObj.DrawString("Multiplier : " + blockLogic.myScore.getScoreMultiplier(), new Font("Arial", 16), myBrush, new Point(40, 140));
            graphicsObj.DrawString("Used shapes : " + blockLogic.usedShapesNr, new Font("Arial", 16), myBrush, new Point(40, 160));
        }
    }
}
