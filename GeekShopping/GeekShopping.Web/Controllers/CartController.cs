﻿using GeekShopping.Web.Filters;
using GeekShopping.Web.Models;
using GeekShopping.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers;

[LoggedUsers]
public class CartController : Controller
{
    private readonly IProductService _productService;
    private readonly ICartService _cartService;
    private readonly ISessionUser _sessionUser;

    public CartController(IProductService productService, ICartService cartService, ISessionUser sessionUser)
    {
        _productService = productService;
        _cartService = cartService;
        _sessionUser = sessionUser;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View(await FindUserCart());
    }

    //[HttpGet("{id:int}")]
    public async Task<IActionResult> RemoveFromCart(int id)
    {
        var response = await _cartService.RemoveFromCart(id);

        if (response)
        {
            return RedirectToAction(nameof(Index));
        }

        return View();
    }

    private async Task<CartModel> FindUserCart()
    {
        var response = await _cartService.FindCartByUserId(_sessionUser.GetUserSession().Id);

        if (response?.CartHeader is not null)
        {
            foreach (var detail in response.CartDetails!)
            {
                response.CartHeader.PurchaseAmount += (detail.Product!.Price * detail.Count);
            }
        }

        return response!;
    }
}
