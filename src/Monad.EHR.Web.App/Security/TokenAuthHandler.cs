using Microsoft.AspNet.Authentication;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.DataProtection;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Authentication;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.OptionsModel;
using Microsoft.Extensions.WebEncoders;
using Monad.EHR.Domain.Entities.Identity;
using Monad.EHR.Services.Business;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Monad.EHR.Web.App.Security
{
    public class TokenAuthOptions : AuthenticationOptions
    {
        public const string Scheme = "TokenAuth";
        public TokenAuthOptions()
        {
            AuthenticationScheme = Scheme;
            AutomaticAuthenticate = true;
        }
    }

    public class TokenAuthHandler : AuthenticationHandler<TokenAuthOptions>
    {
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            string authHeader = Request.Headers["Authorization"];
            authHeader = authHeader ?? "";
            string path = Request.Path.ToString() ?? "";
            bool authenticationRequired = !(SkipRequired(path));

            if (!authenticationRequired)
                return GetSuccessfulResult(authHeader, path);

            var retrievedUser = GetUser(this.Context);
            if (retrievedUser == null)
            {
                this.Context.Response.StatusCode = (int) HttpStatusCode.Forbidden;
                this.Context.Response.Headers.Add("WWW-Authenticate", string.Format("Bearer realm=\"{0}\"", 0)); //request.RequestUri.DnsSafeHost
                return Task.FromResult(AuthenticateResult.Failed("InvalidUser"));
            }
            return GetSuccessfulResult(authHeader, path);
        }

        private async Task<User> GetUser(HttpContext httpContext)
        {
            var authToken = httpContext.Items["AuthToken"].ToString();
            var userManager = httpContext.ApplicationServices.GetService(typeof(UserManager<User>)) as UserManager<User>;
            var tokenProvider = httpContext.ApplicationServices.GetService(typeof(ICustomUserTokenProvider)) as CustomUserTokenProvider;
            return await tokenProvider.GetUserFromToken(authToken, userManager);
        }
        
        private bool SkipRequired(string path)
        {
            var nonAPIPath = !path.StartsWith("/api/");
            return (nonAPIPath || 
                path.Contains("/api/account/Login") || 
                path.Contains("/api/account/Register"));
        }

        private Task<AuthenticateResult> GetSuccessfulResult(string authHeader, string path)
        {

            var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Authentication, authHeader), new Claim(ClaimTypes.Uri, path) },
                                                 Options.AuthenticationScheme);
            var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), new AuthenticationProperties(), Options.AuthenticationScheme);
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }

    public class TokenAuthMiddleware : AuthenticationMiddleware<TokenAuthOptions>
    {
        public TokenAuthMiddleware(
                    RequestDelegate next,
                    IDataProtectionProvider dataProtectionProvider,
                    ILoggerFactory loggerFactory,
                    IUrlEncoder urlEncoder,
                    IOptions<TokenAuthOptions> options,
                    ConfigureOptions<TokenAuthOptions> configureOptions)
              : base(next, options.Value, loggerFactory, urlEncoder)
        {
        }

        protected override AuthenticationHandler<TokenAuthOptions> CreateHandler()
        {
            return new TokenAuthHandler();
        }
    }

    public static class TokenAuthMiddlewareAppBuilderExtensions
    {
        public static IApplicationBuilder UseTokenAuthAuthentication(this IApplicationBuilder app, string optionsName = "")
        {
            return app.UseMiddleware<TokenAuthMiddleware>(
               new ConfigureOptions<TokenAuthOptions>(o => new TokenAuthOptions()) { });
        }
    }


    public class TokenAuthRequirement : AuthorizationHandler<TokenAuthRequirement>, IAuthorizationRequirement
    {
        private bool SkipRequired(string path)
        {
            var nonAPIPath = !path.StartsWith("/api/");
            return (nonAPIPath ||
                path.Contains("/api/account/Login") ||
                path.Contains("/api/account/Register"));
        }

        private async Task<User> GetUser(HttpContext httpContext)
        {
            var authToken = httpContext.Items["AuthToken"].ToString();
            var userManager = httpContext.ApplicationServices.GetService(typeof(UserManager<User>)) as UserManager<User>;
            var tokenProvider = httpContext.ApplicationServices.GetService(typeof(ICustomUserTokenProvider)) as CustomUserTokenProvider;
            return await tokenProvider.GetUserFromToken(authToken, userManager);
        }

        protected override void Handle(AuthorizationContext context, TokenAuthRequirement requirement)
        {
            var actionContext = context.Resource as ActionContext;
            var httpContext = actionContext.HttpContext;
            var request = httpContext.Request;
            var path = request.Path;
          
            bool authorizationRequired = !(SkipRequired(path));
            if(!authorizationRequired)
            {
                context.Succeed(requirement);
                return;
            }

            var retrievedUser = GetUser(httpContext);

            // grab the identity for the TokenAuth authentication
            var TokenAuthIdentities = context.User.Identities.Where(x => x.AuthenticationType == TokenAuthOptions.Scheme).FirstOrDefault();
            if (TokenAuthIdentities == null || 
                (string.Compare(retrievedUser.Result.NormalizedUserName, context.User.Identity.Name.ToUpper()) > 0))
            {
                context.Fail();
                return;
            }

            // grab the authentication header and uri types for our identity
            var authHeaderClaim = TokenAuthIdentities.Claims.Where(x => x.Type == ClaimTypes.Authentication).FirstOrDefault();
            var uriClaim = context.User.Claims.Where(x => x.Type == ClaimTypes.Uri).FirstOrDefault();
            if (uriClaim == null || authHeaderClaim == null)
            {
                context.Fail();
                return;
            }

            var claimComponents = uriClaim.Value.Split('/');
            var controllerName = claimComponents[2];
            var actionName = claimComponents[3];
            var tobeMathedClaim = controllerName + "." + actionName;

            var permission =  context.User.Claims.Where(x => string.Equals(x.Type, tobeMathedClaim, System.StringComparison.InvariantCultureIgnoreCase))
                .Select(y => y.Value).SingleOrDefault();

            if (permission.ToUpper() == "ALLOWED")
                context.Succeed(requirement);
            else
                context.Fail();

        }
    }
}
