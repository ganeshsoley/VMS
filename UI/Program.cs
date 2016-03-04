using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace UI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            /// Following Code restricts multiple instances of 
            /// installed application
            Process[] process = Process.GetProcessesByName(Application.ProductName);
            if (process.Length > 1)
            {
                MessageBox.Show("{Application Name} is already running. This instance will now close.",
                    "{Application Name}", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
            else
            {
                frmLogin objLogin = new frmLogin();
                //objLogin.ShowDialog();
                while (!(objLogin.UserID > 0 & objLogin.SelectedComID >0))
                {
                    objLogin.ShowDialog();
                }
                Application.Run(new frmMain(objLogin.UserID, objLogin.SelectedComID));
                //else
                //{
                //    Application.Exit();
                //}
            }
        }
    }
}
