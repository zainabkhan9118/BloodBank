using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BloodBankSystem.UI
{
    public partial class frmSplash : Form
    {
        int move = 0;
        public frmSplash()
        {
            InitializeComponent();
        }

        private void frmSplash_Load(object sender, EventArgs e)
        {
            //load the timer
            timerSplash.Start();
        }

        private async void timerSplash_Tick(object sender, EventArgs e)
        {
            // Code for loading animation
            timerSplash.Interval = 20;
            panelMovable.Width += 5;

            move += 5;
            //if the loading is complete display loginfom and close this one 
            if (move ==670) 
            {
                //Stop timer
                timerSplash.Stop();

                await Task.Delay(500);// 500 milliseconds delay

                //Display login form
                frmLogin frmLogin = new frmLogin();
                frmLogin.Show();

                this.Close();
            }
        }
    }
}
