using GeekShopping.MessageBus.Models;

namespace GeekShopping.OrderAPI.Dto;

public class PaymentDto : BaseMessage
{
    public int OrderId { get; set; }
    public string Name { get; set; } = null!;
    public string CardNumber { get; set; } = null!;
    public string CVV { get; set; } = null!;
    public string ExpiryMonthYear { get; set; } = null!;
    public decimal PurchaseAmount { get; set; }
    public string Email { get; set; } = null!;

    public PaymentDto(int orderId, string name, string cardNumber, string cvv, string expiryMonthYear, decimal purchaseAmount, string email)
    {
        OrderId = orderId;
        Name = name;
        CardNumber = cardNumber;
        CVV = cvv;
        ExpiryMonthYear = expiryMonthYear;
        PurchaseAmount = purchaseAmount;
        Email = email;
    }
}
