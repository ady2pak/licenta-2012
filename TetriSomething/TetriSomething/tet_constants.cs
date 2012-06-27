﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetriSomething
{
    public class tet_constants
    {
        public const int STATE_TITLE =    00001;
        public const int STATE_PLAY =     00002;
        public const int STATE_OPTIONS =  00003;
        public const int STATE_CONTROLS = 00004;
        public const int STATE_HELP =     00005;
        public const int STATE_SETTINGS = 00006;
        public const int STATE_PLAYING =  00007;
        
        private int gameState = 00001;
        
        public void setState(int state)
        {
            gameState = state;
        }

        public int getState()
        {
            return gameState;
        }
    }

}
