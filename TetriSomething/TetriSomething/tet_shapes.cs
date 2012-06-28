using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetriSomething
{
    public class tet_shapes
    {
        public int[,] O1 = { { 0, 1 }, { 1, 0 }, { 1, 1 } }; //OK 
        //public string[,] O1 = { { "O", "O" }, { "O", "O" } }; //same as O2, O3, O4;

        public int[,] L1 = { { 1, -1 }, { 0, -1 }, { 0, 1 } }; //OK
        public int[,] L2 = { { -1, -1 }, { -1, 0 }, { 1, 0 } };
        public int[,] L3 = { { -1, 1 }, { 0, 1 }, { 0, -1 } };
        public int[,] L4 = { { 1, 1 }, { 1, 0 }, { -1, 0 } }; 
        //public string[,] L1 = { { "B", "L", "B" }, { "B", "L", "B" }, { "B", "L", "L" } };
        //public string[,] L2 = { { "B", "B", "B" }, { "L", "L", "L" }, { "L", "B", "B" } };
        //public string[,] L3 = { { "L", "L", "B" }, { "B", "L", "B" }, { "B", "L", "B" } };
        //public string[,] L4 = { { "B", "B", "L" }, { "L", "L", "L" }, { "B", "B", "B" } };

        public int[,] J1 = { { 0, -1 }, { 0, 1 }, { 1, 1 } }; //OK
        public int[,] J2 = { { -1, 0 }, { 1, 0 }, { 1, -1 } };
        public int[,] J3 = { { 0, 1 }, { 0, -1 }, { -1, -1 } };
        public int[,] J4 = { { 1, 0 }, { -1, 0 }, { -1, 1 } };
        //public string[,] J1 = { { "B", "J", "B" }, { "B", "J", "B" }, { "J", "J", "B" } };
        //public string[,] J2 = { { "J", "B", "B" }, { "J", "J", "J" }, { "B", "B", "B" } };
        //public string[,] J3 = { { "B", "J", "J" }, { "B", "J", "B" }, { "B", "J", "B" } };
        //public string[,] J4 = { { "B", "B", "B" }, { "J", "J", "J" }, { "B", "B", "J" } };

        public int[,] T1 = { { 0, -1 }, { 1, 0 }, { 0, 1 } }; //OK
        public int[,] T2 = { { -1, 0 }, { 0, -1 }, { 1, 0 } };
        public int[,] T3 = { { 0, 1 }, { -1, 0 }, { 0, -1 } };
        public int[,] T4 = { { 1, 0 }, { 0, 1 }, { -1, 0 } };
        //public string[,] T1 = { { "B", "B", "B" }, { "T", "T", "T" }, { "B", "T", "B" } };
        //public string[,] T2 = { { "B", "T", "B" }, { "T", "T", "B" }, { "B", "T", "B" } };
        //public string[,] T3 = { { "B", "T", "B" }, { "T", "T", "T" }, { "B", "B", "B" } };
        //public string[,] T4 = { { "B", "T", "B" }, { "B", "T", "T" }, { "B", "T", "B" } };

        public int[,] S1 = { { 1, -1 }, { 1, 0 }, { 0, 1 } }; //OK
        public int[,] S2 = { { -1, -1 }, { 0, -1 }, { 1, 0 } };
        public int[,] S3 = { { -1, 1 }, { -1, 0 }, { 0, -1 } };
        public int[,] S4 = { { 1, 1 }, { 0, 1 }, { -1, 0 } };
        //public string[,] S1 = { { "B", "S", "B" }, { "B", "S", "S" }, { "B", "B", "S" } }; //same as S3
        //public string[,] S2 = { { "B", "B", "B" }, { "B", "S", "S" }, { "S", "S", "B" } }; //same as S4

        public int[,] Z1 = { { 0, -1 }, { 1, 0 }, { 1, 1 } }; //OK
        public int[,] Z2 = { { -1, 0 }, { 0, -1 }, { 1, -1 } };
        public int[,] Z3 = { { 0, 1 }, { -1, 0 }, { -1, -1 } };
        public int[,] Z4 = { { 1, 0 }, { 0, 1 }, { -1, 1 } };
        //public string[,] Z1 = { { "B", "B", "Z" }, { "B", "Z", "Z" }, { "B", "Z", "B" } }; //same as Z3
        //public string[,] Z2 = { { "B", "B", "B" }, { "Z", "Z", "B" }, { "B", "Z", "Z" } }; //same as Z4

        public int[,] I1 = { { 0, -2 }, { 0, -1 }, { 0, 1 } }; //OK
        public int[,] I2 = { { -2, 0 }, { -1, 0 }, { 1, 0 } };

        //public string[,] I1 = { { "B", "B", "I", "B" }, { "B", "B", "I", "B" }, { "B", "B", "I", "B" }, { "B", "B", "I", "B" } }; //same as I3
        //public string[,] I2 = { { "B", "B", "B", "B" }, { "B", "B", "B", "B" }, { "I", "I", "I", "I" }, { "B", "B", "B", "B" } }; //same as I4

        //public List<string[,]> shapeO;
        //public List<string[,]> shapeL;
        //public List<string[,]> shapeJ;
        //public List<string[,]> shapeT;
        //public List<string[,]> shapeS;
        //public List<string[,]> shapeZ;
        //public List<string[,]> shapeI;

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
