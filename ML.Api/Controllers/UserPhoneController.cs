using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ML.Api.Dtos.DoctorPhone;
using ML.Core.Models;
using ML.Core;
using ML.Api.Dtos.UserPhone;

namespace ML.Api.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class UserPhoneController : ControllerBase
    {
       

            private readonly IUintOfWork _uintOfWork;
            private readonly IMapper _mapper;


            public UserPhoneController(IUintOfWork uintOfWork, IMapper mapper)
            {
                _uintOfWork = uintOfWork;
                _mapper = mapper;
            }




            [HttpGet()]
            public async Task<IActionResult> GetAllAsync() => Ok(_mapper.Map<IEnumerable<ResponseUserPhoneDto>>(await _uintOfWork.UserPhones.GetAllAsync()));





            [HttpGet("{GetById}")]
            public async Task<IActionResult> GetById(int GetById)
            {
                var Phone = await _uintOfWork.UserPhones.Find(p => p.UserPhoneId == GetById);
                if (Phone == null) { return NotFound($"No Phone with Id:{GetById}"); }
                 
                return Ok(_mapper.Map<ResponseUserPhoneDto>(Phone));
            }


            [HttpPost()]
            public async Task<IActionResult> Add([FromBody] CreateUserPhoneDto dto)
            {
                var user = await _uintOfWork.Users.Find(p => p.UserId == dto.UserId);
                if (user == null) { return NotFound($"No User with Id:{dto.UserId} in data base are You sure?"); }
                var phone = await _uintOfWork.UserPhones.Add(_mapper.Map<UserPhone>(dto));
                _uintOfWork.Complete();
                return Ok(_mapper.Map<ResponseUserPhoneDto>(phone));

            }





            [HttpPut("{UserPhoneId}")]

            public async Task<IActionResult> Update(int UserPhoneId, [FromBody] UpdateUserPhoneDto dto)
            {
                var userPhone = await _uintOfWork.UserPhones.Find(b => b.UserPhoneId == UserPhoneId);
                if (userPhone == null) return NotFound($"No Phone with Id:{UserPhoneId}");

                var User = await _uintOfWork.Users.Find(p => p.UserId == dto.UserId);
                if (User == null) { return NotFound($"No User with Id:{dto.UserId} in data base are You sure?"); }

                userPhone.UserId = dto.UserId;
                userPhone.Phone = dto.Phone;

                var update_userPhone = _uintOfWork.UserPhones.Update(userPhone);
                _uintOfWork.Complete();

                return Ok(_mapper.Map<ResponseUserPhoneDto>(update_userPhone));
            }


            [HttpDelete("{Id}")]
            public async Task<IActionResult> Delete(int Id)
            {
                var user = await _uintOfWork.UserPhones.Find(b => b.UserPhoneId == Id);

                if (user == null) return NotFound($"No Phone with Id:{Id}");

                 user = _uintOfWork.UserPhones.Delete(user);
                _uintOfWork.Complete();
                return Ok();
            }


        }
    
}
