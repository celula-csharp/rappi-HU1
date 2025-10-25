using System.Linq.Expressions;

namespace rappi.Application.Interfaces;

public interface IRepository<T> where T : class
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T> AddAsync(T entity);
    Task<bool> UpdateAsync(T entity);
    Task<bool> DeleteAsync(int id);
    Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task<List<T>> GetAllIncludingAsync(params Expression<Func<T, object>>[] includes);
    Task<T?> GetByIdIncludingAsync(int id, params Expression<Func<T, object>>[] includes);
}