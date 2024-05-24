using GeekShopping.Web.Models;
using GeekShopping.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IIdentityUser _identityUser;
        private readonly ISessionUser _session;

        public LoginController(IIdentityUser identityUser, ISessionUser session)
        {
            _identityUser = identityUser;
            _session = session;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (_session.GetUserSession() is not null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(UserLoginModel userLogin)
        {
            if (ModelState.IsValid)
            {
                var response = await _identityUser.LoginUser(userLogin);

                if (response != null)
                {
                    return RedirectToAction("Index","Home");
                }
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult ViewRegister()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(UserRegisterModel userRegister)
        {
            if (ModelState.IsValid)
            {
                var response = await _identityUser.RegisterUser(userRegister);

                if (response != null)
                {
                    return RedirectToAction("Index", "Login");
                }
            }

            return View(userRegister);
        }

        public IActionResult Logout() 
        {
            _session.RemoveUserSession();

            return RedirectToAction("Index", "Login");
        }
    }
}
