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
    public class HeartAttackController : ControllerBase

    {

        //private readonly IBaseRepository<HeartAttack> _HeartAttackRepository;

        private readonly IUintOfWork _uintOfWork;
        private readonly IMapper _mapper;

        public HeartAttackController(IUintOfWork uintOfWork, IMapper mapper)
        {
            _uintOfWork = uintOfWork;
            _mapper=mapper;

        }
      //  [HttpGet()]
     //   public async Task<IActionResult> GetAllAsync() => Ok(await _uintOfWork.HeartAttacks.GetAllAsync());

            
        [HttpPost()]
        public async Task<IActionResult> Add([FromBody] HeartAttackDto dto)
        {   
            var HeartAttack = await _uintOfWork.HeartAttacks.Add(_mapper.Map<HeartAttack>(dto)); 
            _uintOfWork.Complete();
            return Ok(HeartAttack);
        }

            [HttpGet("csv")]
            public async Task<IActionResult> HeartAttackCsv()
            {
                var heartAttacks = await _uintOfWork.HeartAttacks.GetAllAsync();
                var csv = GenerateCsv(heartAttacks);
                var bytes = Encoding.UTF8.GetBytes(csv);
                var result = new FileContentResult(bytes, "text/csv")
                {
                    FileDownloadName = "HeartAttackData.csv"
                };
                return result;
            }

            private string GenerateCsv(IEnumerable<HeartAttack> data)
            {
                var csv = new StringBuilder();
                csv.AppendLine("Age,Gender,Troponin,Glucose,Impluse,Class");

                foreach (var item in data)
                {
                    csv.AppendLine($"{item.Age},{item.Gender},{item.Troponin},{item.Glucose},{item.Impluse},{item.Class}");
                }

                return csv.ToString();
            }
        }
    }






