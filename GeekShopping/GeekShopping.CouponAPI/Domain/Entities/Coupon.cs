using GeekShopping.CouponAPI.Domain.EntityBase;

namespace GeekShopping.CouponAPI.Domain.Entities;

public sealed class Coupon : Base
{
    public string CouponCode { get; set; } = null!;
    public decimal DiscountAmount { get; set; }

    public Coupon() { }
    
}
