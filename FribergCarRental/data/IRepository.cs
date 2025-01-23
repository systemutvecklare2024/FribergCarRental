using FribergCarRental.Models.Entities;
using System.Linq.Expressions;

namespace FribergCarRental.data
{
    public interface IRepository<T>
    {
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<T?> GetAsync(int id);
        Task<T?> AddAsync(T entity);
        Task<IEnumerable<T>?> AllAsync();
        Task RemoveAsync(T entity);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task UpdateAsync(T entity);
    }
}
