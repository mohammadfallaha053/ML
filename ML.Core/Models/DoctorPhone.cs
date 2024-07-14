using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Core.Models
{
    public class DoctorPhone
    {
        public int DoctorPhoneId { get; set; }
        public string Phone { get; set; }
        public Doctor Doctor { get; set; }
         
        public int DoctorId { get; set; }
    }
}
