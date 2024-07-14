using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ML.Api.Dtos.User
{
    public class BaseUserDto
    {
       

        [MaxLength(100)]
        public string FirstName { get; set; }
        [MaxLength(100)]
        public string LastName { get; set; }
        [MaxLength(100)]
        public string UserName { get; set; }
        
        [MaxLength(150)]
        public string Email { get; set; }

        public byte[]? Image { get; set; }

        public string? Image2 { get; set; }

    }
}
