using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ML.Api.Dtos.Doctor;
using ML.Api.Dtos.DoctorPhone;
using ML.Core;
using ML.Core.Interfaces;
using ML.Core.Models;
using System.Xml.Linq;

namespace ML.Api.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class DoctorPhoneController : ControllerBase
    {


        private readonly IUintOfWork _uintOfWork;
        private readonly IMapper _mapper;


        public DoctorPhoneController(IUintOfWork uintOfWork, IMapper mapper)
        {
            _uintOfWork = uintOfWork;
            _mapper = mapper;
        }




        [HttpGet()]
        public async Task<IActionResult> GetAllAsync() => Ok(_mapper.Map<IEnumerable<ResponseDoctorPhoneDto>>(await _uintOfWork.DoctorPhones.GetAllAsync()));
       




        [HttpGet("{GetById}")]
        public async Task<IActionResult> GetById(int GetById)
        {
            var Phone = await _uintOfWork.DoctorPhones.Find(p => p.DoctorPhoneId == GetById);
            if(Phone == null) { return NotFound($"No Phone with Id:{GetById}"); }
           
            return Ok(_mapper.Map<ResponseDoctorPhoneDto>(Phone));
        }


       [HttpPost()]
        public async Task<IActionResult> Add([FromBody] CreateDoctorPhoneDto dto)
        {
            var Doctor = await _uintOfWork.Doctors.Find(p => p.DoctorId == dto.DoctorId);
            if (Doctor == null) { return NotFound($"No Doctor with Id:{dto.DoctorId} in data base are You sure?"); }
            var phone = await _uintOfWork.DoctorPhones.Add(_mapper.Map<DoctorPhone>(dto));
            _uintOfWork.Complete();

            return Ok(_mapper.Map<ResponseDoctorPhoneDto>(phone));

        }





        [HttpPut("{DoctorPhoneId}")]

        public async Task<IActionResult> Update(int DoctorPhoneId, [FromBody] UpdateDoctorPhoneDto dto)
        {
            var old_doctorPhone = await _uintOfWork.DoctorPhones.Find(b => b.DoctorPhoneId == DoctorPhoneId);
            if (old_doctorPhone == null) return NotFound($"No Phone with Id:{DoctorPhoneId}");

            var Doctor = await _uintOfWork.Doctors.Find(p => p.DoctorId == dto.DoctorId);
            if (Doctor == null) { return NotFound($"No Doctor with Id:{dto.DoctorId} in data base are You sure?"); }

            old_doctorPhone.DoctorId = dto.DoctorId;
            old_doctorPhone.Phone= dto.Phone;

            var update_doctorPhone = _uintOfWork.DoctorPhones.Update(old_doctorPhone);
            _uintOfWork.Complete();

            return Ok(_mapper.Map<ResponseDoctorPhoneDto>( update_doctorPhone));
        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var Phone = await _uintOfWork.DoctorPhones.Find(b => b.DoctorPhoneId == Id);

            if (Phone == null) return NotFound($"No Phone with Id:{Id}");
                
            Phone = _uintOfWork.DoctorPhones.Delete(Phone);
            _uintOfWork.Complete();

            return Ok();
        } 


    }
}
