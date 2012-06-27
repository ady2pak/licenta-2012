using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace TetriSomething
{
    class tet_blocks
    {        
        
        string[] shapes = { "I", "J", "L", "O", "S", "Z", "T" };  
        Random random = new Random();
        public string[,] Matrix = new string[20, 10];
        string currentShape;
        int currentRotation;
        int currentAnchorRow;
        int currentAnchorColon;
        tet_shapes myShapes = new tet_shapes();

        

        public tet_blocks()
        {
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 10; j++)
                    Matrix[i, j] = "B";
        }

        public string[] generateNextBlocks()
        {
            string[] _blocks = new string[10];
            int rnd;
            for (int i = 0; i < 10; i++)
            {
                rnd = random.Next(7);
                _blocks[i] = shapes[rnd];
            }

                return _blocks;

        }

        public void pushNewPiece()
        {
            string[] currentBlocks = generateNextBlocks();
            currentShape = currentBlocks[0];            
            currentRotation = 1;
            currentAnchorRow = 4;
            currentAnchorColon = 4;
            applyRotation(currentShape, currentRotation);
            
        }       

        public bool rotateCurrentShape()
        {
            rotateFromX(currentRotation);
            return true;
        }

        string[,] getShape(string shape, int rotation)
        {
            string[,] toModify = new string[10, 20];

            if (shape == "I") toModify = myShapes.shapeI[rotation - 1];
            if (shape == "J") toModify = myShapes.shapeJ[rotation - 1];
            if (shape == "L") toModify = myShapes.shapeL[rotation - 1];
            if (shape == "O") toModify = myShapes.shapeO[rotation - 1];
            if (shape == "S") toModify = myShapes.shapeS[rotation - 1];
            if (shape == "Z") toModify = myShapes.shapeZ[rotation - 1];
            if (shape == "T") toModify = myShapes.shapeT[rotation - 1];

            return toModify;
        }
        void applyRotation(string shape, int rotation)
        {
            string[,] toModify = new string[10,20];
            toModify = getShape(shape, rotation);

            for (int row = currentAnchorRow - 1; row <= currentAnchorRow + 1; row++)
                for (int colon = currentAnchorColon - 1; colon <= currentAnchorColon + 1; colon++)
                    Matrix[row, colon] = toModify[row - currentAnchorRow + 1, colon - currentAnchorColon + 1];  

        }

        public bool moveCurrentShapeToRight()
        {
            string[,] toModify = new string[10, 20];
            toModify = getShape(currentShape, currentRotation);

            applyMoveToRight(toModify);

            return true;
        }

        private void applyMoveToRight(string[,] toModify)
        {
            for (int row = currentAnchorRow - 1; row <= currentAnchorRow + 1; row++)
                for (int colon = currentAnchorColon; colon <= currentAnchorColon + 2; colon++)
                    Matrix[row, colon] = toModify[row - currentAnchorRow + 1, colon - currentAnchorColon];  

            Matrix[currentAnchorRow - 1, currentAnchorColon - 1] = "B";
            Matrix[currentAnchorRow, currentAnchorColon - 1] = "B";
            Matrix[currentAnchorRow + 1, currentAnchorColon - 1] = "B";

            currentAnchorColon += 1;
        }

        

        /// <summary>
        ///   *** 
        /// </summary>
        /// <param name="x"></param>
        private void rotateFromX(int x)
        {
            if (x != 4)
                currentRotation = x + 1;
            else
                currentRotation = 1;

            applyRotation(currentShape, currentRotation);            
        }
        
        /// <summary> 
        /// This function calculates weather a line is full. 
        /// </summary> 
        /// <param name="Matrix">The string 2 sided array of the game board</param> 
        /// <returns>and int indicating the line that needs removed or -1 if there is no such line</returns> 
        private int removeOrNotAndWhat(string[,] Matrix)
        {
            int lineToRemove = -1; //failsafe 
            int lineCandidate = -1; //current Line 

            bool amIaFullLine = true; //line not full condition variable 

            for (int j = 0; j < 20; j++) // line by line 
            {
                lineCandidate = j; // where are we 
                amIaFullLine = true; // incocent until proven guilyy 
                for (int i = 0; i < 10; i++) // block by block 
                {
                    if (Matrix[i, j] == "B") //if there are blanks 
                        amIaFullLine = false; // exit condition 
                }

                if (amIaFullLine == true) //We've found Jeebus 
                    return lineCandidate; // GTFO -> 
            }

            return lineToRemove; //failsafe return 
        }

        /// <summary> 
        /// Call this to kill a row 
        /// </summary> 
        /// <param name="Matrix">The string 2 sided array of the game board</param> 
        private void doTheRemove(ref string[,] Matrix, int lineToRemove) 
        { 
            for (int j = lineToRemove; j > 0 ; j--) // line by line 
            { 
                for (int i = 0; i < 10; i++) // block by block 
                { 
                if( j == 0) //top row 
                    Matrix[i, j] = "B"; //insert blanks 
                else 
                    Matrix[i, j] = Matrix[i, j - 1]; //bottom row 
                } 
            } 
        }

        internal bool moveCurrentShapeToLeft()
        {
            throw new NotImplementedException();
        }

        internal bool moveCurrentShapeDown()
        {
            throw new NotImplementedException();
        }
    }
}
