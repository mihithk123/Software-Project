using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Software_Project.Models
{
    
    public class MedicalHistoryM
    {
        public int HistoryID { get; set; }
        public int PatientID { get; set; }
        public DateTime VisitDate { get; set; }
        public string Notes { get; set; }
    }
}
