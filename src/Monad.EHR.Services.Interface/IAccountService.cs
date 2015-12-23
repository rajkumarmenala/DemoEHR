using Monad.EHR.Domain.Entities.Identity;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Claims;

namespace Monad.EHR.Services.Interface
{
    public interface IAccountService : IService
    {
        Task<SignInResult> Login(string userName, string password, bool rememberMe);
        Task<string> GetLoginToken(string userName, string password);
       // Task<User> GetUserForLoginToken(string token);
        Task<IdentityResult> Register(string user, string password);
        Task< IList<Claim>> GetClaims(User user);

        Task<User> GetUser(string userName);
        void LogOff();
    }
}
