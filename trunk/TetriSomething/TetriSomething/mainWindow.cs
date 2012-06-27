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
        private void redrawMatrix(string[,] Matrix)
        {
            graphicsObj = this.CreateGraphics();
            tet_colors pieceColor = new tet_colors();
            
            for (int row = 0; row < 20; row++)
                for (int colon = 0; colon < 10; colon++)
                {
                    Image _png = Image.FromFile(pieceColor.getPieceColor(Matrix[row, colon]));
                    graphicsObj.DrawImage(_png, new Rectangle(90 + colon * 30, 90 + row * 30, 30, 30));
                 
                }

            pieceColor = null;
        } 

        /// <summary>
        ///  ***
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Shown(object sender, EventArgs e)
        {
            blockLogic.pushNewPiece();
            redrawMatrix(blockLogic.Matrix);

        }       

        void tick(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) 
            { 
                Console.WriteLine("UP!");
                bool isValid = blockLogic.rotateCurrentShape();
                if (isValid) redrawMatrix(blockLogic.Matrix);
            }
            if (e.KeyCode == Keys.Left) { 
                Console.WriteLine("LEFT!");
                bool isValid = blockLogic.moveCurrentShapeToLeft();
                if (isValid) redrawMatrix(blockLogic.Matrix);
            }
            if (e.KeyCode == Keys.Right) { 
                Console.WriteLine("RIGHT!");
                bool isValid = blockLogic.moveCurrentShapeToRight();
                if (isValid) redrawMatrix(blockLogic.Matrix);
            }
            if (e.KeyCode == Keys.Down) { 
                Console.WriteLine("DOWN!");
                bool isValid = blockLogic.moveCurrentShapeDown();
                if (isValid) redrawMatrix(blockLogic.Matrix);
            }
        }   
    }
}
