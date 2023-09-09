using HT366.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace HT366.Infrastructure.Services
{
    public interface IIdentityService
    {
        Task<string?> GetUserNameAsync(Guid userId);

        Task<bool> IsInRoleAsync(Guid userId, string role);

        Task<Tuple<string?, DateTime?>> AuthorizeAsync(string userName, string password);

        Task<IdentityResult> CreateUserAsync(string email, string password);

        Task<bool> DeleteUserAsync(Guid userId);

        Task<List<ApplicationUser>> GetListUsers();

        Task<ApplicationUser?> GetByIdAsync(Guid userId);
    }
}