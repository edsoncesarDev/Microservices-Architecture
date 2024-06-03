
namespace GeekShopping.Web.Models;

public sealed class CartHeaderModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? CouponCode { get; set; }
    //public double PurchaseAmount { get; set; }

    public CartHeaderModel() { }
    
    public CartHeaderModel(int userId, string? couponCode)
    {
        UserId = userId;
        CouponCode = couponCode;
    }
}
