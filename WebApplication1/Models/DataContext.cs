using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Helpers;
using Telerik.SvgIcons;

namespace WebApplication1.Models
{
    public class DataContext
    {
        string cs = ConfigurationManager.ConnectionStrings["basicdb"].ConnectionString;

        public List<Users> GetUsers()
        {
            List<Users> users = new List<Users>();

            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("pcd_GetUsers", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Users user = new Users
                            {
                                UserID = reader.IsDBNull(0) ? null : reader.GetString(0),
                                UserName = reader.IsDBNull(1) ? null : reader.GetString(1),
                                Password = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Email = reader.IsDBNull(3) ? null : reader.GetString(3),
                                Tel = reader.IsDBNull(4) ? null : reader.GetString(4),
                                Disabled = reader.GetByte(5)
                            };
                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }


        //us.UserID = Convert.ToString(dr.GetValue(0).ToString());
        //us.UserName = Convert.ToString(dr.GetValue(1).ToString());
        //us.Password = Convert.ToString(dr.GetValue(2).ToString());
        //us.Email = Convert.ToString(dr.GetValue(3).ToString());
        //us.Tel = Convert.ToString(dr.GetValue(4).ToString());
        //us.Disabled = Convert.ToInt32(dr.GetValue(5).ToString());





        //DataTable dt = new DataTable();
        //SqlDataAdapter da = new SqlDataAdapter(cmd);
        //da.Fill(dt);
        //foreach (DataRow dr in dt.Rows)
        //{
        //    Users us = new Users();
        //    us.UserID = Convert.ToString(dr[0]);
        //    us.UserName = Convert.ToString(dr[1]);
        //    us.Password = Convert.ToString(dr[2]);
        //    us.Email = Convert.ToString(dr[3]);
        //    us.Tel = Convert.ToString(dr[4]);
        //    us.Disabled = Convert.ToInt32(dr[5]);
        //    users.Add(us);
        //}
        //return users;
    
    public int UpdateUser(Users users)
        {
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("pcd_UpdateUser", con);
                //cmd = new SqlCommand("pcd_UpdateUser", con);
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
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("pcd_SaveUsers", con);
            //cmd = new SqlCommand("pcd_UpdateUser", con);
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
        public  Users GetUsersByID(string id)
        {
            SqlConnection con = new SqlConnection(cs);

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
                users.Disabled = Convert.ToByte(dr[5]);

            }
            return users;



        }

        public int DeleteUser(string id)
        {
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("pcd_DeleteUsers", con);

            
            //cmd = new SqlCommand("pcd_DeleteUsers", con);
            cmd.Parameters.AddWithValue("@userID", id);           
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;

        }
        public List<Users> GetUsersByEmail(string search)
        {
            SqlConnection con = new SqlConnection(cs);
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
                        user.Disabled = (byte)reader["Disabled"];

                        users.Add(user);
                    }

                    reader.Close();
            

            return users;
        }


    }
}
