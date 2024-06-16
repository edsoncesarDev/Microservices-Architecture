using GeekShopping.CartAPI.Messages;
using GeekShopping.MessageBus.Models;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace GeekShopping.CartAPI.RabbitMQSender;

public sealed class RabbitMQMessageSender : IRabbitMQMessageSender
{
    private readonly IConfiguration _configuration;
    private IConnection? _connection;
    private readonly string? _hostName;
    private readonly string? _userName;
    private readonly string? _password;

    public RabbitMQMessageSender(IConfiguration configuration)
    {
        _configuration = configuration;
        _hostName = _configuration["RabbitMQ:HostName"]!;
        _userName = _configuration["RabbitMQ:UserName"]!;
        _password = _configuration["RabbitMQ:Password"]!;
    }

    public void SendMessage(BaseMessage message, string queueName)
    {
        var factory = new ConnectionFactory
        {
            HostName = _hostName,
            UserName = _userName,
            Password = _password
        };

        _connection = factory.CreateConnection();

        using var channel = _connection.CreateModel();
        channel.QueueDeclare(
            queue: queueName,               //nome da fila
            durable: false,                 //se igual a true, a fila permanece ativa após o servidor ser reiniciado
            exclusive: false,               //se igual a true, ela só pode ser acessada via conexão atual e são excluídas ao fechar a conexão
            autoDelete: false,              //se igual a true, será deletada automaticamente após os consumidores usar a fila
            arguments: null                 //declara argumentos sobre o tipo da fila
        );

        byte[] bodyMessage = GetMessageAsByteArray(message);

        channel.BasicPublish(
            exchange: "",
            routingKey: queueName,
            basicProperties: null,
            body: bodyMessage
        );
    }

    private byte[] GetMessageAsByteArray(BaseMessage message)
    {
        //var options = new JsonSerializerOptions
        //{
        //    WriteIndented = true
        //};

        //var json = JsonSerializer.Serialize<CheckoutHeader>((CheckoutHeader)message, options);

        var json = JsonSerializer.Serialize((CheckoutHeader)message);

        return Encoding.UTF8.GetBytes(json);
    }
}
