using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace xoxoServer
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
    }
}
