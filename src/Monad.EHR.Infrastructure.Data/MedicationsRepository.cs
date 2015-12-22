using Monad.EHR.Domain.Entities;
using Monad.EHR.Domain.Interfaces;

namespace Monad.EHR.Infrastructure.Data
{
    public class MedicationsRepository : Repository<Medications>, IMedicationsRepository
    {
        public MedicationsRepository(CustomDBContext dataContext):base(dataContext)
        {

        }
    }
}


