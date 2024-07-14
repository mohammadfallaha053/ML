using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Core.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }


        [MaxLength(100)]
        public string?  FirstName { get; set; }
        [MaxLength(100)]
        public string? lastName { get; set; }
        [MaxLength(20)]
        public string? Gender { get; set; }
        [MaxLength(100)]
        public string? Specialization { get; set; }
        [EmailAddress]
        public string? Email { get; set; }

        public List<DoctorPhone> DoctorPhones {  get; set; }

        public List<Test> Tests { get; set; } 

    }
}
