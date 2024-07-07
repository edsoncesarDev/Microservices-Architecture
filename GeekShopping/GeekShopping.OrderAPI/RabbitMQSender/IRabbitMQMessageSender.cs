using GeekShopping.MessageBus.Models;

namespace GeekShopping.OrderAPI.RabbitMQSender;

public interface IRabbitMQMessageSender
{
    void SendMessage(BaseMessage message, string queueName);
}
