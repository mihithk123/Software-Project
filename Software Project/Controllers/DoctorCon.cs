using Microsoft.Data.SqlClient;
using Software_Project.Models; // Ensure this points to your Models namespace
using System;
using System.Collections.ObjectModel;

namespace Software_Project.Controllers
{
    // Class name matches your file: DoctorCon
    public class DoctorCon
    {
        private readonly string connectionString = "Data Source=MIHITH💖😎\\SQLEXPRESS;Initial Catalog=CMS_System;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

        // Return type is now ObservableCollection<DocM>
        public ObservableCollection<DocM> GetAllDoctors()
        {
            var doctors = new ObservableCollection<DocM>();

            // The 'using' statement was already correct and is a good practice!
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT Id, [First Name], [Last Name], Phone, Years, Specialization FROM Doctorlogin";
                SqlCommand command = new SqlCommand(query, con);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Creating an instance of your DocM model
                        doctors.Add(new DocM
                        {
                            Id = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Phone = reader.IsDBNull(3) ? "" : reader.GetString(3),
                            ExperienceYears = reader.IsDBNull(4) ? 0 : Convert.ToInt32(reader.GetValue(4)),
                            Specialization = reader.GetString(5)
                        });
                    }
                }
            }
            return doctors;
        }

        // Parameter type is now DocM
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

        // Parameter type is now DocM
        public bool UpdateDoctor(DocM doctor)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = @"UPDATE Doctorlogin 
                                 SET Phone = @Phone, Years = @Years, Specialization = @Specialization 
                                 WHERE [First Name] = @FirstName AND [Last Name] = @LastName";
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

        public bool DeleteDoctor(string firstName, string lastName)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "DELETE FROM Doctorlogin WHERE [First Name] = @FirstName AND [Last Name] = @LastName";
                SqlCommand command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);

                int result = command.ExecuteNonQuery();
                return result > 0;
            }
        }
    }
}