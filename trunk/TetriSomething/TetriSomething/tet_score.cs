using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetriSomething
{
    public class tet_score
    {
        private long Score = 0;        
        private long scoreMultiplier = 1;

        const int MAXMULTIPLIER = 16;

        public long getScore()
        {
            return Score;
        }

        public long getScoreMultiplier()
        {
            return scoreMultiplier;
        }               

        public void addNonScoringMove()
        {
            scoreMultiplier = 1;                      
        }

        public void addScoringMove(int clearedLines)
        {
            Score += ((clearedLines * 100 * 2) - 100);
            
            scoreMultiplier += clearedLines;

            if (scoreMultiplier > MAXMULTIPLIER) scoreMultiplier = MAXMULTIPLIER;
        }
 
        public void resetScore()
        {
            Score = 0;
            scoreMultiplier = 1;
        }
    }
}
