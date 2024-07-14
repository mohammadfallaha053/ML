namespace ML.Api.Dtos.InsuranceCompanyPatient
{
    public class CreateInsuranceCompanyPatientDto: BaseInsuranceCompanyPatientDto
    {
        public int InsuranceCompanyId { get; set; }

        public int PatientId { get; set; }
    }
}
