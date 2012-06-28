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
        tet_constants game = new tet_constants();
        //public string[,] game.gameMatrix = new string[20, 10];
        string currentShape;
        int currentRotation;
        int currentAnchorRow;
        int currentAnchorColumn;
        tet_shapes myShapes = new tet_shapes();

        

        public tet_blocks()
        {
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 10; j++)
                    tet_constants.gameMatrix[i, j] = 0;
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
            currentAnchorColumn = 4;
            applyRotation(currentShape, currentRotation);
            
        }       

        public bool rotateCurrentShape()
        {
            rotateFromX(currentRotation);
            return true;
        }

        int[,] getShape(string shape, int rotation)
        {
            int[,] toModify = new int[3, 4];

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
            int[,] newPosition = new int[3, 4];
            newPosition = getShape(shape, rotation);

            int[,] oldPosition = new int[3, 4];
            if (rotation != 1) oldPosition = getShape(shape, rotation - 1);
            else oldPosition = getShape(shape, 4);

            tet_constants.gameMatrix[currentAnchorRow, currentAnchorColumn] = 0;
            tet_constants.gameMatrix[currentAnchorRow + oldPosition[0, 0], currentAnchorColumn + oldPosition[0, 1]] = 0;
            tet_constants.gameMatrix[currentAnchorRow + oldPosition[1, 0], currentAnchorColumn + oldPosition[1, 1]] = 0;
            tet_constants.gameMatrix[currentAnchorRow + oldPosition[2, 0], currentAnchorColumn + oldPosition[2, 1]] = 0;

            tet_constants.gameMatrix[currentAnchorRow, currentAnchorColumn] = 1;
            tet_constants.gameMatrix[currentAnchorRow + newPosition[0, 0], currentAnchorColumn + newPosition[0, 1]] = 1;
            tet_constants.gameMatrix[currentAnchorRow + newPosition[1, 0], currentAnchorColumn + newPosition[1, 1]] = 1;
            tet_constants.gameMatrix[currentAnchorRow + newPosition[2, 0], currentAnchorColumn + newPosition[2, 1]] = 1;           

        }

        public bool moveCurrentShape(int where)
        {
            int[,] shape = new int[3, 4];
            shape = getShape(currentShape, currentRotation);

            if (where == 1) applyMoveToRight(shape);
            else applyMoveToLeft(shape);

            return true;
        }

        private void applyMoveToRight(int[,] shape)
        {
            //if ((currentAnchorColumn == 8 || currentAnchorColumn == 1) && toModify[0, 2] == "B" && toModify[1, 2] == "B" && toModify[2, 2] == "B")
            //{   //case for edge of screen
            //    toModify[0, 2] = toModify[0, 1]; toModify[0, 1] = toModify[0, 0]; toModify[0, 0] = "B";
            //    toModify[1, 2] = toModify[1, 1]; toModify[1, 1] = toModify[1, 0]; toModify[1, 0] = "B";
            //    toModify[2, 2] = toModify[2, 1]; toModify[2, 1] = toModify[2, 0]; toModify[2, 0] = "B";

            //    for (int row = currentAnchorRow - 1; row <= currentAnchorRow + 1; row++)
            //        for (int column = currentAnchorColumn - 1; column <= currentAnchorColumn + 1; column++)
            //            tet_constants.gameMatrix[row, column] = toModify[row - currentAnchorRow + 1, column - currentAnchorColumn + 1];

            //    tet_constants.gameMatrix[currentAnchorRow - 1, currentAnchorColumn - 1] = "B";
            //    tet_constants.gameMatrix[currentAnchorRow, currentAnchorColumn - 1] = "B";
            //    tet_constants.gameMatrix[currentAnchorRow + 1, currentAnchorColumn - 1] = "B";

            //    return;
            //}

            if (currentAnchorColumn + 1 > 8) return;

            tet_constants.gameMatrix[currentAnchorRow, currentAnchorColumn] = 0;
            tet_constants.gameMatrix[currentAnchorRow + shape[0, 0], currentAnchorColumn + shape[0, 1]] = 0;
            tet_constants.gameMatrix[currentAnchorRow + shape[1, 0], currentAnchorColumn + shape[1, 1]] = 0;
            tet_constants.gameMatrix[currentAnchorRow + shape[2, 0], currentAnchorColumn + shape[2, 1]] = 0;

            currentAnchorColumn += 1; 

            tet_constants.gameMatrix[currentAnchorRow, currentAnchorColumn] = 1;
            tet_constants.gameMatrix[currentAnchorRow + shape[0, 0], currentAnchorColumn + shape[0, 1]] = 1;
            tet_constants.gameMatrix[currentAnchorRow + shape[1, 0], currentAnchorColumn + shape[1, 1]] = 1;
            tet_constants.gameMatrix[currentAnchorRow + shape[2, 0], currentAnchorColumn + shape[2, 1]] = 1;            
        }

        private void applyMoveToLeft(int[,] shape)
        {
        //    if ((currentAnchorColumn == 8 || currentAnchorColumn == 1) && toModify[0, 0] == "B" && toModify[1, 0] == "B" && toModify[2, 0] == "B")
        //    {   //case for edge of screen
        //        toModify[0, 0] = toModify[0, 1]; toModify[0, 1] = toModify[0, 2]; toModify[0, 2] = "B";
        //        toModify[1, 0] = toModify[1, 1]; toModify[1, 1] = toModify[1, 2]; toModify[1, 2] = "B";
        //        toModify[2, 0] = toModify[2, 1]; toModify[2, 1] = toModify[2, 2]; toModify[2, 2] = "B";

        //        for (int row = currentAnchorRow - 1; row <= currentAnchorRow + 1; row++)
        //            for (int column = currentAnchorColumn - 1; column <= currentAnchorColumn + 1; column++)
        //                tet_constants.gameMatrix[row, column] = toModify[row - currentAnchorRow + 1, column - currentAnchorColumn + 1];

        //        tet_constants.gameMatrix[currentAnchorRow - 1, currentAnchorColumn + 1] = "B";
        //        tet_constants.gameMatrix[currentAnchorRow, currentAnchorColumn + 1] = "B";
        //        tet_constants.gameMatrix[currentAnchorRow + 1, currentAnchorColumn + 1] = "B";

        //        return;
        //    }

            if (currentAnchorColumn - 1 < 1) return;

            tet_constants.gameMatrix[currentAnchorRow, currentAnchorColumn] = 0;
            tet_constants.gameMatrix[currentAnchorRow + shape[0, 0], currentAnchorColumn + shape[0, 1]] = 0;
            tet_constants.gameMatrix[currentAnchorRow + shape[1, 0], currentAnchorColumn + shape[1, 1]] = 0;
            tet_constants.gameMatrix[currentAnchorRow + shape[2, 0], currentAnchorColumn + shape[2, 1]] = 0;

            currentAnchorColumn -= 1;

            tet_constants.gameMatrix[currentAnchorRow, currentAnchorColumn] = 1;
            tet_constants.gameMatrix[currentAnchorRow + shape[0, 0], currentAnchorColumn + shape[0, 1]] = 1;
            tet_constants.gameMatrix[currentAnchorRow + shape[1, 0], currentAnchorColumn + shape[1, 1]] = 1;
            tet_constants.gameMatrix[currentAnchorRow + shape[2, 0], currentAnchorColumn + shape[2, 1]] = 1;    
        }


        internal bool moveCurrentShapeDown()
        {
            int[,] shape = new int[3, 4];
            shape = getShape(currentShape, currentRotation);

            bool isFinalMove = applyMoveDown(shape);

            if (isFinalMove) { pushNewPiece(); }
            return true;
        }

        private bool applyMoveDown(int[,] shape)
        {
            if (currentAnchorRow == 18) return true;

            tet_constants.gameMatrix[currentAnchorRow, currentAnchorColumn] = 0;
            tet_constants.gameMatrix[currentAnchorRow + shape[0, 0], currentAnchorColumn + shape[0, 1]] = 0;
            tet_constants.gameMatrix[currentAnchorRow + shape[1, 0], currentAnchorColumn + shape[1, 1]] = 0;
            tet_constants.gameMatrix[currentAnchorRow + shape[2, 0], currentAnchorColumn + shape[2, 1]] = 0;

            if ((tet_constants.gameMatrix[currentAnchorRow + 1, currentAnchorColumn] == 1) ||
                (tet_constants.gameMatrix[currentAnchorRow + 1 + shape[0, 0], currentAnchorColumn + shape[0, 1]] == 1) ||
                (tet_constants.gameMatrix[currentAnchorRow + 1 + shape[1, 0], currentAnchorColumn + shape[1, 1]] == 1) ||
                (tet_constants.gameMatrix[currentAnchorRow + 1 + shape[2, 0], currentAnchorColumn + shape[2, 1]] == 1))
            {
                tet_constants.gameMatrix[currentAnchorRow, currentAnchorColumn] = 1;
                tet_constants.gameMatrix[currentAnchorRow + shape[0, 0], currentAnchorColumn + shape[0, 1]] = 1;
                tet_constants.gameMatrix[currentAnchorRow + shape[1, 0], currentAnchorColumn + shape[1, 1]] = 1;
                tet_constants.gameMatrix[currentAnchorRow + shape[2, 0], currentAnchorColumn + shape[2, 1]] = 1;
                return true;
            }            

            currentAnchorRow += 1;

            tet_constants.gameMatrix[currentAnchorRow, currentAnchorColumn] = 1;
            tet_constants.gameMatrix[currentAnchorRow + shape[0, 0], currentAnchorColumn + shape[0, 1]] = 1;
            tet_constants.gameMatrix[currentAnchorRow + shape[1, 0], currentAnchorColumn + shape[1, 1]] = 1;
            tet_constants.gameMatrix[currentAnchorRow + shape[2, 0], currentAnchorColumn + shape[2, 1]] = 1; 

            return false;
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
    }
}
