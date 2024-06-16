using GeekShopping.MessageBus.Models;

namespace GeekShopping.CartAPI.RabbitMQSender;

public interface IRabbitMQMessageSender
{
    void SendMessage(BaseMessage message, string queueName);
}
