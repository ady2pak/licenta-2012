using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace xoxoClient
{
    public class NetworkServices
    {
        public Socket socket;
        byte[] m_DataBuffer = new byte[1000];
        public IAsyncResult asyn;
        public bool iAmConnected;
        

        public NetworkServices()
        {
            IPAddress[] ipAddress = Dns.GetHostAddresses("127.0.0.1");
            IPEndPoint ipEnd = new IPEndPoint(ipAddress[0], 8221);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ipEnd);

            iAmConnected = false;
            this.BeginReceive();
        }

        private void BeginReceive()
        {
            this.socket.BeginReceive(
                this.m_DataBuffer, 0,
                this.m_DataBuffer.Length,
                SocketFlags.None,
                new AsyncCallback(this.OnBytesReceived),
                this);
        }

        private void OnBytesReceived(IAsyncResult asyn)
        {
            int responseLenght = 0;
            try
            {
                responseLenght = socket.EndReceive(asyn);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            char[] chars = new char[responseLenght + 1];
            System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
            int charLen = d.GetChars(m_DataBuffer, 0, responseLenght, chars, 0);
            System.String serverResponse = new System.String(chars);

            decideBasedOnResponse(serverResponse);               
           
            this.socket.BeginReceive(
               this.m_DataBuffer, 0,
               this.m_DataBuffer.Length,
               SocketFlags.None,
               new AsyncCallback(this.OnBytesReceived),
               this);            
        }

        private void decideBasedOnResponse(string response)
        {

            if (response.Equals("wasAdded0x0001\0"))
            {
                iAmConnected = true;
            }    
        }

        internal bool isConnected()
        {
            if (iAmConnected) return true;
            else return false;
        }
    }
}
