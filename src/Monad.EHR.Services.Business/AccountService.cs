using Microsoft.AspNet.Identity;
using System.Linq;
using System.Collections.Generic;

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
        public AccountService(UserManager<User> userManager,
            RoleManager<Role> roleMananager,
            SignInManager<User> signInManager,
            IIdentityRepository store,
            IUserActivityRepository userActivityRepository,
            IActivityService activityService,
            IUserService userService)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleMananager;
            _store = store;
            _userActivityRepository = userActivityRepository;
            _activityService = activityService;
            _userService = userService;
        }

        public UserManager<User> UserManager { get; private set; }

        public SignInManager<User> SignInManager { get; private set; }

        public RoleManager<Role> RoleManager { get; private set; }

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

                var newUser = UserManager.FindByNameAsync(user).Result;
                var resultRole = await RoleManager.FindByNameAsync("Clinician");
                // var resultRole =  await RoleManager.FindByNameAsync("Clinician");
                var userRoleResult = await UserManager.AddToRoleAsync(newUser, "Clinician");
                if (userRoleResult.Succeeded)
                {
                    // DONT fire this code if you  dont want activity based security
                   

                    var activities = _activityService.GetActivitiesByRoleId(resultRole.Id);
                    foreach (var a in activities) // loop doing one by one is slower approach though
                    {
                        var userActivity = new UserActivity
                        {
                            ActivityID = a.Id,
                            UserID = newUser.Id
                        };
                        bool ActivityStatus = await _store.AssignActivities(userActivity, _userActivityRepository);
                    }
                }
            }
            return result;
        }
    }
}