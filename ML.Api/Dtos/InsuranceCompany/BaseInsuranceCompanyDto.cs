using System.ComponentModel.DataAnnotations;

namespace ML.Api.Dtos.InsuranceCompany
{
    public class BaseInsuranceCompanyDto
    {
        [MaxLength(100)]
        public string? Name { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
    }
}
