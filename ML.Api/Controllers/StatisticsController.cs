using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ML.EF;

namespace ML.Api.Controllers
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StatisticsController(AppDbContext context)
        {
            _context = context;
        }








        [HttpPost("countByTestName")]
        public async Task<IActionResult> GetTestCounts([FromBody] TestCountRequest request)
        {
            if (request.FromDate > request.ToDate)
            {
                return BadRequest("Invalid date range.");
            }

            var result = await _context.Tests
                .Where(t => t.TestDate >= request.FromDate && t.TestDate <= request.ToDate)
                .Join(
                    _context.TestDetails,
                    test => test.TestId,
                    testDetail => testDetail.TestId,
                    (test, testDetail) => new { test, testDetail }
                )
                .Join(
                    _context.Analyses,
                    combined => combined.testDetail.AnalyseId,
                    analyse => analyse.AnalyseId,
                    (combined, analyse) => new { combined.test, analyse.Name, analyse.NUint }
                )
                .GroupBy(x => new { x.Name, x.NUint })
                .Select(g => new
                {
                    TestName = g.Key.Name,
                    Nuint = g.Key.NUint,
                    Count = g.Count()
                })
                .ToListAsync();

            return Ok(result);
        }






        [HttpGet("doctor-stats")]
        public IActionResult GetDoctorStats()
        {
            var doctorStats = _context.Tests
                .GroupBy(t => new { t.Doctor.DoctorId, t.Doctor.FirstName, t.Doctor.lastName })
                .Select(g => new StatisticsResult
                {
                    Id = g.Key.DoctorId,
                    Name = g.Key.FirstName + " " + g.Key.lastName,
                    Count = g.Count()
                })
                .ToList();

            return Ok(doctorStats);
        }

        [HttpGet("patient-stats")]
        public IActionResult GetPatientStats()
        {
            var patientStats = _context.Tests
                .GroupBy(t => new { t.Patient.PatientId, t.Patient.FirstName, t.Patient.lastName })
                .Select(g => new StatisticsResult
                {
                    Id = g.Key.PatientId,
                    Name = g.Key.FirstName + " " + g.Key.lastName,
                    Count = g.Count()
                })
                .ToList();

            return Ok(patientStats);
        }

        [HttpGet("patient-tests/{patientId}")]
        public IActionResult GetPatientTests(int patientId)
        {
            var patientTests = _context.TestDetails
                .Where(td => td.Test.PatientId == patientId)
                .Select(td => new PatientTestDetails
                {
                    TestId = td.TestId,
                    TestDate = td.Test.TestDate,
                    DoctorName = td.Test.Doctor.FirstName + " " + td.Test.Doctor.lastName,
                    CategoryName = td.Analyse.Category.CategoryName,
                    AnalyseName = td.Analyse.Name,
                    Result = td.Result
                })
                .ToList();

            if (!patientTests.Any())
            {
                return NotFound("No tests found for the given patient ID.");
            }



          //  return Ok(patientTests);

            var groupedResult = patientTests
                                     .GroupBy(td => td.TestDate)
                                     .Select(g => new
                                     {
                                         TestDate = g.Key,
                                         Details = g.ToList()
                                     }).ToList();

            return Ok(new
            {
              
                GroupedTestDetails = groupedResult
            });



        }

        [HttpGet("category-stats")]
        public IActionResult GetCategoryStats()
        {
            var categoryStats = _context.TestDetails
                .GroupBy(td => new { td.Analyse.Category.CategoryId, td.Analyse.Category.CategoryName })
                .Select(g => new StatisticsResult
                {
                    Id = g.Key.CategoryId,
                    Name = g.Key.CategoryName,
                    Count = g.Count()
                })
                .ToList();

            return Ok(categoryStats);
        }
    }


    public class StatisticsResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
    }

    public class PatientTestDetails
    {
        public int TestId { get; set; }
        public DateTime? TestDate { get; set; }
        public string DoctorName { get; set; }
        public string CategoryName { get; set; }
        public string AnalyseName { get; set; }
        public string Result { get; set; }
    }


    public class TestCountRequest
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }

}
