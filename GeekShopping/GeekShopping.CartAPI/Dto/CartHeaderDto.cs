namespace GeekShopping.CartAPI.Dto;

public sealed record CartHeaderDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? CouponCode { get; set; }
}
