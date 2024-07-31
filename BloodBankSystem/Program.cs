using BloodBankSystem.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BloodBankSystem
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Show the splash screen first
            using (frmSplash splash = new frmSplash())
            {
                splash.ShowDialog();
            }

            // After the splash screen is closed, show the login form
            Application.Run(new frmLogin());
        }
    }
}
