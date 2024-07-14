using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ML.Api.Dtos.NaturalField;
using ML.Core;
using ML.Core.Models;

namespace ML.Api.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class NaturalFieldController : ControllerBase
    {

        private readonly IUintOfWork _uintOfWork;
        private readonly IMapper _mapper;

        public NaturalFieldController(IUintOfWork uintOfWork, IMapper mapper)
        {
            _uintOfWork = uintOfWork;
            _mapper = mapper;

        }


        [HttpGet()]
        public async Task<IActionResult> GetAllAsync() => Ok(_mapper.Map<IEnumerable<ResponseNaturalFieldDto>>(await _uintOfWork.NaturalFields.GetAllAsync(new [] { "Analyse" })));




        [HttpGet("{GetById}")]
        public async Task<IActionResult> GetByIdAsync(int GetById)
        {
            var NaturalField = await _uintOfWork.NaturalFields.Find(b => b.NaturalFieldId == GetById);

            if (NaturalField == null) { return NotFound($"No NaturalField with ID:{GetById}"); }

            return Ok(_mapper.Map<ResponseNaturalFieldDto>(NaturalField));
        }

        [HttpPost()]
        public async Task<IActionResult> Add([FromBody] CreateNaturalFieldDto dto)
        {


            var NaturalField = await _uintOfWork.NaturalFields.Add(_mapper.Map<NaturalField>(dto));
            _uintOfWork.Complete();

            return Ok(NaturalField);

        }


        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(int Id, [FromBody] UpdateNaturalFieldDto dto)
        {
            var old_NaturalField = await _uintOfWork.NaturalFields.Find(b => b.NaturalFieldId == Id);
            if (old_NaturalField == null) return NotFound($"No NaturalField with Id:{Id}");


            old_NaturalField.AnalyseId = dto.AnalyseId;
            old_NaturalField.Max=dto.Max;
            old_NaturalField.Min=dto.Min;
          
          
            _uintOfWork.Complete();

            var update_NaturalField = _uintOfWork.NaturalFields.Update(old_NaturalField);
            _uintOfWork.Complete();
            return Ok(_mapper.Map<ResponseNaturalFieldDto>(update_NaturalField));

        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var NaturalField = await _uintOfWork.NaturalFields.Find(b => b.NaturalFieldId == Id);

            if (NaturalField == null) return NotFound($"No NaturalField with Id:{Id}");

            var delete_NaturalField = _uintOfWork.NaturalFields.Delete(NaturalField);
            _uintOfWork.Complete();
            return Ok();

        }


    }
}
