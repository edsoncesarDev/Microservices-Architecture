using GeekShopping.Web.Models;
using GeekShopping.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IIdentityUser _identityUser;

        public LoginController(IIdentityUser identityUser)
        {
            _identityUser = identityUser;
        }

        [HttpGet]
        public IActionResult Index()
        {
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
                    return RedirectToAction("Index","Products");
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
