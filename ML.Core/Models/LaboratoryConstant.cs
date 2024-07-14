using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Core.Models
{
    public class LaboratoryConstant
    {
        public int LaboratoryConstantId { get; set; }


        [MaxLength(100)]
        public string? LabName { get; set; }
        [MaxLength(100)]
        public string? Address { get; set; }
        [MaxLength(100)]
        public string? MangarName { get; set; }
        [MaxLength(100)]
        public string? HospitalName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }

        public double? PriceOfUnit { get; set; }
        public byte[]? LogoImage { get; set; }
        public string? LogoImage2 { get; set; }
        public List<LaboratoryConstantPhone> LaboratoryConstantPhones { get; set; }

    }
}
