using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetriSomething
{
    public class tet_score
    {
        private long Score = 0;
        private string LastAction = "NonScoring";

        public long getScore()
        {
            return Score;
        }

        private long processLastAction()
        {
            long scoreMultiplier = 0;
            switch (LastAction)
            {
                case "NonScoring": scoreMultiplier = 1; break;
                case "Single": scoreMultiplier = 2; break;
                case "Double": scoreMultiplier = 4; break;
                case "Triple": scoreMultiplier = 8; break;
                case "Tetris": scoreMultiplier = 16; break;
                default: scoreMultiplier = 1; break;
            }
            return scoreMultiplier;
        }

        public void addNonScoringMove()
        {
            LastAction = "NonScoring";
        }

        /// <summary>
        /// adds set score to the total score
        /// </summary>
        /// <param name="action"></param>
        public void addScoringMove(string action)
        {
            switch (action)
            {
                case "Single":
                    {
                        Score = Score + (100 * processLastAction());
                        LastAction = "Single";
                    };break;
                case "Double":
                                       {
                        Score = Score + (300 * processLastAction());
                        LastAction = "Double";
                    };break;
                case "Tripple":
                                        {
                        Score = Score + (500 * processLastAction());
                        LastAction = "Triple";
                    };break;
                case "Tetris":
                                       {
                        Score = Score + (800 * processLastAction());
                        LastAction = "Tetris";
                    };break;
                default:
                    { ;}; break;
            }
        }

        /// <summary>
        ///  ***
        /// </summary>
        public void resetScore()
        {
            Score = 0;
        }
    }
}
