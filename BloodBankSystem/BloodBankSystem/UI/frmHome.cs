using BloodBankSystem.DataAccessLayer;
using BloodBankSystem.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BloodBankSystem
{
    public partial class FrmHome : Form
    {
        //create object of donor dal
        donorDAL dal=new donorDAL();
        public FrmHome()
        {
            InitializeComponent();
        }

        private void FrmHome_Load(object sender, EventArgs e)
        {
            //Load all the blood donors count when the form is loaded
            //Call allDonorCount method
            allDonorCount();

            //Display all the donors 
            DataTable dt = dal.Select();
            dgvDonors.DataSource = dt;

            //display the loggedin users name
            lblUsers.Text = frmLogin.loggedInUser;
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            //Close the application//
            this.Close();
        }

        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //open users form//
            frmUsers users = new frmUsers();
            users.Show();

        }

        private void donorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Opens Donors form
            frmDonors donors = new frmDonors();
            donors.Show();
        }

        public void allDonorCount()
        {
            //get donor count from database and set the count in respective labels
            lblOpositiveCount.Text = dal.countDonors("O+");
            lblOnegativeCount.Text = dal.countDonors("O-");
            lblApositiveCount.Text = dal.countDonors("A+");
            lblAnegativeCount.Text = dal.countDonors("A-");
            lblBpositiveCount.Text = dal.countDonors("B+");
            lblBnegativeCount.Text = dal.countDonors("B-");
            lblABpositiveCount.Text = dal.countDonors("AB+");
            lblABnegativeCount.Text = dal.countDonors("AB-");
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //1.Get the keywords from the textbox 
            string keywords = txtSearch.Text;

            //check text box is empty or not
            if (keywords != null)
            {
                //text box is not empty, display donors on data gridview 
                DataTable dt = dal.Search(keywords);
                
                dgvDonors.DataSource = dt;
            }
            else
            {
                //textbox is empty display all donors on grid view
                DataTable dt = dal.Select();
                dgvDonors.DataSource = dt;

            }
        }
    }
}
