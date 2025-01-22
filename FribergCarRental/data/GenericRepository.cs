using FribergCarRental.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FribergCarRental.data
{
    public abstract class GenericRepository<T, TContext> : IRepository<T> 
        where T : class
        where TContext : DbContext
    {
        protected TContext dbContext;

        public GenericRepository(TContext context)
        {
            dbContext = context;
        }

        public virtual T Add(T entity)
        {
            var addedEntity = dbContext.Set<T>().Add(entity).Entity;
            dbContext.SaveChanges();

            return addedEntity;
        }

        public virtual IEnumerable<T> All()
        {
            var all = dbContext.Set<T>().ToList();
            return all;
        }

        public virtual void Delete(int id)
        {
            var deleteEntity = Get(id);
            dbContext.Set<T>().Remove(deleteEntity);
            dbContext.SaveChanges();
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return dbContext.Set<T>().AsQueryable().Where(predicate);
        }

        public virtual Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return dbContext.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public virtual T Get(int id)
        {
            return dbContext.Set<T>().Find(id);
        }

        public virtual void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        public virtual Task<List<T>> ToListAsync()
        {
            return dbContext.Set<T>().ToListAsync();
        }

        public virtual T Update(T entity)
        {
            return dbContext.Set<T>().Update(entity).Entity;
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public async Task<T> FindAsync(int? id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }

        public bool Any(Func<T, bool> value)
        {
            return dbContext.Set<T>().Any(value);
        }

        public void Remove(T entity)
        {
            dbContext.Set<T>().Remove(entity);
            dbContext.SaveChanges();
        }
        public IQueryable<T> Include(Expression<Func<T, object>> includeExpression)
        {
            return dbContext.Set<T>().Include(includeExpression);
        }

        public IQueryable<T> Query()
        {
            return dbContext.Set<T>();
        }
    }
}
