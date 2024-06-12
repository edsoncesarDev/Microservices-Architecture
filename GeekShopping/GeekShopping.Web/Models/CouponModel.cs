namespace GeekShopping.Web.Models;

public sealed class CouponModel
{
    public int Id { get; set; }
    public string CouponCode { get; set; } = null!;
    public decimal DiscountAmount { get; set; }

    public CouponModel() { }
    
}
