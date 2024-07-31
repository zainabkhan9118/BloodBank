using BloodBankSystem.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BloodBankSystem.BusinessLogicLayer;

namespace BloodBankSystem.DataAccessLayer
{
    internal class donorDAL
    {

        //creating static string to connect database//
        static string myconnection = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;


        #region Select data from Database
        public DataTable Select()
        {
            //Create an object to connect database//
            SqlConnection con = new SqlConnection(myconnection);

            //create a data table to hold data from database //
            DataTable dt = new DataTable();
            try
            {
                //write sql query to get data from database//
                String query = "Select * from tbl_donors";

                //create sql command to execute the querry//
                SqlCommand cmd = new SqlCommand(query, con);

                //Create sqlDataAdapter to hold the data from database temporarily
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //opening connection
                con.Open();

                //Transfering the data from adapter to table
                adapter.Fill(dt);


            }
            catch (Exception ex)
            {
                //Displays Error message if there is any exception error//
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //closes Database connection//
                con.Close();
            }
            return dt;
        }
        #endregion



        #region Insert data into Database for User Module
        public bool Insert( donorBLL d)
        {
            //create a boolean variable and set it to false
            bool isSuccess = false;

            //Create an object to connect database//
            SqlConnection con = new SqlConnection(myconnection);

            try
            {
                //write sql query to get data from database//
                String query = "Insert into tbl_donors(first_name,last_name,email,contact,gender,address,blood_group,added_date,image_name,added_by) values (@first_name,@last_name,@email,@contact,@gender,@address,@blood_group,@added_date,@image_name,@added_by)";

                //create sql command to pass the values //
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@first_name", d.first_name);
                cmd.Parameters.AddWithValue("@last_name", d.last_name);
                cmd.Parameters.AddWithValue("@email", d.email);
                cmd.Parameters.AddWithValue("@contact", d.contact);
                cmd.Parameters.AddWithValue("@gender", d.gender);
                cmd.Parameters.AddWithValue("@address", d.address);
                cmd.Parameters.AddWithValue("@blood_group", d.blood_group);
                cmd.Parameters.AddWithValue("@added_date", d.added_date);
                cmd.Parameters.AddWithValue("@image_name", d.image_name);
                cmd.Parameters.AddWithValue("@added_by", d.added_by);


                //opening connection
                con.Open();

                //create  a variable to hold the value after the query is executed
                int row = cmd.ExecuteNonQuery();


                //the value of rows will be greatere than zero if query is executed successfully else will be zero
                if (row > 0)
                {
                    //query successfully executed 
                    isSuccess = true;
                }
                else
                {
                    //failed to execute query
                    isSuccess = false;
                }


            }
            catch (Exception ex)
            {
                //Displays Error message if there is any exception error//
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //closes Dtabase connection//
                con.Close();
            }
            return isSuccess;
        }
        #endregion


        #region Update data in Database for User Module
        public bool Update(donorBLL d)
        {
            //create a boolean variable and set it to false
            bool isSuccess = false;

            //Create an object to connect database//
            SqlConnection con = new SqlConnection(myconnection);

            try
            {
                //write sql query to get data from database//
                String query = "Update tbl_donors SET first_name=@first_name,last_name=@last_name,email=@email,contact=@contact,gender=@gender,address=@address,blood_group=@blood_group,added_date=@added_date,image_name=@image_name,added_by=@added_by Where donor_id=@donor_id";


                //create sql command to pass the values //
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@first_name", d.first_name);
                cmd.Parameters.AddWithValue("@last_name", d.last_name);
                cmd.Parameters.AddWithValue("@email", d.email);
                cmd.Parameters.AddWithValue("@contact", d.contact);
                cmd.Parameters.AddWithValue("@gender", d.gender);
                cmd.Parameters.AddWithValue("@address", d.address);
                cmd.Parameters.AddWithValue("@blood_group", d.blood_group);
                cmd.Parameters.AddWithValue("@added_date", d.added_date);
                cmd.Parameters.AddWithValue("@image_name", d.image_name);
                cmd.Parameters.AddWithValue("@added_by", d.added_by);
                cmd.Parameters.AddWithValue("@donor_id", d.donor_id);


                //opening connection
                con.Open();

                //create a variable to hold the value after the query is executed
                int row = cmd.ExecuteNonQuery();


                //the value of rows will be greatere than zero if query is executed successfully else will be zero
                if (row > 0)
                {
                    //query successfully executed 
                    isSuccess = true;
                }
                else
                {
                    //failed to execute query
                    isSuccess = false;
                }


            }
            catch (Exception ex)
            {
                //Displays Error message if there is any exception error//
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //closes Dtabase connection//
                con.Close();
            }
            return isSuccess;
        }
        #endregion


        #region Delete data from Database (User Module)
        public bool Delete(donorBLL d)
        {
            //create a boolean variable and set it to false
            bool isSuccess = false;

            //Create an object to connect database//
            SqlConnection con = new SqlConnection(myconnection);

            try
            {
                //write sql query to get data from database//
                String query = "Delete from tbl_donors Where donor_id=@donor_id";


                //create sql command to pass the values //
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@donor_id", d.donor_id);


                //opening connection
                con.Open();

                //create a variable to hold the value after the query is executed
                int row = cmd.ExecuteNonQuery();


                //the value of rows will be greatere than zero if query is executed successfully else will be zero
                if (row > 0)
                {
                    //query successfully executed 
                    isSuccess = true;
                }
                else
                {
                    //failed to execute query
                    isSuccess = false;
                }


            }
            catch (Exception ex)
            {
                //Displays Error message if there is any exception error//
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //closes Dtabase connection//
                con.Close();
            }
            return isSuccess;
        }
        #endregion


        #region SEARCH
        public DataTable Search(string keywords)
        {
            //Create an sql connection 
            SqlConnection con = new SqlConnection(myconnection);

            //create a data table to hold data from data base temporarily
            DataTable dt = new DataTable();

            //write the code for searching users
            try
            {
                //write sql query to get data from database//
                String query = "Select * from tbl_donors Where donor_id like '%" + keywords + "%' or first_name like '%" + keywords + "%' or address like '%" + keywords + "%' or blood_group like '%" + keywords + "%'";


                //create sql command to pass the values //
                SqlCommand cmd = new SqlCommand(query, con);

                //Create sql data adapter to get data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //opening connection
                con.Open();

                //pass data from adapter to table 
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally
            {
                con.Close();
            }
            return dt;
        }

        #endregion

        #region Count Donors For Specific Blood Count
        public string countDonors(string blood_group)
        {
            //create sql connecxtion for database connection
            SqlConnection conn = new SqlConnection(myconnection);

            //create a string variable for donor count and set its default value to 0
            string donors = "0";

            try
            {
                //sql querry to count donors for specific blood group

                string query = "select * from tbl_donors where blood_group='" + blood_group + "'";
                SqlCommand cmd= new SqlCommand(query, conn);

                SqlDataAdapter adapter = new SqlDataAdapter( cmd);

                DataTable dt = new DataTable();

                adapter.Fill(dt);

                //get the total number of donors based on blood group
                donors=dt.Rows.Count.ToString();
                
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return donors;
        }
        #endregion
    }
}
