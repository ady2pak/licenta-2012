namespace xoxoChat
{
    partial class ClientMW
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
            this.msgBox = new System.Windows.Forms.TextBox();
            this.sendBTN = new System.Windows.Forms.Button();
            this.loginBTN = new System.Windows.Forms.Button();
            this.loginLBL = new System.Windows.Forms.Label();
            this.passwordLBL = new System.Windows.Forms.Label();
            this.usernameLBL = new System.Windows.Forms.Label();
            this.passwordTB = new System.Windows.Forms.TextBox();
            this.usernameTB = new System.Windows.Forms.TextBox();
            this.userlist = new System.Windows.Forms.ListBox();
            this.uploadFileBTN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // msgHst
            // 
            this.msgHst.Enabled = false;
            this.msgHst.Location = new System.Drawing.Point(34, 23);
            this.msgHst.Multiline = true;
            this.msgHst.Name = "msgHst";
            this.msgHst.Size = new System.Drawing.Size(291, 380);
            this.msgHst.TabIndex = 9;
            this.msgHst.Visible = false;
            this.msgHst.TextChanged += new System.EventHandler(this.msgHst_TextChanged);
            // 
            // msgBox
            // 
            this.msgBox.Location = new System.Drawing.Point(33, 409);
            this.msgBox.Name = "msgBox";
            this.msgBox.Size = new System.Drawing.Size(445, 20);
            this.msgBox.TabIndex = 10;
            this.msgBox.Visible = false;
            // 
            // sendBTN
            // 
            this.sendBTN.Location = new System.Drawing.Point(403, 435);
            this.sendBTN.Name = "sendBTN";
            this.sendBTN.Size = new System.Drawing.Size(75, 23);
            this.sendBTN.TabIndex = 11;
            this.sendBTN.Text = "Send";
            this.sendBTN.UseVisualStyleBackColor = true;
            this.sendBTN.Visible = false;
            this.sendBTN.Click += new System.EventHandler(this.sendBTN_Click);
            // 
            // loginBTN
            // 
            this.loginBTN.Location = new System.Drawing.Point(219, 253);
            this.loginBTN.Name = "loginBTN";
            this.loginBTN.Size = new System.Drawing.Size(75, 23);
            this.loginBTN.TabIndex = 17;
            this.loginBTN.Text = "Log In";
            this.loginBTN.UseVisualStyleBackColor = true;
            this.loginBTN.Click += new System.EventHandler(this.loginBTN_Click);
            // 
            // loginLBL
            // 
            this.loginLBL.AutoSize = true;
            this.loginLBL.Location = new System.Drawing.Point(146, 176);
            this.loginLBL.Name = "loginLBL";
            this.loginLBL.Size = new System.Drawing.Size(221, 13);
            this.loginLBL.TabIndex = 16;
            this.loginLBL.Text = "Please login to be able to use the application.";
            // 
            // passwordLBL
            // 
            this.passwordLBL.AutoSize = true;
            this.passwordLBL.Cursor = System.Windows.Forms.Cursors.Default;
            this.passwordLBL.Location = new System.Drawing.Point(153, 230);
            this.passwordLBL.Name = "passwordLBL";
            this.passwordLBL.Size = new System.Drawing.Size(53, 13);
            this.passwordLBL.TabIndex = 15;
            this.passwordLBL.Text = "Password";
            // 
            // usernameLBL
            // 
            this.usernameLBL.AutoSize = true;
            this.usernameLBL.Location = new System.Drawing.Point(151, 204);
            this.usernameLBL.Name = "usernameLBL";
            this.usernameLBL.Size = new System.Drawing.Size(55, 13);
            this.usernameLBL.TabIndex = 14;
            this.usernameLBL.Text = "Username";
            // 
            // passwordTB
            // 
            this.passwordTB.Location = new System.Drawing.Point(212, 227);
            this.passwordTB.Name = "passwordTB";
            this.passwordTB.Size = new System.Drawing.Size(149, 20);
            this.passwordTB.TabIndex = 13;
            // 
            // usernameTB
            // 
            this.usernameTB.Location = new System.Drawing.Point(212, 201);
            this.usernameTB.Name = "usernameTB";
            this.usernameTB.Size = new System.Drawing.Size(149, 20);
            this.usernameTB.TabIndex = 12;
            // 
            // userlist
            // 
            this.userlist.FormattingEnabled = true;
            this.userlist.Location = new System.Drawing.Point(332, 23);
            this.userlist.Name = "userlist";
            this.userlist.Size = new System.Drawing.Size(146, 381);
            this.userlist.TabIndex = 18;
            this.userlist.Visible = false;
            // 
            // uploadFileBTN
            // 
            this.uploadFileBTN.Location = new System.Drawing.Point(33, 434);
            this.uploadFileBTN.Name = "uploadFileBTN";
            this.uploadFileBTN.Size = new System.Drawing.Size(75, 23);
            this.uploadFileBTN.TabIndex = 19;
            this.uploadFileBTN.Text = "Upload File";
            this.uploadFileBTN.UseVisualStyleBackColor = true;
            this.uploadFileBTN.Visible = false;
            this.uploadFileBTN.Click += new System.EventHandler(this.uploadFileBTN_Click);
            // 
            // ClientMW
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 477);
            this.Controls.Add(this.uploadFileBTN);
            this.Controls.Add(this.userlist);
            this.Controls.Add(this.loginBTN);
            this.Controls.Add(this.loginLBL);
            this.Controls.Add(this.passwordLBL);
            this.Controls.Add(this.usernameLBL);
            this.Controls.Add(this.passwordTB);
            this.Controls.Add(this.usernameTB);
            this.Controls.Add(this.sendBTN);
            this.Controls.Add(this.msgBox);
            this.Controls.Add(this.msgHst);
            this.Name = "ClientMW";
            this.Text = "xoxo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClientMW_FormClosing);
            this.Load += new System.EventHandler(this.ClientMW_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox msgHst;
        private System.Windows.Forms.TextBox msgBox;
        private System.Windows.Forms.Button sendBTN;
        private System.Windows.Forms.Button loginBTN;
        private System.Windows.Forms.Label loginLBL;
        private System.Windows.Forms.Label passwordLBL;
        private System.Windows.Forms.Label usernameLBL;
        private System.Windows.Forms.TextBox passwordTB;
        private System.Windows.Forms.TextBox usernameTB;
        private System.Windows.Forms.ListBox userlist;
        private System.Windows.Forms.Button uploadFileBTN;

    }
}

