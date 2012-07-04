using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TetriSomething
{
    public partial class aboutWindow : Form
    {
        public aboutWindow()
        {
            InitializeComponent();            
        }

        private void aboutWindow_Load(object sender, EventArgs e)
        {
            this.nameLbl.Text = tet_versionInfo.NAME;
            this.versionLbl.Text = tet_versionInfo.VERSION;
            this.copyrightLbl.Text = tet_versionInfo.AUTHOR + "  (c)  " + tet_versionInfo.DATE;            
        }
    }
}
