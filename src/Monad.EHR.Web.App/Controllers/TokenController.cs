using System;
using System.Linq;
using Microsoft.AspNet.Mvc;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNet.Authorization;
using System.Security.Principal;
using Microsoft.AspNet.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using Monad.EHR.Web.App.Policies;
using Monad.EHR.Services.Interface;
using Monad.EHR.Web.App.Models;

namespace Monad.EHR.Web.App.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly TokenAuthOptions tokenOptions;
        private IAccountService _accountService;

        public TokenController(IAccountService accountService, TokenAuthOptions tokenOptions)
        {
            this.tokenOptions = tokenOptions;
            _accountService = accountService;
        }


        [HttpGet]
        [Authorize("Bearer")]
        public dynamic Get()
        {

            bool authenticated = false;
            string user = null;
            int entityId = -1;
            string token = null;
            DateTime? tokenExpires = default(DateTime?);

            var currentUser = HttpContext.User;
            if (currentUser != null)
            {
                authenticated = currentUser.Identity.IsAuthenticated;
                if (authenticated)
                {
                    user = currentUser.Identity.Name;
                    tokenExpires = DateTime.UtcNow.AddMinutes(10);
                    token = GetToken(currentUser.Identity.Name, tokenExpires);
                }
            }
            return new { authenticated = authenticated, user = user, entityId = entityId, token = token, tokenExpires = tokenExpires };
        }

       
        [HttpPost]
        public dynamic Post([FromBody]LoginViewModel model)
        {
            AccountsApiModel accountsWebApiModel = new AccountsApiModel();

            if (ModelState.IsValid)
            {
                var result = _accountService.Login(model.UserName, model.Password, model.RememberMe).Result;
                if (result.Succeeded)
                {
                    DateTime? expires = DateTime.UtcNow.AddMinutes(10);
                    accountsWebApiModel.User.UserName = model.UserName;
                    accountsWebApiModel.Token = GetToken(model.UserName, expires);
                    accountsWebApiModel.Authenticated = true;
                    return new ObjectResult(accountsWebApiModel);
                }
            }
            return new ObjectResult("Invalid username or password.");
        }

        private string GetToken(string user, DateTime? expires)
        {
            var handler = new JwtSecurityTokenHandler();
            var currentUser = _accountService.GetUser(user);

            ClaimsIdentity identity = new ClaimsIdentity(new GenericIdentity(user, "TokenAuth"), currentUser.Result.Claims.Select(x => new Claim(x.ClaimType, x.ClaimValue)).ToList());
            var securityToken = handler.CreateToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                signingCredentials: tokenOptions.SigningCredentials,
                subject: identity,
                expires: expires
                );
            return handler.WriteToken(securityToken);
        }
    }
}
