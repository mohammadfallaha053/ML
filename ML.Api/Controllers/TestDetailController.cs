using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ML.Api.Dtos.Analyse;
using ML.Api.Dtos.TestDetail;
using ML.Core;
using ML.Core.Models;
using ML.EF;

namespace ML.Api.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class TestDetailController : ControllerBase
    {

        private readonly IUintOfWork _uintOfWork;
        private readonly IMapper _mapper;
        

        public TestDetailController(IUintOfWork uintOfWork, IMapper mapper)
        {
            _uintOfWork = uintOfWork;
            _mapper = mapper;
        }


        [HttpGet()]
        public async Task<IActionResult> GetAllAsync() => Ok(_mapper.Map<IEnumerable<ResponseTestDetailDto>>(await _uintOfWork.TestDetails.GetAllAsync()));
        
        [HttpGet("GetAllWithAnalyse")]
        public async Task<IActionResult> GetAllWithAnalyseAsync() => Ok(_mapper.Map<IEnumerable<ResponseTestDetailDto>>(await _uintOfWork.TestDetails.GetAllAsync(new[] { "Analyse" })));
               
        
        [HttpGet("GetAllWithTest")]
        public async Task<IActionResult> GetAllWithTestAsync() => Ok(_mapper.Map<IEnumerable<ResponseTestDetailDto>>(await _uintOfWork.TestDetails.GetAllAsync(new[] { "Test" })));

        
        [HttpGet("GetAllWithAnalyseAndTest")]
        public async Task<IActionResult> GetAllWithDoctorAndAnalyseAsync() => Ok(_mapper.Map<IEnumerable<ResponseTestDetailDto>>(await _uintOfWork.TestDetails.GetAllAsync(new[] { "Analyse" ,"Test" })));



      


        [HttpGet("{GetById}")]
        public async Task<IActionResult> GetByIdAsync(int GetById)
        {
            var TestDetail = await _uintOfWork.TestDetails.Find(b => b.TestDetailId == GetById);

            if (TestDetail == null) { return NotFound($"No TestDetail with ID:{GetById}"); }

            return Ok(_mapper.Map<ResponseTestDetailDto>(TestDetail));
        }

        [HttpPost()]
        public async Task<IActionResult> Add([FromBody] CreateTestDetailDto dto)
        {


            var TestDetail = await _uintOfWork.TestDetails.Add(_mapper.Map<TestDetail>(dto));
            _uintOfWork.Complete();

            return Ok(_mapper.Map<ResponseTestDetailDto>(TestDetail));

        }

        
        [HttpPost("AddMultiAnalysesToTest/{TestId}")]
        public async Task<IActionResult> AddMultiAnalyses(int TestId,[FromBody] String s)
        {
            string[] str_arr = s.Split('|').ToArray();
            int[] AnalyseId = Array.ConvertAll(str_arr, Int32.Parse);

            for (int i = 0; i < AnalyseId.Length; i++)
            {

                await _uintOfWork.TestDetails.Add(_mapper.Map<TestDetail>(new CreateTestDetailDto { AnalyseId = AnalyseId[i], TestId = TestId, Result = "0" }));
            };

            _uintOfWork.Complete();

            return Ok("ok");

        }

     
        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(int Id, [FromBody] UpdateTestDetailDto dto)
        {
            var old_TestDetail = await _uintOfWork.TestDetails.Find(b => b.TestDetailId == Id);
            if (old_TestDetail == null) return NotFound($"No TestDetail with Id:{Id}");
                

            old_TestDetail.AnalyseId = dto.AnalyseId;
            old_TestDetail.TestId = dto.TestId;
            old_TestDetail.Result = dto.Result;
          
          
            _uintOfWork.Complete();

            var update_TestDetail = _uintOfWork.TestDetails.Update(old_TestDetail);
            _uintOfWork.Complete();
            return Ok(_mapper.Map<ResponseTestDetailDto>(update_TestDetail));

        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var TestDetail = await _uintOfWork.TestDetails.Find(b => b.TestDetailId == Id);

            if (TestDetail == null) return NotFound($"No TestDetail with Id:{Id}");

            var delete_TestDetail = _uintOfWork.TestDetails.Delete(TestDetail);
            _uintOfWork.Complete();
            return Ok();

        }


    }
}
