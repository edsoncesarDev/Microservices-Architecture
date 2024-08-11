using GeekShopping.Email.Dto;
using GeekShopping.Email.Repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace GeekShopping.Email.MessagesConsumer;

public sealed class RabbitMQEmailConsumer : BackgroundService
{
    private readonly EmailRepository _repository;
    private readonly IConfiguration _configuration;
    private IConnection _connection;
    private IModel _channel;
    private readonly ConnectionFactory _factory;
    private readonly string _queueName;

    public RabbitMQEmailConsumer(EmailRepository repository, IConfiguration configuration)
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
        _queueName = _configuration["RabbitMQ:PaymentEmailUpdateQueue"]!;

        //_queueName = _channel.QueueDeclare().QueueName;                     //obtendo nome da fila dinamicamente
        
        _channel.ExchangeDeclare(
          _configuration["RabbitMQ:ExchangePayment"]!,                        //nome exchange
          ExchangeType.Direct,                                                //tipo exchange
          false,                                                              //se igual a true, a fila permanece ativa após o servidor ser reiniciado
          false,                                                              //se igual a true, será deletada automaticamente após os consumidores usar a fila
          null                                                                //declara argumentos sobre o tipo da fila
        );

        _channel.QueueDeclare(
            queue: _queueName,                                                //nome da fila
            durable: false,                                                   //se igual a true, a fila permanece ativa após o servidor ser reiniciado
            exclusive: false,                                                 //se igual a true, ela só pode ser acessada via conexão atual e são excluídas ao fechar a conexão
            autoDelete: false,                                                //se igual a true, será deletada automaticamente após os consumidores usar a fila
            arguments: null                                                   //declara argumentos sobre o tipo da fila
        );

        _channel.QueueBind(_queueName, _configuration["RabbitMQ:ExchangePayment"]!, "paymentEmail", null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (cnl, evt) =>
        {
            var content = Encoding.UTF8.GetString(evt.Body.ToArray());
            UpdatePaymentResultDto message = JsonSerializer.Deserialize<UpdatePaymentResultDto>(content)!;
            ProcessLogs(message).GetAwaiter().GetResult();
            _channel.BasicAck(evt.DeliveryTag, false);
        };

        _channel.BasicConsume(_queueName, false, consumer);
        return Task.CompletedTask;
    }

    private async Task ProcessLogs(UpdatePaymentResultDto message)
    {
        await _repository.LogEmail(message);
    }
}
