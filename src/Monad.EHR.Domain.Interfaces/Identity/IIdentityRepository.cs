using Microsoft.AspNet.Identity;
using Monad.EHR.Domain.Entities;
using Monad.EHR.Domain.Entities.Identity;
using System.Threading.Tasks;

namespace Monad.EHR.Domain.Interfaces.Identity
{
    public interface IIdentityRepository 
    {
        Task<bool> AssignActivities(UserActivity userActivity, IUserActivityRepository userActivityRepository);
    }
} 