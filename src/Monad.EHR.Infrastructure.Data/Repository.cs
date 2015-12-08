using Microsoft.Data.Entity;
using System.Linq;
using Monad.EHR.Domain.Interfaces;
using Monad.EHR.Domain.Entities;

namespace Monad.EHR.Infrastructure.Data
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected DbSet<T> DbSet;
        private CustomDBContext _dbContext;

        public Repository(CustomDBContext dataContext)
        {
            _dbContext = dataContext;
            DbSet = dataContext.Set<T>();
        }

        public void Create(T entity)
        {
            DbSet.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            DbSet.Update(entity);
            _dbContext.SaveChanges();

        }
        public void Delete(T entity)
        {
            DbSet.Remove(entity);
            _dbContext.SaveChanges();

        }

        //public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        //{
        //    return null;
        //    //return DbSet.Where(predicate);
        //}

        public IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public T GetById(int id)
        {
            return DbSet.FirstOrDefault( x => x.Id == id);
        }

    }
}
