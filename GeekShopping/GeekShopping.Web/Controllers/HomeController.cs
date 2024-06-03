using GeekShopping.Web.Filters;
using GeekShopping.Web.Models;
using GeekShopping.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers;

//[LoggedUsers]
public class HomeController : Controller
{
    private readonly ISessionUser _sessionUser;
    private readonly IProductService _productService;
    private readonly ICartService _cartService;

    public HomeController(ISessionUser sessionUser, IProductService productService, ICartService cartService)
    {
        _sessionUser = sessionUser;
        _productService = productService;
        _cartService = cartService;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _productService.GetAllProducts());
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

    
    [HttpPost]
    public async Task<IActionResult> DetailsPost(ProductModel productModel)
    {
        if (_sessionUser.GetUserSession() is null)
        {
            return RedirectToAction("Index", "Login");
        }

        var product = await _productService.GetProductById(productModel.Id);

        CartModel cart = new(
            new CartHeaderModel(_sessionUser.GetUserSession().Id, null),
            new CartDetailModel(productModel.Id, product, productModel.Count)
        );

        var response = await _cartService.AddItemToCart(cart);

        if(response is not null)
        {
            return RedirectToAction(nameof(Index));
        }

        return View(productModel);
    }
}
