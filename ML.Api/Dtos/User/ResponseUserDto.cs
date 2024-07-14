using ML.Api.Dtos.UserPhone;

namespace ML.Api.Dtos.User
{
    public class ResponseUserDto : BaseUserDto
    {
        public int UserId { get; set; }

        public List<ResponseUserPhoneDto> UserPhones { get; set; }

    }
}
