using System;
using Microsoft.AspNet.Mvc.Filters;
using Monad.EHR.Web.App.Helper;

namespace Monad.EHR.Web.App.Filters
{
    public class ControllerExceptionFilter:ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception.GetType() == typeof(InvalidOperationException))
            {
                context.Result = Helpers.GetContentResult(context.Result, "ControllerExceptionFilter.OnException");
            }
        }
    }
}
