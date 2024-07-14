using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Core.Models
{
    public class InsuranceCompany
    {
        public int InsuranceCompanyId { get; set; }


        [MaxLength(100)]
        public string?  Name { get; set; }
       
        [EmailAddress]
        public string? Email { get; set; }

        public List<InsuranceCompanyPhone> InsuranceCompanyPhones {  get; set; }


        public ICollection<Patient> Patients { get; set; }

        public List<InsuranceCompanyPatient> InsuranceCompanyPatients { get; set; }

        public List<Test> Tests { get; set; }


    }
}
