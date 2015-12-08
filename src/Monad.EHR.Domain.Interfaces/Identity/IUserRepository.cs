using Monad.EHR.Domain.Entities.Identity;

namespace Monad.EHR.Domain.Interfaces.Identity
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByEmailId(string email);
    }
}