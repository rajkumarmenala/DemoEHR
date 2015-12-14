using Microsoft.AspNet.Identity;
using System.Linq;

using System.Threading.Tasks;
using Monad.EHR.Domain.Interfaces.Identity;
using Monad.EHR.Domain.Interfaces;
using Monad.EHR.Domain.Entities;
using Monad.EHR.Domain.Entities.Identity;
using Monad.EHR.Services.Interface;

namespace Monad.EHR.Services.Business
{
    public class AccountService : IAccountService
    {
        private IUserService _userService;
        private IIdentityRepository _store;
        private IActivityService _activityService;
        private IUserActivityRepository _userActivityRepository;
        private ICustomUserTokenProvider _tokenProvider;
        public AccountService(UserManager<User> userManager,
            RoleManager<Role> roleMananager,
            SignInManager<User> signInManager,
            IIdentityRepository store,
            IUserActivityRepository userActivityRepository,
            IActivityService activityService,
            IUserService userService,
            ICustomUserTokenProvider tokenProvider)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleMananager;
            _store = store;
            _userActivityRepository = userActivityRepository;
            _activityService = activityService;
            _userService = userService;
            _tokenProvider = tokenProvider;
            UserManager.RegisterTokenProvider("CustomToken", _tokenProvider as IUserTokenProvider<User>);
        }

        public UserManager<User> UserManager { get; private set; }

        public SignInManager<User> SignInManager { get; private set; }

        public RoleManager<Role> RoleManager { get; private set; }

        public async Task<SignInResult> Login(string userName, string password, bool rememberMe)
        {
            return await SignInManager.PasswordSignInAsync(userName, password, rememberMe, lockoutOnFailure: false);
        }

        public async Task<string> GetLoginToken(string userName, string password)
        {
            var user = UserManager.FindByNameAsync(userName).Result;
            return await UserManager.GenerateUserTokenAsync(user, "CustomToken", "Token Check");
        }

        public async Task<User> GetUserForLoginToken(string token)
        {
            return await _tokenProvider.GetUserFromToken(token, UserManager);
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

                var newUser = UserManager.FindByNameAsync(user).Result;

                var userRoleResult = await UserManager.AddToRoleAsync(newUser, "Clinician");
                if (userRoleResult.Succeeded)
                {
                    // DONT fire this code if you  dont want activity based security
                    var resultRole = await RoleManager.FindByNameAsync("Clinician");
                   
                    var actions = new string [] { "Add{0}", "Edit{0}", "Delete{0}", "GetAll{0}s","Get{0}" };
                    var activities = _activityService.GetActivitiesByRoleId(resultRole.Id);
                    var formActions = (from activity in activities
                                 from action in actions
                                select activity.Value + "." + string.Format( action, activity.Value));

                    // assign claims (activities)  for current role to this user
                    await UserManager.AddClaimsAsync(newUser, formActions.Select(x => new System.Security.Claims.Claim(x, "Allowed")));
                }
            }
            return result;
        }
    }
}