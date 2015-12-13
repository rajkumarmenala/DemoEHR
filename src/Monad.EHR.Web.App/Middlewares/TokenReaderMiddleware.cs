using Monad.EHR.Common.Logger;
using Microsoft.AspNet.Builder;
using Microsoft.Extensions.Logging;

namespace Monad.EHR.Web.App.Middlewares
{
    public class TokenReaderMiddleware
    {
        private readonly ILogger _logger = LogHelper.CreateLogger<TokenReaderMiddleware>();

        public RequestDelegate Process(RequestDelegate next)
        {
            return async httpContext =>
            {
                var request = httpContext.Request;
                var path = httpContext.Request.Path;
                if (path.Value.StartsWith("/api/"))
                {
                    var tokenValue = request.Headers["authToken"];
                }
                await next(httpContext);
            };
        }
    }
}
