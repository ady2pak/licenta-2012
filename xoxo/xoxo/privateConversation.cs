using System;
using System.Windows.Forms;

namespace xoxoChat
{
    public partial class privateConversation : Form
    {
        delegate void appendTextCallback(string text);

        public string withWho;
        NetworkServices netServ;
        ClientMW clientMW;

        public privateConversation(string withWho, ClientMW clientMW, NetworkServices netServ)
        {
            this.withWho = withWho;
            this.clientMW = clientMW;
            this.netServ = netServ;
            InitializeComponent();
            appendText("Private conversation with " + withWho + ".");
            
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

        private void sendBTN_Click(object sender, EventArgs e)
        {
            privateMessage prvMsg = new privateMessage();
            prvMsg.setWhoSent(clientMW.username);
            prvMsg.setToWho(withWho);
            prvMsg.setMessage(msgToSend.Text.Trim());

            appendText("[" + clientMW.username + "] " + prvMsg.message);

            msgToSend.Clear();

            dataTypes objToSend = new dataTypes();
            objToSend.setType(typeof(privateMessage).ToString());
            objToSend.setObject(prvMsg);

            netServ.sendObjectToServer(objToSend);
        }

        internal void disableControls()
        {
            appendText("This private conversation was closed by the other user. Please begin a new conversation if needed.");            

            sendBTN.BeginInvoke(new MethodInvoker(sendBTN.Hide));
            msgToSend.BeginInvoke(new MethodInvoker(msgToSend.Hide));

        }
    }
}
