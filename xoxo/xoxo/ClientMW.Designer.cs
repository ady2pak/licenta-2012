namespace xoxoClient
{
    partial class ClientMWWindow
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
            this.button1 = new System.Windows.Forms.Button();
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
            // 
            // msgBox
            // 
            this.msgBox.Location = new System.Drawing.Point(34, 451);
            this.msgBox.Name = "msgBox";
            this.msgBox.Size = new System.Drawing.Size(291, 20);
            this.msgBox.TabIndex = 10;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(249, 478);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Send";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ClientMWWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 518);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.msgBox);
            this.Controls.Add(this.msgHst);
            this.Name = "ClientMWWindow";
            this.Text = "xoxo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox msgHst;
        private System.Windows.Forms.TextBox msgBox;
        private System.Windows.Forms.Button button1;

    }
}

