using Monad.EHR.Domain.Entities;
using System.Collections.Generic;

namespace  Monad.EHR.Services.Interface
{
    public interface IRoleService : IService
    {
        IList<RoleRight> GetAllRightByRoleId(string roleId);
        void AddRoleRight(RoleRight roleRight);
        void EditRoleRight(RoleRight roleRight);
        void DeleteRoleRight(RoleRight roleRight);
    }
}
