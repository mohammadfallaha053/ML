    using System.ComponentModel.DataAnnotations;

namespace ML.Api.Dtos.LaboratoryConstant
{
    public class BaseLaboratoryConstantDto
    {

        [MaxLength(100)]
        public string LabName { get; set; }
        [MaxLength(100)]
        public string Address { get; set; }
        [MaxLength(100)]
        public string MangarName { get; set; }
        [MaxLength(100)]
        public string HospitalName { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        public double PriceOfUnit { get; set; }
        public byte[]? LogoImage { get; set; }
        public string? LogoImage2 { get; set; }
    }
}
