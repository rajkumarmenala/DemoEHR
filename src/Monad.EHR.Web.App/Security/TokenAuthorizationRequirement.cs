using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Authorization.Infrastructure;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Monad.EHR.Domain.Entities.Identity;
using Monad.EHR.Services.Business;
using System;

namespace Monad.EHR.Web.App.Security
{
    public interface ITokenAuthorizationRequirement : IAuthorizationRequirement
    {
    }

    public class TokenAuthorizationRequirement : AuthorizationHandler<TokenAuthorizationRequirement>, ITokenAuthorizationRequirement
    {
        public TokenAuthorizationRequirement()
        {
        }

        protected override void Handle(AuthorizationContext context, TokenAuthorizationRequirement requirement)
        {
            var actionContext = context.Resource as ActionContext;
            var httpContext = actionContext.HttpContext;
            var request = httpContext.Request;
            var path = request.Path;
            if (path.Value.StartsWith("/api/"))
            {
                var authToken = httpContext.Items["AuthToken"].ToString();
                if (string.IsNullOrWhiteSpace(authToken))
                {
                    var tokenProvider = httpContext.ApplicationServices.GetService(typeof(ICustomUserTokenProvider)) as CustomUserTokenProvider;
                    var userManager = httpContext.ApplicationServices.GetService(typeof(UserManager<User>)) as UserManager<User>;
                    var retrievedUser = tokenProvider.GetUserFromToken(authToken, userManager);

                }
            }
          
            context.Succeed(requirement);
        }
    }
}
