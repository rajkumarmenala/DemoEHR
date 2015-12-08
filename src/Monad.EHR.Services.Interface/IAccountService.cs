using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Monad.EHR.Services.Interface
{
    public interface IAccountService : IService
    {
        Task<SignInResult> Login(string userName, string password, bool rememberMe);
        Task<IdentityResult> Register(string user, string password);
        void LogOff();
    }
}
