using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ML.Api.Dtos.Classification;
using ML.Core;
using ML.Core.Interfaces;
using ML.Core.Models;
using ML.Core.Models.ForDataSet;
using ML.EF;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ML.Api.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class DiabetesController : ControllerBase

    {

        //private readonly IBaseRepository<Diabetes> _DiabetesRepository;

        private readonly IUintOfWork _uintOfWork;
        private readonly IMapper _mapper;

        public DiabetesController(IUintOfWork uintOfWork, IMapper mapper)
        {
            _uintOfWork = uintOfWork;
            _mapper=mapper;

        }



        //[HttpGet()]
        //  public async Task<IActionResult> GetAllAsync() => Ok(await _uintOfWork.Diabeteses.GetAllAsync());






        [HttpPost()]
        public async Task<IActionResult> Add([FromBody] DiabetesDto dto)
        {


            var Diabetes = await _uintOfWork.Diabeteses.Add(_mapper.Map<Diabetes>(dto));

            _uintOfWork.Complete();
            return Ok(Diabetes);

        }


        [HttpGet("csv")]
        public async Task<IActionResult> GetCkdCsv()
        {
            var diabeteses = await _uintOfWork.Diabeteses.GetAllAsync();
            var csv = GenerateCsv(diabeteses);
            var bytes = Encoding.UTF8.GetBytes(csv);
            var result = new FileContentResult(bytes, "text/csv")
            {
                FileDownloadName = "DiabetesData.csv"
            };
            return result;
        }

        private string GenerateCsv(IEnumerable<Diabetes> data)
        {
            var csv = new StringBuilder();
            csv.AppendLine("Age,Gender,HbA1cLevel,HeartDisease,Glucose,Class");

            foreach (var item in data)
            {
                csv.AppendLine($"{item.Age},{item.Gender},{item.HbA1cLevel},{item.HeartDisease},{item.Glucose},{item.Class}");
            }

            return csv.ToString();
        }


    }
}
