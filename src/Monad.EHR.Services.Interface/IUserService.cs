using Monad.EHR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Monad.EHR.Services.Interface
{
    public interface IUserService : IService
    {
        IList<ApplicationUser> GetUsers();
        ApplicationUser GetUserById(int userId);
        ApplicationUser GetUserByName(string userName);
        void AddUser(ApplicationUser user);
        void EditUser(ApplicationUser user);
        void DeleteUser(ApplicationUser user);
    }
}
