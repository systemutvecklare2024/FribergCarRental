using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FribergCarRental.data
{
    public abstract class GenericRepository<T, CTX> : IRepository<T> 
        where T : class
        where CTX : DbContext
    {
        protected CTX dbContext;

        public GenericRepository(CTX context)
        {
            dbContext = context;
        }
        public virtual T Add(T entity)
        {
            var addedEntity = dbContext.Add(entity).Entity;

            return addedEntity;
        }

        public virtual IEnumerable<T> All()
        {
            var all = dbContext.Set<T>().ToList();
            return all;
        }

        public virtual void Delete(int id)
        {
            dbContext.Remove(id);
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            var result = dbContext.Set<T>().AsQueryable().Where(predicate);
            return result;
        }

        public virtual T Get(int id)
        {
            return dbContext.Find<T>(id);
        }

        public virtual void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        public virtual T Update(T entity)
        {
            return dbContext.Update(entity).Entity;
        }
    }
}
