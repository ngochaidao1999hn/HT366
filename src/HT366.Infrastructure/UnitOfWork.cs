using HT366.Domain.Entities;
using HT366.Domain.Interfaces;
using HT366.Infrastructure.Persistence;

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

        public UnitOfWork(ApplicationContext context,
            IRepository<Exam> examRepository,
            IRepository<Lesson> lessonRepository,
            IRepository<Exercise> exerciseRepository,
            IRepository<Category> categoryRepository,
            IRepository<Domain.Entities.File> fileRepository)
        {
            _context = context;
            this.examRepository = examRepository;
            this.lessonRepository = lessonRepository;
            this.exerciseRepository = exerciseRepository;
            this.categoryRepository = categoryRepository;
            this.fileRepository = fileRepository;
        }

        public async Task<bool> CommitTransactionAsync(CancellationToken cancellationToken = default, Guid? internalCommandId = null)
        {
            if (await _context.SaveChangesAsync() > 0)
            {
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
