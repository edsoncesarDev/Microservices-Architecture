using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.Interfaces;

public interface ICartService
{
    Task<CartModel> FindCartByUserId(int userId);
    Task<CartModel> AddItemToCart(CartModel cart);
    Task<CartModel> UpdateCart(CartModel cart);
    Task<bool> RemoveFromCart(int cartId);

    Task<bool> ApplyCoupon(CartModel cart);
    Task<bool> RemoveCoupon(int userId);
    Task<bool> ClearCart(int userId);

    Task<CartHeaderModel> Checkout(CartHeaderModel cartHeader);
}
