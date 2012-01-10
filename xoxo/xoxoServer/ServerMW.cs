using System;
using System.Windows.Forms;

namespace xoxoChat
{
    public partial class ServerMW : Form
    {
        delegate void appendDebugOutputCallback(string text);

        public ServerMW()
        {
            InitializeComponent();
            
            new NetworkServices(this);
            new fileTransferProtocol(this);
            
            debugOutput.AppendText("Server Started.");
        }
        
        public void appendDebugOutput(string text) {
            if (this.debugOutput.InvokeRequired)
            {
                appendDebugOutputCallback d = new appendDebugOutputCallback(appendDebugOutput);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.debugOutput.AppendText(Environment.NewLine + text);
            }
        }

        private void ServerMW_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }               
    }
}
