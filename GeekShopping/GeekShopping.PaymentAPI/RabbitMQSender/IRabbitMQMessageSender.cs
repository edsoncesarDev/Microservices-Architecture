using GeekShopping.MessageBus.Models;

namespace GeekShopping.PaymentAPI.RabbitMQSender;

public interface IRabbitMQMessageSender
{
    void SendMessage(BaseMessage message, string queueName);
}
