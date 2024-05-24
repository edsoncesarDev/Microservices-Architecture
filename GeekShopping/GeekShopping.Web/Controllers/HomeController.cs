using GeekShopping.Web.Filters;
using GeekShopping.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers;

[LoggedUsers]
public class HomeController : Controller
{
    private readonly ISessionUser _sessionUser;

    public HomeController(ISessionUser sessionUser)
    {
        _sessionUser = sessionUser;
    }

    public IActionResult Index()
    {
        if(_sessionUser.GetUserSession() is null)
        {
           return RedirectToAction("Index", "Login");
        }

        return View();
    }
}
