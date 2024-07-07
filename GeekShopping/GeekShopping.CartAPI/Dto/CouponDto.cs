namespace GeekShopping.CartAPI.Dto;

public sealed class CouponDto
{
    public int id { get; set; }
    public string couponCode { get; set; } = null!;
    public decimal discountAmount { get; set; }
}
