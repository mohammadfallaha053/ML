using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Core.Models
{
    public class PatientPhone
    {
        public int PatientPhoneId { get; set; }
        public string Phone { get; set; }
        public Patient Patient { get; set; }
         
        public int PatientId { get; set; }
    }
}
