using Microsoft.AspNet.Authentication;
using Microsoft.AspNet.Http.Authentication;
using Monad.EHR.Web.App.Security;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Monad.EHR.Web.App.Policies
{
    public class TokenAuthenticationHandler : AuthenticationHandler<TokenAuthOptions>
    {
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            string authHeader = Request.Headers["Authorization"];
            authHeader = authHeader ?? "";
            string path = Request.Path.ToString() ?? "";

            if (SecurityHelper.SkipRequired(path))
                return GetSuccessfulResult(authHeader, path);

            if (SecurityHelper.GetUser(this.Context) == null)
            {
                this.Context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                this.Context.Response.Headers.Add("WWW-Authenticate", string.Format("Bearer realm=\"{0}\"", 0)); //request.RequestUri.DnsSafeHost
                return Task.FromResult(AuthenticateResult.Failed("InvalidUser"));
            }
            return GetSuccessfulResult(authHeader, path);
        }

        private Task<AuthenticateResult> GetSuccessfulResult(string authHeader, string path)
        {
            var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Authentication, authHeader), new Claim(ClaimTypes.Uri, path) },
                                                 Options.AuthenticationScheme);
            var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), new AuthenticationProperties(), Options.AuthenticationScheme);
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}