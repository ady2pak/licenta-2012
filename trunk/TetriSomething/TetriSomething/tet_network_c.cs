using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace TetriSomething
{
    public class tet_network_c
    {
        public class SocketPacket
        {
            public System.Net.Sockets.Socket thisSocket;
            public byte[] dataBuffer = new byte[1000];
        }

        public Socket m_clientSocket;
        IAsyncResult m_result;
        public AsyncCallback m_pfnCallBack;
        public bool iAmConnected;
        mainWindow mainWindow;

        public tet_network_c(mainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            m_clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress[] ipAddress = Dns.GetHostAddresses("127.0.0.1");
            IPEndPoint ipEnd = new IPEndPoint(ipAddress[0], 8221);
            m_clientSocket.Connect(ipEnd);
            if (m_clientSocket.Connected)
            {
                //clientMW._connected = true;
                mainWindow.reloadGame();
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

                char [,] objReceived;
                objReceived = (char[,])formatter.Deserialize(stream);

                parseObject(objReceived);

                WaitForData();
            }
            catch (ObjectDisposedException se)
            {
                MessageBox.Show(se.Message);
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        private void parseObject(char[,] objReceived)
        {
            //mainWindow.appendMatrixToDebug(objReceived);
            mainWindow.drawHisMatrix(mainWindow.graphicsObj2, objReceived);
        }

        public void sendMsgToClient()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();

            formatter.Serialize(stream, tet_constants.colorMatrix);

            byte[] buffer = ((MemoryStream)stream).ToArray();
            m_clientSocket.Send(buffer, buffer.Length, 0);
        }
    }
}
