using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace xoxoChat
{
    class ServerToClientBridge
    {
        NetworkServices netServ;        
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

        internal void sendMsgToClient(string toWho, Object objectToSend)
        {
             //dataTypes objToSend = new dataTypes();

             //objToSend.setType(typeof(userList).ToString());
             //objToSend.setObject(objectToSend);

             IFormatter formatter = new BinaryFormatter();
             Stream stream = new MemoryStream();

             formatter.Serialize(stream, objectToSend);
      
             byte[] buffer = ((MemoryStream)stream).ToArray();
             netServ.getUserSocketByName(toWho).Send(buffer, buffer.Length, 0);
        }

        internal void sendUserlistToClients(userList connectedUsers)
        {
            dataTypes objToSend = new dataTypes();

            objToSend.setType(typeof(userList).ToString());
            objToSend.setObject(connectedUsers);

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();

            formatter.Serialize(stream, objToSend);

            byte[] buffer = ((MemoryStream)stream).ToArray();

            for (int index = 0; index < netServ.clients.Count; index++)
                netServ.clients[index].getSocket().Send(buffer, buffer.Length, 0);

            stream.Close();
        }
    }
}
