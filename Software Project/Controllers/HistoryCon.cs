

using Microsoft.Data.SqlClient;
using Software_Project.Models;
using System;
using System.Collections.ObjectModel;

namespace Software_Project.Controllers
{
    public class HistoryCon
    {
        private readonly string connectionString = "Data Source=MIHITH💖😎\\SQLEXPRESS;Initial Catalog=CMS_System;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

        
        public ObservableCollection<MedicalHistoryM> GetHistoryForPatient(int patientId)
        {
            var historyList = new ObservableCollection<MedicalHistoryM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT HistoryID, PatientID, VisitDate, Notes FROM MedicalHistory WHERE PatientID = @PatientID ORDER BY VisitDate DESC";
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@PatientID", patientId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var historyItem = new MedicalHistoryM
                        {
                            HistoryID = reader.GetInt32(0),
                            PatientID = reader.GetInt32(1),
                            VisitDate = reader.GetDateTime(2),
                            Notes = reader.GetString(3)
                        };
                        historyList.Add(historyItem);
                    }
                }
            }
            return historyList;
        }
    }
}