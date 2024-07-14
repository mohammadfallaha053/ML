using ML.Api.Dtos.PatientPhone;
using ML.Api.Dtos.UserPhone;
using System.ComponentModel.DataAnnotations;

namespace ML.Api.Dtos.User
{
    public class CreateUserDto :BaseUserDto
    {
        public List<BaseUserPhoneDto>? Phones { get; set; }

        [MaxLength(50)]
        public string Password { get; set; }

    }
}
