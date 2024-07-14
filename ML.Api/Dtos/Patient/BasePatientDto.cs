using System.ComponentModel.DataAnnotations;

namespace ML.Api.Dtos.Patient
{
    public class BasePatientDto
    {
        [MaxLength(100)]
        public string? FirstName { get; set; }
        [MaxLength(100)]
        public string? lastName { get; set; }
        [MaxLength(20)]
        public string? Gender { get; set; }

        //public ushort BirthDay { get; set; }


        public DateTime? BirthDay { get; set; }
    }
}
