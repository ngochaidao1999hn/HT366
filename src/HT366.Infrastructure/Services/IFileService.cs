using Microsoft.AspNetCore.Http;

namespace HT366.Infrastructure.Services
{
    public interface IFileService
    {
        Task<string> SaveFile(IFormFile file);
    }
}