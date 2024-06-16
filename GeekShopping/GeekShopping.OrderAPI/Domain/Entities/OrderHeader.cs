using GeekShopping.OrderAPI.Domain.EntityBase;

namespace GeekShopping.OrderAPI.Domain.Entities;

public sealed class OrderHeader : Base
{
    public int UserId { get; set; }
    public string? CouponCode { get; set; }
    public decimal PurchaseAmount { get; set; }
    public decimal DiscountAmount { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime OrderTime { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? CardNumber { get; set; }
    public string? CVV { get; set; }
    public string? ExpiryMonthYear { get; set; }
    public int OrderTotalItens { get; set; }
    public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    public bool PaymentStatus { get; set; }

    public OrderHeader() { }
    
}
