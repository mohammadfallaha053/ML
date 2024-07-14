using ML.Api.Dtos.InsuranceCompanyPhone;
using ML.Api.Dtos.UserPhone;

namespace ML.Api.Dtos.InsuranceCompany
{
    public class CreateInsuranceCompanyDto : BaseInsuranceCompanyDto
    {
        public List<BaseInsuranceCompanyPhoneDto>? Phones { get; set; }

    }
}
