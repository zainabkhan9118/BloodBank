using BloodBankSystem.BLL;
using BloodBankSystem.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BloodBankSystem.UI
{
    public partial class frmUsers : Form
    {
        DataTable dt;
        public frmUsers()
        {
            InitializeComponent();
            
        }

        //creating object for userBLL and userDAL
        userBLL u=new userBLL();
        uDAL dal=new uDAL();

        string imageName = "no-image.png";

        //global variable for the image we want to delete
        string rowHeaderImage;

        string sourcePath = "";
        string destinationPath = "";

        private void frmUsers_Load(object sender, EventArgs e)
        {
            //Display the users data in grid view when form is loaded
            dt = dal.Select();
            dgvUsers.DataSource = dt;

        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            //Closes user form//
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //step 1: Get values from UI
            u.full_name = txtFullName.Text;
            u.email = txtEmail.Text;
            u.username=txtUserName.Text;
            u.password=txtPassword.Text;
            u.contact = txtContact.Text;
            u.address = txtAddress.Text;
            u.added_date= DateTime.Now;
            u.image_name = imageName;

            //upload the image when it is selected
            //check whether user has selected the image or not
            if(imageName!="no-image.png")
            {
                //user has selected the image
                File.Copy(sourcePath, destinationPath);
            }

            //Step 2: adding values from UI to Database
            //create a boolean variable to check whether the data is inserted successfully or not
            bool success=dal.Insert(u);

            //Step 3: check if data is inserted successfully or not
            if(success==true)
            {
                MessageBox.Show("NEW USER ADDED SUCCESSFULLY.");

                //Display users in data grid view
                dt = dal.Select();
                dgvUsers.DataSource = dt;

                //Clear textboxes
                Clear();
            }

            else
            {
                MessageBox.Show("FAILED TO NEW ADD USER.");
            }
        }

        public void Clear()
        {
            txtUserId.Text = "";
            txtFullName.Text = "";
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtEmail.Text = "";
            txtContact.Text = "";
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
                MessageBox.Show("Error loading user image: " + ex.Message);
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //Step 1: get values from UI
            u.user_id=int.Parse(txtUserId.Text);
            u.full_name = txtFullName.Text;
            u.email = txtEmail.Text;
            u.username = txtUserName.Text;
            u.password = txtPassword.Text;
            u.contact = txtContact.Text;
            u.address = txtAddress.Text;
            u.added_date = DateTime.Now;
            u.image_name = imageName;


            //upload the image when it is selected
            //check whether user has selected the image or not
            if (imageName != "no-image.png")
            {
                //user has selected the image
                File.Copy(sourcePath, destinationPath);
            }

            //Step 2:create a boolean variable to check whether the data is updated successfully or not
            bool success = dal.Update(u);

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
                MessageBox.Show("USER UPDATED SUCCESSFULLY.");

                //Refresh data grid view
                dt = dal.Select();
                dgvUsers.DataSource = dt;

                //Clear textboxes
                Clear();
            }

            else
            {
                MessageBox.Show("FAILED TO UPDATE USER.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Step 1: Get ID from the textbox to Delete the data from Database
            u.user_id = int.Parse(txtUserId.Text);

            //Step 2:create a boolean variable to check whether the data is deleted successfully or not
            bool success = dal.Delete(u);

            //remove physical file of the user 
            if(rowHeaderImage!="no-image.png")
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

            //Step 3: check if data is Deleted successfully or not
            if (success == true)
            {
                MessageBox.Show("USER DELETED SUCCESSFULLY.");

                //Refresh data grid view
                dt = dal.Select();
                dgvUsers.DataSource = dt;

                //Clear textboxes
                Clear();
            }

            else
            {
                MessageBox.Show("FAILED TO DELETE USER.");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUserId.Clear();
            txtFullName.Clear();
            txtUserName.Clear();
            txtPassword.Clear();
            txtEmail.Clear();
            txtContact.Clear();
            txtAddress.Clear();

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
                MessageBox.Show("Error loading user image: " + ex.Message);
            }

        }

        private void dgvUsers_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Find the row index of the row clicked of user Data grid view
            int RowIndex=e.RowIndex;
            txtUserId.Text = dgvUsers.Rows[RowIndex].Cells[0].Value.ToString();
            txtUserName.Text= dgvUsers.Rows[RowIndex].Cells[1].Value.ToString();
            txtEmail.Text= dgvUsers.Rows[RowIndex].Cells[2].Value.ToString();
            txtPassword.Text= dgvUsers.Rows[RowIndex].Cells[3].Value.ToString();
            txtFullName.Text= dgvUsers.Rows[RowIndex].Cells[4].Value.ToString();
            txtContact.Text= dgvUsers.Rows[RowIndex].Cells[5].Value.ToString();
            txtAddress.Text= dgvUsers.Rows[RowIndex].Cells[6].Value.ToString();
            imageName= dgvUsers.Rows[RowIndex].Cells[8].Value.ToString();

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
                MessageBox.Show("Error loading user image: " + ex.Message);
            }
        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            //Code for uploading Image
            
            //open dialog box to Select an image
            OpenFileDialog open=new OpenFileDialog();

            //Filter the file type to only open images
            open.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.PNG ; *.gif;)|*.jpg; *.jpeg; *.png; *.PNG ; *.gif;";

            //Check where the image is selected
            if (open.ShowDialog() == DialogResult.OK)
            {
                //Cheks if file Exists or not
                if(open.CheckFileExists)
                {
                    //Display the selected file on picture box
                    pictureBoxProfilePicture.Image=new Bitmap(open.FileName);

                    //Rename the image we selected 
                    //1. get the extension of Image
                    string ext=Path.GetExtension(open.FileName);

                    //2.Generate a random value
                    Random random = new Random();
                    int RandInt = random.Next(0, 1000);

                    //3.Rename the image
                    imageName="Blood_Bank_MS_"+RandInt+ext;

                    //4.Upload Image in the IMAGRE FOLDER so we get the path of the selected image
                    sourcePath=open.FileName;

                    //5.Get the path of the Destination 
                    string paths = Application.StartupPath.Substring(0,Application.StartupPath.Length-10);

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
            if (keywords!= null)
            {
                //text box is not empty, display users or data gridview bases on the users
                dt = dal.Search(keywords);
                dgvUsers.DataSource = dt;
            }
            else
            {
                //textbox is empty display all users on grid view
                dt = dal.Select();
                dgvUsers.DataSource = dt;
                
            }
        }
    }
}
