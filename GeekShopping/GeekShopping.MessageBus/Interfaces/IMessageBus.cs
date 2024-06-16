using GeekShopping.MessageBus.Models;

namespace GeekShopping.MessageBus.Interfaces;

public interface IMessageBus
{
    Task PublicMessage(BaseMessage message, string queueName);
}
