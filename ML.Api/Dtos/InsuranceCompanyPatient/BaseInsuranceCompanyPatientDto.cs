using System.ComponentModel.DataAnnotations;

namespace ML.Api.Dtos.InsuranceCompanyPatient
{
    public class BaseInsuranceCompanyPatientDto
    {
        

        public DateTime? RigesterDate { get; set; }

        [MaxLength(100)]
        public string Status { get; set; }

    }
}
