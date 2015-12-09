
using Monad.EHR.Domain.Entities;
using System.Collections.Generic;

namespace Monad.EHR.Services.Interface
{
    public interface IRoleService : IService
    {
        void AddRole(Role role);
        void EditRole(Role role);
        void DeleteRole(Role role);
        IList<Role> GetAllRole();
        Role GetRoleById(int id);
        IList<Role> GetRolesByUserId(int userId);
    }
}
