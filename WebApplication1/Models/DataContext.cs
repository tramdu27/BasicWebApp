using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class DataContext
    {
        SqlConnection con = new SqlConnection("Data Source=VIP\\SQL2017;Initial Catalog=BasicWeb;Integrated Security=True");
        public List<Users> GetUsers()
        {
            List<Users> users = new List<Users>();
            SqlCommand cmd = new SqlCommand("pcd_GetUsers", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Users us = new Users();
                us.UserID = Convert.ToString(dr[0]);
                us.UserName = Convert.ToString(dr[1]);
                us.Password = Convert.ToString(dr[2]);
                us.Email = Convert.ToString(dr[3]);
                us.Tel = Convert.ToString(dr[4]);
                us.Disabled = Convert.ToInt32(dr[5]);
                users.Add(us);
            }
            return users;
        }
        public int UpdateUser(Users users)
        {

            SqlCommand cmd = new SqlCommand("pcd_SaveUsers", con);
                cmd = new SqlCommand("pcd_UpdateUser", con);
                cmd.Parameters.AddWithValue("@userid", users.UserID);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            //cmd.Parameters.AddWithValue("@userid", users.UserID.Trim());
            cmd.Parameters.AddWithValue("@username", users.UserName);
            cmd.Parameters.AddWithValue("@password", users.Password);
            cmd.Parameters.AddWithValue("@email", users.Email);
            cmd.Parameters.AddWithValue("@tel", users.Tel);
            cmd.Parameters.AddWithValue("@disabled", users.Disabled);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;

        }

        public int SaveUser(Users users)
        {
            
            SqlCommand cmd = new SqlCommand("pcd_SaveUsers", con);
            cmd = new SqlCommand("pcd_UpdateUser", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@userid", users.UserID.Trim());
            cmd.Parameters.AddWithValue("@username", users.UserName);
            cmd.Parameters.AddWithValue("@password", users.Password);
            cmd.Parameters.AddWithValue("@email", users.Email);
            cmd.Parameters.AddWithValue("@tel", users.Tel);
            cmd.Parameters.AddWithValue("@disabled", users.Disabled);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;

        }
        public Users GetUsersByID(string id)
        {


            Users users = new Users();

            SqlCommand cmd = new SqlCommand("pcd_GetUsersById", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@userID", id);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {

                users.UserID = Convert.ToString(dr[0]).Trim();
                users.UserName = Convert.ToString(dr[1]);
                users.Password = Convert.ToString(dr[2]);
                users.Email = Convert.ToString(dr[3]);
                users.Tel = Convert.ToString(dr[4]);
                users.Disabled = Convert.ToInt32(dr[5]);

            }
            return users;



        }

        public int DeleteUser(string id)
        {

            SqlCommand cmd = new SqlCommand("pcd_SaveUsers", con);

            
            cmd = new SqlCommand("pcd_DeleteUsers", con);
            cmd.Parameters.AddWithValue("@userID", id);           
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;

        }
        public List<Users> GetUsersByEmail(string search)
        {
            List<Users> users = new List<Users>();

            SqlCommand cmd = new SqlCommand("GetUsersByEmail", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@searchstring", search);
       

                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Users user = new Users();
                        user.UserID = (string)reader["UserID"];
                        user.UserName = (string)reader["UserName"];
                        user.Password = reader["Password"] == DBNull.Value ? null : (string)reader["Password"];
                        user.Email = (string)reader["Email"];
                        user.Tel = reader["Tel"] == DBNull.Value ? null : (string)reader["Tel"];
                        user.Disabled = (int)reader["Disabled"];

                        users.Add(user);
                    }

                    reader.Close();
            

            return users;
        }


    }
}
