using Microsoft.AspNet.Mvc;

namespace Monad.EHR.Web.App.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
