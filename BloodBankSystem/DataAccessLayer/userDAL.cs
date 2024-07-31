using BloodBankSystem.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BloodBankSystem.DataAccessLayer
{
    internal class uDAL
    {
        //creating static string to connect database//
        static string myconnection = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;
        
        
        #region Select data from Database
        public DataTable Select()
        {
            //Create an object to connect database//
            SqlConnection con=new SqlConnection(myconnection);
           
            //create a data table to hold data from database //
            DataTable dt=new DataTable();
            try
            {
                //write sql query to get data from database//
                String query = "Select * from tbl_users";

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
        public bool Insert( userBLL u)
        {
            //create a boolean variable and set it to false
            bool isSuccess=false;

            //Create an object to connect database//
            SqlConnection con = new SqlConnection(myconnection);
            
            try
            {
                //write sql query to get data from database//
                String query = "Insert into tbl_users(username,email,password,full_name,contact,address,added_date,image_name) values (@username,@email,@password,@full_name,@contact,@address,@added_date,@image_name)";

                //create sql command to pass the values //
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@username", u.username);
                cmd.Parameters.AddWithValue("@email", u.email);
                cmd.Parameters.AddWithValue("@password", u.password);
                cmd.Parameters.AddWithValue("@full_name", u.full_name);
                cmd.Parameters.AddWithValue("@contact", u.contact);
                cmd.Parameters.AddWithValue("@address", u.address);
                cmd.Parameters.AddWithValue("@added_date", u.added_date);
                cmd.Parameters.AddWithValue("@image_name", u.image_name);
                

                //opening connection
                con.Open();

                //create  a variable to hold the value after the query is executed
                int row=cmd.ExecuteNonQuery();


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
                //closes Database connection//
                con.Close();
            }
            return isSuccess;
        }
        #endregion


        #region Update data in Database for User Module
        public bool Update(userBLL u)
        {
            //create a boolean variable and set it to false
            bool isSuccess = false;

            //Create an object to connect database//
            SqlConnection con = new SqlConnection(myconnection);

            try
            {
                //write sql query to get data from database//
                String query = "Update tbl_users SET username=@username,email=@email,password=@password,full_name=@full_name,contact=@contact,address=@address,added_date=@added_date,image_name=@image_name Where user_id=@user_id";


                //create sql command to pass the values //
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@username", u.username);
                cmd.Parameters.AddWithValue("@email", u.email);
                cmd.Parameters.AddWithValue("@password", u.password);
                cmd.Parameters.AddWithValue("@full_name", u.full_name);
                cmd.Parameters.AddWithValue("@contact", u.contact);
                cmd.Parameters.AddWithValue("@address", u.address);
                cmd.Parameters.AddWithValue("@added_date", u.added_date);
                cmd.Parameters.AddWithValue("@image_name", u.image_name);
                cmd.Parameters.AddWithValue("@user_id", u.user_id);


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
                //closes Database connection//
                con.Close();
            }
            return isSuccess;
        }
        #endregion


        #region Delete data from Database (User Module)
        public bool Delete(userBLL u)
        {
            //create a boolean variable and set it to false
            bool isSuccess = false;

            //Create an object to connect database//
            SqlConnection con = new SqlConnection(myconnection);

            try
            {
                //write sql query to get data from database//
                String query = "Delete from tbl_users Where user_id=@user_id";


                //create sql command to pass the values //
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@user_id", u.user_id);


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
                String query = "Select * from tbl_users Where user_id like '%"+keywords+"%' or full_name like '%"+keywords+"%' or address like '%"+keywords+"%'";


                //create sql command to pass the values //
                SqlCommand cmd = new SqlCommand(query, con);

                //Create sql data adapter to get data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //opening connection
                con.Open();

                //pass data from adapter to tabke 
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally
            { 
                con.Close( );
            }
            return dt;
        }

        #endregion

        #region
        public userBLL GetIdFromUserName(string username)
        {
            userBLL userBLL = new userBLL();
            SqlConnection con = new SqlConnection(myconnection);

            DataTable dt=new DataTable();

            try
            {
                //write sql query to get data from database//
                String query = "Select user_id from tbl_users Where  username like '"+username+"'";



                //Create sql data adapter to get data from database
                SqlDataAdapter adapter = new SqlDataAdapter(query,con);

                //opening connection
                con.Open();

                //pass data from adapter to tabke 
                adapter.Fill(dt);

                //if true then get the id against that user name 
                if (dt.Rows.Count > 0)
                {
                    userBLL.user_id = int.Parse(dt.Rows[0]["user_id"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally
            {
                con.Close();
            }

            return userBLL;
        }
        #endregion
    }
}

    

