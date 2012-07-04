using System.Drawing;

namespace TetriSomething
{
    public class tet_blocks
    {        
        
        char[] shapes = { 'i', 'j', 'l', 'o', 's', 'z', 't' };
        tet_random random = new tet_random();
        tet_constants game = new tet_constants();
        tet_shapes myShapes = new tet_shapes();
        tet_colors colors = new tet_colors();
        public tet_network_object objectToSend = new tet_network_object();
        public tet_network_object oldReceivedObject = new tet_network_object();
        public tet_score myScore = new tet_score();
        public char currentShape;
        public int currentRotation;
        int currentAnchorRow;
        int currentAnchorColumn;        
        public int usedShapesNr = 0;
        public int clearedLines = 0;
        char [] currentBlocks, futureBlocks = new char[10];
        private mainWindow mainWindow;

        public tet_blocks(mainWindow mainWindow)
        {            
            this.mainWindow = mainWindow;
        }

        public int[,] getShape(char shape, int rotation)
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

        private void setShapeInColorMatrix(int[,] shape, char value)
        {
            tet_constants.colorMatrix[currentAnchorRow, currentAnchorColumn] = value;
            tet_constants.colorMatrix[currentAnchorRow + shape[0, 0], currentAnchorColumn + shape[0, 1]] = value;
            tet_constants.colorMatrix[currentAnchorRow + shape[1, 0], currentAnchorColumn + shape[1, 1]] = value;
            tet_constants.colorMatrix[currentAnchorRow + shape[2, 0], currentAnchorColumn + shape[2, 1]] = value;
        }

        private void setShapeInGameMatrix(int[,] shape, int value)
        {
            tet_constants.gameMatrix[currentAnchorRow, currentAnchorColumn] = value;
            tet_constants.gameMatrix[currentAnchorRow + shape[0, 0], currentAnchorColumn + shape[0, 1]] = value;
            tet_constants.gameMatrix[currentAnchorRow + shape[1, 0], currentAnchorColumn + shape[1, 1]] = value;
            tet_constants.gameMatrix[currentAnchorRow + shape[2, 0], currentAnchorColumn + shape[2, 1]] = value;
        }

        public void initializePieces()
        {
            currentBlocks = generateNextBlocks();
            futureBlocks = generateNextBlocks();
        }

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
                rnd = random.getRandomInt(7);
                _blocks[i] = shapes[rnd];
            }

