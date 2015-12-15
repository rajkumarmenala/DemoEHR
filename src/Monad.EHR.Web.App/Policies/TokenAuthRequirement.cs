using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Monad.EHR.Common.StateManagement;
using Monad.EHR.Web.App.Security;
using System.Linq;
using System.Net;
using System.Security.Claims;

namespace Monad.EHR.Web.App.Policies
{
    public class TokenAuthRequirement : AuthorizationHandler<TokenAuthRequirement>, IAuthorizationRequirement
    {
        protected override void Handle(AuthorizationContext context, TokenAuthRequirement requirement)
        {
            var actionContext = context.Resource as ActionContext;
            var httpContext = actionContext.HttpContext;
            var request = httpContext.Request;
            var path = request.Path;
            var cacheInstance = httpContext.ApplicationServices.GetService(typeof(ICacheProvider)) as ICacheProvider;

            bool authorizationRequired = !(SecurityHelper.SkipRequired(path));
            if (!authorizationRequired)
            {
                context.Succeed(requirement);
                return;
            }

            var retrievedUser = SecurityHelper.GetUser(httpContext);

            // grab the identity for the TokenAuth authentication
            var TokenAuthIdentities = context.User.Identities.Where(x => x.AuthenticationType == TokenAuthOptions.Scheme).FirstOrDefault();
            if (TokenAuthIdentities == null ||
                (string.Compare(retrievedUser.NormalizedUserName, context.User.Identity.Name.ToUpper()) > 0))
            {
                RespondUnauthorized(context, requirement);
                return;
            }

            // grab the authentication header and uri types for our identity
            var authHeaderClaim = TokenAuthIdentities.Claims.Where(x => x.Type == ClaimTypes.Authentication).FirstOrDefault();
            var uriClaim = context.User.Claims.Where(x => x.Type == ClaimTypes.Uri).FirstOrDefault();
            if (uriClaim == null || authHeaderClaim == null)
            {
                RespondUnauthorized(context, requirement);
                return;
            }

            var claimComponents = uriClaim.Value.Split('/').Skip(2).ToList();
            var controllerName = claimComponents[0];
            var actionName = claimComponents[1];
            var tobeMathedClaim = controllerName + "." + actionName;
            var currentCacheKey = string.Format("User-{0}-{1}", context.User.GetUserId(), tobeMathedClaim);

            if (!cacheInstance.Contains(currentCacheKey))
            {
                var permission = context.User.Claims.Where(x => string.Equals(x.Type, tobeMathedClaim, System.StringComparison.InvariantCultureIgnoreCase))
                                .Select(y => y.Value).SingleOrDefault();
                cacheInstance.Set<string>(currentCacheKey, permission,300);// set for 5 minutes
            }

            if (cacheInstance.Get<string>(currentCacheKey).ToUpper() == "ALLOWED")
                context.Succeed(requirement);
            else
                RespondUnauthorized(context, requirement);
        }

        private void RespondUnauthorized(AuthorizationContext context, TokenAuthRequirement requirement)
        {
            var actionContext = context.Resource as ActionContext;
            var httpContext = actionContext.HttpContext;
            httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Fail();
        }
    }
}
