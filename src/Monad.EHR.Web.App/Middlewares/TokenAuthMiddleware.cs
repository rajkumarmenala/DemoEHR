using Microsoft.AspNet.Authentication;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.DataProtection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.OptionsModel;
using Microsoft.Extensions.WebEncoders;
using Monad.EHR.Web.App.Policies;

namespace Monad.EHR.Web.App.Middlewares
{
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
        { }

        protected override AuthenticationHandler<TokenAuthOptions> CreateHandler()
        {
            return new TokenAuthenticationHandler();
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
}
