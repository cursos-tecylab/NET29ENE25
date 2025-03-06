using System.Text;
using System.Text.Json;
using Cursos.Application.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Cursos.Infrastructure.Service;

public class RabbitMQEventBus : IEventBus, IDisposable
{
    private IConnection _connection;

    private readonly IModel _channel;

    private readonly ILogger<RabbitMQEventBus> _logger;

    public RabbitMQEventBus(IConfiguration configuration, ILogger<RabbitMQEventBus> logger)
    {
        _logger = logger;
        var factory = new ConnectionFactory
        {
            Uri = new Uri(configuration["UrlRabbit"]!)
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.ExchangeDeclare("CursosEstudiantesExchange", ExchangeType.Fanout, durable: true);
        _logger.LogInformation("RabbitMQ connection created");
    }

    public void Publish<T>(T @event) where T : class
    {
        var eventName = typeof(T).Name;
        var message = JsonSerializer.Serialize(@event);
        var body = Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish(
            exchange: "CursosEstudiantesExchange",
            routingKey: "",
            basicProperties: CreatePersistentProperties(eventName),
            body: body
        );

        _logger.LogInformation("Published event {EventName}", eventName);
    }

    private IBasicProperties CreatePersistentProperties(string type)
    {
       var properties = _channel.CreateBasicProperties();
       properties.Persistent = true;
       properties.Type = type;
       return properties;
    }

    public void Dispose()
    {
       _channel?.Close();
       _connection?.Close();
    }
}