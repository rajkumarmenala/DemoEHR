using Monad.EHR.Domain.Entities;
using Monad.EHR.Domain.Interfaces;

namespace Monad.EHR.Infrastructure.Data
{
    public class PatientHeightRepository : Repository<PatientHeight>, IPatientHeightRepository
    {
        public PatientHeightRepository(CustomDBContext dataContext):base(dataContext)
        {

        }
    }
}


