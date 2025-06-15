

using Microsoft.Data.SqlClient;
using Software_Project.Models;
using System; 
using System.Collections.ObjectModel;

namespace Software_Project.Controllers
{
    public class OthersCon
    {
        private readonly string connectionString = "Data Source=MIHITH💖😎\\SQLEXPRESS;Initial Catalog=CMS_System;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

        
        public ObservableCollection<OthM> GetAllUsers()
        {
            var users = new ObservableCollection<OthM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT FirstName, LastName, Phone, Age, UserName FROM OtherUsers";
                SqlCommand command = new SqlCommand(query, con);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var user = new OthM
                        {
                            FirstName = reader.IsDBNull(0) ? "" : reader.GetString(0),
                            LastName = reader.IsDBNull(1) ? "" : reader.GetString(1),
                            Phone = reader.IsDBNull(2) ? "" : reader.GetString(2),

                            
                            Age = reader.IsDBNull(3) ? 0 : Convert.ToInt32(reader.GetValue(3)),

                            UserName = reader.IsDBNull(4) ? "" : reader.GetString(4)
                        };
                        users.Add(user);
                    }
                }
            }
            return users;
        }

       
        public bool AddUser(OthM user)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "INSERT INTO OtherUsers (FirstName, LastName, Phone, Age, UserName) VALUES (@FirstName, @LastName, @Phone, @Age, @UserName)";
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@FirstName", user.FirstName);
                command.Parameters.AddWithValue("@LastName", user.LastName);
                command.Parameters.AddWithValue("@Phone", user.Phone);
                command.Parameters.AddWithValue("@Age", user.Age);
                command.Parameters.AddWithValue("@UserName", user.UserName);
                int result = command.ExecuteNonQuery();
                return result > 0;
            }
        }

     
        public bool UpdateUser(OthM user)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = @"UPDATE OtherUsers 
                                 SET FirstName = @FirstName, LastName = @LastName, Phone = @Phone, Age = @Age 
                                 WHERE UserName = @UserName";
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@FirstName", user.FirstName);
                command.Parameters.AddWithValue("@LastName", user.LastName);
                command.Parameters.AddWithValue("@Phone", user.Phone);
                command.Parameters.AddWithValue("@Age", user.Age);
                command.Parameters.AddWithValue("@UserName", user.UserName);
                int result = command.ExecuteNonQuery();
                return result > 0;
            }
        }

       
        public bool DeleteUser(string userName)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "DELETE FROM OtherUsers WHERE UserName = @UserName";
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@UserName", userName);
                int result = command.ExecuteNonQuery();
                return result > 0;
            }
        }
    }
}