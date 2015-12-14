using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

namespace Monad.EHR.Web.App.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
