using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace xoxoChat
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

        private void addNewUser(string msg)
        {
            netServ.clients[netServ.clients.Count - 1].setName(msg);
        }

        public void sendMsgToAllClients(messageToEveryone msg)
        {
            try
            {
                dataTypes objToSend = new dataTypes();

                objToSend.setType(typeof(messageToEveryone).ToString());
                objToSend.setObject(msg);

                IFormatter formatter = new BinaryFormatter();
                Stream stream = new MemoryStream();

                formatter.Serialize(stream, objToSend);
      
                byte[] buffer = ((MemoryStream)stream).ToArray();

                for (int index = 0; index < netServ.clients.Count; index++)
                    netServ.clients[index].getSocket().Send(buffer, buffer.Length, 0);

                stream.Close();
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
