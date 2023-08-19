using HT366.Application.Dtos.Identity;
using HT366.Domain.Entities;

namespace HT366.Application.Services
{
    public interface IUserService
    {
        Task<AuthenticationReadDto> Authorize(LoginDto dto);
        Task Register(RegisterDto dto);

        Task<ApplicationUser?> GetByIdAsync(Guid Id);
    }
}
