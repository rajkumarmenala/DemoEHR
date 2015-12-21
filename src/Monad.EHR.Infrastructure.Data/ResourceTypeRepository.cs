using Monad.EHR.Domain.Entities;
using Monad.EHR.Domain.Interfaces;

namespace Monad.EHR.Infrastructure.Data
{
    public class ResourceTypeRepository : Repository<ResourceType>, IResourceTypeRepository
    {
        public ResourceTypeRepository(CustomDBContext dataContext) : base(dataContext)
        {
        }
    }
}
