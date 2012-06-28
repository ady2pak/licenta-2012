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
        
        char[] shapes = { 'i', 'j', 'l', 'o', 's', 'z', 't' };  
        Random random = new Random();
        tet_constants game = new tet_constants();
        tet_shapes myShapes = new tet_shapes();
        tet_colors colors = new tet_colors();
        public tet_score myScore = new tet_score();
        public char currentShape;
        int currentRotation;
        int currentAnchorRow;
        int currentAnchorColumn;        
        public int usedShapesNr = -1;
        public int clearedLines = 0;
        char [] currentBlocks, futureBlocks = new char[10];
        

        public void initGameMatrix()
        {
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 10; j++)
                    tet_constants.gameMatrix[i, j] = 0;
        }

        public char[] generateNextBlocks()
        {
            char[] _blocks = new char[10];
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
            usedShapesNr++;

            if (usedShapesNr != 0 && usedShapesNr % 10 == 0)
            {
                currentBlocks = futureBlocks;
                futureBlocks = generateNextBlocks();
            }

            currentShape = currentBlocks[usedShapesNr % 10];
            
            currentRotation = 1;
            currentAnchorRow = 1;
            currentAnchorColumn = 5;

            int[,] newPosition = new int[3, 4];
            newPosition = getShape(currentShape, currentRotation);

            tet_constants.gameMatrix[currentAnchorRow, currentAnchorColumn] = 1;
            tet_constants.gameMatrix[currentAnchorRow + newPosition[0, 0], currentAnchorColumn + newPosition[0, 1]] = 1;
            tet_constants.gameMatrix[currentAnchorRow + newPosition[1, 0], currentAnchorColumn + newPosition[1, 1]] = 1;
            tet_constants.gameMatrix[currentAnchorRow + newPosition[2, 0], currentAnchorColumn + newPosition[2, 1]] = 1;

            tet_constants.colorMatrix[currentAnchorRow, currentAnchorColumn] = currentShape;
            tet_constants.colorMatrix[currentAnchorRow + newPosition[0, 0], currentAnchorColumn + newPosition[0, 1]] = currentShape;
            tet_constants.colorMatrix[currentAnchorRow + newPosition[1, 0], currentAnchorColumn + newPosition[1, 1]] = currentShape;
            tet_constants.colorMatrix[currentAnchorRow + newPosition[2, 0], currentAnchorColumn + newPosition[2, 1]] = currentShape;
           
            
        }       

        public bool rotateCurrentShape()
        {
            bool wasSuccesfull = rotateFromX(currentRotation);
            return wasSuccesfull;
        }

        int[,] getShape(char shape, int rotation)
        {
            int[,] toModify = new int[3, 4];

            if (shape == 'i') toModify = myShapes.shapeI[rotation - 1];
            else if (shape == 'j') toModify = myShapes.shapeJ[rotation - 1];
            else if (shape == 'l') toModify = myShapes.shapeL[rotation - 1];
            else if (shape == 'o') toModify = myShapes.shapeO[rotation - 1];
            else if (shape == 's') toModify = myShapes.shapeS[rotation - 1];
            else if (shape == 'z') toModify = myShapes.shapeZ[rotation - 1];
            else if (shape == 't') toModify = myShapes.shapeT[rotation - 1];

            return toModify;
        }

        bool applyRotation(char shape, int rotation)
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

            tet_constants.colorMatrix[currentAnchorRow, currentAnchorColumn] = 'w';
            tet_constants.colorMatrix[currentAnchorRow + oldPosition[0, 0], currentAnchorColumn + oldPosition[0, 1]] = 'w';
            tet_constants.colorMatrix[currentAnchorRow + oldPosition[1, 0], currentAnchorColumn + oldPosition[1, 1]] = 'w';
            tet_constants.colorMatrix[currentAnchorRow + oldPosition[2, 0], currentAnchorColumn + oldPosition[2, 1]] = 'w';

            currentAnchorColumn += bounce;

            tet_constants.gameMatrix[currentAnchorRow, currentAnchorColumn] = 1;
            tet_constants.gameMatrix[currentAnchorRow + newPosition[0, 0], currentAnchorColumn + newPosition[0, 1]] = 1;
            tet_constants.gameMatrix[currentAnchorRow + newPosition[1, 0], currentAnchorColumn + newPosition[1, 1]] = 1;
            tet_constants.gameMatrix[currentAnchorRow + newPosition[2, 0], currentAnchorColumn + newPosition[2, 1]] = 1;

            tet_constants.colorMatrix[currentAnchorRow, currentAnchorColumn] = currentShape;
            tet_constants.colorMatrix[currentAnchorRow + newPosition[0, 0], currentAnchorColumn + newPosition[0, 1]] = currentShape;
            tet_constants.colorMatrix[currentAnchorRow + newPosition[1, 0], currentAnchorColumn + newPosition[1, 1]] = currentShape;
            tet_constants.colorMatrix[currentAnchorRow + newPosition[2, 0], currentAnchorColumn + newPosition[2, 1]] = currentShape;

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

            tet_constants.colorMatrix[currentAnchorRow, currentAnchorColumn] = 'w';
            tet_constants.colorMatrix[currentAnchorRow + shape[0, 0], currentAnchorColumn + shape[0, 1]] = 'w';
            tet_constants.colorMatrix[currentAnchorRow + shape[1, 0], currentAnchorColumn + shape[1, 1]] = 'w';
            tet_constants.colorMatrix[currentAnchorRow + shape[2, 0], currentAnchorColumn + shape[2, 1]] = 'w';

            currentAnchorColumn += 1; 

            tet_constants.gameMatrix[currentAnchorRow, currentAnchorColumn] = 1;
            tet_constants.gameMatrix[currentAnchorRow + shape[0, 0], currentAnchorColumn + shape[0, 1]] = 1;
            tet_constants.gameMatrix[currentAnchorRow + shape[1, 0], currentAnchorColumn + shape[1, 1]] = 1;
            tet_constants.gameMatrix[currentAnchorRow + shape[2, 0], currentAnchorColumn + shape[2, 1]] = 1;

            tet_constants.colorMatrix[currentAnchorRow, currentAnchorColumn] = currentShape;
            tet_constants.colorMatrix[currentAnchorRow + shape[0, 0], currentAnchorColumn + shape[0, 1]] = currentShape;
            tet_constants.colorMatrix[currentAnchorRow + shape[1, 0], currentAnchorColumn + shape[1, 1]] = currentShape;
            tet_constants.colorMatrix[currentAnchorRow + shape[2, 0], currentAnchorColumn + shape[2, 1]] = currentShape;
        }        

        private void applyMoveToLeft(int[,] shape)
        {
            //if (currentAnchorColumn - 1 < 1) return;

            //if (!isInBounds(-1, shape)) return;

            tet_constants.gameMatrix[currentAnchorRow, currentAnchorColumn] = 0;
            tet_constants.gameMatrix[currentAnchorRow + shape[0, 0], currentAnchorColumn + shape[0, 1]] = 0;
            tet_constants.gameMatrix[currentAnchorRow + shape[1, 0], currentAnchorColumn + shape[1, 1]] = 0;
            tet_constants.gameMatrix[currentAnchorRow + shape[2, 0], currentAnchorColumn + shape[2, 1]] = 0;

            tet_constants.colorMatrix[currentAnchorRow, currentAnchorColumn] = 'w';
            tet_constants.colorMatrix[currentAnchorRow + shape[0, 0], currentAnchorColumn + shape[0, 1]] = 'w';
            tet_constants.colorMatrix[currentAnchorRow + shape[1, 0], currentAnchorColumn + shape[1, 1]] = 'w';
            tet_constants.colorMatrix[currentAnchorRow + shape[2, 0], currentAnchorColumn + shape[2, 1]] = 'w';

            currentAnchorColumn -= 1;

            tet_constants.gameMatrix[currentAnchorRow, currentAnchorColumn] = 1;
            tet_constants.gameMatrix[currentAnchorRow + shape[0, 0], currentAnchorColumn + shape[0, 1]] = 1;
            tet_constants.gameMatrix[currentAnchorRow + shape[1, 0], currentAnchorColumn + shape[1, 1]] = 1;
            tet_constants.gameMatrix[currentAnchorRow + shape[2, 0], currentAnchorColumn + shape[2, 1]] = 1;

            tet_constants.colorMatrix[currentAnchorRow, currentAnchorColumn] = currentShape;
            tet_constants.colorMatrix[currentAnchorRow + shape[0, 0], currentAnchorColumn + shape[0, 1]] = currentShape;
            tet_constants.colorMatrix[currentAnchorRow + shape[1, 0], currentAnchorColumn + shape[1, 1]] = currentShape;
            tet_constants.colorMatrix[currentAnchorRow + shape[2, 0], currentAnchorColumn + shape[2, 1]] = currentShape;
        }

        internal bool moveCurrentShapeDown()
        {
            int[,] shape = new int[3, 4];
            shape = getShape(currentShape, currentRotation);

            bool isFinalMove = applyMoveDown(shape);

            if (isFinalMove)
            {
                int clearedLinesThisDrop = 0;
                int iCanStillRemove = removeOrNotAndWhat(tet_constants.gameMatrix);
                while (iCanStillRemove != -1)
                {
                    doTheRemove(tet_constants.gameMatrix, iCanStillRemove);
                    clearedLinesThisDrop++;
                    iCanStillRemove = removeOrNotAndWhat(tet_constants.gameMatrix);                   
                    
                }

                //clearedLinesThisDrop--;

                if (clearedLinesThisDrop != 0)
                {
                    myScore.addScoringMove(clearedLinesThisDrop);
                    clearedLines += clearedLinesThisDrop;
                }
                else myScore.addNonScoringMove();
                pushNewPiece(); }
            return true;
        }

        private bool applyMoveDown(int[,] shape)
        {
            if (touchingLowerBound(shape))
            {
                tet_constants.gameMatrix[currentAnchorRow, currentAnchorColumn] = 1;
                tet_constants.gameMatrix[currentAnchorRow + shape[0, 0], currentAnchorColumn + shape[0, 1]] = 1;
                tet_constants.gameMatrix[currentAnchorRow + shape[1, 0], currentAnchorColumn + shape[1, 1]] = 1;
                tet_constants.gameMatrix[currentAnchorRow + shape[2, 0], currentAnchorColumn + shape[2, 1]] = 1;

                tet_constants.colorMatrix[currentAnchorRow, currentAnchorColumn] = currentShape;
                tet_constants.colorMatrix[currentAnchorRow + shape[0, 0], currentAnchorColumn + shape[0, 1]] = currentShape;
                tet_constants.colorMatrix[currentAnchorRow + shape[1, 0], currentAnchorColumn + shape[1, 1]] = currentShape;
                tet_constants.colorMatrix[currentAnchorRow + shape[2, 0], currentAnchorColumn + shape[2, 1]] = currentShape;

                return true;
            }

            tet_constants.gameMatrix[currentAnchorRow, currentAnchorColumn] = 0;
            tet_constants.gameMatrix[currentAnchorRow + shape[0, 0], currentAnchorColumn + shape[0, 1]] = 0;
            tet_constants.gameMatrix[currentAnchorRow + shape[1, 0], currentAnchorColumn + shape[1, 1]] = 0;
            tet_constants.gameMatrix[currentAnchorRow + shape[2, 0], currentAnchorColumn + shape[2, 1]] = 0;

            tet_constants.colorMatrix[currentAnchorRow, currentAnchorColumn] = 'w';
            tet_constants.colorMatrix[currentAnchorRow + shape[0, 0], currentAnchorColumn + shape[0, 1]] = 'w';
            tet_constants.colorMatrix[currentAnchorRow + shape[1, 0], currentAnchorColumn + shape[1, 1]] = 'w';
            tet_constants.colorMatrix[currentAnchorRow + shape[2, 0], currentAnchorColumn + shape[2, 1]] = 'w';

            if ((tet_constants.gameMatrix[currentAnchorRow + 1, currentAnchorColumn] == 1) ||
                (tet_constants.gameMatrix[currentAnchorRow + 1 + shape[0, 0], currentAnchorColumn + shape[0, 1]] == 1) ||
                (tet_constants.gameMatrix[currentAnchorRow + 1 + shape[1, 0], currentAnchorColumn + shape[1, 1]] == 1) ||
                (tet_constants.gameMatrix[currentAnchorRow + 1 + shape[2, 0], currentAnchorColumn + shape[2, 1]] == 1))
            {
                tet_constants.gameMatrix[currentAnchorRow, currentAnchorColumn] = 1;
                tet_constants.gameMatrix[currentAnchorRow + shape[0, 0], currentAnchorColumn + shape[0, 1]] = 1;
                tet_constants.gameMatrix[currentAnchorRow + shape[1, 0], currentAnchorColumn + shape[1, 1]] = 1;
                tet_constants.gameMatrix[currentAnchorRow + shape[2, 0], currentAnchorColumn + shape[2, 1]] = 1;

                tet_constants.colorMatrix[currentAnchorRow, currentAnchorColumn] = currentShape;
                tet_constants.colorMatrix[currentAnchorRow + shape[0, 0], currentAnchorColumn + shape[0, 1]] = currentShape;
                tet_constants.colorMatrix[currentAnchorRow + shape[1, 0], currentAnchorColumn + shape[1, 1]] = currentShape;
                tet_constants.colorMatrix[currentAnchorRow + shape[2, 0], currentAnchorColumn + shape[2, 1]] = currentShape;

                return true;
            }            

            currentAnchorRow += 1;

            tet_constants.gameMatrix[currentAnchorRow, currentAnchorColumn] = 1;
            tet_constants.gameMatrix[currentAnchorRow + shape[0, 0], currentAnchorColumn + shape[0, 1]] = 1;
            tet_constants.gameMatrix[currentAnchorRow + shape[1, 0], currentAnchorColumn + shape[1, 1]] = 1;
            tet_constants.gameMatrix[currentAnchorRow + shape[2, 0], currentAnchorColumn + shape[2, 1]] = 1;

            tet_constants.colorMatrix[currentAnchorRow, currentAnchorColumn] = currentShape;
            tet_constants.colorMatrix[currentAnchorRow + shape[0, 0], currentAnchorColumn + shape[0, 1]] = currentShape;
            tet_constants.colorMatrix[currentAnchorRow + shape[1, 0], currentAnchorColumn + shape[1, 1]] = currentShape;
            tet_constants.colorMatrix[currentAnchorRow + shape[2, 0], currentAnchorColumn + shape[2, 1]] = currentShape;

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
                    tet_constants.colorMatrix[i, j] = tet_constants.colorMatrix[i - 1, j];
                    Matrix[i, j] = Matrix[i - 1, j];
                }
        }

        public void initializePieces()
        {
            currentBlocks = generateNextBlocks();
            futureBlocks = generateNextBlocks();
        }

    }
}
