using Monad.EHR.Domain.Entities;
using Monad.EHR.Domain.Interfaces;

namespace Monad.EHR.Infrastructure.Data
{
    public class RoleRightRepository : Repository<RoleRight>, IRoleRightRepository
    {
        public RoleRightRepository(CustomDBContext dataContext) : base(dataContext)
        {
        }
    }
}
