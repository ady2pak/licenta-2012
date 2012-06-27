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
        
        blocks blockLogic;
        string[] currentBlocks, futureBlocks = new string[10];
        
        Random random = new Random();

        System.Drawing.Graphics graphicsObj;
        Pen myPen = new Pen(System.Drawing.Color.Black, 3);
        System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);

        public mainWindow()
        {            
            InitializeComponent();
            blockLogic = new blocks();
            currentBlocks = blockLogic.generateNextBlocks();
            futureBlocks = blockLogic.generateNextBlocks();           
            this.KeyDown += new KeyEventHandler(tick);            
        }

        private void drawGameMatrix(string[,] Matrix)
        {
            graphicsObj = this.CreateGraphics();

            for (int row = 0; row < 20; row++)
                for (int colon = 0; colon < 10; colon++)
                    switch (Matrix[row, colon])
                    {
                        case "B":
                            {
                                Image _png = Image.FromFile("png/block_white.png");
                                graphicsObj.DrawImage(_png, new Rectangle(90 + colon*30, 90 + row*30, 30, 30));
                                break;
                            }
                        case "I":
                            {
                                Image _png = Image.FromFile("png/block_blue.png");
                                graphicsObj.DrawImage(_png, new Rectangle(90 + colon * 30, 90 + row * 30, 30, 30));
                                break;
                            }
                        case "J":
                            {
                                Image _png = Image.FromFile("png/block_green.png");
                                graphicsObj.DrawImage(_png, new Rectangle(90 + colon * 30, 90 + row * 30, 30, 30));
                                break;
                            }
                        case "L":
                            {
                                Image _png = Image.FromFile("png/block_grey.png");
                                graphicsObj.DrawImage(_png, new Rectangle(90 + colon * 30, 90 + row * 30, 30, 30));
                                break;
                            }
                        case "O":
                            {
                                Image _png = Image.FromFile("png/block_orange.png");
                                graphicsObj.DrawImage(_png, new Rectangle(90 + colon * 30, 90 + row * 30, 30, 30));
                                break;
                            }
                        case "S":
                            {
                                Image _png = Image.FromFile("png/block_pink.png");
                                graphicsObj.DrawImage(_png, new Rectangle(90 + colon * 30, 90 + row * 30, 30, 30));
                                break;
                            }
                        case "Z":
                            {
                                Image _png = Image.FromFile("png/block_red.png");
                                graphicsObj.DrawImage(_png, new Rectangle(90 + colon * 30, 90 + row * 30, 30, 30));
                                break;
                            }
                        case "T":
                            {
                                Image _png = Image.FromFile("png/block_yellow.png");
                                graphicsObj.DrawImage(_png, new Rectangle(90 + colon * 30, 90 + row * 30, 30, 30));
                                break;
                            }
                    }
        } 

        private void Form1_Shown(object sender, EventArgs e)
        {
            blockLogic.pushNewPiece();
            drawGameMatrix(blockLogic.Matrix);

        }       

        void tick(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) { 
                Console.WriteLine("UP!");
                bool isValid = blockLogic.rotateCurrentShape();
                if (isValid) drawGameMatrix(blockLogic.Matrix);
            }
            if (e.KeyCode == Keys.Left) { Console.WriteLine("LEFT!"); }
            if (e.KeyCode == Keys.Right) { Console.WriteLine("RIGHT!"); }
            if (e.KeyCode == Keys.Down) { Console.WriteLine("DOWN!"); }
        }   
    }
}
