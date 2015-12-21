using Microsoft.AspNet.Identity;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Monad.EHR.Domain.Interfaces.Identity;
using Monad.EHR.Domain.Interfaces;
using Monad.EHR.Domain.Entities;
using Monad.EHR.Domain.Entities.Identity;
using Monad.EHR.Services.Interface;
using System;
using System.Security.Claims;

namespace Monad.EHR.Services.Business
{
    public class AccountService : IAccountService
    {
        private IUserService _userService;
        private IIdentityRepository _store;
        private IActivityService _activityService;
        private ICustomUserTokenProvider _tokenProvider;
        private IRoleRightRepository _roleRightRepository;
        private IActivityRepository _activityRepository;
        private IResourceRepository _resourceRepository;

        public AccountService(UserManager<User> userManager,
            RoleManager<Role> roleMananager,
            SignInManager<User> signInManager,
            IIdentityRepository store,
            IActivityService activityService,
            IUserService userService,
            ICustomUserTokenProvider tokenProvider,
            IRoleRightRepository roleRightRepository,
             IActivityRepository activityRepository,
             IResourceRepository resourceRepository)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleMananager;
            _store = store;
            _activityService = activityService;
            _userService = userService;
            _tokenProvider = tokenProvider;
            _roleRightRepository = roleRightRepository;
            _activityRepository = activityRepository;
            _resourceRepository = resourceRepository;
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
            var createdUser = await UserManager.CreateAsync(targetUser, password);

            if (createdUser.Succeeded)
            { 
                var code = await UserManager.GenerateEmailConfirmationTokenAsync(targetUser);
                _userService.AddUser(new ApplicationUser() { UserName = user });

                var newUser = UserManager.FindByNameAsync(user).Result;

                var userRoleResult = await UserManager.AddToRoleAsync(newUser, "Administrator");
                if (userRoleResult.Succeeded)
                {
                    var resultRole = await RoleManager.FindByNameAsync("Administrator");
                    // DONT fire this code if you  dont want activity based security
                    var roleRights = _roleRightRepository.GetAll().Where(x => string.Equals(x.RoleId, resultRole.Id)).ToList();

                    var tobeAddedClaims = from r in _resourceRepository.GetAll()
                              join rr in roleRights on r.Id equals rr.ResourceId
                              join a in _activityRepository.GetAll() on rr.ActivityId equals a.Id
                              select r.Name+"."+a.Value;

                    //// assign claims (activities)  for current role to this user
                    await UserManager.AddClaimsAsync(newUser, tobeAddedClaims.Select(x => new System.Security.Claims.Claim(x, "Allowed")));
                }
            }
           return(createdUser);
        }

        public async Task<IList<Claim>> GetClaims(User user)
        {
            return await UserManager.GetClaimsAsync(UserManager.FindByNameAsync(user.Email).Result);
        }


    }
}