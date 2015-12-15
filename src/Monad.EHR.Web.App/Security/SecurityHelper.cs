using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
using Monad.EHR.Domain.Entities.Identity;
using Monad.EHR.Services.Business;
using System.Threading.Tasks;

namespace Monad.EHR.Web.App.Security
{
    internal static class SecurityHelper
    {
        internal static async Task<User> GetUser(HttpContext httpContext)
        {
            var authToken = httpContext.Items["AuthToken"].ToString();
            var userManager = httpContext.ApplicationServices.GetService(typeof(UserManager<User>)) as UserManager<User>;
            var tokenProvider = httpContext.ApplicationServices.GetService(typeof(ICustomUserTokenProvider)) as CustomUserTokenProvider;
            return await tokenProvider.GetUserFromToken(authToken, userManager);
        }

        internal static  bool SkipRequired(string path)
        {
            var nonAPIPath = !path.StartsWith("/api/");
            // add more path as per requirement in the project
            return (nonAPIPath ||
                path.Contains("/api/account/Login") ||
                path.Contains("/api/account/Register"));
        }
    }
}
