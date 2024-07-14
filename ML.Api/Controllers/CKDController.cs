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
using System.Threading.Tasks;

namespace ML.Api.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class CKDController : ControllerBase
    {
        private readonly IUintOfWork _uintOfWork;
        private readonly IMapper _mapper;

        public CKDController(IUintOfWork uintOfWork, IMapper mapper)
        {
            _uintOfWork = uintOfWork;
            _mapper = mapper;
        }

       // [HttpGet()]
      //  public async Task<IActionResult> GetAllAsync() => Ok(await _uintOfWork.CKDs.GetAllAsync());

        [HttpPost()]
        public async Task<IActionResult> Add([FromBody] CKDDto dto)
        {
            var ckd = await _uintOfWork.CKDs.Add(_mapper.Map<CKD>(dto));
            _uintOfWork.Complete();
            return Ok(ckd);
        }

        

        [HttpGet("csv")]
        public async Task<IActionResult> GetCkdCsv()
        {
            var ckdData = await _uintOfWork.CKDs.GetAllAsync();
            var csv = GenerateCsv(ckdData);
            var bytes = Encoding.UTF8.GetBytes(csv);
            var result = new FileContentResult(bytes, "text/csv")
            {
                FileDownloadName = "CkdData.csv"
            };
            return result;
        }

        private string GenerateCsv(IEnumerable<CKD> data)
        {
            var csv = new StringBuilder();
            csv.AppendLine("Age,Gender,SerumCreatinine,Potassium,BloodUrea,Hemoglobin,DiabetesMellitus,Appetite,Class");

            foreach (var item in data)
            {
                csv.AppendLine($"{item.Age},{item.Gender},{item.SerumCreatinine},{item.Potassium},{item.BloodUrea},{item.Hemoglobin},{item.DiabetesMellitus},{item.Appetite},{item.Class}");
            }

            return csv.ToString();
        }
    }
}
