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

namespace xoxoClient
{
    public partial class ClientMW : Form
    {

        Socket socket;
        Encoding encoding = Encoding.UTF8;
        NetworkServices netServ;
        doLogIn dlg;

        public ClientMW(NetworkServices netServ)
        {
            this.netServ = netServ;
            InitializeComponent();
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            String msg = "ALL~" + msgBox.Text.Trim();
            socket.Send(this.encoding.GetBytes(msg));
            msgHst.AppendText(Environment.NewLine + ">>" + msg);
            msgBox.Text = "";
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
    }
}
