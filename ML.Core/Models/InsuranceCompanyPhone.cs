using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Core.Models
{
    public class InsuranceCompanyPhone
    {
        public int InsuranceCompanyPhoneId { get; set; }
        public string Phone { get; set; }
        public InsuranceCompany InsuranceCompany { get; set; }
         
        public int InsuranceCompanyId { get; set; }
    }
}
