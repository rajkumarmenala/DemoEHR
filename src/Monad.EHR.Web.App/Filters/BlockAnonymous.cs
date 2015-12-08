using Microsoft.AspNet.Mvc.Filters;

namespace Monad.EHR.Web.App.Filters
{
    public class BlockAnonymous : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(AuthorizationContext context)
        {
            if (!HasAllowAnonymous(context))
            {
                var user = context.HttpContext.User;
                var userIsAnonymous =
                    user == null ||
                    user.Identity == null ||
                    !user.Identity.IsAuthenticated;

                if (userIsAnonymous)
                {
                    base.Fail(context);
                }
            }
        }
    }
}
