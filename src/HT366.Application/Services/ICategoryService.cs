using HT366.Application.Dtos.Category;
using HT366.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT366.Application.Services
{
    public interface ICategoryService
    {
        Task<Category?> GetById(Guid Id);
        Task<Category> Insert(CategoryCreateDto request);
        Task<IEnumerable<Category>> GetAll();
    }
}
