using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.SvgIcons;
using System.Configuration;

namespace WebApplication1.Models
{
    public class UserSvc : IDisposable
    {
        
        private static bool UpdateDatabase = false;
        private SqlConnection connection;
        string cs = ConfigurationManager.ConnectionStrings["basicdb"].ConnectionString;

        //SqlConnection con = new SqlConnection("Data Source=VIP\\SQL2017;Initial Catalog=BasicWeb;Integrated Security=True");
        public UserSvc(string constring)
        {
            this.connection = new SqlConnection(constring);
            this.connection.Open();
        }

        public UserSvc()
        {
        }

        public IList<Users> GetUsers()

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
                                UserID = reader.GetString(0),
                                UserName = reader.GetString(1),
                                Password = reader.GetString(2),
                                Email = reader.GetString(3),
                                Tel = reader.GetString(4),
                                Disabled = reader.GetByte(5)
                            };
                            users.Add(user);
                        }
                    }
                }


                return users;

            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }








        // Sử dụng danh sách usersList cho mục đích của bạn





        public IEnumerable<Users> Read()
        {
            return GetUsers();
        }

        public void Create(Users users)
        {
            //if (!UpdateDatabase)
            //{
            //    var first = GetUsers().OrderByDescending(e => e.UserID).FirstOrDefault();
            //    var id = (first != null) ? first.UserID : "AS0";

            //    users.UserID = id + 1;

            //    GetUsers().Add(users);
            //}
            //else
            //{
                using (SqlConnection connection = new SqlConnection(cs))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("pcd_SaveUsers", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@userid", users.UserID);
                        command.Parameters.AddWithValue("@username", users.UserName);
                        command.Parameters.AddWithValue("@password", users.Password);
                        command.Parameters.AddWithValue("@email", users.Email);
                        command.Parameters.AddWithValue("@tel", users.Tel);
                        command.Parameters.AddWithValue("@disabled", users.Disabled);

                    //SqlParameter outputParameter = new SqlParameter("@userid", SqlDbType.VarChar, 50);
                    //outputParameter.Direction = ParameterDirection.Output;
                    //command.Parameters.Add(outputParameter);

                    command.ExecuteNonQuery();

                    //users.UserID = outputParameter.Value.ToString();

                    //}
                }
            }
        }

        //public void Create(Users users)
        //{
        //    DataContext enti = new DataContext();
        //    if (!UpdateDatabase)
        //    {
        //        var first = GetUsers().OrderByDescending(e => e.UserID).FirstOrDefault();
        //        var id = (first != null) ? first.UserID : "AS" + 0;

        //        users.UserID = id + 1;


        //        GetUsers().Insert(0, users);
        //    }
        //    else
        //    {
        //        var entity = new Users();




        //        enti.SaveUser(entity);


        //        users.UserID = entity.UserID;
        //       entity.UserName = users.UserName;
        //        entity.Email = users.Email;
        //        entity.Password = users.Password;
        //        entity.Tel = users.Tel;
        //        entity.Disabled = users.Disabled;

        //    }
        //}

        //public void Update(Users users)
        //{



        //    Users users1 = new Users();
        //    if (!UpdateDatabase)
        //    {
        //        var target = One(e => e.UserID == users.UserID);

        //        if (target != null)
        //        {
        //            target.UserName = users.UserName;
        //            target.Email = users.Email;
        //            target.Password = users.Password;
        //            target.Tel = users.Tel;
        //            target.Disabled = users.Disabled;
        //            target.UserID = users.UserID;

        //        }
        //    }
        //    else
        //    {
        //        var entity = new Users();

        //        entity.UserName = users.UserName;
        //        entity.Email = users.Email;
        //        entity.Password = users.Password;
        //        entity.Tel = users.Tel;
        //        entity.Disabled = users.Disabled;


        //        entities.UpdateUser(entity);

        //    }
        //}
        public void Update(Users users)
        {
            //if (!UpdateDatabase)
            //{
            //    var target = One(e => e.UserID == users.UserID);

            //    if (target != null)
            //    {
            //        target.UserName = users.UserName;
            //        target.Email = users.Email;
            //        target.Password = users.Password;
            //        target.Tel = users.Tel;
            //        target.Disabled = users.Disabled;
            //        target.UserID = users.UserID;
            //    }
            //}
            //else
            //{
                using (SqlConnection connection = new SqlConnection(cs))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("pcd_UpdateUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                       
                        command.Parameters.AddWithValue("@userid", users.UserID);
                        command.Parameters.AddWithValue("@username", users.UserName);
                        command.Parameters.AddWithValue("@email", users.Email);
                        command.Parameters.AddWithValue("@password", users.Password);
                        command.Parameters.AddWithValue("@tel", users.Tel);
                        command.Parameters.AddWithValue("@disabled", users.Disabled);

                        
                        command.ExecuteNonQuery();
                    }
               }
            //}
        }

        public void Destroy(Users users)
        {
            //if (!UpdateDatabase)
            //{
            //    var target = GetUsers().FirstOrDefault(p => p.UserID == users.UserID);
            //    if (target != null)
            //    {
            //        GetUsers().Remove(target);
            //    }
            //}
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("pcd_DeleteUsers", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số cho stored procedure để xác định người dùng cần xóa
                    command.Parameters.AddWithValue("@userID", users.UserID);

                    // Thực thi stored procedure để xóa người dùng
                    command.ExecuteNonQuery();
                }
            }
        }

        public Users One(Func<Users, bool> predicate)
        {
            return GetUsers().FirstOrDefault(predicate);

        }
      
    }
}