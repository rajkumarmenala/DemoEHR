using System;
using Microsoft.AspNet.Mvc.Filters;

namespace Monad.EHR.Web.App.Filters
{
    public class ThrowingAuthorizationFilter : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(AuthorizationContext context)
        {
            throw new InvalidProgramException("ThrowingAuthorizationFilter");
        }
    }
}
