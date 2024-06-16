using GeekShopping.Web.Filters;
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
    private readonly ICouponService _couponService;

    public CartController(IProductService productService, ICartService cartService, ISessionUser sessionUser, ICouponService couponService)
    {
        _productService = productService;
        _cartService = cartService;
        _sessionUser = sessionUser;
        _couponService = couponService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View(await FindUserCart());
    }

    [HttpGet]
    public async Task<IActionResult> Checkout()
    {
        return View(await FindUserCart());
    }

    [HttpPost]
    public async Task<IActionResult> Checkout(CartModel model)
    {
        var cart = await _cartService.Checkout(model.CartHeader!);

        if(cart is not null)
        {
            return RedirectToAction(nameof(Confirmation));
        }

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Confirmation()
    {
        return View();
    }

    [HttpPost]
    [ActionName("ApplyCoupon")]
    public async Task<IActionResult> ApplyCoupon(CartModel cart)
    {
        var response = await _cartService.ApplyCoupon(cart);

        if (response)
        {
            return RedirectToAction(nameof(Index));
        }

        return View();
    }

    [HttpPost]
    [ActionName("RemoveCoupon")]
    public async Task<IActionResult> RemoveCoupon()
    {
        var response = await _cartService.RemoveCoupon(_sessionUser.GetUserSession().Id);

        if (response)
        {
            return RedirectToAction(nameof(Index));
        }

        return View();
    }

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
            if (!string.IsNullOrEmpty(response.CartHeader.CouponCode))
            {
                var coupon = await _couponService.GetCouponAsync(response.CartHeader.CouponCode.ToUpper());

                if (!string.IsNullOrEmpty(coupon.CouponCode))
                {
                    response.CartHeader.DiscountAmount = coupon.DiscountAmount;
                }
            }

            foreach (var detail in response.CartDetails!)
            {
                response.CartHeader.PurchaseAmount += (detail.Product!.Price * detail.Count);
            }

            response.CartHeader.PurchaseAmount -= response.CartHeader.DiscountAmount;
        }

        return response!;
    }
}
