﻿using ML.Api.Dtos.DoctorPhone;
using ML.Api.Dtos.InsuranceCompany;
using ML.Api.Dtos.PatientPhone;
using System.ComponentModel.DataAnnotations;

namespace ML.Api.Dtos.Patient
{
    public class ResponsePatientWithInsuranceCompaniesDto : BasePatientDto 
    {
        public int PatientId { get; set; }

        public List<ResponsePatientPhoneDto> PatientPhones { get; set; }

        public List<ResponseInsuranceCompanyDto> InsuranceCompanies { get; set; }

    }
}
