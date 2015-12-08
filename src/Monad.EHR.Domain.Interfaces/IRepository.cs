using System.Linq;

namespace Monad.EHR.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T GetById(int id);
      //  IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
