using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace xoxoClient
{
    public class NetworkServices
    {
        public Socket m_clientSocket;
        //byte[] m_DataBuffer = new byte[1000];
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
                SocketPacket theSockId = (SocketPacket)asyn.AsyncState;
                int iRx = theSockId.thisSocket.EndReceive(asyn);
                char[] chars = new char[iRx + 1];
                System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
                int charLen = d.GetChars(theSockId.dataBuffer, 0, iRx, chars, 0);
                System.String serverResponse = new System.String(chars);
                
                decideBasedOnResponse(serverResponse);

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

        void decideBasedOnResponse(string response)
        {                       
                if (response.Substring(0,3).Equals("ALL")) clientMW.appendText(response.Substring(4));
        }
    }
}
