using Monad.EHR.Domain.Entities;
using Monad.EHR.Domain.Entities.Identity;
using Monad.EHR.Services.Interface;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace Monad.EHR.Services.Business
{
    public class AccountService : IAccountService
    {
        private IUserService _userService;
        public AccountService(UserManager<User> userManager,
            SignInManager<User> signInManager,
            IUserService userService)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            _userService = userService;
        }

        public UserManager<User> UserManager { get; private set; }

        public SignInManager<User> SignInManager { get; private set; }

        public async Task<SignInResult> Login(string userName, string password, bool rememberMe)
        {
            return await SignInManager.PasswordSignInAsync(userName, password, rememberMe, lockoutOnFailure: false);
        }

        public void LogOff()
        {
           SignInManager.SignOutAsync();
        }

        public async Task<IdentityResult> Register(string user, string password)
        {
            var targetUser = new User { UserName = user, Email = user };
            var result = await UserManager.CreateAsync(targetUser, password);

            if (result.Succeeded)
            {
                var code = await UserManager.GenerateEmailConfirmationTokenAsync(targetUser);
                _userService.AddUser(new ApplicationUser() { UserName = user });
            }
            return result;
        }
    }
}