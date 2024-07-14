using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ML.Api.Dtos.Doctor;
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
    public class DoctorController : ControllerBase

    {


        private readonly IUintOfWork _uintOfWork;
        private readonly IMapper _mapper;

        public DoctorController(IUintOfWork uintOfWork, IMapper mapper)
        {
            _uintOfWork = uintOfWork;
            _mapper=mapper;

        }
      


        [HttpGet()]
        public async Task<IActionResult> GetAllAsync() => Ok(_mapper.Map<IEnumerable<ResponseDoctorDto>>(await _uintOfWork.Doctors.GetAllAsync(new[] { "DoctorPhones" })));

        

        [HttpGet("{GetById}")]
        public async Task<IActionResult> GetByIdAsync(int GetById)
        {
            var doctor = await _uintOfWork.Doctors.Find(b => b.DoctorId == GetById, new[] { "DoctorPhones" });
           
            if(doctor == null) { return NotFound($"No Doctor with ID:{GetById}"); }
           
            return Ok(_mapper.Map<ResponseDoctorDto>(doctor));
        }


        [HttpGet("GetByName/{Name}")]

       public async Task<IActionResult> GetByName(string Name)
       {
                var doctor = await _uintOfWork.Doctors.Find(b => b.FirstName == Name);
                if (doctor == null)
                {
                   doctor = await _uintOfWork.Doctors.Find(b => b.lastName == Name);
                   if(doctor == null)
                   return NotFound($"No Doctor with Name:{Name}");
                }

               return Ok(_mapper.Map<IEnumerable<ResponseDoctorDto>>(doctor));
       }

        [HttpPost()]
        public async Task<IActionResult> Add([FromBody] CreateDoctorDto dto)
        {
            var doctor = await _uintOfWork.Doctors.Add(_mapper.Map<Doctor>(dto));
            _uintOfWork.Complete();
            var doctroId=doctor.DoctorId;
            var Phones=dto.Phones;
            if (Phones != null)
            {
                foreach (var Phone in Phones) await _uintOfWork.DoctorPhones.Add(new DoctorPhone { DoctorId = doctroId, Phone = Phone.Phone });
                _uintOfWork.Complete();
            }

            return Ok(_mapper.Map<ResponseDoctorDto>(doctor));

        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(int Id, [FromBody] UpdateDoctorDto dto)
        {
            var old_doctor = await _uintOfWork.Doctors.Find(b => b.DoctorId == Id);
            if (old_doctor == null) return NotFound($"No Doctor with Id:{Id}");

           
            old_doctor.FirstName = dto.FirstName;
            old_doctor.lastName = dto.lastName;
            old_doctor.Gender = dto.Gender;
            old_doctor.Email = dto.Email;
            old_doctor.Specialization = dto.Specialization;

            var update_doctor = _uintOfWork.Doctors.Update(old_doctor);

            _uintOfWork.Complete();
            return Ok/*_mapper.Map<IEnumerable<ResponseDoctorDto>>(*/(update_doctor);

        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var doctor = await _uintOfWork.Doctors.Find(b => b.DoctorId == Id);

            if (doctor == null) return NotFound($"No Doctor with Id:{Id}");

            var delete_doctor = _uintOfWork.Doctors.Delete(doctor);

            _uintOfWork.Complete();

            return Ok();

        }



    }
}
