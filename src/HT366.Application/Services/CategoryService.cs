using AutoMapper;
using HT366.Application.Dtos.Category;
using HT366.Domain.Entities;
using HT366.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT366.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Category?> GetById(Guid Id)
        {
            return await _unitOfWork.categoryRepository.GetByIdAsync(Id);
        }

        public async Task<Category> Insert(CategoryCreateDto request)
        {
            var category = _mapper.Map<Category>(request);
            var res = await _unitOfWork.categoryRepository.CreateAsync(category);
            await _unitOfWork.CommitTransactionAsync();
            return res;
        }
    }
}
