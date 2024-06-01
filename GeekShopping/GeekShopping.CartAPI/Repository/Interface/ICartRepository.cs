using GeekShopping.CartAPI.Dto;

namespace GeekShopping.CartAPI.Repository.Interface;

public interface ICartRepository
{
    Task<CartDto> FindCartByUserId(int userId);
    Task<CartDto> SaveOrUpdateCart(CartDto model);
    Task<bool> RemoveFromCart(int cartDetailId);
    Task<bool> ApplyCoupon(int userId, string couponCode);
    Task<bool> RemoveCoupon(int userId);
    Task<bool> ClearCart(int userId);
}
