using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace xoxoServer
{
    class Server
    {

        Socket serverSocket;
        Socket socket;
        ServerToClientBridge stcb;
        public ServerMW mw;
        public List<Client> clients = new List<Client>();
        public int clientsIndex = 0;

        public Socket m_socListener;
        ServerMW windowForm;
        public Socket m_socWorker;
        Encoding encoding = Encoding.UTF8;
        byte[] m_DataBuffer = new byte[1000];
        ServerToClientBridge STCB;

        public Server(ServerMW windowForm)
        {
            this.windowForm = windowForm;
            startListening();
            STCB = new ServerToClientBridge(this);

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
                windowForm.appendDebugOutput(se.Message);
            }
        }


        public void OnClientConnect(IAsyncResult asyn)
        {
            string userName = "placeholder"; // to be edited
            try
            {
                m_socWorker = m_socListener.EndAccept(asyn);
                Client _newUser = new Client(m_socWorker, userName);
                clients.Add(_newUser);
                WaitForData(m_socWorker);
                m_socListener.BeginAccept(new AsyncCallback(OnClientConnect), null);
            }
            catch (ObjectDisposedException)
            {
                System.Diagnostics.Debugger.Log(0, "1", "\n OnClientConnection: Socket has been closed\n");
            }
            catch (SocketException se)
            {
                windowForm.appendDebugOutput(se.Message);
            }
            catch (Exception ex)
            {
                windowForm.appendDebugOutput(ex.Message);
            }
        }

        public void WaitForData(Socket m_socWorker)
        {
            this.m_socWorker.BeginReceive(
                this.m_DataBuffer, 0,
                this.m_DataBuffer.Length,
                SocketFlags.None,
                new AsyncCallback(this.OnDataReceived),
                this); 
        }

        public void OnDataReceived(IAsyncResult asyn)
        {
            //end receive...
            int iRx = 0;
            iRx = m_socWorker.EndReceive(asyn);
            char[] chars = new char[iRx + 1];
            System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
            int charLen = d.GetChars(m_DataBuffer, 0, iRx, chars, 0);
            System.String szData = new System.String(chars);
            windowForm.appendDebugOutput(szData);

            STCB.decideAction(szData.Substring(0, 3), szData.Substring(szData.IndexOf("~") + 1, szData.Length - 4));

            //this.m_socWorker.Send(this.encoding.GetBytes(szData));
            WaitForData(m_socWorker);
        }

        public AsyncCallback pfnCallBack { get; set; }
        IAsyncResult m_asynResult;

        
    }
}
