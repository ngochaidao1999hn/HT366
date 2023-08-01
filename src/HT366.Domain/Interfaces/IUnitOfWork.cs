using HT366.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT366.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IRepository<Lesson> lessonRepository { get; }
        public IRepository<Exam> examRepository { get; }
        public IRepository<Exercise> exerciseRepository { get; }
        public IRepository<Category> categoryRepository { get; }
        IRepository<Entities.File> fileRepository { get; }
        Task<bool> CommitTransactionAsync(CancellationToken cancellationToken = default, Guid? internalCommandId = null);
        Task RollBackTransactionAsync();
    }
}
