namespace TetriSomething
{
    partial class mainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainWindow));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.myFlashController = new AxShockwaveFlashObjects.AxShockwaveFlash();
            this.hisFlashController = new AxShockwaveFlashObjects.AxShockwaveFlash();
            this.soloBTN = new System.Windows.Forms.Button();
            this.hostBTN = new System.Windows.Forms.Button();
            this.connectBTN = new System.Windows.Forms.Button();
            this.optionsBTN = new System.Windows.Forms.Button();
            this.aboutBTN = new System.Windows.Forms.Button();
            this.quitBTN = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myFlashController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hisFlashController)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1234, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.resetToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.startToolStripMenuItem.Text = "Start";
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.resetToolStripMenuItem.Text = "Reset";
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // myFlashController
            // 
            this.myFlashController.Enabled = true;
            this.myFlashController.Location = new System.Drawing.Point(40, 370);
            this.myFlashController.Name = "myFlashController";
            this.myFlashController.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("myFlashController.OcxState")));
            this.myFlashController.Size = new System.Drawing.Size(180, 330);
            this.myFlashController.TabIndex = 1;
            this.myFlashController.Visible = false;
            // 
            // hisFlashController
            // 
            this.hisFlashController.Enabled = true;
            this.hisFlashController.Location = new System.Drawing.Point(1020, 370);
            this.hisFlashController.Name = "hisFlashController";
            this.hisFlashController.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("hisFlashController.OcxState")));
            this.hisFlashController.Size = new System.Drawing.Size(180, 330);
            this.hisFlashController.TabIndex = 2;
            this.hisFlashController.Visible = false;
            // 
            // soloBTN
            // 
            this.soloBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.soloBTN.Location = new System.Drawing.Point(429, 205);
            this.soloBTN.Name = "soloBTN";
            this.soloBTN.Size = new System.Drawing.Size(376, 50);
            this.soloBTN.TabIndex = 3;
            this.soloBTN.Text = "Play SOLO";
            this.soloBTN.UseVisualStyleBackColor = true;
            this.soloBTN.Click += new System.EventHandler(this.soloBTN_Click);
            // 
            // hostBTN
            // 
            this.hostBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hostBTN.Location = new System.Drawing.Point(429, 261);
            this.hostBTN.Name = "hostBTN";
            this.hostBTN.Size = new System.Drawing.Size(376, 50);
            this.hostBTN.TabIndex = 4;
            this.hostBTN.Text = "Host a NETWORK game";
            this.hostBTN.UseVisualStyleBackColor = true;
            this.hostBTN.Click += new System.EventHandler(this.hostBTN_Click);
            // 
            // connectBTN
            // 
            this.connectBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.connectBTN.Location = new System.Drawing.Point(429, 317);
            this.connectBTN.Name = "connectBTN";
            this.connectBTN.Size = new System.Drawing.Size(376, 50);
            this.connectBTN.TabIndex = 5;
            this.connectBTN.Text = "Connect to a NETWORK game";
            this.connectBTN.UseVisualStyleBackColor = true;
            this.connectBTN.Click += new System.EventHandler(this.connectBTN_Click);
            // 
            // optionsBTN
            // 
            this.optionsBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optionsBTN.Location = new System.Drawing.Point(429, 373);
            this.optionsBTN.Name = "optionsBTN";
            this.optionsBTN.Size = new System.Drawing.Size(376, 50);
            this.optionsBTN.TabIndex = 6;
            this.optionsBTN.Text = "Options";
            this.optionsBTN.UseVisualStyleBackColor = true;
            // 
            // aboutBTN
            // 
            this.aboutBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aboutBTN.Location = new System.Drawing.Point(429, 429);
            this.aboutBTN.Name = "aboutBTN";
            this.aboutBTN.Size = new System.Drawing.Size(376, 50);
            this.aboutBTN.TabIndex = 7;
            this.aboutBTN.Text = "About";
            this.aboutBTN.UseVisualStyleBackColor = true;
            // 
            // quitBTN
            // 
            this.quitBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quitBTN.Location = new System.Drawing.Point(429, 485);
            this.quitBTN.Name = "quitBTN";
            this.quitBTN.Size = new System.Drawing.Size(376, 50);
            this.quitBTN.TabIndex = 8;
            this.quitBTN.Text = "Quit";
            this.quitBTN.UseVisualStyleBackColor = true;
            this.quitBTN.Click += new System.EventHandler(this.quitBTN_Click);
            // 
            // mainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1234, 740);
            this.Controls.Add(this.quitBTN);
            this.Controls.Add(this.aboutBTN);
            this.Controls.Add(this.optionsBTN);
            this.Controls.Add(this.connectBTN);
            this.Controls.Add(this.hostBTN);
            this.Controls.Add(this.soloBTN);
            this.Controls.Add(this.hisFlashController);
            this.Controls.Add(this.myFlashController);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "mainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tetris";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myFlashController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hisFlashController)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private AxShockwaveFlashObjects.AxShockwaveFlash myFlashController;
        private AxShockwaveFlashObjects.AxShockwaveFlash hisFlashController;
        private System.Windows.Forms.Button soloBTN;
        private System.Windows.Forms.Button hostBTN;
        private System.Windows.Forms.Button connectBTN;
        private System.Windows.Forms.Button optionsBTN;
        private System.Windows.Forms.Button aboutBTN;
        private System.Windows.Forms.Button quitBTN;



    }
}

