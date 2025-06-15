

using System;

namespace Software_Project.Models
{
    
    public class PrescriptionM
    {
        public int PrescriptionID { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public string Medication { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; } 
        public string Notes { get; set; }

        
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
    }
}