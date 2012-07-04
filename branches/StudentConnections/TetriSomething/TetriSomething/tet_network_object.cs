using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetriSomething
{
    [Serializable]
    public class tet_network_object
    {
        public char[,] enemyColorMatrix;
        public long enemyScore;
        public int enemyUsedShapes;
        public int enemyClearedLines;
        public char enemyNextShape;
    }
}