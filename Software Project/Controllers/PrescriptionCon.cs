// FILE: Software_Project/Controllers/PrescriptionCon.cs

using Microsoft.Data.SqlClient;
using Software_Project.Models;
using System;
using System.Collections.ObjectModel;

namespace Software_Project.Controllers
{
    public class PrescriptionCon
    {
        private readonly string connectionString = "Data Source=MIHITH💖😎\\SQLEXPRESS;Initial Catalog=CMS_System;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

        // READ all prescriptions
        public ObservableCollection<PrescriptionM> GetAllPrescriptions()
        {
            var prescriptions = new ObservableCollection<PrescriptionM>();
            // Query joins with Patient and Doctor tables to get their names
            string query = @"SELECT 
                                pr.PrescriptionID, pr.PatientID, pr.DoctorID, pr.Medication, pr.Dosage, pr.Frequency, pr.StartDate, pr.EndDate, pr.Notes,
                                p.FristName + ' ' + p.LastName AS PatientName,
                                d.[First Name] + ' ' + d.[Last Name] AS DoctorName
                             FROM Prescriptions pr
                             LEFT JOIN Patient p ON pr.PatientID = p.ID
                             LEFT JOIN Doctorlogin d ON pr.DoctorID = d.ID
                             ORDER BY pr.StartDate DESC";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(query, con);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        prescriptions.Add(new PrescriptionM
                        {
                            PrescriptionID = reader.GetInt32(0),
                            PatientID = reader.GetInt32(1),
                            DoctorID = reader.GetInt32(2),
                            Medication = reader.GetString(3),
                            Dosage = reader.IsDBNull(4) ? "" : reader.GetString(4),
                            Frequency = reader.IsDBNull(5) ? "" : reader.GetString(5),
                            StartDate = reader.GetDateTime(6),
                            EndDate = reader.IsDBNull(7) ? (DateTime?)null : reader.GetDateTime(7),
                            Notes = reader.IsDBNull(8) ? "" : reader.GetString(8),
                            PatientName = reader.IsDBNull(9) ? "N/A" : reader.GetString(9),
                            DoctorName = reader.IsDBNull(10) ? "N/A" : reader.GetString(10)
                        });
                    }
                }
            }
            return prescriptions;
        }

        // CREATE a new prescription
        public bool CreatePrescription(PrescriptionM pres)
        {
            string query = "INSERT INTO Prescriptions (PatientID, DoctorID, Medication, Dosage, Frequency, StartDate, EndDate, Notes) VALUES (@PatientID, @DoctorID, @Medication, @Dosage, @Frequency, @StartDate, @EndDate, @Notes)";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@PatientID", pres.PatientID);
                command.Parameters.AddWithValue("@DoctorID", pres.DoctorID);
                command.Parameters.AddWithValue("@Medication", pres.Medication);
                command.Parameters.AddWithValue("@Dosage", (object)pres.Dosage ?? DBNull.Value);
                command.Parameters.AddWithValue("@Frequency", (object)pres.Frequency ?? DBNull.Value);
                command.Parameters.AddWithValue("@StartDate", pres.StartDate);
                command.Parameters.AddWithValue("@EndDate", (object)pres.EndDate ?? DBNull.Value);
                command.Parameters.AddWithValue("@Notes", (object)pres.Notes ?? DBNull.Value);
                return command.ExecuteNonQuery() > 0;
            }
        }

        // UPDATE an existing prescription
        public bool UpdatePrescription(PrescriptionM pres)
        {
            string query = @"UPDATE Prescriptions SET 
                                PatientID = @PatientID, DoctorID = @DoctorID, Medication = @Medication, Dosage = @Dosage, Frequency = @Frequency, StartDate = @StartDate, EndDate = @EndDate, Notes = @Notes
                             WHERE PrescriptionID = @PrescriptionID";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@PrescriptionID", pres.PrescriptionID);
                command.Parameters.AddWithValue("@PatientID", pres.PatientID);
                command.Parameters.AddWithValue("@DoctorID", pres.DoctorID);
                command.Parameters.AddWithValue("@Medication", pres.Medication);
                command.Parameters.AddWithValue("@Dosage", (object)pres.Dosage ?? DBNull.Value);
                command.Parameters.AddWithValue("@Frequency", (object)pres.Frequency ?? DBNull.Value);
                command.Parameters.AddWithValue("@StartDate", pres.StartDate);
                command.Parameters.AddWithValue("@EndDate", (object)pres.EndDate ?? DBNull.Value);
                command.Parameters.AddWithValue("@Notes", (object)pres.Notes ?? DBNull.Value);
                return command.ExecuteNonQuery() > 0;
            }
        }
    }
}