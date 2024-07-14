using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ML.Api.Dtos.Test;
using ML.Core;
using ML.Core.Models;
using ML.EF;
using System.Drawing;
using System.Linq;

namespace ML.Api.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        private readonly IUintOfWork _uintOfWork;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public TestController(IUintOfWork uintOfWork, IMapper mapper, AppDbContext context)
        {
            _uintOfWork = uintOfWork;
            _mapper = mapper;
            _context = context;
        }


        [HttpGet()]
        public async Task<IActionResult> GetAllAsync() => Ok(_mapper.Map<IEnumerable<ResponseTestDto>>(await _uintOfWork.Tests.GetAllAsync()));
        [HttpGet("GetAllWithPatient")]
        public async Task<IActionResult> GetAllWithPatientAsync() => Ok(_mapper.Map<IEnumerable<ResponseTestDto>>(await _uintOfWork.Tests.GetAllAsync(new[] { "Patient" })));
               
        
        [HttpGet("GetAllWithUser")]
        public async Task<IActionResult> GetAllWithUserAsync() => Ok(_mapper.Map<IEnumerable<ResponseTestDto>>(await _uintOfWork.Tests.GetAllAsync(new[] { "User" })));

        [HttpGet("GetAllWithDoctor")]
        public async Task<IActionResult> GetAllWithDoctorAsync() => Ok(_mapper.Map<IEnumerable<ResponseTestDto>>(await _uintOfWork.Tests.GetAllAsync(new[] { "Doctor" })));

        [HttpGet("GetAllWithDoctorAndPatientAndUser")]
        public async Task<IActionResult> GetAllWithDoctorAndPatientAsync()
        {
            //DateTime currenteDate = DateTime.UtcNow.Date.AddDays(-1);
            //var t = await _context.Tests.Where(entry => entry.TestDate >= currenteDate).ToListAsync();
            return  Ok(_mapper.Map<IEnumerable<ResponseTestDto>>(await _uintOfWork.Tests.GetAllAsync(new[] { "Doctor", "Patient", "User" })));

        }

        [HttpGet("GetByFromDateToDate")]
        public async Task<IActionResult> GetByFromDateToDate(DateTime from,DateTime to) 
        {
            var t=await _context.Tests.Where(entry => entry.TestDate >= from)
            .Where(entry => entry.TestDate <= to).Include("Doctor").Include("Patient").Include("User").ToListAsync();   
             return Ok(_mapper.Map<IEnumerable<ResponseTestDto>>(t));
            
        }

       



        [HttpGet("GetByToday")]
        public async Task<IActionResult> GetByToday()
        {
            DateTime currenteDate = DateTime.UtcNow.Date.AddDays(-1);
            var t = await _context.Tests.Where(entry => entry.TestDate >= currenteDate).Include("Doctor").Include("Patient").Include("User").OrderByDescending(a => a.TestDate).ToListAsync();
     
            return Ok(_mapper.Map<IEnumerable<ResponseTestDto>>(t));

        }


        [HttpGet("{GetById}")]
        public async Task<IActionResult> GetByIdAsync(int GetById)
        {
            var Test = await _uintOfWork.Tests.Find(b => b.TestId == GetById);

            if (Test == null) { return NotFound($"No Test with ID:{GetById}"); }

            return Ok(_mapper.Map<ResponseTestDto>(Test));
        }





        [HttpGet("GetTestWithGroupByCategory/{testId}")]
        public IActionResult t(int testId)
        {
            var result = _context.Tests
                                 .Include(t => t.Patient)
                                 .Include(t => t.Doctor)
                                 .Include(t => t.TestDetails)
                                 .ThenInclude(td => td.Analyse)
                                 .ThenInclude(a => a.Category)
                                 .Where(t => t.TestId == testId)
                                 .Select(t => new
                                 {
                                     TestId = t.TestId,
                                     TestDate = t.TestDate,
                                     PatientName = t.Patient.FirstName + " " + t.Patient.lastName,
                                     DoctorName = t.Doctor.FirstName + " " + t.Doctor.lastName,
                                     PatientBirth = t.Patient.BirthDay,
                                     PatientGender = t.Patient.Gender,
                                     TestDetails = t.TestDetails.Select(td => new
                                     {
                                         AnalyseName = td.Analyse.Name,
                                         AnalyseId = td.Analyse.AnalyseId,
                                         NUint =td.Analyse.NUint,
                                         CategoryName = td.Analyse.Category.CategoryName,
                                         Result = td.Result,
                                         TestDetailId=td.TestDetailId
                                     }).ToList()
                                 }).FirstOrDefault();

            if (result == null)
            {
                return NotFound();
            }

            var groupedResult = result.TestDetails
                                      .GroupBy(td => td.CategoryName)
                                      .Select(g => new
                                      {
                                          CategoryName = g.Key,
                                          Details = g.ToList()
                                      }).ToList();


            return Ok(new
            {   result.PatientGender,
                result.PatientBirth,
                result.TestId,
                result.TestDate,
                result.PatientName,
                result.DoctorName,
                
      
                GroupedTestDetails = groupedResult
            });
        }



        [HttpGet("GetTestWihtNaturalFieldsGroupByCategory/{testId}")]
        public IActionResult t2(int testId)
        {
            var result = _context.Tests
     .Include(t => t.Patient)
     .Include(t => t.Doctor)
     .Include(t => t.TestDetails)
         .ThenInclude(td => td.Analyse)
             .ThenInclude(a => a.Category)
     .Include(t => t.TestDetails)
         .ThenInclude(td => td.Analyse)
             .ThenInclude(a => a.NaturalFields) // تضمين NaturalFields بدون شروط إضافية
     .Where(t => t.TestId == testId)
     .Select(t => new
     {
         TestId = t.TestId,
         TestDate = t.TestDate,
         PatientName = t.Patient.FirstName + " " + t.Patient.lastName,
         DoctorName = t.Doctor.FirstName + " " + t.Doctor.lastName,
         DoctorGender = t.Doctor.Gender,
         PatientBirth = t.Patient.BirthDay,
         PatientGender = t.Patient.Gender,
         TestDetails = t.TestDetails.Select(td => new
         {
             AnalyseName = td.Analyse.Name,
             AnalyseId = td.Analyse.AnalyseId,
             NUint = td.Analyse.NUint,
             CategoryName = td.Analyse.Category.CategoryName,
             Result = td.Result,
             TestDetailId = td.TestDetailId,
             NaturalFields = td.Analyse.NaturalFields // إعادة جميع NaturalFields
         }).ToList()
     }).FirstOrDefault();

            if (result == null)
            {
                return NotFound();
            }

            var groupedResult = result.TestDetails
                .GroupBy(td => td.CategoryName)
                .Select(g => new
                {
                    CategoryName = g.Key,
                    Details = g.ToList()
                }).ToList();

            return Ok(new
            {
                result.PatientGender,
                result.PatientBirth,
                result.TestId,
                result.TestDate,
                result.PatientName,
                result.DoctorName,
                result.DoctorGender,
                GroupedTestDetails = groupedResult
            });
        }

        [HttpGet("{testId}/unused-analyses")]
        public async Task<IActionResult> GetUnusedAnalyses(int testId)
        {
            // Get all analyses used in the specified test
            var usedAnalyses = _context.TestDetails
                                       .Where(td => td.TestId == testId)
                                       .Select(td => td.AnalyseId)
                                       .ToList();

            // Get all analyses that are not in the usedAnalyses list
            var unusedAnalyses = from category in _context.Categories
                                 join analysis in _context.Analyses
                                 on category.CategoryId equals analysis.CategoryId
                                 where !usedAnalyses.Contains(analysis.AnalyseId)
                                 select new
                                 {
                                     CategoryId = category.CategoryId,
                                     CategoryName = category.CategoryName,
                                     analyseId = analysis.AnalyseId,
                                     name = analysis.Name
                                 };

            // Group the results by category
            var groupedResults = unusedAnalyses
                                 .GroupBy(x => new { x.CategoryId, x.CategoryName })
                                 .Select(g => new
                                 {
                                     CategoryId = g.Key.CategoryId,
                                     CategoryName = g.Key.CategoryName,
                                     Analyses = g.Select(a => new
                                     {
                                         a.analyseId,
                                         a.name
                                     }).ToList()
                                 });

            return Ok(await groupedResults.ToListAsync());
        }

        [HttpGet("{testId}/used-analyses")]
        public async Task<IActionResult> GetUsedAnalyses(int testId)
        {
            // Get all analyses used in the specified test
            var usedAnalyses = _context.TestDetails
                                       .Where(td => td.TestId == testId)
                                       .Select(td => new { td.AnalyseId, td.TestDetailId })
                                       .ToList();

            var usedAnalysisIds = usedAnalyses.Select(ua => ua.AnalyseId).ToList();

            // Get all analyses that are not in the usedAnalyses list
            var unusedAnalyses = from category in _context.Categories
                                 join analysis in _context.Analyses
                                 on category.CategoryId equals analysis.CategoryId
                                 where usedAnalysisIds.Contains(analysis.AnalyseId)
                                 select new
                                 {
                                     CategoryId = category.CategoryId,
                                     CategoryName = category.CategoryName,
                                     AnalyseId = analysis.AnalyseId,
                                     AnalysisName = analysis.Name
                                 };

            // Group the results by category
            var groupedResults = unusedAnalyses
                                 .GroupBy(x => new { x.CategoryId, x.CategoryName })
                                 .Select(g => new
                                 {
                                     CategoryId = g.Key.CategoryId,
                                     CategoryName = g.Key.CategoryName,
                                     Analyses = g.Select(a => new
                                     {
                                         a.AnalyseId,
                                         a.AnalysisName,
                                         TestDetailId = (int?)null // Since these are unused, there is no TestDetailId
                                     }).ToList()
                                 }).ToList();

            // Add used analyses with their TestDetailIds
            var usedAnalysesGrouped = usedAnalyses
                                      .Join(_context.Analyses, ua => ua.AnalyseId, a => a.AnalyseId, (ua, a) => new { ua, a })
                                      .Join(_context.Categories, j => j.a.CategoryId, c => c.CategoryId, (j, c) => new
                                      {
                                          j.ua.TestDetailId,
                                          j.a.AnalyseId,
                                          j.a.Name,
                                          c.CategoryId,
                                          c.CategoryName
                                      })
                                      .GroupBy(x => new { x.CategoryId, x.CategoryName })
                                      .Select(g => new
                                      {
                                          CategoryId = g.Key.CategoryId,
                                          CategoryName = g.Key.CategoryName,
                                          Analyses = g.Select(a => new
                                          {
                                              a.AnalyseId,
                                              a.Name,
                                              a.TestDetailId
                                          }).ToList()
                                      });

            // Combine both results
            //var combinedResults = groupedResults
            //                      .Union(usedAnalysesGrouped)
            //                      .OrderBy(gr => gr.CategoryId);

            return Ok(usedAnalysesGrouped);
        }








        [HttpPost()]
        public async Task<IActionResult> Add([FromBody] CreateTestDto dto)
        {

            var test = await _uintOfWork.Tests.Add(_mapper.Map<Test>(dto));
            _uintOfWork.Complete();
            var testId = test.TestId;
            var testDetailes = dto.TestDetailes;
            if (testDetailes != null)
            {
                foreach (var testDetail in testDetailes)
                {
                    if (testDetail.Result == null) testDetail.Result = "0";
                    await _uintOfWork.TestDetails.Add(new TestDetail { TestId = testId, Result = testDetail.Result, AnalyseId = testDetail.AnalyseId });
                    _uintOfWork.Complete();
                }
            }

            return Ok(test);

        }



        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(int Id, [FromBody] UpdateTestDto dto)
        {
            var old_Test = await _uintOfWork.Tests.Find(b => b.TestId == Id);
            if (old_Test == null) return NotFound($"No Test with Id:{Id}");
                

            old_Test.TestDate = dto.TestDate;
            old_Test.UserId = dto.UserId;
            old_Test.DoctorId = dto.DoctorId;
            old_Test.Discount = dto.Discount;
            old_Test.PatientId= dto.PatientId;
            old_Test.InsuranceCompanyId = dto.InsuranceCompanyId;


          
            _uintOfWork.Complete();

            var update_Test = _uintOfWork.Tests.Update(old_Test);
            _uintOfWork.Complete();
            return Ok(_mapper.Map<ResponseTestDto>(update_Test));

        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var Test = await _uintOfWork.Tests.Find(b => b.TestId == Id);

            if (Test == null) return NotFound($"No Test with Id:{Id}");

            var delete_Test = _uintOfWork.Tests.Delete(Test);
            _uintOfWork.Complete();
            return Ok();

        }


    }
}

