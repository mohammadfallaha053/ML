using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ML.Core.Models;
using ML.Core;
using ML.Api.Dtos.LaboratoryConstantPhone;

namespace ML.Api.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class LaboratoryConstantPhoneController : ControllerBase
    {
       

            private readonly IUintOfWork _uintOfWork;
            private readonly IMapper _mapper;


            public LaboratoryConstantPhoneController(IUintOfWork uintOfWork, IMapper mapper)
            {
                _uintOfWork = uintOfWork;
                _mapper = mapper;
            }




            [HttpGet()]
            public async Task<IActionResult> GetAllAsync() => Ok(_mapper.Map<IEnumerable<ResponseLaboratoryConstantPhoneDto>>(await _uintOfWork.LaboratoryConstantPhones.GetAllAsync()));





            [HttpGet("{GetById}")]
            public async Task<IActionResult> GetById(int GetById)
            {
                var Phone = await _uintOfWork.LaboratoryConstantPhones.Find(p => p.LaboratoryConstantPhoneId == GetById);
                if (Phone == null) { return NotFound($"No Phone with Id:{GetById}"); }
                 
                return Ok(_mapper.Map<ResponseLaboratoryConstantPhoneDto>(Phone));
            }


            [HttpPost()]
            public async Task<IActionResult> Add([FromBody] CreateLaboratoryConstantPhoneDto dto)
            {
                var LaboratoryConstant = await _uintOfWork.LaboratoryConstants.Find(p => p.LaboratoryConstantId == dto.LaboratoryConstantId);
                if (LaboratoryConstant == null) { return NotFound($"No LaboratoryConstant with Id:{dto.LaboratoryConstantId} in data base are You sure?"); }
                var phone = await _uintOfWork.LaboratoryConstantPhones.Add(_mapper.Map<LaboratoryConstantPhone>(dto));
                _uintOfWork.Complete();
                return Ok(_mapper.Map<ResponseLaboratoryConstantPhoneDto>(phone));

            }





            [HttpPut("{LaboratoryConstantPhoneId}")]

            public async Task<IActionResult> Update(int LaboratoryConstantPhoneId, [FromBody] UpdateLaboratoryConstantPhoneDto dto)
            {
                var LaboratoryConstantPhone = await _uintOfWork.LaboratoryConstantPhones.Find(b => b.LaboratoryConstantPhoneId == LaboratoryConstantPhoneId);
                if (LaboratoryConstantPhone == null) return NotFound($"No Phone with Id:{LaboratoryConstantPhoneId}");

                var LaboratoryConstant = await _uintOfWork.LaboratoryConstants.Find(p => p.LaboratoryConstantId == dto.LaboratoryConstantId);
                if (LaboratoryConstant == null) { return NotFound($"No LaboratoryConstant with Id:{dto.LaboratoryConstantId} in data base are You sure?"); }

                LaboratoryConstantPhone.LaboratoryConstantId = dto.LaboratoryConstantId;
                LaboratoryConstantPhone.Phone = dto.Phone;

                var update_LaboratoryConstantPhone = _uintOfWork.LaboratoryConstantPhones.Update(LaboratoryConstantPhone);
                _uintOfWork.Complete();

                return Ok(_mapper.Map<ResponseLaboratoryConstantPhoneDto>(update_LaboratoryConstantPhone));
            }


            [HttpDelete("{Id}")]
            public async Task<IActionResult> Delete(int Id)
            {
                var LaboratoryConstant = await _uintOfWork.LaboratoryConstantPhones.Find(b => b.LaboratoryConstantPhoneId == Id);

                if (LaboratoryConstant == null) return NotFound($"No Phone with Id:{Id}");

                 LaboratoryConstant = _uintOfWork.LaboratoryConstantPhones.Delete(LaboratoryConstant);
                _uintOfWork.Complete();
                return Ok();
            }


        }
    
}
