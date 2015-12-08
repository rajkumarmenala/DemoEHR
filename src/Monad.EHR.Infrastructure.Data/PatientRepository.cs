using Monad.EHR.Domain.Entities;
using Monad.EHR.Domain.Interfaces;

namespace Monad.EHR.Infrastructure.Data
{
    public class PatientRepository : Repository<Patient>, IPatientRepository
    {
        public PatientRepository(CustomDBContext dataContext):base(dataContext)
        {

        }
    }
}


