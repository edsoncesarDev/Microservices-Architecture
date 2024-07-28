using GeekShopping.MessageBus.Models;
using RabbitMQ.Client;
using Shared.Exceptions;
using System.Text.Json;
using System.Text;
using GeekShopping.PaymentAPI.Messages;

namespace GeekShopping.PaymentAPI.RabbitMQSender;

public sealed class RabbitMQMessageSender : IRabbitMQMessageSender
{
    private readonly IConfiguration _configuration;
    private IConnection? _connection;
    private readonly string? _hostName;
    private readonly string? _userName;
    private readonly string? _password;
    private readonly string? _exchangeName;

    public RabbitMQMessageSender(IConfiguration configuration)
    {
        _configuration = configuration;
        _hostName = _configuration["RabbitMQ:HostName"]!;
        _userName = _configuration["RabbitMQ:UserName"]!;
        _password = _configuration["RabbitMQ:Password"]!;
        _exchangeName = _configuration["RabbitMQ:ExchangePayment"]!;

    }

    public void SendMessage(BaseMessage message)
    {
        if (ConnectionExists())
        {
            using var channel = _connection!.CreateModel();

            //channel.QueueDeclare(
            //    queue: queueName,               //nome da fila
            //    durable: false,                 //se igual a true, a fila permanece ativa após o servidor ser reiniciado
            //    exclusive: false,               //se igual a true, ela só pode ser acessada via conexão atual e são excluídas ao fechar a conexão
            //    autoDelete: false,              //se igual a true, será deletada automaticamente após os consumidores usar a fila
            //    arguments: null                 //declara argumentos sobre o tipo da fila
            //);

            channel.ExchangeDeclare(
                _exchangeName,                    //nome exchange
                ExchangeType.Fanout,              //tipo exchange
                false,                            //se igual a true, a fila permanece ativa após o servidor ser reiniciado
                false,                            //se igual a true, será deletada automaticamente após os consumidores usar a fila
                null                              //declara argumentos sobre o tipo da fila
            );                            

            byte[] bodyMessage = GetMessageAsByteArray(message);

            channel.BasicPublish(
                exchange: _exchangeName,
                routingKey: "",
                basicProperties: null,
                body: bodyMessage
            );
        }
    }

    private byte[] GetMessageAsByteArray(BaseMessage message)
    {
        var json = JsonSerializer.Serialize((UpdatePaymentResultMessage)message);

        return Encoding.UTF8.GetBytes(json);
    }

    private bool ConnectionExists()
    {
        if (_connection != null)
        {
            return true;
        }

        return CreateConnection();

    }

    private bool CreateConnection()
    {
        var factory = new ConnectionFactory
        {
            HostName = _hostName,
            UserName = _userName,
            Password = _password
        };

        _connection = factory.CreateConnection();

        if (_connection is null)
        {
            BusinessException.When(true, "Error trying to connect to rabbitmq");
        }

        return _connection != null;
    }
}