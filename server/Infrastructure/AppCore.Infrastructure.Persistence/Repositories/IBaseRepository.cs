using AppCore.Infrastructure.Persistence.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AppCore.Infrastructure.Persistence.Repositories
{
    public interface IBaseRepository<T> : IRepository where T : class
    {
        Task<T> AddAsyncAndGet(T entity);

        Task AddAsync(T entity);

        Task AddRange(IEnumerable<T> entities);

        Task<int> AddRangeAsync(IEnumerable<T> entities);

        Task DeleteAsync(T entity);

        void Delete(T entity);

        Task UpdateAsync(T entity);

        Task<int> DeleteRangeAsync(IEnumerable<T> entities);

        Task AddOrUpdateAsync(T entity, Expression<Func<T, bool>> specification);

        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(Guid id);

        Task<T> GetAsync(Expression<Func<T, bool>> specification, bool trackingChanges);

        Task<T> GetAsync(Expression<Func<T, bool>> specification);

        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "", int take = 15, int skip = 0);

        Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> specification, bool trackingChanges = true);

        Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> specification);

        IEnumerable<T> FindBy(Expression<Func<T, bool>> expression, bool trackingChanges = true);

        IEnumerable<T> FindBy(Expression<Func<T, bool>> specification);

        long Count(Expression<Func<T, bool>> specification);

        Task<long> CountAsync(Expression<Func<T, bool>> specification);

        bool Exists(Expression<Func<T, bool>> specification);

        Task<bool> ExistsAsync(Expression<Func<T, bool>> specification);

        IEnumerable<T> GetFilteredElements(Expression<Func<T, bool>> specification,
             long pageIndex, int pageSize, IList<SortByInfo> orderByExpression = null);

        Task<IEnumerable<T>> GetFilteredElementsAsync(Expression<Func<T, bool>> specification,
            long pageIndex, int pageSize, IList<SortByInfo> orderByExpression = null);

        Task<int> SaveChangesAsync();
        string UserId();
    }
}
