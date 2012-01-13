using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace xoxoChat
{
    public class NetworkServices
    {
        public Socket m_clientSocket;
        IAsyncResult m_result;
        public AsyncCallback m_pfnCallBack;
        public bool iAmConnected;
        ClientMW clientMW;

        public class SocketPacket
        {
            public System.Net.Sockets.Socket thisSocket;
            public byte[] dataBuffer = new byte[1000];
        }

        public NetworkServices(ClientMW clientMW)
        {
            this.clientMW = clientMW;
            m_clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress[] ipAddress = Dns.GetHostAddresses("127.0.0.1");
            IPEndPoint ipEnd = new IPEndPoint(ipAddress[0], 8221);
            m_clientSocket.Connect(ipEnd);
            if (m_clientSocket.Connected)
            {
                clientMW._connected = true;
                WaitForData();
            }
        }

        private void WaitForData()
        {
            try
            {
                if (m_pfnCallBack == null)
                {
                    m_pfnCallBack = new AsyncCallback(OnDataReceived);
                }
                SocketPacket theSocPkt = new SocketPacket();
                theSocPkt.thisSocket = m_clientSocket;
                m_result = m_clientSocket.BeginReceive(theSocPkt.dataBuffer, 0, theSocPkt.dataBuffer.Length, SocketFlags.None, m_pfnCallBack, theSocPkt);
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        private void OnDataReceived(IAsyncResult asyn)
        {
            try
            {
                SocketPacket socketData = (SocketPacket)asyn.AsyncState;

                int bytesReceived = 0;
                bytesReceived = socketData.thisSocket.EndReceive(asyn);
                byte[] buffer = new byte[bytesReceived + 1];

                buffer = socketData.dataBuffer;

                IFormatter formatter = new BinaryFormatter();
                Stream stream = new MemoryStream();

                stream.Write(buffer, 0, buffer.Length);
                stream.Seek(0, 0);

                dataTypes objReceived = new dataTypes();

                objReceived = (dataTypes)formatter.Deserialize(stream);

                parseObject(objReceived);

                WaitForData();
            }
            catch (ObjectDisposedException)
            {
                System.Diagnostics.Debugger.Log(0, "1", "\nOnDataReceived: Socket has been closed\n");
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }          
        }

        private void parseObject(dataTypes objReceived)
        {
            if (objReceived.objectType.Equals(typeof(messageToEveryone).ToString()))
            {
                messageToEveryone msg = (messageToEveryone)objReceived.myObject;
                clientMW.appendText("["+ msg.whoAmI + "] " + msg.message);
            }
            else if (objReceived.objectType.Equals(typeof(userList).ToString()))
            {
                userList onlineClients = (userList)objReceived.myObject;
                clientMW.appendUsers(onlineClients.users);
            }
            else if (objReceived.objectType.Equals(typeof(startPrivate).ToString()))
            {
                startPrivate startPRV = (startPrivate)objReceived.myObject;
                clientMW.startPrivateConversationByServer(startPRV.whoStarts);
            }
            else if (objReceived.objectType.Equals(typeof(privateMessage).ToString()))
            {
                privateMessage prvMsg = (privateMessage)objReceived.myObject;
                clientMW.pushPrivateToWindow(prvMsg);
            }
            else if (objReceived.objectType.Equals(typeof(closePrivate).ToString()))
            {
                closePrivate closePrv = (closePrivate)objReceived.myObject;
                clientMW.closePrivate(closePrv);
            }
            else throw new Exception("Unsupported object type");
        }

        public void sendObjectToServer(dataTypes objToSend)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();

            formatter.Serialize(stream, objToSend);

            byte[] buffer = ((MemoryStream)stream).ToArray();
            
            m_clientSocket.Send(buffer, buffer.Length, 0);

            stream.Close();
        }        
    }
}
