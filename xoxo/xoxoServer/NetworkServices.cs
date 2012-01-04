using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace xoxoServer
{
    class NetworkServices
    {
        public ServerMW serverMW;

        public List<Client> clients = new List<Client>();        
        public int clientsIndex = 0;

        public Socket m_socListener;
        public Socket[] m_socWorker = new Socket[10];
        int m_clientCount = 0;
        
        Encoding encoding = Encoding.UTF8;
        
        ServerToClientBridge STCB;
        DatabaseServices DS;

        public AsyncCallback pfnWorkerCallback;
        
        public NetworkServices(ServerMW windowForm)
        {
            this.serverMW = windowForm;
            startListening();
            STCB = new ServerToClientBridge(this);
            DS = new DatabaseServices(this);

            DS.executeQuery();

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
                serverMW.appendDebugOutput(se.Message);
            }
        }


        public void OnClientConnect(IAsyncResult asyn)
        {
            string userName = "placeholder"; // to be edited
            try
            {
                m_socWorker[m_clientCount] = m_socListener.EndAccept(asyn);

                Client _newUser = new Client(m_socWorker[m_clientCount], userName);
                clients.Add(_newUser);
                WaitForData(m_socWorker[m_clientCount]);
                ++m_clientCount;

                m_socListener.BeginAccept(new AsyncCallback(OnClientConnect), null);
            }
            catch (ObjectDisposedException)
            {
                System.Diagnostics.Debugger.Log(0, "1", "\n OnClientConnection: Socket has been closed\n");
            }
            catch (SocketException se)
            {
                serverMW.appendDebugOutput(se.Message + se.StackTrace);
            }
            catch (Exception ex)
            {
                serverMW.appendDebugOutput(ex.Message + ex.StackTrace);
            }
        }

        public class SocketPacket
        {
            public Socket m_currentSocket;
            public byte[] dataBuffer = new byte[100];
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
                serverMW.appendDebugOutput(se.Message);
            }
        }

        public void OnDataReceived(IAsyncResult asyn)
        {
            try
            {
                SocketPacket socketData = (SocketPacket)asyn.AsyncState;

                int iRx = 0;
                iRx = socketData.m_currentSocket.EndReceive(asyn);
                char[] chars = new char[iRx + 1];
                System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
                int charLen = d.GetChars(socketData.dataBuffer, 0, iRx, chars, 0);
                System.String clientResponse = new System.String(chars);

                serverMW.appendDebugOutput(clientResponse);

                STCB.decideAction(clientResponse.Substring(0, 3), clientResponse.Substring(clientResponse.IndexOf("~") + 1, clientResponse.Length - 4));
                
                WaitForData(socketData.m_currentSocket);
            }
            catch (ObjectDisposedException)
            {
                System.Diagnostics.Debugger.Log(0, "1", "\nOnDataReceived: Socket has been closed\n");
            }
            catch (SocketException se)
            {
                serverMW.appendDebugOutput(se.Message);
            }
        }

        public Socket getUserSocketByName(string username)
        {
            for (int index = 0 ; index < clients.Count ; index++)
            {
                if (clients[index].getUserName().Equals(username))
                {
                    return clients[index].getSocket();
                }
            }
            return null;
        }
    }
}
