using HT366.Domain.Common;
using HT366.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HT366.Infrastructure.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected ApplicationContext _context { get; set; }

        public IQueryable<T> Table { get; }

        protected DbSet<T> dbSet;

        public Repository(ApplicationContext context)
        {
            _context = context;
            this.dbSet = context.Set<T>();
            Table = this.dbSet.AsNoTracking();
        }

        public async Task<T> CreateAsync(T entity)
        {
            var res = await dbSet.AddAsync(entity);
            return res.Entity;
        }

        public void Delete(Guid id)
        {
            T? entityToDelete = dbSet.Find(id);
            if (entityToDelete is not null)
            {
                _context.Remove(entityToDelete);
            }
        }

        public async Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>>? filter = null, string[]? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            var data = await query.ToListAsync();
            return data.AsQueryable();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await dbSet.FindAsync(id);
        }

        public void Update(T entity)
        {
            dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
