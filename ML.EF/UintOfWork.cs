using ML.Core;
using ML.Core.Interfaces;
using ML.Core.Models;
using ML.Core.Models.ForDataSet;
using ML.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.EF
{
    public class UintOfWork : IUintOfWork
    {
        private readonly AppDbContext _context;


       
        public IBaseRepository<Doctor> Doctors {  get;private set; }


        public IBaseRepository<DoctorPhone> DoctorPhones { get; private set; }


        public IBaseRepository<User> Users { get; private set; }

        public IBaseRepository<UserPhone> UserPhones { get; private set; }


        public IBaseRepository<LaboratoryConstant> LaboratoryConstants { get; private set; }
        public IBaseRepository<LaboratoryConstantPhone> LaboratoryConstantPhones { get; private set; }

        public IBaseRepository<Patient> Patients { get; private set; }

        public IBaseRepository<PatientPhone> PatientPhones { get; private set; }

        public IBaseRepository<InsuranceCompany> InsuranceCompanies { get; private set; }

        public IBaseRepository<InsuranceCompanyPhone> InsuranceCompanyPhones { get; private set; }

        public IBaseRepository<InsuranceCompanyPatient> InsuranceCompanyPatients { get; private set; }

        public IBaseRepository<Group> Groups { get; private set; }

        public IBaseRepository<Category> Categories { get; private set; }

        public IBaseRepository<Analyse> Analyses { get; private set; }

        public IBaseRepository<NaturalField> NaturalFields { get; private set; }

        public IBaseRepository<Test> Tests { get; private set; }

        public IBaseRepository<TestDetail> TestDetails { get; private set; }

        public IBaseRepository<CKD> CKDs { get; private set; }

        public IBaseRepository<Diabetes> Diabeteses {  get; private set; }

        public IBaseRepository<HeartAttack> HeartAttacks { get; private set; }

        public UintOfWork(AppDbContext context)
        {
            _context = context;
            
            Doctors     = new BaseRepository<Doctor>(_context);
            Users       = new BaseRepository<User>(_context);
            DoctorPhones= new BaseRepository<DoctorPhone>(_context);
            UserPhones  = new BaseRepository<UserPhone>(_context);
            LaboratoryConstants = new BaseRepository<LaboratoryConstant>(_context); 
            LaboratoryConstantPhones = new BaseRepository<LaboratoryConstantPhone>(_context);
            Patients = new BaseRepository<Patient>(_context);
            PatientPhones= new BaseRepository<PatientPhone>(_context);
            InsuranceCompanies = new BaseRepository<InsuranceCompany>(_context);
            InsuranceCompanyPhones=new BaseRepository<InsuranceCompanyPhone>(_context);
            InsuranceCompanyPatients = new BaseRepository<InsuranceCompanyPatient>(_context);
            Groups = new BaseRepository<Group>(_context);
            Categories= new BaseRepository<Category>(_context);
            NaturalFields= new BaseRepository<NaturalField>(_context);
            Analyses = new BaseRepository<Analyse>(_context);
            Tests=new BaseRepository<Test>(_context);
            TestDetails=new BaseRepository<TestDetail>(_context);
            CKDs=new BaseRepository<CKD>(_context);
            Diabeteses=new BaseRepository<Diabetes>(_context);
            HeartAttacks=new BaseRepository<HeartAttack>(_context);
        }

        public int Complete()
        {
            
            return _context.SaveChanges();
        }

        public void Dispose()
        {
              _context.Dispose();
        }



    }
}
