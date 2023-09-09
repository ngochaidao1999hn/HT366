using AutoMapper;
using HT366.Application.Dtos.Category;
using HT366.Domain.Entities;
using HT366.Domain.Interfaces;

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

        public async Task<IEnumerable<CategoryReadDto>> GetAll()
        {
            return _mapper.Map<IEnumerable<CategoryReadDto>>(await _unitOfWork.categoryRepository.GetAsync());
        }

        public async Task<CategoryReadDto?> GetById(Guid Id)
        {
            return _mapper.Map<CategoryReadDto>(await _unitOfWork.categoryRepository.GetByIdAsync(Id));
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