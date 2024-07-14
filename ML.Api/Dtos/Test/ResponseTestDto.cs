using ML.Api.Dtos.Doctor;
using ML.Api.Dtos.InsuranceCompany;
using ML.Api.Dtos.Patient;
using ML.Api.Dtos.User;

namespace ML.Api.Dtos.Test
{
    public class ResponseTestDto :BaseTestDto
    {

        public int TestId { get; set; }

        public ResponsePatientDto Patient { get; set; }

        public ResponseUserDto User { get; set; }

        public ResponseInsuranceCompanyDto InsuranceCompany { get; set; }

        public ResponseDoctorDto Doctor { get; set; }
    }
}
