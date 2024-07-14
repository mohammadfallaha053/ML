using ML.Api.Dtos.InsuranceCompanyPhone;
using ML.Api.Dtos.Patient;

namespace ML.Api.Dtos.InsuranceCompany
{
    public class ResponseInsuranceCompanyDto : BaseInsuranceCompanyDto
    {
        public int InsuranceCompanyId { get; set; }

        public List<ResponseInsuranceCompanyPhoneDto> InsuranceCompanyPhones { get; set; }

        



    }
}
