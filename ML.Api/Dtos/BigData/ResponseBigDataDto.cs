using ML.Api.Dtos.Doctor;
using ML.Api.Dtos.DoctorPhone;
using ML.Api.Dtos.InsuranceCompany;
using ML.Api.Dtos.Patient;

namespace ML.Api.Dtos.BigData
{
    public class ResponseBigDataDto
    {
        public ICollection<ResponseDoctorDto> Doctors { get; set; }
        public ICollection<ResponsePatientDto> Patiens { get; set; }

        public ICollection<ResponseInsuranceCompanyDto> InsuranceCompanys { get;set; }
       


    }
}
