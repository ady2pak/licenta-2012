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
        tet_constants game = new tet_constants();
        tet_colors colors = new tet_colors();

        System.Drawing.Graphics graphicsObj;
        Pen myPen = new Pen(System.Drawing.Color.Black, 3);
        System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);

        public mainWindow()
        {            
            InitializeComponent();
            blockLogic = new tet_blocks();
            currentBlocks = blockLogic.generateNextBlocks();            
            futureBlocks = blockLogic.generateNextBlocks();           
            this.KeyDown += new KeyEventHandler(tick);            
        }

        /// <summary>
        ///  ***
        /// </summary>
        /// <param name="Matrix"></param>
        public void redrawMatrix(int[,] Matrix)
        {
            graphicsObj = this.CreateGraphics();
            tet_colors pieceColor = new tet_colors();
            Image _png;

            //for (int row = 0; row < 20; row++)
            //    for (int column = 0; column < 10; column++)
            //    {
            //        if (tet_constants.gameMatrix[row, column] == 1) _png = Image.FromFile("png/block_red.png");
            //        else _png = Image.FromFile("png/block_white.png");
            //        graphicsObj.DrawImage(_png, new Rectangle(90 + column * 30, 90 + row * 30, 30, 30));
            //    }

            for (int row = 0; row < 20; row++)
                for (int column = 0; column < 10; column++)
                {
                    _png = Image.FromFile(colors.getPieceColor(tet_constants.colorMatrix[row,column]));
                    graphicsObj.DrawImage(_png, new Rectangle(90 + column * 30, 90 + row * 30, 30, 30));
                }


            myBrush.Color = Color.White;
            graphicsObj.FillRectangle(myBrush, new Rectangle(400, 20, 200, 60));

            myBrush.Color = Color.Black;
            graphicsObj.DrawString("Used shapes : " + blockLogic.usedShapesNr, new Font("Arial", 16), myBrush, new Point(400,20));
            graphicsObj.DrawString("Cleared lines : " + blockLogic.clearedLines, new Font("Arial", 16), myBrush, new Point(400, 40));
            graphicsObj.DrawString("Score : " + blockLogic.myScore.getScore(), new Font("Arial", 16), myBrush, new Point(400, 60));
        } 
      
        private void Form1_Shown(object sender, EventArgs e)
        {
            blockLogic.initializePieces();
            //blockLogic.pushNewPiece();
            //redrawMatrix(tet_constants.gameMatrix);

            game.setState(tet_constants.STATE_PLAYING);
            tet_game TheGame = new tet_game();
            TheGame.gameLoop(this);
            //TheGame = null;

            colors.initColorMatrix();
            blockLogic.initGameMatrix();

        }       

        void tick(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                bool isValid = blockLogic.rotateCurrentShape();
                if (isValid) redrawMatrix(tet_constants.gameMatrix);
            }
            if (e.KeyCode == Keys.Left)
            {
                bool isValid = blockLogic.moveCurrentShape(-1);
                if (isValid) redrawMatrix(tet_constants.gameMatrix);
            }
            if (e.KeyCode == Keys.Right)
            {
                bool isValid = blockLogic.moveCurrentShape(1);
                if (isValid) redrawMatrix(tet_constants.gameMatrix);
            }
            if (e.KeyCode == Keys.Down)
            {
                bool isValid = blockLogic.moveCurrentShapeDown();
                if (isValid)
                {
                    redrawMatrix(tet_constants.gameMatrix);                    
                }
            }
        }   
    }
}
