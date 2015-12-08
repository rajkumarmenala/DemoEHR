using Monad.EHR.Domain.Entities;
using Monad.EHR.Domain.Interfaces;

namespace Monad.EHR.Infrastructure.Data
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(CustomDBContext dataContext):base(dataContext)
        {

        }
    }
}


