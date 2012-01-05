using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace xoxoChat
{
    class Client
    {
        Socket socket;
        String UserName;        

        public Client(Socket socket, string UserName)
        {
            this.socket = socket;
            this.UserName = UserName;
        }       

        public Socket getSocket()
        {
            return socket;
        }

        public void setName(string msg)
        {
            this.UserName = msg;
        }

        public string getUserName()
        {
            return UserName;
        }
    }
}
