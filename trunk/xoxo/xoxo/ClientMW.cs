using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading;

namespace xoxoChat
{
    public partial class ClientMW : Form
    {
        Encoding encoding = Encoding.UTF8;
        NetworkServices netServ;
        fileTransferProtocol fTP;
        List<privateConversation> prvConversations;

        public string username;

        public bool _connected = false;

        delegate void appendTextCallback(string text);
        delegate void appendUsersCallback(List<string> list);
        
        public ClientMW()
        {
            netServ = new NetworkServices(this);
            fTP = new fileTransferProtocol(this);
            prvConversations = new List<privateConversation>();
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
            this.uploadFileBTN.Visible = true;
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
       
        private string SelectFile()
        {
            OpenFileDialog fbd = new OpenFileDialog();
            if (fbd.ShowDialog() != DialogResult.OK)
            {
                MessageBox.Show("No file selected");
                return "";
            }
            else
                return fbd.FileName;
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

        private void selectBTN_Click(object sender, EventArgs e)
        {
            fTP.OpenFile();
        }

        private void uploadFileBTN_Click(object sender, EventArgs e)
        {
            fTP.SendFile();
        }

        private void userlist_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                appendText("Mouse Click" + e.X + "." + e.Y);
                //select the item under the mouse pointer
                userlist.SelectedIndex = userlist.IndexFromPoint(e.Location);
                if (userlist.SelectedIndex != -1)
                {
                    userlistContMenu.Show();
                }
            }
        }

        private void userlistContMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (userlist.SelectedItem != null)
            {
                userlistContMenu.Items[0].Text = userlist.SelectedItem.ToString();
            }
            else e.Cancel = true;
        }

        private void prvConv_Click(object sender, EventArgs e)
        {
            startPrivateConversation(userlistContMenu.Items[0].Text);
        }

        public void startPrivateConversation(string withWho)
        {
            appendText(withWho);

            if (notAlreadyOpen(withWho))
            {
                dataTypes objToSend = new dataTypes();
                objToSend.setType(typeof(startPrivate).ToString());

                startPrivate startPrv = new startPrivate();
                startPrv.setWhoStarts(username);
                startPrv.setWithWho(withWho);

                objToSend.setObject(startPrv);

                netServ.sendObjectToServer(objToSend);

                try
                {
                    Thread sf = new Thread(showForm);
                    sf.Start(withWho);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else MessageBox.Show("Already in a private conversation with " + withWho + ".");

        }

        private bool notAlreadyOpen(string withWho)
        {
           for (int index = 0 ; index < prvConversations.Count ; index++)
               if (prvConversations[index].withWho.Equals(withWho))
                   return false;
           return true;
        }

        public void startPrivateConversationByServer(string withWho)
        {
            appendText(withWho);            
            Thread sf= new Thread(showForm);
            sf.Start(withWho);
        }

        private void showForm(object data)
        {
            privateConversation thisConversation = new privateConversation((string)data, this, netServ);
            prvConversations.Add(thisConversation);

            thisConversation.FormClosed += new FormClosedEventHandler(prvConvWindow_FormClosed);
            thisConversation.Text = "PRV: " + (string)data;
            thisConversation.ShowDialog();
        }

        privateConversation getWindowByUser(string withWho)
        {
            for (int index = 0; index < prvConversations.Count; index++ )
            {
                if (prvConversations[index].withWho == withWho)
                    return prvConversations[index];
            }
            return null;
        }

        private void prvConvWindow_FormClosed(object sender, FormClosedEventArgs e)
        {

            //TODO : filter send to server by ifremoved from list<>
            privateConversation _temp = (privateConversation)sender;
            _temp.Dispose();
            prvConversations.Remove(_temp);

            closePrivate closePrv = new closePrivate();
            closePrv.setWhoSent(username);
            closePrv.setToWho(_temp.withWho);

            dataTypes objectToSend = new dataTypes();
            objectToSend.setType(typeof(closePrivate).ToString());
            objectToSend.setObject(closePrv);

            netServ.sendObjectToServer(objectToSend);
        }

        internal void pushPrivateToWindow(privateMessage prvMsg)
        {
            privateConversation thisConv = getWindowByUser(prvMsg.whoSent);

            thisConv.appendText("[" + prvMsg.whoSent + "] " + prvMsg.message);
        }

        internal void closePrivate(closePrivate closePrv)
        {
            privateConversation thisConversation = getWindowByUser(closePrv.whoSent);
            thisConversation.disableControls();
            prvConversations.Remove(thisConversation);

        }
    }
}
