using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Software_Project.Models
{
    public class AppointmentM
    {
        public int AppointmentID { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
    }
}
