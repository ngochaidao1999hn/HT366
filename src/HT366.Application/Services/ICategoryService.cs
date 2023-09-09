using HT366.Application.Dtos.Category;
using HT366.Domain.Entities;

namespace HT366.Application.Services
{
    public interface ICategoryService
    {
        Task<CategoryReadDto?> GetById(Guid Id);

        Task<Category> Insert(CategoryCreateDto request);

        Task<IEnumerable<CategoryReadDto>> GetAll();
    }
}