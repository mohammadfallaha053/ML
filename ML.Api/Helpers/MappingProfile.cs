using AutoMapper;
using ML.Api.Dtos.Analyse;
using ML.Api.Dtos.Category;
using ML.Api.Dtos.Classification;
using ML.Api.Dtos.Doctor;
using ML.Api.Dtos.DoctorPhone;
using ML.Api.Dtos.Group;
using ML.Api.Dtos.InsuranceCompany;
using ML.Api.Dtos.InsuranceCompanyPatient;
using ML.Api.Dtos.InsuranceCompanyPhone;
using ML.Api.Dtos.LaboratoryConstant;
using ML.Api.Dtos.LaboratoryConstantPhone;
using ML.Api.Dtos.NaturalField;
using ML.Api.Dtos.Patient;
using ML.Api.Dtos.PatientPhone;
using ML.Api.Dtos.Test;
using ML.Api.Dtos.TestDetail;
using ML.Api.Dtos.User;
using ML.Api.Dtos.UserPhone;
using ML.Core.Models;
using ML.Core.Models.ForDataSet;
namespace ML.Api.Helpers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Doctor, ResponseDoctorDto>();
            CreateMap<CreateDoctorDto, Doctor>();
            CreateMap<DoctorPhone, ResponseDoctorPhoneDto>();
            CreateMap<CreateDoctorPhoneDto, DoctorPhone>();



            CreateMap<User, ResponseUserDto>();
            CreateMap<CreateUserDto, User>();
            CreateMap<UserPhone, ResponseUserPhoneDto>();
            CreateMap<CreateUserPhoneDto, UserPhone>();


            CreateMap<Patient, ResponsePatientDto>();
            CreateMap<Patient, ResponsePatientWithInsuranceCompaniesDto>();
            CreateMap<CreatePatientDto, Patient>();
            CreateMap<PatientPhone, ResponsePatientPhoneDto>();
            CreateMap<CreatePatientPhoneDto, PatientPhone>();



            CreateMap<LaboratoryConstant, ResponseLaboratoryConstantDto>();
            CreateMap<CreateLaboratoryConstantDto, LaboratoryConstant>();
            CreateMap<LaboratoryConstantPhone, ResponseLaboratoryConstantPhoneDto>();
            CreateMap<CreateLaboratoryConstantPhoneDto, LaboratoryConstantPhone>();



            CreateMap<InsuranceCompany, ResponseInsuranceCompanyDto>();
            CreateMap<InsuranceCompany, ResponseInsuranceCompanyWithPatientDto>();
            CreateMap<CreateInsuranceCompanyDto, InsuranceCompany>();

            CreateMap<InsuranceCompanyPhone, ResponseInsuranceCompanyPhoneDto>();
            CreateMap<CreateInsuranceCompanyPhoneDto, InsuranceCompanyPhone>();

            CreateMap<InsuranceCompanyPatient, ResponseInsuranceCompanyPatientDto>();
            CreateMap<CreateInsuranceCompanyPatientDto, InsuranceCompanyPatient>();


            CreateMap<Group, ResponseGroupDto>();
            CreateMap<CreateGroupDto, Group>();

            CreateMap <Category, ResponseCategoryDto>();
            CreateMap<CreateCategoryDto, Category>();


            CreateMap<Analyse, ResponseAnalyseDto>();
            CreateMap<CreateAnalyseDto, Analyse>();


            CreateMap<NaturalField, ResponseNaturalFieldDto>();
            CreateMap<CreateNaturalFieldDto, NaturalField>();

            CreateMap<Test, ResponseTestDto>();
            CreateMap<CreateTestDto,Test>();


            CreateMap<TestDetail, ResponseTestDetailDto>();

            CreateMap<CreateTestDetailDto, TestDetail>();

            CreateMap<CKDDto, CKD>();

            CreateMap<DiabetesDto, Diabetes>();

            CreateMap<HeartAttackDto, HeartAttack>();



        }
    }
}
