using ML.Api.Dtos.DoctorPhone;
using ML.Api.Dtos.PatientPhone;

namespace ML.Api.Dtos.Patient
{
    public class CreatePatientDto: BasePatientDto
    {
        public List<BasePatientPhoneDto>? Phones { get; set; }
    }
}
