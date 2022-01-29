using System.Linq.Expressions;

namespace Management.Services.Services.IRepository;

public interface IRepositoryAsync<T> where T :class
{
    Task<T> GetAsync(int id);
    Task<T> GetAsync(string id);
    Task<IEnumerable<T>> GetAllAsync(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string includeProperties = null
    );
    Task<T> GetFirstOrDefaultAsync(
        Expression<Func<T, bool>> filter = null,
        string includeProperties = null
    );

    Task AddRangeAsync(List<T> entities);
    Task AddAsync(T entity);
    Task RemoveAsync(int id);
    Task RemoveRangeAsync(IEnumerable<T> entity);
}