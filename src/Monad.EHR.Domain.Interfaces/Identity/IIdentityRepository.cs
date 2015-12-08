using Microsoft.AspNet.Identity;
using Monad.EHR.Domain.Entities;
using Monad.EHR.Domain.Entities.Identity;
using System.Threading.Tasks;

namespace Monad.EHR.Domain.Interfaces.Identity
{
    public interface IIdentityRepository : IRepository<User>, IUserStore<User>, IUserPasswordStore<User>,
         IUserSecurityStampStore<User>
    {
        Task<bool> AssignRole(UserRole userRole);

        Task<bool> AssignActivities(UserActivity userActivity);
    }
}