using Microsoft.Data.SqlClient;
using Software_Project.Models;
using System;
using System.Collections.ObjectModel;

namespace Software_Project.Controllers
{
    public class PatientCon
    {
        private readonly string connectionString = "Data Source=MIHITH💖😎\\SQLEXPRESS;Initial Catalog=CMS_System;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

        public ObservableCollection<PatientM> GetAllPatients()
        {
            var patients = new ObservableCollection<PatientM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        patients.Add(new PatientM
                        {
                            Id = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Phone = reader.GetString(3),

                            // *** THIS IS THE FIX FOR YOUR ERROR ***
                            // Read the 'Age' column as an integer, not a string.
                            Age = reader.GetInt32(4)
                        });
                    }
                }
            }
            return patients;
        }

        public bool AddPatient(PatientM patient)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // FIX: Using the correct 'FirstName' column name.
                string query = "INSERT INTO Patient (FirstName, LastName, Phone, Age) VALUES (@FirstName, @LastName, @Phone, @Age)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@FirstName", patient.FirstName);
                cmd.Parameters.AddWithValue("@LastName", patient.LastName);
                cmd.Parameters.AddWithValue("@Phone", patient.Phone);
                cmd.Parameters.AddWithValue("@Age", patient.Age); // This now correctly sends an int.
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdatePatient(PatientM patient)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // FIX: Using the correct 'FirstName' column name.
                string query = "UPDATE Patient SET Phone = @Phone, Age = @Age WHERE FirstName = @FirstName AND LastName = @LastName";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@FirstName", patient.FirstName);
                cmd.Parameters.AddWithValue("@LastName", patient.LastName);
                cmd.Parameters.AddWithValue("@Phone", patient.Phone);
                cmd.Parameters.AddWithValue("@Age", patient.Age); // This now correctly sends an int.
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool DeletePatient(string firstName, string lastName)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // FIX: Using the correct 'FirstName' column name.
                string query = "DELETE FROM Patient WHERE FirstName = @FirstName AND LastName = @LastName";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}