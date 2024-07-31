using BloodBankSystem.BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BloodBankSystem.DataAccessLayer
{
    internal class loginDAL
    {
        //Create a static string to connect database
        static string myconnection = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        public bool login(loginBLL loginBLL)
        {
            //create a boolean varaible and set value to false 
            bool isSuccess = false;

            //connecting database
            SqlConnection sqlConnection=new SqlConnection(myconnection);

            try
            {
                //Sql query to check user name and passwords
                string query = "select * from tbl_users where username=@username and password=@password";

                //create sql command to pass the values //
                SqlCommand cmd = new SqlCommand(query, sqlConnection);

                cmd.Parameters.AddWithValue("@username", loginBLL.username);
                cmd.Parameters.AddWithValue("@password", loginBLL.password);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                adapter.Fill(dt);

                sqlConnection.Open();

                if (dt.Rows.Count>0)
                {
                    //users exists login successfull
                    isSuccess = true;
                }
                else
                {
                    isSuccess= false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
            return isSuccess;
        }
         
    }
}
