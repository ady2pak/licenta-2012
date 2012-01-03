using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace xoxoServer
{
    class ServerToClientBridge
    {
        Server server;
        Socket socket;
        Encoding encoding = Encoding.UTF8; 

        public ServerToClientBridge(Server server)
        {
            this.server = server;
            //this.socket = socket;
            
        }        

        public void decideAction(string code, string msg)
        {
            if (code.Equals("ADD")) addNewUser(msg);
            if (code.Equals("ALL")) sendMsgToAllClients(msg);
            //sendMsgToSpecificClient(this.socket, msg);
        }

        private void addNewUser(string msg)
        {
            server.clients[server.clients.Count - 1].setName(msg);

            string added = "wasAdded0x0001";
            Socket _thisUser = server.getUserSocketByName(msg);
            _thisUser.Send(encoding.GetBytes(added));
        }

        private void sendMsgToAllClients(string msg)
        {
            try
            {
                for (int index = 0; index < server.clients.Count; index++)
                    sendMsgToSpecificClient(server.clients[index].getSocket(), "ALL~" + msg);
            }
            catch (Exception ex)
            {
                server.windowForm.appendDebugOutput(ex.Message);
            }
        }

        private void sendMsgToSpecificClient(Socket socket, string msg)
        {
            try
            {
                socket.Send(encoding.GetBytes(msg));
            }
            catch (Exception ex)
            {
                
                server.windowForm.appendDebugOutput(ex.ToString());
            }
        }
       
    }
}
