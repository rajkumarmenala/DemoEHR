using Monad.EHR.Domain.Entities;
using Monad.EHR.Domain.Interfaces;

namespace Monad.EHR.Infrastructure.Data
{
    public class ActivityRoleRepository : Repository<ActivityRole>, IActivityRoleRepository
    {
        public ActivityRoleRepository(CustomDBContext dataContext):base(dataContext)
        {

        }
    }
}


