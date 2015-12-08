using Microsoft.AspNet.Mvc.Filters;
using Monad.EHR.Web.App.Helper;

namespace Monad.EHR.Web.App.Filters
{
    public class HandleExceptionActionFilter:ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                context.Result = Helpers.GetContentResult(null, "HandleExceptionActionFilter.OnActionExecuted");
            }
        }
    }
}
