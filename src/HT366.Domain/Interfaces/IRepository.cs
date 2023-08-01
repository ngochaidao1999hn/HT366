using HT366.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
