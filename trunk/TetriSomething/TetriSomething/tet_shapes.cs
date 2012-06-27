using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetriSomething
{
    public class tet_shapes
    {
        public string[,] O1 = { { "O", "O" }, { "O", "O" } }; //same as O2, O3, O4;

        public string[,] L1 = { { "B", "L", "B" }, { "B", "L", "B" }, { "B", "L", "L" } };
        public string[,] L2 = { { "B", "B", "B" }, { "L", "L", "L" }, { "L", "B", "B" } };
        public string[,] L3 = { { "L", "L", "B" }, { "B", "L", "B" }, { "B", "L", "B" } };
        public string[,] L4 = { { "B", "B", "L" }, { "L", "L", "L" }, { "B", "B", "B" } };

        public string[,] J1 = { { "B", "J", "B" }, { "B", "J", "B" }, { "J", "J", "B" } };
        public string[,] J2 = { { "J", "B", "B" }, { "J", "J", "J" }, { "B", "B", "B" } };
        public string[,] J3 = { { "B", "J", "J" }, { "B", "J", "B" }, { "B", "J", "B" } };
        public string[,] J4 = { { "B", "B", "B" }, { "J", "J", "J" }, { "B", "B", "J" } };

        public string[,] T1 = { { "B", "B", "B" }, { "T", "T", "T" }, { "B", "T", "B" } };
        public string[,] T2 = { { "B", "T", "B" }, { "T", "T", "B" }, { "B", "T", "B" } };
        public string[,] T3 = { { "B", "T", "B" }, { "T", "T", "T" }, { "B", "B", "B" } };
        public string[,] T4 = { { "B", "T", "B" }, { "B", "T", "T" }, { "B", "T", "B" } };

        public string[,] S1 = { { "B", "S", "B" }, { "B", "S", "S" }, { "B", "B", "S" } }; //same as S3
        public string[,] S2 = { { "B", "B", "B" }, { "B", "S", "S" }, { "S", "S", "B" } }; //same as S4

        public string[,] Z1 = { { "B", "B", "Z" }, { "B", "Z", "Z" }, { "B", "Z", "B" } }; //same as Z3
        public string[,] Z2 = { { "B", "B", "B" }, { "Z", "Z", "B" }, { "B", "Z", "Z" } }; //same as Z4

        public string[,] I1 = { { "B", "B", "I", "B" }, { "B", "B", "I", "B" }, { "B", "B", "I", "B" }, { "B", "B", "I", "B" } }; //same as I3
        public string[,] I2 = { { "B", "B", "B", "B" }, { "B", "B", "B", "B" }, { "I", "I", "I", "I" }, { "B", "B", "B", "B" } }; //same as I4

        public List<string[,]> shapeO;
        public List<string[,]> shapeL;
        public List<string[,]> shapeJ;
        public List<string[,]> shapeT;
        public List<string[,]> shapeS;
        public List<string[,]> shapeZ;
        public List<string[,]> shapeI;

        public tet_shapes()
        {
            shapeO = new List<string[,]>();
            shapeO.Add(O1); shapeO.Add(O1); shapeO.Add(O1); shapeO.Add(O1);

            shapeL = new List<string[,]>();
            shapeL.Add(L1); shapeL.Add(L2); shapeL.Add(L3); shapeL.Add(L4);

            shapeJ = new List<string[,]>();
            shapeJ.Add(J1); shapeJ.Add(J2); shapeJ.Add(J3); shapeJ.Add(J4);

            shapeT = new List<string[,]>();
            shapeT.Add(T1); shapeT.Add(T2); shapeT.Add(T3); shapeT.Add(T4);

            shapeS = new List<string[,]>();
            shapeS.Add(S1); shapeS.Add(S2); shapeS.Add(S1); shapeS.Add(S2);

            shapeZ = new List<string[,]>();
            shapeZ.Add(Z1); shapeZ.Add(Z2); shapeZ.Add(Z1); shapeZ.Add(Z2);

            shapeI = new List<string[,]>();
            shapeI.Add(I1); shapeI.Add(I2); shapeI.Add(I2); shapeI.Add(I2);
        }
        

    }
}
