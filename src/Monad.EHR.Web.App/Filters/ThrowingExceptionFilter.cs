using System;
using Microsoft.AspNet.Mvc.Filters;

namespace Monad.EHR.Web.App.Filters
{
    public class ThrowingExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            throw new InvalidProgramException("ThrowingExceptionFilter");
        }
    }
}
