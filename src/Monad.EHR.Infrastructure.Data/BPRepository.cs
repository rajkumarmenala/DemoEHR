using Monad.EHR.Domain.Entities;
using Monad.EHR.Domain.Interfaces;

namespace Monad.EHR.Infrastructure.Data
{
    public class BPRepository : Repository<BP>, IBPRepository
    {
        public BPRepository(CustomDBContext dataContext):base(dataContext)
        {

        }
    }
}


