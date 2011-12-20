namespace xoxoServer
{
    partial class ServerMW
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
            this.debugOutput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // debugOutput
            // 
            this.debugOutput.Enabled = false;
            this.debugOutput.Location = new System.Drawing.Point(12, 12);
            this.debugOutput.Multiline = true;
            this.debugOutput.Name = "debugOutput";
            this.debugOutput.Size = new System.Drawing.Size(370, 328);
            this.debugOutput.TabIndex = 0;
            // 
            // ServerMW
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 352);
            this.Controls.Add(this.debugOutput);
            this.Name = "ServerMW";
            this.Text = "xoxoServer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox debugOutput;
    }
}

