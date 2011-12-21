using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;

namespace xoxoClient
{
    public partial class doLogIn : Form
    {
        string _username;
        string _password;
        Socket socket;
        NetworkServices NS;
        Encoding encoding = Encoding.UTF8;

        public doLogIn()
        {
            InitializeComponent();
             NS = new NetworkServices(this);

        }

        private void OKbtn_Click(object sender, EventArgs e)
        {
            _username = usernameTB.Text;
            _password = passwordTB.Text;

            NS.socket.Send(encoding.GetBytes("ADD~" + _username));

            


        }

        public void closeThisForm()
        {
            this.Close();
        }
    }
}
