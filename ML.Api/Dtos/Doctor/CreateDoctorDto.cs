using ML.Api.Dtos.DoctorPhone;
using System.Collections.Generic;

namespace ML.Api.Dtos.Doctor
{
    public class CreateDoctorDto : BaseDoctorDto
    {
        public List<BaseDoctorPhoneDto>?Phones { get; set; }
    }
}
