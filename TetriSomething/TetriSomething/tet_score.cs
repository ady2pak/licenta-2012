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
        private long scoreMultiplierGlobal = 0;

        public long getScore()
        {
            return Score;
        }

        public long getScoreMultiplier()
        {
            return scoreMultiplierGlobal;
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
            scoreMultiplierGlobal = scoreMultiplier;
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
        public void addScoringMove(int clearedLines)
        {
            switch (clearedLines)
            {
                case 1:
                    {
                        Score = Score + (100 * processLastAction());
                        LastAction = "Single";
                    };break;
                case 2:
                                       {
                        Score = Score + (300 * processLastAction());
                        LastAction = "Double";
                    };break;
                case 3:
                                        {
                        Score = Score + (500 * processLastAction());
                        LastAction = "Triple";
                    };break;
                case 4:
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
