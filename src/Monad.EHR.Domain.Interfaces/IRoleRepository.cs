using System.Collections.Generic;
using Monad.EHR.Domain.Entities;

namespace Monad.EHR.Domain.Interfaces
{
    public interface IRoleRepository : IRepository<Role>
    {

        /// <summary>
        /// Gets the roles by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        IEnumerable<Role> GetRolesByUserId(int userId);
    }
}