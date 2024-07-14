﻿using GeekShopping.OrderAPI.Dto;
using GeekShopping.OrderAPI.Repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace GeekShopping.OrderAPI.MessagesConsumer;

public sealed class RabbitMQPaymentConsumer : BackgroundService
{
    private readonly OrderRepository _repository;
    private readonly IConfiguration _configuration;
    private IConnection _connection;
    private IModel _channel;
    private readonly ConnectionFactory _factory;

    public RabbitMQPaymentConsumer(OrderRepository repository, IConfiguration configuration)
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
           queue: _configuration["RabbitMQ:PaymentResultQueue"]!,               //nome da fila
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
            UpdatePaymentResultDto model = JsonSerializer.Deserialize<UpdatePaymentResultDto>(content)!;
            UpdatePaymentStatus(model).GetAwaiter().GetResult();
            _channel.BasicAck(evt.DeliveryTag, false);
        };

        _channel.BasicConsume(_configuration["RabbitMQ:PaymentResultQueue"]!, false, consumer);
        return Task.CompletedTask;
    }

    private async Task UpdatePaymentStatus(UpdatePaymentResultDto model)
    {
        await _repository.UpdateOrderPaymentStatus(model.OrderId, model.Status);
    }
}
