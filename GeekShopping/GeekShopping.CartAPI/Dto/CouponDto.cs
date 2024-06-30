namespace GeekShopping.CartAPI.Dto;

public sealed class CouponDto
{
    public int Id { get; set; }
    public string CouponCode { get; set; } = null!;
    public decimal DiscountAmount { get; set; }
}
