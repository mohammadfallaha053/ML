using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ML.Api.Dtos.User;
using ML.Core;
using ML.Core.Models;
using ML.EF;
using BCrypt.Net;

namespace ML.Api.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUintOfWork _uintOfWork;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserController(IUintOfWork uintOfWork, IMapper mapper,AppDbContext context)
        {
            _uintOfWork = uintOfWork;
            _mapper = mapper;
            _context=context;
        }


        [HttpGet()]
        public async Task<IActionResult> GetAllAsync() => Ok(_mapper.Map<IEnumerable<ResponseUserDto>>(await _uintOfWork.Users.GetAllAsync(new[] { "UserPhones" })));




        [HttpGet("{GetById}")]
        public async Task<IActionResult> GetByIdAsync(int GetById)
        {
            var User = await _uintOfWork.Users.Find(b => b.UserId == GetById, new[] { "UserPhones" });

            if (User == null) { return NotFound($"No User with ID:{GetById}"); }

            return Ok(_mapper.Map<ResponseUserDto>(User));
        }

        [HttpPost()]
        public async Task<IActionResult> Add([FromBody] CreateUserDto dto)
        {
            var match = await _uintOfWork.Users.Find(a => a.UserName == dto.UserName);
            if (match!= null) return BadRequest("هذا الاسم مستخدم بالفعل");
            dto.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            var User = await _uintOfWork.Users.Add(_mapper.Map<User>(dto));

            _uintOfWork.Complete();
            var userId = User.UserId;
            var Phones = dto.Phones;
            if (Phones != null)
            {
                foreach (var Phone in Phones) await _uintOfWork.UserPhones.Add(new UserPhone { UserId = userId, Phone = Phone.Phone });
               
            }
            _uintOfWork.Complete();

            return Ok(_mapper.Map<ResponseUserDto>(User));

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == loginDto.UserName);
            if (user == null)
            {
                return Unauthorized("المستخدم غير موجود");
            }

            ;
            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                return Unauthorized("كلمة المرور أو اسم المستخدم غير صحيحة");
            }


            return Ok(_mapper.Map<ResponseUserDto>(user));
        }



        
    [HttpPost("ChangePassword")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
    {
        if (changePasswordDto == null)
        {
            return BadRequest("البيانات المدخلة غير صحيحة");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var user = await _context.Users.FindAsync(changePasswordDto.UserId);
            if (user == null)
            {
                return NotFound("المستخدم غير موجود");
            }

            if (!BCrypt.Net.BCrypt.Verify(changePasswordDto.OldPassword, user.Password ))
            {
                return BadRequest("كلمة المرور القديمة غير صحيحة");
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(changePasswordDto.NewPassword);
                _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok("تم تغيير كلمة المرور بنجاح");
        }
        catch (Exception ex)
        {
            // تسجيل الخطأ لتتبع المشكلة
            Console.WriteLine(ex.ToString());
            return StatusCode(500, "حدث خطأ غير متوقع");
        }
    }




        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(int Id, [FromBody] UpdateUserDto dto)
        {
            var old_user = await _uintOfWork.Users.Find(b => b.UserId == Id);
            if (old_user == null) return NotFound($"No ‘User with Id:{Id}");
                

            old_user.FirstName = dto.FirstName;
            old_user.LastName = dto.LastName;
            old_user.UserName = dto.UserName;
            old_user.Email = dto.Email;
           // old_user.Password = dto.Password;
            old_user.Image  = dto.Image;
            old_user.Image2=dto.Image2;
            _uintOfWork.Complete();

            var update_user = _uintOfWork.Users.Update(old_user);
            _uintOfWork.Complete();
            return Ok(_mapper.Map<ResponseUserDto>(update_user));

        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var user = await _uintOfWork.Users.Find(b => b.UserId == Id);

            if (user == null) return NotFound($"No User with Id:{Id}");

            var delete_user = _uintOfWork.Users.Delete(user);
            _uintOfWork.Complete();
            return Ok();

        }


    }
}
public class ChangePasswordDto
{
    public int UserId { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}

    public class LoginDto
    {
        public string Password { get; set; }

        public string UserName { get; set; }
    }

  