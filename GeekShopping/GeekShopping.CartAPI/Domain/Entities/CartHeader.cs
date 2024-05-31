using GeekShopping.CartAPI.Domain.EntityBase;

namespace GeekShopping.CartAPI.Domain.Entities;

public sealed class CartHeader : Base
{
    public int UserId { get; set; }
    public string? CouponCode { get; set; }
}
