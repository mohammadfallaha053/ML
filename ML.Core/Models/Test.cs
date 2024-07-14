using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Core.Models
{
    public class Test
    {
            public int TestId { get; set; }


            public DateTime ? TestDate { get; set; }
         
            public double  Discount { get; set; }

            public Patient Patient { get; set; }

            public int PatientId { get; set; }  

            public Doctor Doctor { get; set; }

            public int DoctorId { get; set; }

            public User User { get; set; }

            public int UserId { get; set; }

            public InsuranceCompany InsuranceCompany { get; set; }

            public int? InsuranceCompanyId { get;set; }

            public List<TestDetail> TestDetails { get; set; }



    }
}
