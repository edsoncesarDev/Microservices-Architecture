using GeekShopping.PaymentAPI.Messages;
using GeekShopping.PaymentAPI.RabbitMQSender;
using GeekShopping.PaymentProcessor;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace GeekShopping.PaymentAPI.MessagesConsumer;

public class RabbitMQPaymentConsumer : BackgroundService
{
    private readonly IConfiguration _configuration;
    private readonly IProcessPayment _processPayment;
    private readonly IRabbitMQMessageSender _rabbitMQSender;
    private IConnection _connection;
    private IModel _channel;
    private readonly ConnectionFactory _factory;

    public RabbitMQPaymentConsumer(IConfiguration configuration, IProcessPayment processPayment, IRabbitMQMessageSender rabbitMQSender)
    {
        _configuration = configuration;
        _processPayment = processPayment;
        _rabbitMQSender = rabbitMQSender;

        _factory = new ConnectionFactory()
        {
            HostName = _configuration["RabbitMQ:HostName"]!,
            UserName = _configuration["RabbitMQ:UserName"]!,
            Password = _configuration["RabbitMQ:Password"]!
        };

        _connection = _factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare();

        _channel.QueueDeclare(
           queue: _configuration["RabbitMQ:PaymentQueue"]!,               //nome da fila
           durable: false,                                                 //se igual a true, a fila permanece ativa após o servidor ser reiniciado
           exclusive: false,                                               //se igual a true, ela só pode ser acessada via conexão atual e são excluídas ao fechar a conexão
           autoDelete: false,                                              //se igual a true, será deletada automaticamente após os consumidores usar a fila
           arguments: null                                                 //declara argumentos sobre o tipo da fila
       );
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (cnl, evt) =>
        {
            var content = Encoding.UTF8.GetString(evt.Body.ToArray());
            PaymentMessage payment = JsonSerializer.Deserialize<PaymentMessage>(content)!;
            ProcessPayment(payment).GetAwaiter().GetResult();
            _channel.BasicAck(evt.DeliveryTag, false);
        };

        _channel.BasicConsume(_configuration["RabbitMQ:PaymentQueue"]!, false, consumer);
        return Task.CompletedTask;
    }

    private async Task ProcessPayment(PaymentMessage payment)
    {
        var result = _processPayment.PaymentProcessor();

        var paymentResult = new UpdatePaymentResultMessage()
        {
            Status = result,
            OrderId = payment.OrderId,
            Email = payment.Email,
        };

        _rabbitMQSender.SendMessage(paymentResult, _configuration["RabbitMQ:PaymentResultQueue"]!);
    }
}
