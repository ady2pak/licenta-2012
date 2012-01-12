using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace xoxoChat
{
    public partial class privateConversation : Form
    {
        delegate void appendTextCallback(string text);

        public string withWho;
        NetworkServices netServ;

        public privateConversation(string withWho, NetworkServices netServ)
        {
            this.withWho = withWho;
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
    }
}
