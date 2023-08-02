using AutoMapper;
using HT366.Application.Dtos.Exam;
using HT366.Domain.Common;
using HT366.Domain.Entities;
using HT366.Domain.Interfaces;
using HT366.Infrastructure.Services;
using HT366.Infrastructure.Utils.Exceptions;
using System.Linq.Expressions;

namespace HT366.Application.Services
{
    public class ExamService : IExamService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly IFileService _fileService;
        public ExamService(IUnitOfWork unitOfWork,
            IMapper mapper,
            ICategoryService categoryService,
            IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _categoryService = categoryService;
            _fileService = fileService;

        }
        public async Task<bool> Delete(Guid Id)
        {
            var exam = await _unitOfWork.examRepository.GetByIdAsync(Id);
            if(exam is null)
                return false;
            if (exam is ISoftDeleted)
            { 
                exam.IsDeleted = true;
                exam.DeleteTime = DateTime.UtcNow;
                _unitOfWork.examRepository.Update(exam);
                await _unitOfWork.CommitTransactionAsync();
                return true;
            }
            _unitOfWork.examRepository.Delete(Id);
            return true;
        }

        public async Task<IEnumerable<ExamReadDto>> GetAll(GetExamFilter filter)
        {
            var exams = await _unitOfWork.examRepository.GetAsync(
                x => (filter.Name == null || x.Name.Contains(filter.Name)) &&
                (filter.Description == null || x.Description.Contains(filter.Description)) &&
                (filter.CreateDate == null || DateOnly.FromDateTime(x.CreatedDate) == filter.CreateDate), 
                includeProperties: new string[] { "Files" });
            return _mapper.Map<IEnumerable<ExamReadDto>>(exams);
        }

        public async Task<Guid> Insert(CreateExamDto ex)
        {
            var category = await _categoryService.GetById(ex.CateId);
            if (category is null)
            {
                throw new ResourceNotFoundException("Category not found");
            }
            if (ex.Files is not null)
            { 
                foreach (var file in ex.Files) 
                {
                    var fileUrl = await _fileService.SaveFile(file);
                    Domain.Entities.File newFile = new Domain.Entities.File();
                    newFile.Url = fileUrl;
                    await _unitOfWork.fileRepository.CreateAsync(newFile);
                }
            }
            var exam = _mapper.Map<Exam>(ex);
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

        public async Task<Guid> Update(Exam ex)
        {
            _unitOfWork.examRepository.Update(ex);
            await _unitOfWork.CommitTransactionAsync();
            return ex.Id;
        }
    }
}
