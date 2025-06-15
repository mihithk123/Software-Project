

using Microsoft.Data.SqlClient;
using Software_Project.Models;
using System;
using System.Collections.ObjectModel;

namespace Software_Project.Controllers
{

    public class AppointmentCon
    {
        private readonly string connectionString = "Data Source=MIHITH💖😎\\SQLEXPRESS;Initial Catalog=CMS_System;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

        public ObservableCollection<AppointmentM> GetAllAppointments()
        {
            var appointments = new ObservableCollection<AppointmentM>();
            
            string query = @"SELECT 
                                a.AppointmentID, a.PatientID, a.DoctorID, a.AppointmentDate, a.Status, a.Reason,
                                p.FristName + ' ' + p.LastName AS PatientName,
                                d.[First Name] + ' ' + d.[Last Name] AS DoctorName
                             FROM Appointments a
                             LEFT JOIN Patient p ON a.PatientID = p.ID
                             LEFT JOIN Doctorlogin d ON a.DoctorID = d.ID
                             ORDER BY a.AppointmentDate DESC";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(query, con);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        appointments.Add(new AppointmentM
                        {
                            AppointmentID = reader.GetInt32(0),
                            PatientID = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                            DoctorID = reader.IsDBNull(2) ? 0 : reader.GetInt32(2),
                            AppointmentDate = reader.IsDBNull(3) ? DateTime.MinValue : reader.GetDateTime(3),
                            Status = reader.IsDBNull(4) ? "" : reader.GetString(4),
                            Reason = reader.IsDBNull(5) ? "" : reader.GetString(5),
                            PatientName = reader.IsDBNull(6) ? "N/A" : reader.GetString(6),
                            DoctorName = reader.IsDBNull(7) ? "N/A" : reader.GetString(7)
                        });
                    }
                }
            }
            return appointments;
        }

        public bool BookAppointment(AppointmentM appt)
        {
            string query = "INSERT INTO Appointments (PatientID, DoctorID, AppointmentDate, Status, Reason) VALUES (@PatientID, @DoctorID, @AppointmentDate, @Status, @Reason)";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@PatientID", appt.PatientID);
                command.Parameters.AddWithValue("@DoctorID", appt.DoctorID);
                command.Parameters.AddWithValue("@AppointmentDate", appt.AppointmentDate);
                command.Parameters.AddWithValue("@Status", appt.Status);
                command.Parameters.AddWithValue("@Reason", (object)appt.Reason ?? DBNull.Value);
                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateAppointment(AppointmentM appt)
        {
            string query = "UPDATE Appointments SET PatientID = @PatientID, DoctorID = @DoctorID, AppointmentDate = @AppointmentDate, Status = @Status, Reason = @Reason WHERE AppointmentID = @AppointmentID";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@AppointmentID", appt.AppointmentID);
                command.Parameters.AddWithValue("@PatientID", appt.PatientID);
                command.Parameters.AddWithValue("@DoctorID", appt.DoctorID);
                command.Parameters.AddWithValue("@AppointmentDate", appt.AppointmentDate);
                command.Parameters.AddWithValue("@Status", appt.Status);
                command.Parameters.AddWithValue("@Reason", (object)appt.Reason ?? DBNull.Value);
                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool CancelAppointment(int appointmentId)
        {
            string query = "UPDATE Appointments SET Status = 'Cancelled' WHERE AppointmentID = @AppointmentID";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@AppointmentID", appointmentId);
                return command.ExecuteNonQuery() > 0;
            }
        }
    }
}