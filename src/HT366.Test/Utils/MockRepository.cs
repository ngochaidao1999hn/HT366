using HT366.Domain.Common;
using HT366.Domain.Interfaces;
using System.Linq.Expressions;
using static HT366.Test.Utils.Helper;

namespace HT366.Test.Utils
{
    public class MockRepository<T> : IRepository<T> where T : BaseEntity
    {
        private List<T> _mockDataList { get; set; } = new List<T>();
        public IQueryable<T> Table => _mockDataList.AsQueryable();

        public async Task<T> CreateAsync(T entity)
        {
            entity.Id = GenerateId();
            await Task.Yield();
            _mockDataList.Add(entity);
            return entity;
        }

        public void Delete(Guid id)
        {
            var data = _mockDataList.Find(x => x.Id == id);
            if (data is not null)
            {
                _mockDataList.Remove(data);
            }
        }

        public async Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>>? filter = null, string[]? includeProperties = null)
        {
            var query = _mockDataList.AsQueryable();
            await Task.Yield();
            if (filter is not null)
            {
                query.Where(filter);
            }
            return query;
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            var query = _mockDataList.AsQueryable();
            await Task.Yield();
            var ret = query.FirstOrDefault(e => e.Id == id);
            return ret;
        }

        public void Update(T entity)
        {
            var index = _mockDataList.FindIndex(x => x.Id == entity.Id);
            _mockDataList[index] = entity;
        }
    }
}
