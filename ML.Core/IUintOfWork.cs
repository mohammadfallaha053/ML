using ML.Core.Interfaces;
using ML.Core.Models;
using ML.Core.Models.ForDataSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Core
{
    public interface IUintOfWork:IDisposable 
    {
        IBaseRepository<Doctor> Doctors { get; }
        IBaseRepository<DoctorPhone> DoctorPhones { get; }

        IBaseRepository<User> Users { get; }
        IBaseRepository<UserPhone> UserPhones { get; }

        IBaseRepository<Patient> Patients { get; }
        IBaseRepository<PatientPhone> PatientPhones { get; }


        IBaseRepository<InsuranceCompany> InsuranceCompanies { get; }
        IBaseRepository<InsuranceCompanyPhone> InsuranceCompanyPhones { get; }

        IBaseRepository<InsuranceCompanyPatient> InsuranceCompanyPatients { get; }


        IBaseRepository<LaboratoryConstant> LaboratoryConstants { get; }
        IBaseRepository<LaboratoryConstantPhone> LaboratoryConstantPhones { get; }

        IBaseRepository<Group> Groups { get; }
        IBaseRepository<Category> Categories { get; }

        IBaseRepository<Analyse> Analyses { get; }

        IBaseRepository<NaturalField> NaturalFields { get; }

        IBaseRepository<Test> Tests { get; }

        IBaseRepository<TestDetail> TestDetails { get; }

        IBaseRepository<CKD> CKDs { get; }

       IBaseRepository<Diabetes> Diabeteses { get; }

        IBaseRepository<HeartAttack> HeartAttacks { get; }
        int Complete();


    }
}
