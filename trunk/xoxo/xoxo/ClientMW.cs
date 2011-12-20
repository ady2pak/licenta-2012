using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace xoxo
{
    public partial class MainWindow : Form
    {
        Socket socket;        
        Encoding encoding = Encoding.UTF8;
        byte[] m_DataBuffer = new byte[1000];

        public MainWindow()
        {
            InitializeComponent();

            IPAddress[] ipAddress = Dns.GetHostAddresses("127.0.0.1");
            IPEndPoint ipEnd = new IPEndPoint(ipAddress[0], 8221);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ipEnd);

            this.BeginReceive();
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            String msg = "ALL~" + msgBox.Text.Trim();
            this.socket.Send(this.encoding.GetBytes(msg));
            msgHst.AppendText(">>" + msg + Environment.NewLine);
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
            int iRx = 0;
            iRx = socket.EndReceive(asyn);
            char[] chars = new char[iRx + 1];
            System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
            int charLen = d.GetChars(m_DataBuffer, 0, iRx, chars, 0);
            System.String szData = new System.String(chars);

            appendText(szData);
            
           // WaitForData(m_socWorker);

            //msgHst.AppendText(strReceived + Environment.NewLine);

            this.socket.BeginReceive(
                this.m_DataBuffer, 0,
                this.m_DataBuffer.Length,
                SocketFlags.None,
                new AsyncCallback(this.OnBytesReceived),
                this);
        }

        delegate void appendTextCallback(string text);

        private void appendText(string szData)
        {
            if (this.msgHst.InvokeRequired)
            {
                appendTextCallback d = new appendTextCallback(appendText);
                this.Invoke(d, new object[] { szData });
            }
            else
            {
                this.msgHst.AppendText(Environment.NewLine + szData);
            }
        }


        public IAsyncResult asyn { get; set; }
    }
}
