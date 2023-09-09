using HT366.Application.Dtos.Identity;
using HT366.Domain.Entities;
using HT366.Infrastructure.Services;

namespace HT366.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IIdentityService _identityService;

        public UserService(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<AuthenticationReadDto> Authorize(LoginDto dto)
        {
            var res = new AuthenticationReadDto();
            var authRes = await _identityService.AuthorizeAsync(dto.Email, dto.Password);
            res.AccessToken = authRes.Item1;
            res.ExpiredDate = authRes.Item2;
            return res;
        }

        public async Task<ApplicationUser?> GetByIdAsync(Guid Id)
        {
            return await _identityService.GetByIdAsync(Id);
        }

        public async Task Register(RegisterDto dto)
        {
            var res = await _identityService.CreateUserAsync(dto.Email, dto.Password);
            if (!res.Succeeded)
            {
                throw new Exception(res?.Errors?.FirstOrDefault()?.Description);
            }
        }
    }
}