using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetriSomething
{
    public class tet_colors
    {
        /// <summary>
        /// this returns a path to a random color
        /// </summary>
        /// <returns>a string that is a path to a random color</returns>
        public string getPieceColor(string pieceData)
        {

             string[] listColors = new string[8] {"png/block_white.png",
                                                "png/block_green.png" ,
                                                "png/block_blue.png",
                                                "png/block_grey.png",
                                                "png/block_orange.png",
                                                "png/block_pink.png",
                                                "png/block_red.png",
                                                "png/block_yellow.png"};

            switch (pieceData)
            {
                case "B": 
                    return listColors[0];
                case "I": 
                    return listColors[1];
                case "J": 
                    return listColors[2];
                case "L": 
                    return listColors[3];
                case "O": 
                    return listColors[4];
                case "S": 
                    return listColors[5];
                case "Z":
                    return listColors[6];
                case "T": 
                    return listColors[7];
                default:
                    return listColors[0]; // default is blank
            }
           

            
        }
    }
}
