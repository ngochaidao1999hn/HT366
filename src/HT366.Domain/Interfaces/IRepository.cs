using HT366.Domain.Common;
using System.Linq.Expressions;

namespace HT366.Domain.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> Table { get; }

        Task<T> CreateAsync(T entity);

        void Update(T entity);

        Task<T?> GetByIdAsync(Guid id);

        void Delete(Guid id);

        Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>>? filter = null, string[]? includeProperties = null);
    }
}