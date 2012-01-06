using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

namespace xoxoChat
{
    public partial class ClientMW : Form
    {
        Encoding encoding = Encoding.UTF8;
        NetworkServices netServ;

        string username;

        public bool _connected = false;

        delegate void appendTextCallback(string text);
        delegate void appendUsersCallback(List<string> list);
        
        public ClientMW()
        {
            netServ = new NetworkServices(this);
            InitializeComponent();
        }
       
        private void sendBTN_Click(object sender, EventArgs e)
        {
            messageToEveryone msg = new messageToEveryone();

            msg.setMessage(msgBox.Text.Trim());
            msg.setMe(username);

            dataTypes objToSend = new dataTypes();

            objToSend.setType(typeof(messageToEveryone).ToString());
            objToSend.setObject(msg);

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();

            formatter.Serialize(stream, objToSend);
      
            byte[] buffer = ((MemoryStream)stream).ToArray();

            netServ.m_clientSocket.Send(buffer, buffer.Length, 0);

            stream.Close();

            msgBox.Clear();

        }

        private void loginBTN_Click(object sender, EventArgs e)
        {
            loginInfo myLoginInfo = new loginInfo();

            username = usernameTB.Text.Trim().ToString();
            myLoginInfo.setUsername(username);
            myLoginInfo.setPassword(passwordTB.Text.Trim().ToString());

            dataTypes objToSend = new dataTypes();

            objToSend.setType(typeof(loginInfo).ToString());
            objToSend.setObject(myLoginInfo);

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();

            formatter.Serialize(stream, objToSend);

            byte[] buffer = ((MemoryStream)stream).ToArray();

            netServ.m_clientSocket.Send(buffer, buffer.Length, 0);

            stream.Close();

            showChat();

            msgHst.Text = "Welcome to xoxoChat. You are connected as [" + username + "].";

        }

        public void showChat()
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
            this.userlist.Visible = true;
        }

        public void appendText(string msg)
        {
            if (this.msgHst.InvokeRequired)
            {
                appendTextCallback d = new appendTextCallback(appendText);
                this.Invoke(d, new object[] { msg });
            }
            else
            {
                this.msgHst.AppendText(Environment.NewLine + msg);
            }
        }

        private void msgHst_TextChanged(object sender, EventArgs e)
        {

        }

        private void ClientMW_Load(object sender, EventArgs e)
        {

        }

        public void appendUsers(System.Collections.Generic.List<string> list)
        {

            if (this.userlist.InvokeRequired)
            {
                appendUsersCallback d = new appendUsersCallback(appendUsers);
                this.Invoke(d, new object[] { null });
                this.Invoke(d, new object[] { list });
            }
            else
            {
                this.userlist.DataSource = null;
                this.userlist.DataSource = list;
            }
        }

        private void ClientMW_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit the application?", "My Application",
                MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
            }
            //else tellServerToRemoveMe();
        }

       /* private void tellServerToRemoveMe()
        {
            iQuit rageQuit = new iQuit();

            rageQuit.setUsername(username);

            dataTypes objToSend = new dataTypes();

            objToSend.setType(typeof(iQuit).ToString());
            objToSend.setObject(rageQuit);

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();

            formatter.Serialize(stream, objToSend);

            byte[] buffer = ((MemoryStream)stream).ToArray();

            netServ.m_clientSocket.Send(buffer, buffer.Length, 0);

            stream.Close();
        }*/
    }
}
