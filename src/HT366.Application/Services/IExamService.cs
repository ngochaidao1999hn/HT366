using HT366.Application.Dtos.Exam;
using HT366.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HT366.Application.Services
{
    public interface IExamService
    {
        Task<Guid> Insert(Guid userId, CreateExamDto ex);
        Task<Guid> Update(Exam ex);
        Task<bool> Delete(Guid id);
        Task<IEnumerable<ExamReadDto>> GetAll(GetExamFilter filter);
        Task<ExamReadDto?> GetById(Guid id);
    }
}
