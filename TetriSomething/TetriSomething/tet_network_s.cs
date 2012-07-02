using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TetriSomething
{
    public class tet_network_s
    {
        public class SocketPacket
        {
            public Socket m_currentSocket;
            public byte[] dataBuffer = new byte[(1024 * 1024 * 4) + 100];
        }

        public mainWindow mainWindow;

        //public List<Client> clients = new List<Client>();
        public int clientsIndex = 0;

        public Socket m_socListener;
        public Socket[] m_socWorker = new Socket[10];
        int m_clientCount = 0;

        Encoding encoding = Encoding.UTF8;

        //ServerToClientBridge STCB;
        //DatabaseServices DS;

        public AsyncCallback pfnWorkerCallback;

        public tet_network_s(mainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            startListening();
            //STCB = new ServerToClientBridge(this);
            //DS = new DatabaseServices(this);

            //DS.executeQuery();
        }
        public void startListening()
        {
            try
            {
                m_socListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ipLocal = new IPEndPoint(IPAddress.Any, 8221);
                m_socListener.Bind(ipLocal);
                m_socListener.Listen(4);
                m_socListener.BeginAccept(new AsyncCallback(OnClientConnect), null);
            }
            catch (SocketException se)
            {
                //serverMW.appendDebugOutput(se.Message);
            }
        }

        public void OnClientConnect(IAsyncResult asyn)
        {
            //string userName = "placeholder"; // to be edited
            try
            {
                m_socWorker[m_clientCount] = m_socListener.EndAccept(asyn);

                //Client _newUser = new Client(m_socWorker[m_clientCount], userName);
                //clients.Add(_newUser);

                WaitForData(m_socWorker[m_clientCount]);
                ++m_clientCount;

                m_socListener.BeginAccept(new AsyncCallback(OnClientConnect), null);
            }
            catch (ObjectDisposedException)
            {
                //System.Diagnostics.Debugger.Log(0, "1", "\n OnClientConnection: Socket has been closed\n");
            }
            catch (SocketException se)
            {
                //serverMW.appendDebugOutput(se.Message + se.StackTrace);
            }
            catch (Exception ex)
            {
                //serverMW.appendDebugOutput(ex.Message + ex.StackTrace);
            }
        }
        
        public void WaitForData(Socket m_socWorker)
        {
            try
            {
                if (pfnWorkerCallback == null)
                {
                    pfnWorkerCallback = new AsyncCallback(OnDataReceived);
                }
                SocketPacket theSokPkt = new SocketPacket();
                theSokPkt.m_currentSocket = m_socWorker;
                m_socWorker.BeginReceive(theSokPkt.dataBuffer, 0,
                    theSokPkt.dataBuffer.Length,
                    SocketFlags.None,
                    pfnWorkerCallback,
                    theSokPkt);
            }
            catch (SocketException se)
            {
                //serverMW.appendDebugOutput(se.Message);
            }
        }

        public void OnDataReceived(IAsyncResult asyn)
        {
            try
            {
                SocketPacket socketData = (SocketPacket)asyn.AsyncState;

                int bytesReceived = 0;
                bytesReceived = socketData.m_currentSocket.EndReceive(asyn);
                byte[] buffer = new byte[bytesReceived + 1];

                buffer = socketData.dataBuffer;

                IFormatter formatter = new BinaryFormatter();
                Stream stream = new MemoryStream();

                stream.Write(buffer, 0, buffer.Length);
                stream.Seek(0, 0);

                //dataTypes objReceived = new dataTypes();

                char[,] objReceived = (char[,])formatter.Deserialize(stream);

                parseObject(objReceived);

                stream.Close();
                
                WaitForData(socketData.m_currentSocket);
            }
            catch (ObjectDisposedException)
            {
                //System.Diagnostics.Debugger.Log(0, "1", "\nOnDataReceived: Socket has been closed\n");
            }
            catch (SocketException se)
            {
               
            }
            catch (Exception ex)
            {
                //serverMW.appendDebugOutput(ex.Message);
            }
        }

        private void parseObject(char[,] objReceived)
        {
            //mainWindow.appendMatrixToDebug(objReceived);
            mainWindow.drawHisMatrix(mainWindow.graphicsObj2, objReceived);
                
        }

        internal void sendMsgToClient()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();

            formatter.Serialize(stream, tet_constants.colorMatrix);

            byte[] buffer = ((MemoryStream)stream).ToArray();
            m_socWorker[0].Send(buffer, buffer.Length, 0);
        }
        
    }
}

