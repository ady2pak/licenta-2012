using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

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
            catch (Exception ex)
            {
                MessageBox.Show("Something bad happened : " + ex.Message, "Battle Tetrix", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                Application.Restart();
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

                mainWindow.reloadGame();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something bad happened : " + ex.Message, "Battle Tetrix", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                Application.Restart();
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
            catch (Exception ex)
            {
                MessageBox.Show("Something bad happened : " + ex.Message, "Battle Tetrix", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                Application.Restart();
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

                tet_network_object objReceived = (tet_network_object)formatter.Deserialize(stream);

                parseObject(objReceived);

                stream.Close();
                
                WaitForData(socketData.m_currentSocket);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something bad happened : " + ex.Message, "Battle Tetrix", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                Application.Restart();
            }
        }

        private void parseObject(tet_network_object objReceived)
        {

            if (mainWindow.blockLogic.oldReceivedObject != null)
            {
                if (objReceived.enemyScore != mainWindow.blockLogic.oldReceivedObject.enemyScore)
                    mainWindow.drawHisScore(mainWindow.hisGraphics, objReceived.enemyScore);
                if (objReceived.enemyNextShape != mainWindow.blockLogic.oldReceivedObject.enemyNextShape)
                    mainWindow.drawHisNexShape(mainWindow.hisGraphics, objReceived.enemyNextShape);
                mainWindow.drawHisMatrix(mainWindow.hisGraphics, objReceived.enemyColorMatrix);
            }
            else
            {
                mainWindow.drawHisMatrix(mainWindow.hisGraphics, objReceived.enemyColorMatrix);
                mainWindow.drawHisScore(mainWindow.hisGraphics, objReceived.enemyScore);
                mainWindow.drawHisNexShape(mainWindow.hisGraphics, objReceived.enemyNextShape);
            }

            mainWindow.blockLogic.oldReceivedObject = objReceived;
                
        }

        internal void sendMsgToClient(tet_network_object objToSend)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();

            formatter.Serialize(stream, objToSend);

            byte[] buffer = ((MemoryStream)stream).ToArray();
            m_socWorker[0].Send(buffer, buffer.Length, 0);
        }
        
    }
}

