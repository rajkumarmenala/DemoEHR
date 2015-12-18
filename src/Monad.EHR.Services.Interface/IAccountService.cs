using Monad.EHR.Domain.Entities.Identity;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace Monad.EHR.Services.Interface
{
    public interface IAccountService : IService
    {
        Task<SignInResult> Login(string userName, string password, bool rememberMe);
        Task<string> GetLoginToken(string userName, string password);
        Task<User> GetUserForLoginToken(string token);
        Task<IdentityResult> Register(string user, string password);
        void LogOff();
    }
}
