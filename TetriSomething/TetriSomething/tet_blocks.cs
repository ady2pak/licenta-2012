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
            bool wasSuccesfull = rotateFromX(currentRotation);
            return wasSuccesfull;
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

        bool applyRotation(string shape, int rotation)
        {
            int[,] newPosition = new int[3, 4];
            newPosition = getShape(shape, rotation);

            int[,] oldPosition = new int[3, 4];
            if (rotation != 1) oldPosition = getShape(shape, rotation - 1);
            else oldPosition = getShape(shape, 4);

            int bounce = requiredBounce(newPosition, rotation);

            if (!iCanRotate(tet_constants.gameMatrix, oldPosition, newPosition, bounce)) return false;            

            tet_constants.gameMatrix[currentAnchorRow, currentAnchorColumn] = 0;
            tet_constants.gameMatrix[currentAnchorRow + oldPosition[0, 0], currentAnchorColumn + oldPosition[0, 1]] = 0;
            tet_constants.gameMatrix[currentAnchorRow + oldPosition[1, 0], currentAnchorColumn + oldPosition[1, 1]] = 0;
            tet_constants.gameMatrix[currentAnchorRow + oldPosition[2, 0], currentAnchorColumn + oldPosition[2, 1]] = 0;

            currentAnchorColumn += bounce;

            tet_constants.gameMatrix[currentAnchorRow, currentAnchorColumn] = 1;
            tet_constants.gameMatrix[currentAnchorRow + newPosition[0, 0], currentAnchorColumn + newPosition[0, 1]] = 1;
            tet_constants.gameMatrix[currentAnchorRow + newPosition[1, 0], currentAnchorColumn + newPosition[1, 1]] = 1;
            tet_constants.gameMatrix[currentAnchorRow + newPosition[2, 0], currentAnchorColumn + newPosition[2, 1]] = 1;

            return true;
        }

        private bool iCanRotate(int[,] gameMatrixReference, int[,] oldPosition, int[,] newPosition, int bounce)
        {
            int[,] gameMatrix = gameMatrixReference;

            int currentAnchorColumnReference = currentAnchorColumn + bounce;

            gameMatrix[currentAnchorRow, currentAnchorColumn] = 0;
            gameMatrix[currentAnchorRow + oldPosition[0, 0], currentAnchorColumn + oldPosition[0, 1]] = 0;
            gameMatrix[currentAnchorRow + oldPosition[1, 0], currentAnchorColumn + oldPosition[1, 1]] = 0;
            gameMatrix[currentAnchorRow + oldPosition[2, 0], currentAnchorColumn + oldPosition[2, 1]] = 0;

            if (gameMatrix[currentAnchorRow, currentAnchorColumn] == 1) return false;
            if (gameMatrix[currentAnchorRow + newPosition[0, 0], currentAnchorColumnReference + newPosition[0, 1]] == 1) return false;
            if (gameMatrix[currentAnchorRow + newPosition[1, 0], currentAnchorColumnReference + newPosition[1, 1]] == 1) return false;
            if (gameMatrix[currentAnchorRow + newPosition[2, 0], currentAnchorColumnReference + newPosition[2, 1]] == 1) return false;

            return true;
        }

        private int requiredBounce(int[,] newPosition, int rotation)
        {
            int bounce = 0;
            bool positive = true;

            if (currentAnchorColumn + newPosition[0, 1] > 9)
                if (currentAnchorColumn + newPosition[0, 1] - 9 > bounce)
                {
                    bounce = currentAnchorColumn + newPosition[0, 1] - 9;
                    positive = false;
                }
            if (currentAnchorColumn + newPosition[1, 1] > 9)
                if (currentAnchorColumn + newPosition[1, 1] - 9 > bounce)
                {
                    bounce = currentAnchorColumn + newPosition[1, 1] - 9;
                    positive = false;
                }
            if (currentAnchorColumn + newPosition[2, 1] > 9)
                if (currentAnchorColumn + newPosition[2, 1] - 9 > bounce)
                {
                    bounce = currentAnchorColumn + newPosition[2, 1] - 9;
                    positive = false;
                }

            if (positive)
            {
                if (currentAnchorColumn + newPosition[0, 1] < 0)
                    if (currentAnchorColumn + newPosition[0, 1] < bounce)
                    {
                        bounce = currentAnchorColumn + newPosition[0, 1];
                        //positive = true;
                    }
                if (currentAnchorColumn + newPosition[1, 1] < 0)
                    if (currentAnchorColumn + newPosition[1, 1] < bounce)
                    {
                        bounce = currentAnchorColumn + newPosition[1, 1];
                        //positive = false;
                    }
                if (currentAnchorColumn + newPosition[2, 1] < 0)
                    if (currentAnchorColumn + newPosition[2, 1] < bounce)
                    {
                        bounce = currentAnchorColumn + newPosition[2, 1];
                        //positive = false;
                    }
            }

            return -bounce;
        }       

        public bool moveCurrentShape(int where)
        {
            int[,] shape = new int[3, 4];
            shape = getShape(currentShape, currentRotation);

            if (!isInBounds(where, shape)) return false;
            if (!icanMoveThere(tet_constants.gameMatrix, shape, where)) return false;

            if (where == 1) applyMoveToRight(shape);
            else applyMoveToLeft(shape);

            return true;
        }

        private bool icanMoveThere(int[,] gameMatrix, int[,] shape, int where)
        {
            int[,] gameMatrixReference = gameMatrix;
            int anchorColumnReference = currentAnchorColumn;

            gameMatrixReference[currentAnchorRow, anchorColumnReference] = 0;
            gameMatrixReference[currentAnchorRow + shape[0, 0], anchorColumnReference + shape[0, 1]] = 0;
            gameMatrixReference[currentAnchorRow + shape[1, 0], anchorColumnReference + shape[1, 1]] = 0;
            gameMatrixReference[currentAnchorRow + shape[2, 0], anchorColumnReference + shape[2, 1]] = 0;

            anchorColumnReference += where;

            if (gameMatrixReference[currentAnchorRow, anchorColumnReference] == 1) return false;
            if (gameMatrixReference[currentAnchorRow + shape[0, 0], anchorColumnReference + shape[0, 1]] == 1) return false;
            if (gameMatrixReference[currentAnchorRow + shape[1, 0], anchorColumnReference + shape[1, 1]] == 1) return false;
            if (gameMatrixReference[currentAnchorRow + shape[2, 0], anchorColumnReference + shape[2, 1]] == 1) return false;

            return true;
        }

        private bool isInBounds(int anchorShift, int[,] shape)
        {
            if ((currentAnchorColumn + anchorShift > 9) || (currentAnchorColumn + anchorShift < 0)) return false;
            if ((currentAnchorColumn + anchorShift + shape[0, 1] > 9) || (currentAnchorColumn + anchorShift + shape[0, 1] < 0)) return false;
            if ((currentAnchorColumn + anchorShift + shape[1, 1] > 9) || (currentAnchorColumn + anchorShift + shape[1, 1] < 0)) return false;
            if ((currentAnchorColumn + anchorShift + shape[2, 1] > 9) || (currentAnchorColumn + anchorShift + shape[2, 1] < 0)) return false;

            return true;
        }

        private void applyMoveToRight(int[,] shape)
        {
            //if (currentAnchorColumn + 1 > 8) return;

            //if (!isInBounds(1, shape)) return;

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
            //if (currentAnchorColumn - 1 < 1) return;

            //if (!isInBounds(-1, shape)) return;

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

            if (isFinalMove)
            {
                int iCanStillRemove = removeOrNotAndWhat(tet_constants.gameMatrix);
                while (iCanStillRemove != -1)
                {
                    iCanStillRemove = removeOrNotAndWhat(tet_constants.gameMatrix);
                    doTheRemove(tet_constants.gameMatrix, iCanStillRemove);
                }
                pushNewPiece(); }
            return true;
        }

        private bool applyMoveDown(int[,] shape)
        {
            //if (currentAnchorRow == 18) return true;

            if (touchingLowerBound(shape))
            {
                tet_constants.gameMatrix[currentAnchorRow, currentAnchorColumn] = 1;
                tet_constants.gameMatrix[currentAnchorRow + shape[0, 0], currentAnchorColumn + shape[0, 1]] = 1;
                tet_constants.gameMatrix[currentAnchorRow + shape[1, 0], currentAnchorColumn + shape[1, 1]] = 1;
                tet_constants.gameMatrix[currentAnchorRow + shape[2, 0], currentAnchorColumn + shape[2, 1]] = 1;
                return true;
            }

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

        private bool touchingLowerBound(int[,] shape)
        {
            if (currentAnchorRow + 1 > 19) return true;
            if (currentAnchorRow + shape[0, 0] + 1 > 19) return true;
            if (currentAnchorRow + shape[1, 0] + 1 > 19) return true;
            if (currentAnchorRow + shape[2, 0] + 1 > 19) return true;

            return false;
        }        
                
        private bool rotateFromX(int x)
        {
            int previousRotation = currentRotation;
            if (x != 4)
                currentRotation = x + 1;
            else
                currentRotation = 1;

            bool wasRotated = applyRotation(currentShape, currentRotation);

            if (!wasRotated) currentRotation = previousRotation; 

            return wasRotated;

        }        
    
        private int removeOrNotAndWhat(int[,] Matrix)
        {
            int lineToRemove = -1; //failsafe 
            int lineCandidate = -1; //current Line 

            bool amIaFullLine = true; //line not full condition variable 

            for (int i = 0; i < 20; i++) // line by line 
            {
                lineCandidate = i; // where are we 
                amIaFullLine = true; // incocent until proven guilyy 
                for (int j = 0; j < 10; j++) // block by block 
                {
                    if (Matrix[i, j] == 0) //if there are blanks 
                        amIaFullLine = false; // exit condition 
                }

                if (amIaFullLine == true) //We've found Jeebus 
                    return lineCandidate; // GTFO -> 
            }

            return lineToRemove; //failsafe return 
        }

        private void doTheRemove(int[,] Matrix, int lineToRemove)
        {
            for (int i = lineToRemove; i > 0; i--)
                for (int j = 0; j < 10; j++)
                {
                    Matrix[i, j] = Matrix[i - 1, j];
                }
        }

    }
}
