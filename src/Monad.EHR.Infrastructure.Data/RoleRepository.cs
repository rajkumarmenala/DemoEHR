
using System;
using System.Collections.Generic;
using Monad.EHR.Domain.Entities;
using Monad.EHR.Domain.Interfaces;

namespace Monad.EHR.Infrastructure.Data
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(CustomDBContext dataContext):base(dataContext)
        {

        }
        public IEnumerable<Role> GetRolesByUserId(int userId)
        {
            throw new NotImplementedException();
        }
    }
}


