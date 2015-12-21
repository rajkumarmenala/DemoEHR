using Monad.EHR.Domain.Entities;
using Monad.EHR.Domain.Interfaces;

namespace Monad.EHR.Infrastructure.Data
{
    public class ResourceRepository : Repository<Resource>, IResourceRepository
    {
        public ResourceRepository(CustomDBContext dataContext) : base(dataContext)
        {
        }
    }
}
