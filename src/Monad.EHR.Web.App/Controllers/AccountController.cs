using Monad.EHR.Services.Interface;
using Monad.EHR.Web.App.Models;
using Microsoft.AspNet.Mvc;
using System.Threading.Tasks;

namespace Monad.EHR.Web.App.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            return null;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody]LoginViewModel model, string returnUrl = null)
        {
            AccountsApiModel accountsWebApiModel = new AccountsApiModel();

            if (ModelState.IsValid)
            {

                var result = await _accountService.Login(model.UserName, model.Password, model.RememberMe);
                if (result.Succeeded)
                {
                    accountsWebApiModel.User.UserName = model.UserName;
                    //  FormsAuthentication.SetAuthCookie(user.UserId.ToString(), createPersistentCookie: false);
                     return new ObjectResult(accountsWebApiModel);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    return View("Lockout");
                }
                else
                {
                    return new ObjectResult("Invalid username or password.");
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("LogOff")]
        public IActionResult LogOff()
        {
             _accountService.LogOff();
             return new HttpStatusCodeResult(200);
        }

        //ForgotPassword
        //ResetPassword
        //SendCode
        //VerifyCode

        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.Register(model.Email, model.Password);
                if (result.Succeeded)
                {
                    //TO DO 
                }
                return new HttpStatusCodeResult(200);
            }
            return new HttpStatusCodeResult(204);
        }
    }
}