                return _blocks;

        }

        void decideIfStar()
        {
            tet_random random = new tet_random();
            int highestBOnus = highestRowWithPieces(tet_constants.gameMatrix);
            if (highestBOnus != -1)//being and empty row
            {
                if (random.getRandomInt(tet_constants.CHANCE_POWERUP_STAR) == tet_constants.POWER_UP_OCCURED)
                {
                    //this piece's powerup;
                    int whatColumn = random.getRandomInt(10);
                    int whatRow = random.getRandomInt(20 - highestBOnus);
                    //try
                    //{
                    tet_constants.gameMatrix[19 - whatRow, whatColumn] = 1;
                    tet_constants.colorMatrix[19 - whatRow, whatColumn] = 'p';

                    Image image = Image.FromFile(tet_colors.STAR);
                    mainWindow.graphicsObj2.DrawImage(image, new Rectangle(260 + whatColumn * 30, 100 + (19 - whatRow) * 30, 30, 30));
                    //}
                    //catch
                    //{
                    //    //quick, dirty, slutty, filthy, disease ridden fix.
                    //}
                }
            }
        }

        public bool pushNewPiece()
        {
            //decideIfStar();

            //usedShapesNr++;

            if (usedShapesNr != 0 && usedShapesNr % 10 == 0)
            {
                currentBlocks = futureBlocks;
                futureBlocks = generateNextBlocks();
            }

            if (usedShapesNr % 10 == 9)
            {
                mainWindow.drawMyNextShape(futureBlocks[0]);
                objectToSend.enemyNextShape = futureBlocks[0];
            }
            else
            {
                mainWindow.drawMyNextShape(currentBlocks[usedShapesNr % 10 + 1]);
                objectToSend.enemyNextShape = currentBlocks[usedShapesNr % 10 + 1];
            }

            currentShape = currentBlocks[usedShapesNr % 10];            
            currentRotation = 1;
            currentAnchorRow = 0;
            currentAnchorColumn = 5;            

            int[,] shape = new int[3, 4];
            shape = getShape(currentShape, currentRotation);

            bool gameOver = isItPossible(shape);            
            
            tet_constants.gameMatrix[currentAnchorRow, currentAnchorColumn] = 1;
            tet_constants.gameMatrix[currentAnchorRow + shape[0, 0], currentAnchorColumn + shape[0, 1]] = 1;
            tet_constants.gameMatrix[currentAnchorRow + shape[1, 0], currentAnchorColumn + shape[1, 1]] = 1;
            tet_constants.gameMatrix[currentAnchorRow + shape[2, 0], currentAnchorColumn + shape[2, 1]] = 1;

            tet_constants.colorMatrix[currentAnchorRow, currentAnchorColumn] = currentShape;
            tet_constants.colorMatrix[currentAnchorRow + shape[0, 0], currentAnchorColumn + shape[0, 1]] = currentShape;
            tet_constants.colorMatrix[currentAnchorRow + shape[1, 0], currentAnchorColumn + shape[1, 1]] = currentShape;
            tet_constants.colorMatrix[currentAnchorRow + shape[2, 0], currentAnchorColumn + shape[2, 1]] = currentShape;

            mainWindow.drawNewPiece(mainWindow.graphicsObj1, shape, currentShape, currentAnchorRow, currentAnchorColumn);

            if (gameOver) return false;

            return true;
        }

        public bool rotateCurrentShape()
        {
            while (currentAnchorRow == 0) moveCurrentShapeDown("STEP");

            bool wasSuccesfull = rotateFromX(currentRotation);
            return wasSuccesfull;
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

        internal bool moveCurrentShapeDown(string type)
        {
            int[,] shape = new int[3, 4];
            int[] removedPs = new int[10];
            bool isFinalMove = false;
            shape = getShape(currentShape, currentRotation);

            if (type == "SNAP")
            {
                myScore.addInstaDropBonus(20 - currentAnchorRow);
                while (!isFinalMove) isFinalMove = applyMoveDown(shape);                
            }
            else isFinalMove = applyMoveDown(shape);

            if (isFinalMove)
            {
                int clearedLinesThisDrop = 0;
                int iCanStillRemove = removeOrNotAndWhat(tet_constants.gameMatrix);
                while (iCanStillRemove != -1)
                {
                    removedPs = doTheRemove(tet_constants.gameMatrix, iCanStillRemove);
                    for (int i = 0; i < 10; i++)
                    {
                        if (removedPs[i] == 'p')
                            myScore.addStarBonus();
                    }
                    clearedLinesThisDrop++;
                    iCanStillRemove = removeOrNotAndWhat(tet_constants.gameMatrix);

                }

                if (clearedLinesThisDrop != 0)
                {
                    myScore.addScoringMove(clearedLinesThisDrop);
                    clearedLines += clearedLinesThisDrop;
                }
                else myScore.addNonScoringMove();

                usedShapesNr++;

                mainWindow.drawMyMatrix(mainWindow.graphicsObj1);

                mainWindow.drawMyScore(mainWindow.graphicsObj1);

                objectToSend.enemyScore = myScore.getScore();

                if (!pushNewPiece()) return false;
            }

            return true;
        }       

        private bool isItPossible(int[,] shape)
        {
            if (tet_constants.gameMatrix[currentAnchorRow, currentAnchorColumn] == 1) return true;
            if (tet_constants.gameMatrix[currentAnchorRow + shape[0, 0], currentAnchorColumn + shape[0, 1]] == 1) return true;
            if (tet_constants.gameMatrix[currentAnchorRow + shape[1, 0], currentAnchorColumn + shape[1, 1]] == 1) return true;
            if (tet_constants.gameMatrix[currentAnchorRow + shape[2, 0], currentAnchorColumn + shape[2, 1]] == 1) return true;

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
 
        private bool applyRotation(char shape, int rotation)
        {
            if (currentAnchorRow == 0) return false;
            int[,] newPosition = new int[3, 4];
            newPosition = getShape(shape, rotation);

            int[,] oldPosition = new int[3, 4];
            if (rotation != 1) oldPosition = getShape(shape, rotation - 1);
            else oldPosition = getShape(shape, 4);

            int bounce = requiredBounce(newPosition, rotation);            

            if (!iCanRotate(tet_constants.gameMatrix, oldPosition, newPosition, bounce)) return false;

            setShapeInGameMatrix(oldPosition, 0); 
            setShapeInColorMatrix(oldPosition, 'w');

            currentAnchorColumn += bounce;            

            setShapeInGameMatrix(newPosition, 1);
            setShapeInColorMatrix(newPosition, currentShape);

            mainWindow.drawRotation(mainWindow.graphicsObj1, oldPosition, newPosition, currentAnchorRow, currentAnchorColumn, currentShape);

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
            setShapeInGameMatrix(shape, 0);
            setShapeInColorMatrix(shape, 'w');

            currentAnchorColumn += 1;

            setShapeInGameMatrix(shape, 1);
            setShapeInColorMatrix(shape, currentShape);

            mainWindow.drawMoveRight(mainWindow.graphicsObj1, shape, currentAnchorRow, currentAnchorColumn, currentShape);
        }        

        private void applyMoveToLeft(int[,] shape)
        {
            setShapeInGameMatrix(shape, 0);
            setShapeInColorMatrix(shape, 'w');            

            currentAnchorColumn -= 1;

            setShapeInGameMatrix(shape, 1);
            setShapeInColorMatrix(shape, currentShape);

            mainWindow.drawMoveLeft(mainWindow.graphicsObj1, shape, currentAnchorRow, currentAnchorColumn, currentShape);
        }      

        private bool applyMoveDown(int[,] shape)
        {
            if (touchingLowerBound(shape))
            {
                setShapeInGameMatrix(shape, 1);
                setShapeInColorMatrix(shape, currentShape);
                return true;
            }

            setShapeInGameMatrix(shape, 0);
            setShapeInColorMatrix(shape, 'w');            

            if ((tet_constants.gameMatrix[currentAnchorRow + 1, currentAnchorColumn] == 1) ||
                (tet_constants.gameMatrix[currentAnchorRow + 1 + shape[0, 0], currentAnchorColumn + shape[0, 1]] == 1) ||
                (tet_constants.gameMatrix[currentAnchorRow + 1 + shape[1, 0], currentAnchorColumn + shape[1, 1]] == 1) ||
                (tet_constants.gameMatrix[currentAnchorRow + 1 + shape[2, 0], currentAnchorColumn + shape[2, 1]] == 1))
            {
                setShapeInGameMatrix(shape, 1);
                setShapeInColorMatrix(shape, currentShape);    
                return true;
            }            

            currentAnchorRow += 1;

            setShapeInGameMatrix(shape, 1);
            setShapeInColorMatrix(shape, currentShape);

            mainWindow.drawMoveDown(mainWindow.graphicsObj1, shape, currentAnchorRow, currentAnchorColumn, currentShape);

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
         
        private int removeOrNotAndWhat(int[,] Matrix)
        {
            int lineToRemove = -1; 
            int lineCandidate = -1; 

            bool amIaFullLine = true; 

            for (int i = 0; i < 20; i++) 
            {
                lineCandidate = i; 
                amIaFullLine = true; 
                for (int j = 0; j < 10; j++)
                {
                    if (Matrix[i, j] == 0) 
                        amIaFullLine = false; 
                }

                if (amIaFullLine == true) 
                    return lineCandidate;
            }

            return lineToRemove; 
        }

        private int[] doTheRemove(int[,] Matrix, int lineToRemove)
        {
            int[] returnVec = new int[10];
            for (int i = 0; i < 10; i++)
            { 
                returnVec[i] = Matrix[lineToRemove, i]; 
            }

            for (int i = lineToRemove; i > 0; i--)
                for (int j = 0; j < 10; j++)
                {
                    //if (tet_constants.colorMatrix[i, j] == 'p')
                    //{
                    //    myScore.addStarBonus();
                    //}
                    tet_constants.colorMatrix[i, j] = tet_constants.colorMatrix[i - 1, j];
                    Matrix[i, j] = Matrix[i - 1, j];
                }
            return returnVec;
        }

        private int highestRowWithPieces(int[,] Matrix)
        {
            int returnRow = -1;
            for (int i = 4; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (Matrix[i, j] == 1)
                    {
                        return i;
                    }
                }
            }

            return returnRow;
        }
   

    }
}
