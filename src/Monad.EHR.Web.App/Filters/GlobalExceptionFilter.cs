using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNet.Mvc.Filters;
using Monad.EHR.Common.Logger;

namespace Monad.EHR.Web.App.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger _logger = LogHelper.CreateLogger<GlobalExceptionFilter>();
        public void OnException(ExceptionContext context)
        {
            if (context == null || context.Exception == null)
                return;

            // only for logging purpose
            var exceptionContentForLog = context.Exception.Message;
            _logger.LogError(exceptionContentForLog);
			_logger.LogError(context.Exception.StackTrace);
            context.Result = new HttpStatusCodeResult(500);
        }
    }
}
