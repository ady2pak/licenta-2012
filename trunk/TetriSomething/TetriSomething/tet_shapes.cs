using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetriSomething
{
    public class tet_shapes
    {
        private int[,] O1 = { { 0, 1 }, { 1, 0 }, { 1, 1 } }; //OK 

        private int[,] L1 = { { 1, -1 }, { 0, -1 }, { 0, 1 } }; //OK
        private int[,] L2 = { { -1, -1 }, { -1, 0 }, { 1, 0 } };
        private int[,] L3 = { { -1, 1 }, { 0, 1 }, { 0, -1 } };
        private int[,] L4 = { { 1, 1 }, { 1, 0 }, { -1, 0 } };

        private int[,] J1 = { { 0, -1 }, { 0, 1 }, { 1, 1 } }; //OK
        private int[,] J2 = { { -1, 0 }, { 1, 0 }, { 1, -1 } };
        private int[,] J3 = { { 0, 1 }, { 0, -1 }, { -1, -1 } };
        private int[,] J4 = { { 1, 0 }, { -1, 0 }, { -1, 1 } };

        private int[,] T1 = { { 0, -1 }, { 1, 0 }, { 0, 1 } }; //OK
        private int[,] T2 = { { -1, 0 }, { 0, -1 }, { 1, 0 } };
        private int[,] T3 = { { 0, 1 }, { -1, 0 }, { 0, -1 } };
        private int[,] T4 = { { 1, 0 }, { 0, 1 }, { -1, 0 } };

        private int[,] S1 = { { 1, -1 }, { 1, 0 }, { 0, 1 } }; //OK
        private int[,] S2 = { { -1, -1 }, { 0, -1 }, { 1, 0 } };
        private int[,] S3 = { { -1, 1 }, { -1, 0 }, { 0, -1 } };
        private int[,] S4 = { { 1, 1 }, { 0, 1 }, { -1, 0 } };

        private int[,] Z1 = { { 0, -1 }, { 1, 0 }, { 1, 1 } }; //OK
        private int[,] Z2 = { { -1, 0 }, { 0, -1 }, { 1, -1 } };
        private int[,] Z3 = { { 0, 1 }, { -1, 0 }, { -1, -1 } };
        private int[,] Z4 = { { 1, 0 }, { 0, 1 }, { -1, 1 } };

        private int[,] I1 = { { 0, -1 }, { 0, 1 }, { 0, 2 } }; //OK
        private int[,] I2 = { { -1, 0 }, { 1, 0 }, { 2, 0 } };

        public List<int[,]> shapeO;
        public List<int[,]> shapeL;
        public List<int[,]> shapeJ;
        public List<int[,]> shapeT;
        public List<int[,]> shapeS;
        public List<int[,]> shapeZ;
        public List<int[,]> shapeI;

        public tet_shapes()
        {
            shapeO = new List<int[,]>();
            shapeO.Add(O1); shapeO.Add(O1); shapeO.Add(O1); shapeO.Add(O1);

            shapeL = new List<int[,]>();
            shapeL.Add(L1); shapeL.Add(L2); shapeL.Add(L3); shapeL.Add(L4);

            shapeJ = new List<int[,]>();
            shapeJ.Add(J1); shapeJ.Add(J2); shapeJ.Add(J3); shapeJ.Add(J4);

            shapeT = new List<int[,]>();
            shapeT.Add(T1); shapeT.Add(T2); shapeT.Add(T3); shapeT.Add(T4);

            shapeS = new List<int[,]>();
            shapeS.Add(S1); shapeS.Add(S2); shapeS.Add(S3); shapeS.Add(S4);

            shapeZ = new List<int[,]>();
            shapeZ.Add(Z1); shapeZ.Add(Z2); shapeZ.Add(Z3); shapeZ.Add(Z4);

            shapeI = new List<int[,]>();
            shapeI.Add(I1); shapeI.Add(I2); shapeI.Add(I1); shapeI.Add(I2);
        }
        

    }
}
