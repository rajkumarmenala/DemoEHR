using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Monad.EHR.Common.StateManagement;
using Monad.EHR.Services.Interface;
using Monad.EHR.Web.App.Security;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;

namespace Monad.EHR.Web.App.Policies
{
    public class TokenAuthRequirement : AuthorizationHandler<TokenAuthRequirement>, IAuthorizationRequirement
    {
        protected override void Handle(AuthorizationContext context, TokenAuthRequirement requirement)
        {
            var httpContext = (context.Resource as ActionContext).HttpContext;
            var request = httpContext.Request;
           
            if (SecurityHelper.SkipRequired(request.Path))
            {
                context.Succeed(requirement);
                return;
            }

            if (IsAuthorizedForRequestedAction(context, requirement))
            {
                context.Succeed(requirement);
            }
            else
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Fail();
            }
        }

        private bool IsAuthorizedForRequestedAction(AuthorizationContext context, TokenAuthRequirement requirement)
        {
            
            var httpContext = (context.Resource as ActionContext).HttpContext;
            var cacheInstance = httpContext.ApplicationServices.GetService(typeof(ICacheProvider)) as ICacheProvider;
            var accountService = httpContext.ApplicationServices.GetService(typeof(IAccountService)) as IAccountService;
            var tokenAuthIdentities = context.User.Identities.Where(x => x.AuthenticationType == TokenAuthOptions.Scheme).FirstOrDefault();

            if (tokenAuthIdentities == null ) return false;

            var authHeaderClaim = tokenAuthIdentities.Claims.Where(x => x.Type == ClaimTypes.Authentication).FirstOrDefault();

            //context.User.Identities.Select(x=> x.)
            var uriClaim = context.User.Claims.Where(x => x.Type == ClaimTypes.Uri).FirstOrDefault();
            if (uriClaim == null || authHeaderClaim == null)  return false;

            var claimComponents = uriClaim.Value.Split('/').Skip(2).ToList();
            var tobeMathedClaim = claimComponents[0].ToLower() + "/" + claimComponents[1].ToLower(); // CONTROLLER.ACTION
            var currentCacheKey = string.Format("User-{0}-{1}", context.User.GetUserId(), tobeMathedClaim);
         
            if (!cacheInstance.Contains(currentCacheKey))
            {
                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.ReadJwtToken(Convert.ToString(httpContext.Items["AuthToken"]));
                var clm = securityToken.Claims.Where(x => string.Equals(x.Type, "unique_name", StringComparison.CurrentCultureIgnoreCase)).SingleOrDefault();
                var usr = accountService.GetUser(clm.Value).Result;
                var claims = accountService.GetClaims(usr).Result;
                var permission = claims.Where(x => tobeMathedClaim.Contains(x.Type.ToLower()))
                                .Select(y => y.Value).SingleOrDefault();
                cacheInstance.Set<string>(currentCacheKey, permission, 300);// set for 5 minutes, change this according to project requirement
            }
            var result = cacheInstance.Get<string>(currentCacheKey); 
            return ((!string.IsNullOrWhiteSpace(result)) && (result.ToLower() == "allowed"));
        }
       
    }
}
