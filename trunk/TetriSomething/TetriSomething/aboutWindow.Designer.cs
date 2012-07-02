namespace TetriSomething
{
    partial class aboutWindow
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
            this.nameLbl = new System.Windows.Forms.Label();
            this.versionLbl = new System.Windows.Forms.Label();
            this.copyrightLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // nameLbl
            // 
            this.nameLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.nameLbl.AutoSize = true;
            this.nameLbl.Location = new System.Drawing.Point(12, 111);
            this.nameLbl.Name = "nameLbl";
            this.nameLbl.Size = new System.Drawing.Size(33, 13);
            this.nameLbl.TabIndex = 0;
            this.nameLbl.Text = "name";
            this.nameLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // versionLbl
            // 
            this.versionLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.versionLbl.AutoSize = true;
            this.versionLbl.Location = new System.Drawing.Point(12, 124);
            this.versionLbl.Name = "versionLbl";
            this.versionLbl.Size = new System.Drawing.Size(41, 13);
            this.versionLbl.TabIndex = 1;
            this.versionLbl.Text = "version";
            this.versionLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // copyrightLbl
            // 
            this.copyrightLbl.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.copyrightLbl.AutoSize = true;
            this.copyrightLbl.Location = new System.Drawing.Point(12, 137);
            this.copyrightLbl.Name = "copyrightLbl";
            this.copyrightLbl.Size = new System.Drawing.Size(50, 13);
            this.copyrightLbl.TabIndex = 2;
            this.copyrightLbl.Text = "copyright";
            this.copyrightLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // aboutWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.copyrightLbl);
            this.Controls.Add(this.versionLbl);
            this.Controls.Add(this.nameLbl);
            this.Name = "aboutWindow";
            this.Text = "aboutWindow";
            this.Load += new System.EventHandler(this.aboutWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nameLbl;
        private System.Windows.Forms.Label versionLbl;
        private System.Windows.Forms.Label copyrightLbl;
    }
}