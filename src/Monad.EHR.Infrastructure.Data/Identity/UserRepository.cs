
using System;
using Monad.EHR.Domain.Entities.Identity;
using Monad.EHR.Domain.Interfaces.Identity;
using Microsoft.Data.Entity;
using System.Linq;

namespace Monad.EHR.Infrastructure.Data.Identity
{
    //public class UserRepository : IUserRepository
    //{
    //    protected DbSet<User> DbSet;
    //    private CustomDBContext _dbContext;

    //    public UserRepository(CustomDBContext dataContext)
    //    {
    //        _dbContext = dataContext;
    //        DbSet = dataContext.Set<User>();
    //    }

    //    public void Create(User entity)
    //    {
    //        DbSet.Add(entity);
    //        _dbContext.SaveChanges();
    //    }

    //    public void Update(User entity)
    //    {
    //        throw new NotImplementedException();

    //    }
    //    public void Delete(User entity)
    //    {
    //        DbSet.Remove(entity);
    //        _dbContext.SaveChanges();
    //    }

    //    public IQueryable<User> GetAll()
    //    {
    //        return DbSet;
    //    }

    //    public User GetById(int id)
    //    {
    //        return DbSet.FirstOrDefault();
    //    }

    //    public User GetByEmailId(string email)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

}

