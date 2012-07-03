using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetriSomething
{
    public class tet_colors
    {
        public const string WHITE = "png/block_white.png";
        public const string CYAN = "png/block_cyan.png";
        public const string BLUE = "png/block_blue.png";
        public const string ORANGE = "png/block_orange.png";
        public const string YELLOW = "png/block_yellow.png";
        public const string GREEN = "png/block_green.png";
        public const string PURPLE = "png/block_purple.png";
        public const string RED = "png/block_red.png";
        public const string STAR = "png/block_star.png";
        public const string BOMB = "png/block_bomb.png";

        public void initColorMatrix()
        {
            for (int row = 0; row < 20; row++)
                for (int column = 0; column < 10; column++)
                {
                    tet_constants.colorMatrix[row, column] = 'w';
                    tet_constants.hisColorMatrix[row, column] = 'w';
                }
        }

        public string getPieceColor(char pieceData)
        {
            //string[] shapes = { "I", "J", "L", "O", "S", "Z", "T" };  
            string[] listColors = new string[10] {"png/block_white.png",
                                                "png/block_cyan.png" ,
                                                "png/block_blue.png",
                                                "png/block_orange.png",
                                                "png/block_yellow.png",
                                                "png/block_green.png",
                                                "png/block_purple.png",
                                                "png/block_red.png",
                                                "png/block_star.png",
                                                "png/block_bomb.png"};

            int powerUpChance = 0;
            string returnStringColor = "";
            tet_random randomPowerup = new tet_random();
            powerUpChance = randomPowerup.getRandomInt(tet_constants.CHANCE_POWERUP_STAR);

            switch (pieceData)
            {
                case 'i':
                    {
                        returnStringColor = listColors[1];
                        return returnStringColor;
                    }
                case 'j':
                    {
                        returnStringColor = listColors[2];
                        return returnStringColor;
                    }
                case 'l':
                    {
                        returnStringColor = listColors[3];
                        return returnStringColor;
                    }
                case 'o':
                    {
                       returnStringColor = listColors[4];
                        return returnStringColor;
                    }
                case 's':
                    {
                        returnStringColor = listColors[5];
                        return returnStringColor;
                    }
                case 'z':
                    {
                        returnStringColor = listColors[6];
                        return returnStringColor;
                    }
                case 't':
                    {
                        returnStringColor = listColors[7];
                        return returnStringColor;
                    }
                case 'p':
                    {
                        returnStringColor = listColors[8];
                        return returnStringColor;
                    }
                case 'b':
                    {
                        returnStringColor = listColors[9];
                        return returnStringColor;
                    }
                default:
                    {
                        returnStringColor = listColors[0];
                        return returnStringColor;
                    } // default is blank
            }
           

            
        }
    }
}
