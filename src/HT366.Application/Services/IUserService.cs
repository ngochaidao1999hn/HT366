using HT366.Application.Dtos.Identity;

namespace HT366.Application.Services
{
    public interface IUserService
    {
        public Task<AuthenticationReadDto> Authorize(LoginDto dto);
        public Task Register(RegisterDto dto);
    }
}
