using Monad.EHR.Domain.Entities;
using Monad.EHR.Domain.Interfaces;

namespace Monad.EHR.Infrastructure.Data
{
    public class ProblemsRepository : Repository<Problems>, IProblemsRepository
    {
        public ProblemsRepository(CustomDBContext dataContext):base(dataContext)
        {

        }
    }
}


