using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Monad.EHR.Common.StateManagement;
using Monad.EHR.Domain.Entities.Identity;
using Monad.EHR.Services.Business;
using System.Linq;

namespace Monad.EHR.Web.App.Security
{
    internal static class SecurityHelper
    {
        internal static bool SkipRequired(string path)
        {
            var nonAPIPath = !path.StartsWith("/api/");
            // add more path as per requirement in the project
            return (nonAPIPath ||
                path.Contains("/api/account/Login") ||
                path.Contains("/api/account/Register"));
        }

        internal static bool HasAllowAnonymous(this ActionContext actionContext)
        {
            return actionContext.ActionDescriptor.FilterDescriptors.Any(item => item.Filter is IAllowAnonymous);
        }
    }
}
