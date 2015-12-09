using Monad.EHR.Domain.Entities;
using Monad.EHR.Domain.Interfaces;

namespace Monad.EHR.Infrastructure.Data
{
    public class UserActivityRepository : Repository<UserActivity>, IUserActivityRepository
    {
        public UserActivityRepository(CustomDBContext dataContext):base(dataContext)
        {

        }
    }
}


