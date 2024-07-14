using ML.Core.Models;

namespace ML.Api.Dtos.Test
{
    public class BaseTestDto 
    {
        


        public DateTime? TestDate { get; set; }

        public double  Discount { get; set; }


        public int PatientId { get; set; }


        public int UserId { get; set; }

        public int  DoctorId { get; set; }
 

        public int? InsuranceCompanyId { get; set; }

        

    }
}
