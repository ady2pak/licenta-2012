using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetriSomething
{
    class tet_random
    {
        public Random randomValue;

        public tet_random()
        {
             randomValue = new Random();
        }

        public int getRandomInt(int howFar)
        {
            int returnValue = 0;
            
            returnValue = randomValue.Next(howFar);

            return returnValue;
        }
    }
}
