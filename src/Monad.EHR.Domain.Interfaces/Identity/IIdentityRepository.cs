using Monad.EHR.Domain.Entities;
using System.Threading.Tasks;

namespace Monad.EHR.Domain.Interfaces.Identity
{
    public interface IIdentityRepository 
    {
        Task<bool> AssignActivities(UserActivity userActivity, IUserActivityRepository userActivityRepository);
    }
} 