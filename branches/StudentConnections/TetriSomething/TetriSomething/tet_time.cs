using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetriSomething
{
    class tet_time
    {
        private DateTime currentTime;
        private DateTime lastTime = DateTime.MinValue;

        private DateTime readCurrentTime()
        {
            DateTime bufferTime;
            bufferTime = DateTime.Now;

            return bufferTime;
        }

        public void setCurrentTime()
        {
            lastTime = currentTime;
            currentTime = readCurrentTime();
        }

        public DateTime getCurrentTime()
        {
            readCurrentTime();
            return currentTime;
        }

        public DateTime getLastTime()
        {
            return lastTime;
        }
        
       

    }
}
