using BloodBankSystem.BusinessLogicLayer;
using BloodBankSystem.DataAccessLayer;
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
    public partial class frmLogin : Form
    {
        //create object of bll and dll
        loginBLL l=new loginBLL();
        loginDAL dal = new loginDAL();

        //creating static string method to save the loggedin user

        public static string loggedInUser;
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //get user name and password from login form
            l.username = txtUserName.Text;
            l.password = txtPassword.Text;

            //check the credentials
            bool isSuccess = dal.login(l);
            if (isSuccess == true)
            {
                MessageBox.Show("LOGIN SUCCESSFULL!!!!!!!.");

                //save user name in loggedInUser
                loggedInUser = l.username;
                FrmHome frmHome = new FrmHome();
                frmHome.Show();

                
            }
            else
            {
                MessageBox.Show("LOGIN FAILED!!!!!!!.");
            }
        }
    }
}
