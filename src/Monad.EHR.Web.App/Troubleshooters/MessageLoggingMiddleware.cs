using Monad.EHR.Common.Logger;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Monad.EHR.Web.App.Troubleshooters
{
    public class MessageLoggingMiddleware
    {
        private readonly ILogger _logger = LogHelper.CreateLogger<MessageLoggingMiddleware>();
        private Stopwatch timer;

        public RequestDelegate Process(RequestDelegate next)
        {
            return async httpContext =>
            {
                var request = httpContext.Request;
                var path = httpContext.Request.Path;
                 var ignnoreExtensions = new string []{".js",".css",".jpg",".gif",".png",".woff",".xml"};
                bool ignoreLogging = ignnoreExtensions.Any(x=> path.Value.EndsWith(x));

                var correlationId = String.Format("{0} {1} ", DateTime.UtcNow.Ticks, Thread.CurrentThread.ManagedThreadId);
                string requestInfo = String.Format("{0} {1} ", request.Method, path);

                timer = new Stopwatch();
                timer.Start();
                //if (!ignoreLogging)
                //{
                //    _logger.LogInformation("{0} - Request: {1}\r\n{2}", correlationId, requestInfo, 2);
                //}
                await next(httpContext);
                timer.Stop();
                var elapsedTimes = timer.ElapsedMilliseconds;
                if (!ignoreLogging)
                {
                    _logger.LogInformation(" Url {0} took {1} Milliseconds \r\n", httpContext.Request.Path, elapsedTimes);
                    //_logger.LogInformation("{0} - Response: {1}\r\n{2}", correlationId, requestInfo, 3);
                }
            };
        }
    }
}
