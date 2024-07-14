using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Core.Models
{
    public class LaboratoryConstantPhone
    {
        public int LaboratoryConstantPhoneId { get; set; } 
        
        public int LaboratoryConstantId { get; set; }

        public LaboratoryConstant LaboratoryConstant;
        public string Phone { get; set; }
    }
}
