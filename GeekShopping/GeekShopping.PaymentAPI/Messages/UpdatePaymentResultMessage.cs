using GeekShopping.MessageBus.Models;

namespace GeekShopping.PaymentAPI.Messages;

public sealed class UpdatePaymentResultMessage : BaseMessage
{
    public int OrderId { get; set; }
    public bool Status { get; set; }
    public string Email { get; set; } = null!;

}
