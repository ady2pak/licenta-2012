namespace StudentConnections
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientMW));
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
            this.userlistContMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.usernameSelected = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.prvConv = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadFileBTN = new System.Windows.Forms.Button();
            this.selectBTN = new System.Windows.Forms.Button();
            this.fileTB = new System.Windows.Forms.TextBox();
            this.userlistContMenu.SuspendLayout();
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
            this.userlist.ContextMenuStrip = this.userlistContMenu;
            this.userlist.FormattingEnabled = true;
            this.userlist.Location = new System.Drawing.Point(332, 23);
            this.userlist.Name = "userlist";
            this.userlist.Size = new System.Drawing.Size(146, 381);
            this.userlist.TabIndex = 18;
            this.userlist.Visible = false;
            this.userlist.MouseDown += new System.Windows.Forms.MouseEventHandler(this.userlist_MouseDown);
            // 
            // userlistContMenu
            // 
            this.userlistContMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usernameSelected,
            this.toolStripSeparator1,
            this.prvConv});
            this.userlistContMenu.Name = "userlistContMenu";
            this.userlistContMenu.Size = new System.Drawing.Size(218, 50);
            this.userlistContMenu.Opening += new System.ComponentModel.CancelEventHandler(this.userlistContMenu_Opening);
            // 
            // usernameSelected
            // 
            this.usernameSelected.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.usernameSelected.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.usernameSelected.Enabled = false;
            this.usernameSelected.Name = "usernameSelected";
            this.usernameSelected.Size = new System.Drawing.Size(100, 16);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(214, 6);
            // 
            // prvConv
            // 
            this.prvConv.Name = "prvConv";
            this.prvConv.Size = new System.Drawing.Size(217, 22);
            this.prvConv.Text = "Start a private conversation";
            this.prvConv.Click += new System.EventHandler(this.prvConv_Click);
            // 
            // uploadFileBTN
            // 
            this.uploadFileBTN.Location = new System.Drawing.Point(119, 435);
            this.uploadFileBTN.Name = "uploadFileBTN";
            this.uploadFileBTN.Size = new System.Drawing.Size(75, 23);
            this.uploadFileBTN.TabIndex = 19;
            this.uploadFileBTN.Text = "Upload File";
            this.uploadFileBTN.UseVisualStyleBackColor = true;
            this.uploadFileBTN.Visible = false;
            this.uploadFileBTN.Click += new System.EventHandler(this.uploadFileBTN_Click);
            // 
            // selectBTN
            // 
            this.selectBTN.Location = new System.Drawing.Point(34, 435);
            this.selectBTN.Name = "selectBTN";
            this.selectBTN.Size = new System.Drawing.Size(75, 23);
            this.selectBTN.TabIndex = 20;
            this.selectBTN.Text = "Select File";
            this.selectBTN.UseVisualStyleBackColor = true;
            this.selectBTN.Visible = false;
            this.selectBTN.Click += new System.EventHandler(this.selectBTN_Click);
            // 
            // fileTB
            // 
            this.fileTB.Enabled = false;
            this.fileTB.Location = new System.Drawing.Point(201, 437);
            this.fileTB.Name = "fileTB";
            this.fileTB.Size = new System.Drawing.Size(166, 20);
            this.fileTB.TabIndex = 21;
            this.fileTB.Visible = false;
            // 
            // ClientMW
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 477);
            this.Controls.Add(this.fileTB);
            this.Controls.Add(this.selectBTN);
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
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ClientMW";
            this.Text = "Student Connections";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClientMW_FormClosing);
            this.userlistContMenu.ResumeLayout(false);
            this.userlistContMenu.PerformLayout();
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
        private System.Windows.Forms.Button selectBTN;
        private System.Windows.Forms.TextBox fileTB;
        private System.Windows.Forms.ContextMenuStrip userlistContMenu;
        private System.Windows.Forms.ToolStripMenuItem prvConv;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripTextBox usernameSelected;

    }
}

