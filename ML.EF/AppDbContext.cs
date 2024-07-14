using Microsoft.EntityFrameworkCore;
using ML.Core.Models;
using ML.Core.Models.ForDataSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.EF
{
    public class AppDbContext : DbContext
    {
       public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>()
             .HasMany(p => p.InsuranceCompanies)
             .WithMany(p => p.Patients)
             .UsingEntity<InsuranceCompanyPatient>(
                j => j 
                .HasOne(i => i.InsuranceCompany)
                .WithMany(p => p.InsuranceCompanyPatients)
                .HasForeignKey(f => f.InsuranceCompanyId),
                j => j
                .HasOne(p => p.Patient)
                .WithMany(p => p.InsuranceCompanyPatients)
                .HasForeignKey(f => f.PatientId)  ,
                
                j =>
                {
                  
                    j.HasKey(K => new { K.InsuranceCompanyId, K.PatientId });
                } 
                );
                   
        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DoctorPhone> DoctorPhones { get; set; }


        public DbSet<User> Users { get; set; }

        public DbSet<UserPhone> UserPhones { get; set; }
        

        public DbSet<LaboratoryConstant> LaboratoryConstants { get; set; }
        public DbSet<LaboratoryConstantPhone> LaboratoryConstantPhones { get; set; }


        public DbSet<Group> Groups { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<PatientPhone> PatientPhones { get; set; }

        public DbSet<InsuranceCompany> InsuranceCompanies { get;set; }

        public DbSet<InsuranceCompanyPhone> InsuranceCompanyPhones { get; set; }


        public DbSet<Analyse> Analyses { get; set; }

        public DbSet<NaturalField> NaturalFields { get; set; }

        public DbSet<Test> Tests { get; set; }
        
        public DbSet<TestDetail> TestDetails { get; set; }

        public DbSet<Diabetes> Diabeteses { get; set; }

        public DbSet<HeartAttack> HeartAttacks { get; set;}

        public DbSet<CKD> CKDs { get; set;}






    }
}
