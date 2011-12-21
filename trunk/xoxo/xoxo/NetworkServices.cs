using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;

namespace xoxoClient
{
    public class NetworkServices
    {
        public Socket socket;
        byte[] m_DataBuffer = new byte[1000];
        public IAsyncResult asyn;        
        bool notAdded = false;

        public NetworkServices()
        {
            IPAddress[] ipAddress = Dns.GetHostAddresses("127.0.0.1");
            IPEndPoint ipEnd = new IPEndPoint(ipAddress[0], 8221);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ipEnd);

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
            responseLenght = socket.EndReceive(asyn);
            char[] chars = new char[responseLenght + 1];
            System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
            int charLen = d.GetChars(m_DataBuffer, 0, responseLenght, chars, 0);
            System.String serverResponse = new System.String(chars);

            Console.Write(serverResponse);
            string isOK = "wasAdded0x0001";

            
            if (serverResponse.Equals(isOK) //&& !notAdded)
            {
                Application.Run(new ClientMW(this));                
                notAdded = true;
                Console.Write("dafuq");
            }

            //else decideBasedOnResponse(serverResponse);
                
            
           
            this.socket.BeginReceive(
               this.m_DataBuffer, 0,
               this.m_DataBuffer.Length,
               SocketFlags.None,
               new AsyncCallback(this.OnBytesReceived),
               this);            
        }

        private void decideBasedOnResponse(string szData)
        {
            throw new NotImplementedException();
        }
    }
}
