namespace xoxoChat
{
    partial class privateConversation
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.msgHst = new System.Windows.Forms.TextBox();
            this.msgToSend = new System.Windows.Forms.TextBox();
            this.sendBTN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // msgHst
            // 
            this.msgHst.Location = new System.Drawing.Point(13, 11);
            this.msgHst.Multiline = true;
            this.msgHst.Name = "msgHst";
            this.msgHst.Size = new System.Drawing.Size(279, 488);
            this.msgHst.TabIndex = 0;
            // 
            // msgToSend
            // 
            this.msgToSend.Location = new System.Drawing.Point(13, 506);
            this.msgToSend.Name = "msgToSend";
            this.msgToSend.Size = new System.Drawing.Size(279, 20);
            this.msgToSend.TabIndex = 1;
            // 
            // sendBTN
            // 
            this.sendBTN.Location = new System.Drawing.Point(216, 533);
            this.sendBTN.Name = "sendBTN";
            this.sendBTN.Size = new System.Drawing.Size(75, 23);
            this.sendBTN.TabIndex = 2;
            this.sendBTN.Text = "Send";
            this.sendBTN.UseVisualStyleBackColor = true;
            this.sendBTN.Click += new System.EventHandler(this.sendBTN_Click);
            // 
            // privateConversation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 567);
            this.Controls.Add(this.sendBTN);
            this.Controls.Add(this.msgToSend);
            this.Controls.Add(this.msgHst);
            this.Name = "privateConversation";
            this.Text = "privateConversation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox msgHst;
        private System.Windows.Forms.TextBox msgToSend;
        private System.Windows.Forms.Button sendBTN;
    }
}