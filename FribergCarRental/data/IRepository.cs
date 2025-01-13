using FribergCarRental.Models;
using System.Linq.Expressions;

namespace FribergCarRental.data
{
    public interface IRepository<T>
    {
        T Add(T entity);
        T Update(T entity);
        T Get(int id);
        void Delete(int id);
        IEnumerable<T> All();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        void SaveChanges();

        Task SaveChangesAsync();

        Task<List<T>> ToListAsync();
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

        Task<T> FindAsync(int? id);

        bool Any(Func<T, bool> value);
        void Remove(T entity);
    }
}
