using BloodBankSystem.BLL;
using BloodBankSystem.BusinessLogicLayer;
using BloodBankSystem.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BloodBankSystem.UI
{
    public partial class frmDonors : Form
    {
        DataTable dt;

        //creating object for donorBLL and donorDAL
        donorBLL d=new donorBLL();
        donorDAL dal = new donorDAL();

        uDAL uDAL = new uDAL();

        string imageName = "no-image.png";
        //global variable for the image we want to delete
        string rowHeaderImage;

        string sourcePath = "";
        string destinationPath = "";

        public frmDonors()
        {
            InitializeComponent();
        }

        private void frmDonors_Load(object sender, EventArgs e)
        {
            //Display users in data grid view
            dt = dal.Select();
            dgvDonors.DataSource = dt;
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            //Closes user form//
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //step 1: Get values from UI
            d.first_name = txtFirstName.Text;
            d.last_name = txtLastName.Text;
            d.email = txtEmail.Text;
            d.contact = txtContact.Text;
            d.gender = cmbGender.Text;
            d.address = txtAddress.Text;
            d.blood_group = cmbBloodGroup.Text;
            d.added_date = DateTime.Now;
           

            string loggedInUser = frmLogin.loggedInUser;
            userBLL usr = uDAL.GetIdFromUserName(loggedInUser);
            
            d.added_by = usr.user_id;//get id of loggedin user

            d.image_name = imageName;

            //upload the image when it is selected
            //check whether user has selected the image or not
            if (imageName != "no-image.png")
            {
                //user has selected the image
                File.Copy(sourcePath, destinationPath);
            }


            //Step 2: adding values from UI to Database
            //create a boolean variable to check whether the data is inserted successfully or not
            bool success = dal.Insert(d);

            //Step 3: check if data is inserted successfully or not
            if (success == true)
            {
                MessageBox.Show("NEW DONOR ADDED SUCCESSFULLY.");

                //Display users in data grid view
                dt = dal.Select();
                dgvDonors.DataSource = dt;

                //Clear textboxes
                Clear();
            }

            else
            {
                MessageBox.Show("FAILED TO NEW ADD DONOR.");
            }
        }

        public void Clear()
        {
            txtDonorId.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtContact.Text = "";
            cmbGender.Text = "";
            cmbBloodGroup.Text = "";
            txtAddress.Text = "";
            try
            {
                //Dispaly the image of related user.Get the image path
                string paths = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);

                string imagePath = paths + "\\images\\no-image.png";

                //Display in picture box
                pictureBoxProfilePicture.Image = new Bitmap(imagePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading donor image: " + ex.Message);
            }

        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //step 1: Get values from UI
            d.donor_id=int.Parse(txtDonorId.Text);
            d.first_name = txtFirstName.Text;
            d.last_name = txtLastName.Text;
            d.email = txtEmail.Text;
            d.contact = txtContact.Text;
            d.gender = cmbGender.Text;
            d.address = txtAddress.Text;
            d.blood_group = cmbBloodGroup.Text;
            d.added_date = DateTime.Now;
            d.image_name = imageName;

            string loggedInUser = frmLogin.loggedInUser;
            userBLL usr = uDAL.GetIdFromUserName(loggedInUser);

            d.added_by = usr.user_id;//get id of loggedin user

            //upload the image when it is selected
            //check whether user has selected the image or not
            if (imageName != "no-image.png")
            {
                //user has selected the image
                File.Copy(sourcePath, destinationPath);
            }

            //Step 2:create a boolean variable to check whether the data is updated successfully or not
            bool success = dal.Update(d);

            //Remove the previous image
            if (rowHeaderImage != "no-image.png")
            {
                string paths = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);

                //give the path of image folder
                string imagePath = paths + "\\images\\" + rowHeaderImage;
                Clear();

                //call garbage collection function
                GC.Collect();
                GC.WaitForPendingFinalizers();

                File.Delete(imagePath);
            }

            //Step 3: check if data is Updated successfully or not
            if (success == true)
            {
                MessageBox.Show("DONOR UPDATED SUCCESSFULLY.");

                //Refresh data grid view
                dt = dal.Select();
                dgvDonors.DataSource = dt;

                //Clear textboxes
                Clear();
            }

            else
            {
                MessageBox.Show("FAILED TO UPDATE DONOR.");
            }
        }

       
        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Step 1: Get ID from the textbox to Delete the data from Database
            d.donor_id = int.Parse(txtDonorId.Text);
            //remove physical file of the user 
            if (rowHeaderImage != "no-image.png")
            {
                string paths = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);

                //give the path of image folder
                string imagePath = paths + "\\images\\" + rowHeaderImage;
                Clear();

                //call garbage collection function
                GC.Collect();
                GC.WaitForPendingFinalizers();

                File.Delete(imagePath);
            }

            //Step 2:create a boolean variable to check whether the data is deleted successfully or not
            bool success = dal.Delete(d);

            


            //Step 3: check if data is Deleted successfully or not
            if (success == true)
            {
                MessageBox.Show("DONOR DELETED SUCCESSFULLY.");

                //Refresh data grid view
                dt = dal.Select();
                dgvDonors.DataSource = dt;

                //Clear textboxes
                Clear();
            }

            else
            {
                MessageBox.Show("FAILED TO DELETE DONOR.");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDonorId.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtContact.Text = "";
            cmbGender.Text = "";
            cmbBloodGroup.Text = "";
            txtAddress.Text = "";
            try
            {
                //Dispaly the image of related user.Get the image path
                string paths = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);

                string imagePath = paths + "\\images\\no-image.png";

                //Display in picture box
                pictureBoxProfilePicture.Image = new Bitmap(imagePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading donor image: " + ex.Message);
            }
        }

        private void dgvDonors_RowHeaderMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {

            //Find the row index of the row clicked of user Data grid view
            int RowIndex = e.RowIndex;
            txtDonorId.Text = dgvDonors.Rows[RowIndex].Cells[0].Value.ToString();
            txtFirstName.Text = dgvDonors.Rows[RowIndex].Cells[1].Value.ToString();
            txtLastName.Text = dgvDonors.Rows[RowIndex].Cells[2].Value.ToString();
            txtEmail.Text = dgvDonors.Rows[RowIndex].Cells[3].Value.ToString();
            txtContact.Text = dgvDonors.Rows[RowIndex].Cells[4].Value.ToString();
            cmbGender.Text = dgvDonors.Rows[RowIndex].Cells[5].Value.ToString();
            txtAddress.Text = dgvDonors.Rows[RowIndex].Cells[6].Value.ToString();
            cmbBloodGroup.Text = dgvDonors.Rows[RowIndex].Cells[7].Value.ToString();
            imageName = dgvDonors.Rows[RowIndex].Cells[9].Value.ToString();

            //update value of global variable
            rowHeaderImage = imageName;

            //Dispaly the image of related user.Get the image path
            string paths = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);

            
            try
            {
                if (imageName != "no-image.jpg")
                {
                    //Path to Destination folder
                    string imagePath = paths + "\\images\\" + imageName;

                    //Display in picture box
                    pictureBoxProfilePicture.Image = new Bitmap(imagePath);
                }
                else
                {
                    string imagePath = paths + "\\images\\no-image.png";

                    //Display in picture box
                    pictureBoxProfilePicture.Image = new Bitmap(imagePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading donor image: " + ex.Message);
            }
        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            //Code for uploading Image

            //open dialog box to Select an image
            OpenFileDialog open = new OpenFileDialog();

            //Filter the file type to only open images
            open.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.PNG ; *.gif;)|*.jpg; *.jpeg; *.png; *.PNG ; *.gif;";

            //Check where the image is selected
            if (open.ShowDialog() == DialogResult.OK)
            {
                //Cheks if file Exists or not
                if (open.CheckFileExists)
                {
                    //Display the selected file on picture box
                    pictureBoxProfilePicture.Image = new Bitmap(open.FileName);

                    //Rename the image we selected 
                    //1. get the extension of Image
                    string ext = Path.GetExtension(open.FileName);

                    //2.Generate a random value
                    Random random = new Random();
                    int RandInt = random.Next(0, 1000);

                    //3.Rename the image
                    imageName = "Blood_Bank_MS_" + RandInt + ext;

                    //4.Upload Image in the IMAGRE FOLDER so we get the path of the selected image
                    sourcePath = open.FileName;

                    //5.Get the path of the Destination 
                    string paths = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);

                    //path to destination folder
                    destinationPath = paths + "\\images\\" + imageName;

                    //6.Copy image to the Destination Folder
                    //File.Copy(sourcePath, destinationPath);

                    //Diplsy a message 
                    //MessageBox.Show("IMAGE SUCCESSFULLY UPLOADED");
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //1.Get the keywords from the textbox 
            string keywords = txtSearch.Text;

            //check text box is empty or not
            if (keywords != null)
            {
                //text box is not empty, display users or data gridview bases on the users
                dt = dal.Search(keywords);
                dgvDonors.DataSource = dt;
            }
            else
            {
                //textbox is empty display all users on grid view
                dt = dal.Select();
                dgvDonors.DataSource = dt;

            }
        }
    }
}
