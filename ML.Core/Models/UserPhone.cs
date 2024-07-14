using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Core.Models
{
    public class UserPhone
    {
        public int UserPhoneId { get; set; }
        public string Phone { get; set; }


        public int UserId { get; set; }

        public User User { get; set; }
    }
}
