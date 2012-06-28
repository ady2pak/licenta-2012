using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetriSomething
{
    public class tet_colors
    {
        

        public void initColorMatrix()
        {
            for (int row = 0; row < 20; row++)
                for (int column = 0; column < 10; column++)
                    tet_constants.colorMatrix[row, column] = 'w';
        }

        public string getPieceColor(char pieceData)
        {
            

            //string[] shapes = { "I", "J", "L", "O", "S", "Z", "T" };  
            string[] listColors = new string[8] {"png/block_white.png",
                                                "png/block_cyan.png" ,
                                                "png/block_blue.png",
                                                "png/block_orange.png",
                                                "png/block_yellow.png",
                                                "png/block_green.png",
                                                "png/block_purple.png",
                                                "png/block_red.png"};

            switch (pieceData)
            {
                case 'i': 
                    return listColors[1];
                case 'j': 
                    return listColors[2];
                case 'l': 
                    return listColors[3];
                case 'o': 
                    return listColors[4];
                case 's': 
                    return listColors[5];
                case 'z':
                    return listColors[6];
                case 't': 
                    return listColors[7];
                default:
                    return listColors[0]; // default is blank
            }
           

            
        }
    }
}
