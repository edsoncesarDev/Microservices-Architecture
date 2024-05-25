using GeekShopping.Web.Filters;
using GeekShopping.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers;

//[LoggedUsers]
public class HomeController : Controller
{
    private readonly ISessionUser _sessionUser;
    private readonly IProductService _productService;

    public HomeController(ISessionUser sessionUser, IProductService productService)
    {
        _sessionUser = sessionUser;
        _productService = productService;
    }

    public async Task<IActionResult> Index()
    {
        return View( await _productService.GetAllProducts());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Details(int id)
    {
        if (_sessionUser.GetUserSession() is null)
        {
            return RedirectToAction("Index", "Login");
        }

        return View(await _productService.GetProductById(id));
    }
}
