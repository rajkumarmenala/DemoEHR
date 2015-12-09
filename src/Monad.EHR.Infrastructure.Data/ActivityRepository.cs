using Monad.EHR.Domain.Entities;
using Monad.EHR.Domain.Interfaces;

namespace Monad.EHR.Infrastructure.Data
{
    public class ActivityRepository : Repository<Activity>, IActivityRepository
    {
        public ActivityRepository(CustomDBContext dataContext):base(dataContext)
        {

        }
    }
}


