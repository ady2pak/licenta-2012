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
        NetworkServices netServ;
        Socket socket;
        Encoding encoding = Encoding.UTF8; 

        public ServerToClientBridge(NetworkServices netServ)
        {
            this.netServ = netServ;
        }        

        public void decideAction(string code, string msg)
        {
            if (code.Equals("ADD")) addNewUser(msg);
            if (code.Equals("ALL")) sendMsgToAllClients(msg);
        }

        private void addNewUser(string msg)
        {
            netServ.clients[netServ.clients.Count - 1].setName(msg);
        }

        private void sendMsgToAllClients(string msg)
        {
            try
            {
                for (int index = 0; index < netServ.clients.Count; index++)
                    sendMsgToSpecificClient(netServ.clients[index].getSocket(), "ALL~" + msg);
            }
            catch (Exception ex)
            {
                netServ.serverMW.appendDebugOutput(ex.Message);
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
                
                netServ.serverMW.appendDebugOutput(ex.ToString());
            }
        }
       
    }
}
