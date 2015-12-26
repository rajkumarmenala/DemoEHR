using Microsoft.AspNet.Identity;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Monad.EHR.Domain.Interfaces.Identity;
using Monad.EHR.Domain.Interfaces;
using Monad.EHR.Domain.Entities;
using Monad.EHR.Domain.Entities.Identity;
using Monad.EHR.Services.Interface;
using System.Security.Claims;
using Monad.EHR.Common.Utility;

namespace Monad.EHR.Services.Business
{
    public class AccountService : IAccountService
    {
        private IUserService _userService;
        private IIdentityRepository _store;
        private IActivityService _activityService;
        private IRoleRightRepository _roleRightRepository;
        private IActivityRepository _activityRepository;
        private IResourceRepository _resourceRepository;
        private IResourceTypeRepository _resourceTypeRepository;

        public AccountService(UserManager<User> userManager,
            RoleManager<Role> roleMananager,
            SignInManager<User> signInManager,
            IIdentityRepository store,
            IActivityService activityService,
            IUserService userService,
            IRoleRightRepository roleRightRepository,
             IActivityRepository activityRepository,
             IResourceRepository resourceRepository,
             IResourceTypeRepository resourceTypeRepository)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleMananager;
            _store = store;
            _activityService = activityService;
            _userService = userService;
            _roleRightRepository = roleRightRepository;
            _activityRepository = activityRepository;
            _resourceRepository = resourceRepository;
            _resourceTypeRepository = resourceTypeRepository;
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
                    var resourceTypes = _resourceTypeRepository.GetAll().ToList();
                    var formResourceTypeId = resourceTypes.Where(x => string.Equals(x.Name, "Form")).SingleOrDefault().Id;
                    var URLResourceTypeId = resourceTypes.Where(x => string.Equals(x.Name, "URL")).SingleOrDefault().Id;
                    var APIResourceTypeId = resourceTypes.Where(x => string.Equals(x.Name, "API")).SingleOrDefault().Id;
                    var resultRole = await RoleManager.FindByNameAsync("Administrator");
                    // DONT fire this code if you  dont want activity based security
                    var roleRights = _roleRightRepository.GetAll().Where(x => string.Equals(x.RoleId, resultRole.Id)).ToList();

                    var formElementsClaims = from r in _resourceRepository.GetAll().Where(x => x.ResourceTypeId == formResourceTypeId)
                                             join rr in roleRights on r.Id equals rr.ResourceId
                                             join a in _activityRepository.GetAll().Where(x => x.ResourceTypeId == formResourceTypeId) on rr.ActivityId equals a.Id
                                             select r.Name + "." + a.Value;

                    //// assign claims (activities)  for current role to this user
                    await UserManager.AddClaimsAsync(newUser, formElementsClaims.Select(x => new System.Security.Claims.Claim(x, "Allowed")));

                    var URLElementsClaims = from r in _resourceRepository.GetAll().Where(x => x.ResourceTypeId == URLResourceTypeId)
                                            join rr in roleRights on r.Id equals rr.ResourceId
                                            join a in _activityRepository.GetAll().Where(x => x.ResourceTypeId == URLResourceTypeId) on rr.ActivityId equals a.Id
                                            select "/" + a.Value.ToCamelCase() + r.Name;

                    await UserManager.AddClaimsAsync(newUser, URLElementsClaims.Select(x => new System.Security.Claims.Claim(x, "Allowed")));


                    var apiClaims = from r in _resourceRepository.GetAll().Where(x => x.ResourceTypeId == APIResourceTypeId)
                                    join rr in roleRights on r.Id equals rr.ResourceId
                                    join a in _activityRepository.GetAll().Where(x => x.ResourceTypeId == APIResourceTypeId) on rr.ActivityId equals a.Id
                                    select r.Name + "/" + a.Value;

                    await UserManager.AddClaimsAsync(newUser, apiClaims.Select(x => new System.Security.Claims.Claim(x, "Allowed")));
                }
            }
            return (createdUser);
        }

        public async Task<IList<Claim>> GetClaims(User user)
        {
            return await UserManager.GetClaimsAsync(user);
        }

        public async Task<User> GetUser(string userName)
        {
            var user = UserManager.FindByNameAsync(userName).Result;
            return await Task.FromResult<User>(user);
        }
    }
}