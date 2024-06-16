
namespace GeekShopping.MessageBus.Models;

public class BaseMessage
{
    public int Id { get; set; }
    public DateTime MessageCreated { get; set; }
}
