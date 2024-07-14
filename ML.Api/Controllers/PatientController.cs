using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ML.Api.Dtos.Patient;
using ML.Core;
using ML.Core.Models;
using ML.EF;
using System.Numerics;

namespace ML.Api.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {

        private readonly IUintOfWork _uintOfWork;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public PatientController(IUintOfWork uintOfWork, IMapper mapper, AppDbContext context)
        {
            _uintOfWork = uintOfWork;
            _mapper = mapper;
            _context = context;

        }


        [HttpGet()]
        public async Task<IActionResult> GetAllAsync() => Ok(_mapper.Map<IEnumerable<ResponsePatientDto>>(await _uintOfWork.Patients.GetAllAsync(new[] { "PatientPhones" })));

        [HttpGet("WithInsuranceCompanies")]
        public async Task<IActionResult> GetAllWithInsuranceCompaniesAsync() => Ok(_mapper.Map<IEnumerable<ResponsePatientWithInsuranceCompaniesDto>>(await _uintOfWork.Patients.GetAllAsync(new[] { "PatientPhones", "InsuranceCompanies" })));


        [HttpGet("{patientId}/unused-analyses")]
        public async Task<IActionResult> GetUnusedAnalyses(int patientId)
        {
            var unusedAnalyses = from category in _context.Categories
                                 join analysis in _context.Analyses
                                 on category.CategoryId equals analysis.CategoryId
                                 join testDetail in _context.TestDetails
                                 on analysis.AnalyseId equals testDetail.AnalyseId into gj
                                 from subTestDetail in gj.DefaultIfEmpty()
                                 join test in _context.Tests
                                 on subTestDetail.TestId equals test.TestId into g
                                 from subTest in g.DefaultIfEmpty()
                                 where subTest == null || subTest.PatientId != patientId
                                 select new
                                 {
                                     CategoryId = category.CategoryId,
                                     CategoryName = category.CategoryName,
                                     AnalyseId = analysis.AnalyseId,
                                     AnalysisName = analysis.Name
                                 };

            var groupedResults = unusedAnalyses
                                 .GroupBy(x => new { x.CategoryId, x.CategoryName })
                                 .Select(g => new
                                 {
                                     CategoryId = g.Key.CategoryId,
                                     CategoryName = g.Key.CategoryName,
                                     Analyses = g.Select(a => new
                                     {
                                         a.AnalyseId,
                                         a.AnalysisName
                                     }).ToList()
                                 });

            return Ok(await groupedResults.ToListAsync());
        }
    



    [HttpGet("{GetById}")]
        public async Task<IActionResult> GetByIdAsync(int GetById)
        {
            var Patient = await _uintOfWork.Patients.Find(b => b.PatientId == GetById, new[] { "PatientPhones" });

            if (Patient == null) { return NotFound($"No Patient with ID:{GetById}"); }

            return Ok(_mapper.Map<ResponsePatientDto>(Patient));
        }



         [HttpGet("SearchByName")]
    public async Task<IActionResult> SearchByName(string firstName, string? lastName)
    {
            try
            {
                if (lastName == null || lastName == "")
                {
                    var patients = await _context.Patients
                        .Where(p => p.FirstName.Contains(firstName) )
                        .Select(p => new
                        {
                            p.PatientId,
                            p.FirstName,
                            p.lastName,
                            p.BirthDay,
                            p.Gender,
                            Tests = p.Tests.Select(t => new
                            {
                                t.TestId,
                                t.TestDate
                            }).ToList()
                        })
                        .ToListAsync();

                    if (patients == null || patients.Count == 0)
                    {
                        return NotFound("لم يتم العثور على أي مريض يطابق الأسماء المدخلة.");
                    }

                    return Ok(patients);
                }

                else
                {
                    var patients = await _context.Patients
                       .Where(p => p.FirstName.Contains(firstName) && p.lastName.Contains(lastName))
                       .Select(p => new
                       {
                           p.PatientId,
                           p.FirstName,
                           p.lastName,
                           p.BirthDay,
                           p.Gender,
                           Tests = p.Tests.Select(t => new
                           {
                               t.TestId,
                               t.TestDate
                           }).ToList()
                       })
                       .ToListAsync();

                    if (patients == null || patients.Count == 0)
                    {
                        return NotFound("لم يتم العثور على أي مريض يطابق الأسماء المدخلة.");
                    }

                    return Ok(patients);


                }
            }
            catch (Exception ex)
            {
                // تسجيل الخطأ لتتبع المشكلة
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "حدث خطأ غير متوقع.");
            }
    }
    
    

    
    
    
        [HttpPost()]
        public async Task<IActionResult> Add([FromBody] CreatePatientDto dto)
        {


            var Patient = await _uintOfWork.Patients.Add(_mapper.Map<Patient>(dto));
            _uintOfWork.Complete();
            var PatientId = Patient.PatientId;
            var Phones = dto.Phones;
            if (Phones != null)
            {
                foreach (var Phone in Phones) await _uintOfWork.PatientPhones.Add(new PatientPhone { PatientId = PatientId, Phone = Phone.Phone });
                _uintOfWork.Complete();
            }

            return Ok(_mapper.Map<ResponsePatientDto>(Patient));

        }


        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(int Id, [FromBody] UpdatePatientDto dto)
        {
            var old_Patient = await _uintOfWork.Patients.Find(b => b.PatientId == Id);
            if (old_Patient == null) return NotFound($"No Patient with Id:{Id}");
                

            old_Patient.FirstName = dto.FirstName;
            old_Patient.lastName = dto.lastName;
            old_Patient.BirthDay = dto.BirthDay;
            old_Patient.Gender = dto.Gender;
            _uintOfWork.Complete();

            var update_Patient = _uintOfWork.Patients.Update(old_Patient);
            _uintOfWork.Complete();
            return Ok(_mapper.Map<ResponsePatientDto>(update_Patient));

        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var Patient = await _uintOfWork.Patients.Find(b => b.PatientId == Id);

            if (Patient == null) return NotFound($"No Patient with Id:{Id}");

            var delete_Patient = _uintOfWork.Patients.Delete(Patient);
            _uintOfWork.Complete();
            return Ok();

        }


    }
}
