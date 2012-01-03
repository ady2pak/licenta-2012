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
using System.Threading;

namespace xoxoClient
{
    public partial class ClientMW : Form
    {
        Encoding encoding = Encoding.UTF8;
        NetworkServices netServ;
        string _username;
        string _password;
        

        public ClientMW(NetworkServices netServ)
        {
            this.netServ = netServ;
            InitializeComponent();
        }
       
        private void sendBTN_Click(object sender, EventArgs e)
        {
            String msg = "ALL~" + msgBox.Text.Trim();
            netServ.socket.Send(this.encoding.GetBytes(msg));
            msgHst.AppendText(Environment.NewLine + ">>" + msg);
            msgBox.Text = "";
        }

        private void loginBTN_Click(object sender, EventArgs e)
        {
            _username = usernameTB.Text;
            _password = passwordTB.Text;

            netServ.socket.Send(encoding.GetBytes("ADD~" + _username));

            while (netServ.iAmConnected == false) Thread.Sleep(200);

            showChat();
        }

        private void showChat()
        {
            this.loginLBL.Visible = false;
            this.usernameLBL.Visible = false;
            this.usernameTB.Visible = false;
            this.passwordLBL.Visible = false;
            this.passwordTB.Visible = false;
            this.loginBTN.Visible = false;
            this.msgHst.Visible = true;
            this.msgBox.Visible = true;
            this.sendBTN.Visible = true;
        }

        delegate void appendTextCallback(string text);

        public void appendText(string szData)
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
