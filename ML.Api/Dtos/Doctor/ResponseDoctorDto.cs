
using ML.Api.Dtos.DoctorPhone;
using ML.Core.Models;

namespace ML.Api.Dtos.Doctor
{
    public class ResponseDoctorDto :BaseDoctorDto
    {
        public int DoctorId { get; set; }

        public List<ResponseDoctorPhoneDto> DoctorPhones {get; set;}
    }
}
