
using GeekShopping.OrderAPI.Domain.Entities;
using GeekShopping.OrderAPI.Messages;
using GeekShopping.OrderAPI.Repository;
using GeekShopping.OrderAPI.Repository.Interface;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace GeekShopping.OrderAPI.MessagesConsumer;

public sealed class RabbitMQCheckoutConsumer : BackgroundService
{
    private readonly OrderRepository _repository;
    private readonly IConfiguration _configuration;
    private IConnection _connection;
    private IModel _channel;
    private readonly ConnectionFactory _factory;

    public RabbitMQCheckoutConsumer(OrderRepository repository, IConfiguration configuration)
    {
        _repository = repository;
        _configuration = configuration;
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
           queue: _configuration["RabbitMQ:CheckoutQueue"]!,               //nome da fila
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
            CheckoutHeader header = JsonSerializer.Deserialize<CheckoutHeader>(content)!;
            ProcessOrder(header).GetAwaiter().GetResult();
            _channel.BasicAck(evt.DeliveryTag, false);
        };

        _channel.BasicConsume(_configuration["RabbitMQ:CheckoutQueue"]!, false, consumer);
        return Task.CompletedTask;
    }

    private async Task ProcessOrder(CheckoutHeader header)
    {
        OrderHeader order = new() 
        {
            UserId = header.UserId,
            FirstName = header.FirstName,
            LastName = header.LastName,
            OrderDetails = new List<OrderDetail>(),
            CardNumber = header.CardNumber,
            CouponCode = header.CouponCode,
            CVV = header.CVV,
            DiscountAmount = header.DiscountAmount,
            Email = header.Email,
            ExpiryMonthYear = header.ExpiryMonthYear,
            OrderTime = DateTime.Now,
            PurchaseAmount = header.PurchaseAmount,
            PaymentStatus = false,
            Phone = header.Phone,
            CreationDate = DateTime.Now,
        };

        foreach (var detail in header.CartDetails)
        {
            OrderDetail orderDetail = new()
            {
                ProductId = detail.ProductId,
                ProductName = detail.Product!.Name,
                Price = detail.Product.Price,
                Count = detail.Count
            };

            order.CartTotalItens += detail.Count;
            order.OrderDetails.Add(orderDetail);
        }

        await _repository.AddOrder(order);
    }
}
