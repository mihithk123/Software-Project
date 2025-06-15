

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
         
                string query = "SELECT ID, FristName, LastName, Phone, Age FROM Patient";
                SqlCommand command = new SqlCommand(query, con);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var patient = new PatientM
                        {
                            Id = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Phone = reader.IsDBNull(3) ? "" : reader.GetString(3),
                            Age = reader.IsDBNull(4) ? "" : reader.GetString(4)
                        };
                        patients.Add(patient);
                    }
                }
            }
            return patients;
        }

        public bool AddPatient(PatientM patient)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                
                string query = "INSERT INTO Patient (FristName, LastName, Phone, Age) VALUES (@FirstName, @LastName, @Phone, @Age)";
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@FirstName", patient.FirstName);
                command.Parameters.AddWithValue("@LastName", patient.LastName);
                command.Parameters.AddWithValue("@Phone", patient.Phone);
                command.Parameters.AddWithValue("@Age", patient.Age);
                int result = command.ExecuteNonQuery();
                return result > 0;
            }
        }

        public bool UpdatePatient(PatientM patient)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                
                string query = @"UPDATE Patient 
                                 SET FristName = @FirstName, LastName = @LastName, Phone = @Phone, Age = @Age 
                                 WHERE ID = @ID";
                SqlCommand command = new SqlCommand(query, con);
                
                
                command.Parameters.AddWithValue("@ID", patient.Id);
                command.Parameters.AddWithValue("@FirstName", patient.FirstName);
                command.Parameters.AddWithValue("@LastName", patient.LastName);
                command.Parameters.AddWithValue("@Phone", patient.Phone);
                command.Parameters.AddWithValue("@Age", patient.Age);
                int result = command.ExecuteNonQuery();
                return result > 0;
            }
        }

     
        public bool DeletePatient(int patientId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "DELETE FROM Patient WHERE ID = @ID";
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@ID", patientId);
                int result = command.ExecuteNonQuery();
                return result > 0;
            }
        }
    }
}