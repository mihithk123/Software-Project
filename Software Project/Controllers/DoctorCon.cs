

using Microsoft.Data.SqlClient;
using Software_Project.Models;
using System;
using System.Collections.ObjectModel;

namespace Software_Project.Controllers
{
    public class DoctorCon
    {
        private readonly string connectionString = "Data Source=MIHITH💖😎\\SQLEXPRESS;Initial Catalog=CMS_System;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

        

        public ObservableCollection<DocM> GetAllDoctors()
        {
            var doctors = new ObservableCollection<DocM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT ID, [First Name], [Last Name], Phone, Years, Specialization FROM Doctorlogin";
                SqlCommand command = new SqlCommand(query, con);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        
                        string idString = reader.IsDBNull(0) ? null : reader.GetValue(0).ToString();
                        string yearsString = reader.IsDBNull(4) ? null : reader.GetValue(4).ToString();

                        var doctor = new DocM
                        {
                            
                            Id = string.IsNullOrEmpty(idString) ? 0 : Convert.ToInt32(idString),

                            FirstName = reader.IsDBNull(1) ? "" : reader.GetString(1),
                            LastName = reader.IsDBNull(2) ? "" : reader.GetString(2),
                            Phone = reader.IsDBNull(3) ? "" : reader.GetString(3),

                            ExperienceYears = string.IsNullOrEmpty(yearsString) ? 0 : Convert.ToInt32(yearsString),

                            Specialization = reader.IsDBNull(5) ? "" : reader.GetString(5)
                        };
                        doctors.Add(doctor);
                    }
                }
            }
            return doctors;
        }

        

        public bool AddDoctor(DocM doctor)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "INSERT INTO Doctorlogin ([First Name], [Last Name], Phone, Years, Specialization) VALUES (@FirstName, @LastName, @Phone, @Years, @Specialization)";
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@FirstName", doctor.FirstName);
                command.Parameters.AddWithValue("@LastName", doctor.LastName);
                command.Parameters.AddWithValue("@Phone", doctor.Phone);
                command.Parameters.AddWithValue("@Years", doctor.ExperienceYears);
                command.Parameters.AddWithValue("@Specialization", doctor.Specialization);
                int result = command.ExecuteNonQuery();
                return result > 0;
            }
        }

        public bool UpdateDoctor(DocM doctor)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = @"UPDATE Doctorlogin 
                                 SET [First Name] = @FirstName, [Last Name] = @LastName, Phone = @Phone, Years = @Years, Specialization = @Specialization 
                                 WHERE ID = @ID";
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@ID", doctor.Id);
                command.Parameters.AddWithValue("@FirstName", doctor.FirstName);
                command.Parameters.AddWithValue("@LastName", doctor.LastName);
                command.Parameters.AddWithValue("@Phone", doctor.Phone);
                command.Parameters.AddWithValue("@Years", doctor.ExperienceYears);
                command.Parameters.AddWithValue("@Specialization", doctor.Specialization);
                int result = command.ExecuteNonQuery();
                return result > 0;
            }
        }

        public bool DeleteDoctor(int doctorId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "DELETE FROM Doctorlogin WHERE ID = @ID";
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@ID", doctorId);
                int result = command.ExecuteNonQuery();
                return result > 0;
            }
        }
    }
}