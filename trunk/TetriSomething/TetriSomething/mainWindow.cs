using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace TetriSomething
{
    public partial class mainWindow : Form
    {
        const int WINHEIGHT = 780;
        const int WINWIDTH = 660; 
        
        tet_blocks blockLogic;
        char[] currentBlocks, futureBlocks = new char[10];
        
        Random random = new Random();
        tet_constants constants = new tet_constants();
        tet_colors colors = new tet_colors();
        tet_game TheGame = new tet_game();

        public bool isGameStarted = false;

        public System.Drawing.Graphics graphicsObj1;
        public System.Drawing.Graphics graphicsObj2;

        Pen myPen = new Pen(System.Drawing.Color.Black, 3);
        System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);

        public mainWindow()
        {
            this.KeyDown += new KeyEventHandler(tick);
            InitializeComponent();
            blockLogic = new tet_blocks();
            currentBlocks = blockLogic.generateNextBlocks();            
            futureBlocks = blockLogic.generateNextBlocks(); 
            graphicsObj1 = this.CreateGraphics();
            graphicsObj2 = this.CreateGraphics();
        }

        /// <summary>
        ///  ***
        /// </summary>
        /// <param name="Matrix"></param>
        public void redrawMatrix(System.Drawing.Graphics graphicsObj)
        {
            tet_colors pieceColor = new tet_colors();
            Image _png;           

            for (int row = 0; row < 20; row++)
                for (int column = 0; column < 10; column++)
                {
                    _png = Image.FromFile(colors.getPieceColor(tet_constants.colorMatrix[row, column]));
                    try
                    {
                        graphicsObj.DrawImage(_png, new Rectangle(90 + column * 30, 90 + row * 30, 30, 30));
                    }
                    catch (Exception ex)
                    {
                        break;
                    }
                }


            try
            {
                myBrush.Color = Color.White;
                graphicsObj.FillRectangle(myBrush, new Rectangle(400, 20, 200, 80));

                myBrush.Color = Color.Black;
                graphicsObj.DrawString("Used shapes : " + blockLogic.usedShapesNr, new Font("Arial", 16), myBrush, new Point(400, 20));
                graphicsObj.DrawString("Cleared lines : " + blockLogic.clearedLines, new Font("Arial", 16), myBrush, new Point(400, 40));
                graphicsObj.DrawString("Score : " + blockLogic.myScore.getScore(), new Font("Arial", 16), myBrush, new Point(400, 60));
                graphicsObj.DrawString("Multiplier : " + blockLogic.myScore.getScoreMultiplier(), new Font("Arial", 16), myBrush, new Point(400, 80));
            }
            catch (Exception ex)
            {
                return;
            }
        }
      
        private void Form1_Shown(object sender, EventArgs e)
        {
            Image background;

            background = Image.FromFile("png/background.png");

            graphicsObj1.DrawImage(background, new Rectangle(0, 0, WINWIDTH, WINHEIGHT));

            graphicsObj1.DrawString("Press ENTER to play", new Font("Arial", 22), myBrush, new Point(10, WINHEIGHT / 2));
        }

        void tick(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                if (isGameStarted)
                {                   
                    bool isValid = blockLogic.rotateCurrentShape();                    
                    if (isValid) redrawMatrix(graphicsObj1);
                }
            }
            if (e.KeyCode == Keys.Left)
            {
                if (isGameStarted)
                {                    
                    bool isValid = blockLogic.moveCurrentShape(-1);                   
                    if (isValid) redrawMatrix(graphicsObj1);
                }
            }
            if (e.KeyCode == Keys.Right)
            {
                if (isGameStarted)
                {                   
                    bool isValid = blockLogic.moveCurrentShape(1);                    
                    if (isValid) redrawMatrix(graphicsObj1);
                }
            }
            if (e.KeyCode == Keys.Down)
            {
                if (isGameStarted)
                {                   
                    bool isValid = blockLogic.snapItDown();                    
                    if (isValid) redrawMatrix(graphicsObj1);
                    else
                    {
                        isGameStarted = false;
                        TheGame.autodrop.Enabled = false;
                        
                        redrawMatrix(graphicsObj1);
                        graphicsObj1.DrawString("Game over, press ENTER to play again", new Font("Arial", 22), myBrush, new Point(10, WINHEIGHT / 2));

                    }
                   
                }
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (!isGameStarted)
                    reloadGame();
            }
            if (e.KeyCode == Keys.R)
            {
                TheGame.autodrop.Enabled = false;
                reloadGame();
            }
            if (e.KeyCode == Keys.Escape)
                Application.Exit();
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

            blockLogic.usedShapesNr = -1;
            blockLogic.clearedLines = 0;
            blockLogic.myScore.resetScore();
            blockLogic.initializePieces();
            blockLogic.pushNewPiece();

            redrawMatrix(graphicsObj1);

            isGameStarted = true;
            
        }

        public void drawString()
        {
            graphicsObj2.DrawString("Game over, press ENTER to play again", new Font("Arial", 22), myBrush, new Point(10, WINHEIGHT / 2));
        }
    }
}
