using Monad.EHR.Domain.Entities;
using Monad.EHR.Domain.Interfaces;

namespace Monad.EHR.Infrastructure.Data
{
    public class WeightRepository : Repository<Weight>, IWeightRepository
    {
        public WeightRepository(CustomDBContext dataContext):base(dataContext)
        {

        }
    }
}


