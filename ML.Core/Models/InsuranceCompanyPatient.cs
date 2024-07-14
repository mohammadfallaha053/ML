using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Core.Models
{
    public class InsuranceCompanyPatient
    {
       public int InsuranceCompanyId { get; set; }

       public InsuranceCompany InsuranceCompany { get; set; }


        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public DateTime? RigesterDate { get; set; }
       
        [MaxLength(100)]
        public string Status { get; set; }



    }
}
