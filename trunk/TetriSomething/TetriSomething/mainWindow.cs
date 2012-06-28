using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace TetriSomething
{
    public partial class mainWindow : Form
    {
        const int WINHEIGHT = 780;
        const int WINWIDTH = 480; 
        
        tet_blocks blockLogic;
        string[] currentBlocks, futureBlocks = new string[10];
        
        Random random = new Random();
        tet_constants game = new tet_constants();

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
        private void redrawMatrix(int[,] Matrix)
        {
            graphicsObj = this.CreateGraphics();
            tet_colors pieceColor = new tet_colors();
            Image _png;

            for (int row = 0; row < 20; row++)
                for (int column = 0; column < 10; column++)
                {
                    if (tet_constants.gameMatrix[row, column] == 1) _png = Image.FromFile("png/block_red.png");
                    else _png = Image.FromFile("png/block_white.png");
                    graphicsObj.DrawImage(_png, new Rectangle(90 + column * 30, 90 + row * 30, 30, 30));
                }         
        } 
      
        private void Form1_Shown(object sender, EventArgs e)
        {
            blockLogic.pushNewPiece();
            redrawMatrix(tet_constants.gameMatrix);

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
                if (isValid) redrawMatrix(tet_constants.gameMatrix);
            }
        }   
    }
}
