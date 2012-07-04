using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TetriSomething
{
    static class Program
    {   
        [STAThread]
        static void Main()
        {
            //try
            //{
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new mainWindow());
            //}
            //catch(Exception e)
            //{
            //    MessageBox.Show(e.Message);
            //}
        }
    }
}
