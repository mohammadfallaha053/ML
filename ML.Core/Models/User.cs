using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Core.Models
{
    public class User
    {
        public int UserId { get; set; }

        [MaxLength(100)]
        public string? FirstName { get; set; }
        [MaxLength(100)]
        public string? LastName { get; set; }
        [MaxLength(100)]
        
        public string? UserName { get; set; }
        [MaxLength(255)]
        public string? Password { get; set; }
        [MaxLength(150)]
        public string? Email { get; set; }

        public byte[]? Image { get; set; }

        public string? Image2 { get; set; }


        public List<UserPhone> UserPhones { get; set; }

        public List<Test> Tests { get; set; }


    }
}
