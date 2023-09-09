using HT366.Domain.Entities;
using HT366.Domain.Interfaces;
using HT366.Infrastructure.Persistence;
using HT366.Infrastructure.Persistence.Repositories;

namespace HT366.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRepository<Exam> examRepository { get; }
        public IRepository<Lesson> lessonRepository { get; }
        public IRepository<Exercise> exerciseRepository { get; }
        public IRepository<Category> categoryRepository { get; }
        public IRepository<Domain.Entities.File> fileRepository { get; }
        private bool disposedValue;
        private ApplicationContext _context { get; set; }

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            this.examRepository = new Repository<Exam>(_context);
            this.lessonRepository = new Repository<Lesson>(_context);
            this.exerciseRepository = new Repository<Exercise>(_context);
            this.categoryRepository = new Repository<Category>(_context);
            this.fileRepository = new Repository<Domain.Entities.File>(_context);
        }

        public async Task<bool> CommitTransactionAsync(CancellationToken cancellationToken = default, Guid? internalCommandId = null)
        {
            if (await _context.SaveChangesAsync() > 0)
            {
                await _context.Database.CommitTransactionAsync(cancellationToken);
                return true;
            }
            return false;
        }

        public async Task RollBackTransactionAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    _context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~UnitOfWork()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}