using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ML.Api.Dtos.PatientPhone;
using ML.Core;
using ML.Core.Interfaces;
using ML.Core.Models;
using System.Xml.Linq;

namespace ML.Api.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class PatientPhoneController : ControllerBase
    {


        private readonly IUintOfWork _uintOfWork;
        private readonly IMapper _mapper;


        public PatientPhoneController(IUintOfWork uintOfWork, IMapper mapper)
        {
            _uintOfWork = uintOfWork;
            _mapper = mapper;
        }



        
        [HttpGet()]
        public async Task<IActionResult> GetAllAsync() => Ok(_mapper.Map<IEnumerable<ResponsePatientPhoneDto>>(await _uintOfWork.PatientPhones.GetAllAsync()));
       




        [HttpGet("{GetById}")]
        public async Task<IActionResult> GetById(int GetById)
        {
            var Phone = await _uintOfWork.PatientPhones.Find(p => p.PatientPhoneId == GetById);
            if(Phone == null) { return NotFound($"No Phone with Id:{GetById}"); }
           
            return Ok(_mapper.Map<ResponsePatientPhoneDto>(Phone));
        }


       [HttpPost()]
        public async Task<IActionResult> Add([FromBody] CreatePatientPhoneDto dto)
        {
            var Patient = await _uintOfWork.Patients.Find(p => p.PatientId == dto.PatientId);
            if (Patient == null) { return NotFound($"No Patient with Id:{dto.PatientId} in data base are You sure?"); }
            var phone = await _uintOfWork.PatientPhones.Add(_mapper.Map<PatientPhone>(dto));
            _uintOfWork.Complete();

            return Ok(_mapper.Map<ResponsePatientPhoneDto>(phone));

        }





        [HttpPut("{PatientPhoneId}")]

        public async Task<IActionResult> Update(int PatientPhoneId, [FromBody] UpdatePatientPhoneDto dto)
        {
            var old_PatientPhone = await _uintOfWork.PatientPhones.Find(b => b.PatientPhoneId == PatientPhoneId);
            if (old_PatientPhone == null) return NotFound($"No Phone with Id:{PatientPhoneId}");

            var Patient = await _uintOfWork.Patients.Find(p => p.PatientId == dto.PatientId);
            if (Patient == null) { return NotFound($"No Patient with Id:{dto.PatientId} in data base are You sure?"); }

            old_PatientPhone.PatientId = dto.PatientId;
            old_PatientPhone.Phone= dto.Phone;

            var update_PatientPhone = _uintOfWork.PatientPhones.Update(old_PatientPhone);
            _uintOfWork.Complete();

            return Ok(_mapper.Map<ResponsePatientPhoneDto>( update_PatientPhone));
        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var Phone = await _uintOfWork.PatientPhones.Find(b => b.PatientPhoneId == Id);

            if (Phone == null) return NotFound($"No Phone with Id:{Id}");
                
            Phone = _uintOfWork.PatientPhones.Delete(Phone);
            _uintOfWork.Complete();

            return Ok();
        } 


    }
}
