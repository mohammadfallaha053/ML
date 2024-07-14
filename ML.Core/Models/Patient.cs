using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Core.Models
{
    public class Patient
    {
        public int PatientId { get; set; }


        [MaxLength(100)]
        public string? FirstName { get; set; }
        [MaxLength(100)]
        public string? lastName { get; set; }
        [MaxLength(20)]
        public string? Gender { get; set; }

        //public ushort? BirthDay { get; set; }

        public DateTime ?BirthDay { get; set; }

        public List<PatientPhone> PatientPhones { get; set; }

        public ICollection<InsuranceCompany> InsuranceCompanies { get; set; }
        public List<InsuranceCompanyPatient> InsuranceCompanyPatients { get; set; }

        public List<Test> Tests { get; set; }

    }
}
