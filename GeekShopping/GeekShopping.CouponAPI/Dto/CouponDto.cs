namespace GeekShopping.CouponAPI.Dto;

public sealed record CouponDto
{
    public int Id { get; set; }
    public string CouponCode { get; set; } = null!;
    public decimal DiscountAmount { get; set; }
}
