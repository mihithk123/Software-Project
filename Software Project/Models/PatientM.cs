using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Software_Project.Models
{
    public class PatientM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }

        // --- CHANGE HERE ---
        // Age is now a string to match the database column.
        public int Age { get; set; }
    }
}
