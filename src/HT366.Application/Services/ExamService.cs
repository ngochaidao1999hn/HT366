using AutoMapper;
using HT366.Application.Dtos.Exam;
using HT366.Domain.Common;
using HT366.Domain.Entities;
using HT366.Domain.Interfaces;
using HT366.Infrastructure.Services;
using HT366.Infrastructure.Utils.Exceptions;

namespace HT366.Application.Services
{
    public class ExamService : IExamService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly IFileService _fileService;
        private readonly IUserService _userService;

        public ExamService(IUnitOfWork unitOfWork,
            IMapper mapper,
            ICategoryService categoryService,
            IFileService fileService,
            IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _categoryService = categoryService;
            _fileService = fileService;
            _userService = userService;
        }

        public async Task<bool> Delete(Guid id)
        {
            var exam = await _unitOfWork.examRepository.GetByIdAsync(id);
            if (exam is ISoftDeleted)
            {
                exam.IsDeleted = true;
                exam.DeleteTime = DateTime.UtcNow;
                _unitOfWork.examRepository.Update(exam);
                await _unitOfWork.CommitTransactionAsync();
                return true;
            }
            _unitOfWork.examRepository.Delete(exam.Id);
            return true;
        }

        public async Task<IEnumerable<ExamReadDto>> GetAll(GetExamFilter filter)
        {
            var exams = await _unitOfWork.examRepository.GetAsync(x =>
                (filter.Name == null || x.Name.Contains(filter.Name)) &&
                (filter.Description == null || x.Description.Contains(filter.Description)) &&
                (filter.CreateDate == null || DateOnly.FromDateTime(x.CreatedDate) == filter.CreateDate) &&
                (filter.Status == null || x.Status == filter.Status) &&
                (filter.Level == null || x.Level == filter.Level) &&
                (filter.CateId == null || x.CategoryId == filter.CateId) &&
                (filter.CreatedBy == null || x.User.UserName.Contains(filter.CreatedBy)),
                includeProperties: new string[] { "Files", "User" });

            var totalCount = exams.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / filter.Size);
            var currentPage = Math.Min(filter.Page, totalPages);

            exams = exams
                .Skip((currentPage - 1) * filter.Size)
                .Take(filter.Size);
            return _mapper.Map<IEnumerable<ExamReadDto>>(exams);
        }

        public async Task<ExamReadDto?> GetById(Guid Id)
        {
            var exam = await _unitOfWork.examRepository.GetByIdAsync(Id);
            return _mapper.Map<ExamReadDto>(exam);
        }

        public async Task<Guid> Insert(Guid userId, CreateExamDto ex)
        {
            try
            {
                var user = await _userService.GetByIdAsync(userId);
                if (user is null)
                {
                    throw new ResourceNotFoundException($"user with Id {userId} not found");
                }
                var category = await _categoryService.GetById(ex.CateId);
                if (category is null)
                {
                    throw new ResourceNotFoundException($"Category with Id {ex.CateId} not found");
                }
                var exam = _mapper.Map<Exam>(ex);
                exam.CreatedBy = user.Id;
                exam.User = user;
                exam.CategoryId = category.Id;
                exam.Category = category;
                var newExam = await _unitOfWork.examRepository.CreateAsync(exam);
                if (ex.Files is not null)
                {
                    foreach (var file in ex.Files)
                    {
                        var fileUrl = await _fileService.SaveFile(file);
                        Domain.Entities.File newFile = new Domain.Entities.File();
                        newFile.Url = fileUrl;
                        newFile.ExamId = newExam.Id;
                        await _unitOfWork.fileRepository.CreateAsync(newFile);
                    }
                }
                await _unitOfWork.CommitTransactionAsync();
                return newExam.Id;
            }
            catch (Exception exception)
            {
                await _unitOfWork.RollBackTransactionAsync();
                throw new Exception(exception.Message);
            }
        }

        public async Task<Guid> Update(Exam ex)
        {
            _unitOfWork.examRepository.Update(ex);
            await _unitOfWork.CommitTransactionAsync();
            return ex.Id;
        }
    }
}