using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ML.Api.Dtos.LaboratoryConstant;
using ML.Core;
using ML.Core.Interfaces;
using ML.Core.Models;
using ML.EF;
using System.Linq;
using System.Xml.Linq;

namespace ML.Api.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class LaboratoryConstantController : ControllerBase

    {

        //private readonly IBaseRepository<LaboratoryConstant> _LaboratoryConstantRepository;

        private readonly IUintOfWork _uintOfWork;
        private readonly IMapper _mapper;

        public LaboratoryConstantController(IUintOfWork uintOfWork, IMapper mapper)
        {
            _uintOfWork = uintOfWork;
            _mapper=mapper;

        }



        [HttpGet()]
        public async Task<IActionResult> GetAllAsync() => Ok(_mapper.Map<IEnumerable<ResponseLaboratoryConstantDto>>(await _uintOfWork.LaboratoryConstants.GetAllAsync(new[] { "LaboratoryConstantPhones" })));

        

        [HttpGet("{GetById}")]
        public async Task<IActionResult> GetByIdAsync(int GetById)
        {
            var LaboratoryConstant = await _uintOfWork.LaboratoryConstants.Find(b => b.LaboratoryConstantId == GetById, new[] { "LaboratoryConstantPhones" });
           
            if(LaboratoryConstant == null) { return NotFound($"No LaboratoryConstant with ID:{GetById}"); }
           
            return Ok(_mapper.Map<ResponseLaboratoryConstantDto>(LaboratoryConstant));
        }


     
        [HttpPost()]
        public async Task<IActionResult> Add([FromBody] CreateLaboratoryConstantDto dto)
        {

            
            var LaboratoryConstant = await _uintOfWork.LaboratoryConstants.Add(_mapper.Map<LaboratoryConstant>(dto));
            _uintOfWork.Complete();

            return Ok(_mapper.Map<ResponseLaboratoryConstantDto>(LaboratoryConstant));

        }


        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(int Id, [FromBody] UpdateLaboratoryConstantDto dto)
        {
            var old_LaboratoryConstant = await _uintOfWork.LaboratoryConstants.Find(b => b.LaboratoryConstantId == Id);
            if (old_LaboratoryConstant == null) return NotFound($"No LaboratoryConstant with Id:{Id}");


            old_LaboratoryConstant.Address = dto.Address;
            old_LaboratoryConstant.Email = dto.Email;
            old_LaboratoryConstant.PriceOfUnit = dto.PriceOfUnit;
            old_LaboratoryConstant.HospitalName = dto.HospitalName;
            old_LaboratoryConstant.LabName = dto.LabName;
            old_LaboratoryConstant.MangarName = dto.MangarName;
            old_LaboratoryConstant.LogoImage = dto.LogoImage;
            old_LaboratoryConstant.LogoImage2= dto.LogoImage2;
           

            var update_LaboratoryConstant = _uintOfWork.LaboratoryConstants.Update(old_LaboratoryConstant);
            _uintOfWork.Complete();
            return Ok(_mapper.Map<ResponseLaboratoryConstantDto>(update_LaboratoryConstant));

        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var LaboratoryConstant = await _uintOfWork.LaboratoryConstants.Find(b => b.LaboratoryConstantId == Id);

            if (LaboratoryConstant == null) return NotFound($"No LaboratoryConstant with Id:{Id}");

            var delete_LaboratoryConstant = _uintOfWork.LaboratoryConstants.Delete(LaboratoryConstant);
            _uintOfWork.Complete();
            return Ok();

        }



    }
}
