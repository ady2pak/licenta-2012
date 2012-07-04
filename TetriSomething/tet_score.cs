using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetriSomething
{
    public class tet_score
    {
        private long Score = 0;        //game score
        private long scoreMultiplier = 1; //current mu;tip[lier
        private long StarsScored = 0; 

        const int MAXMULTIPLIER = 16; //the maximum ammount of multiplier you can reach.

        /// <summary>
        /// gets the number of stars that scored
        /// </summary>
        /// <returns></returns>
        public long getStarsScored()
        {
            return StarsScored;
        }

        /// <summary>
        /// use this to get the current score
        /// </summary>
        /// <returns>a long score</returns>
        public long getScore()
        {
            return Score;
        }

        /// <summary>
        /// Use this to read the score multiplier
        /// </summary>
        /// <returns></returns>
        public long getScoreMultiplier()
        {
            return scoreMultiplier;
        }

        /// <summary>
        /// gives you some score for insta drop
        /// </summary>
        /// <param name="currentRow">from where</param>
        public void addInstaDropBonus(int currentRow)
        {
            Score = Score + 2*currentRow;
        }

        public void addStarBonus()
        {
            Score = Score + 50;
            scoreMultiplier = scoreMultiplier + 1;
        }

        /// <summary>
        /// Signals a non scoring move was made
        /// </summary>
        public void addNonScoringMove()
        {
            scoreMultiplier = 1;                      
        }

        /// <summary>
        /// Signals a scoring move was made and returns the number of lines cleared
        /// </summary>
        /// <param name="clearedLines">an int , the number of lines cleared</param>
        public void addScoringMove(int clearedLines)
        {
            Score += ((clearedLines * 100 * 2) - 100);
            
            scoreMultiplier += clearedLines;

            if (scoreMultiplier > MAXMULTIPLIER) scoreMultiplier = MAXMULTIPLIER;
        }
 
        /// <summary>
        /// resets the score
        /// </summary>
        public void resetScore()
        {
            Score = 0;
            scoreMultiplier = 1;
        }
    }
}
