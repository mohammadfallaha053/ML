using System.ComponentModel.DataAnnotations;

namespace ML.Api.Dtos.Doctor
{
    public class BaseDoctorDto
    {
        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string lastName { get; set; }

        [MaxLength(20)]
        public string Gender { get; set; }

        [MaxLength(100)]
        public string Specialization { get; set; }

        [EmailAddress]
        public string Email { get; set; }

     



    }
}
