using System;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace xoxoClient
{
    public partial class doLogIn : Form
    {
        string _username;
        string _password;
        NetworkServices netServ;
        Encoding encoding = Encoding.UTF8;

        public doLogIn(NetworkServices netServ)
        {
            this.netServ = netServ;
            InitializeComponent();
        }

        private void OKbtn_Click(object sender, EventArgs e)
        {
            _username = usernameTB.Text;
            _password = passwordTB.Text;

           netServ.socket.Send(encoding.GetBytes("ADD~" + _username));

           while (netServ.iAmConnected == false) Thread.Sleep(200);           
           
           Thread mainWindow = new Thread(new ThreadStart(runClient));
           mainWindow.Start();
           this.BeginInvoke(new MethodInvoker(this.Close));
        }

        void runClient()
        {
            Application.Run(new ClientMWWindow(this.netServ));
        }
    }
}
