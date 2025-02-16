using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace LessonService.Application.Services.MessageProducer;

public class BrokerConnection
{
    public required string HostName { get; set; }
    public int Port { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public required  string Queue { get; set; }
}
public class RabbitMessageProducer : IMessageProducer
{
    private readonly ILogger<RabbitMessageProducer> _logger;
    private readonly IConnection _connection;
    private readonly string _queueName;

    public RabbitMessageProducer(ILogger<RabbitMessageProducer> logger, IConfiguration configuration)
    {
        _logger = logger;
        var brokerConnection = configuration.GetSection("BrokerConnection").Get<BrokerConnection>();
        if (brokerConnection == null)
            throw new ArgumentException("Broker connection string is missing");
        var factory = new ConnectionFactory
        {
            HostName = brokerConnection.HostName,
            Port = brokerConnection.Port,
            UserName = brokerConnection.UserName,
            Password = brokerConnection.Password
        };

        _queueName = brokerConnection.Queue;
        _connection = factory.CreateConnection();
    }
    
    public void Send<T>(T message)
    {
        _logger.LogInformation("Getting {0} for sending to message broker", typeof(T).Name);

        using var channel = _connection.CreateModel();

        channel.QueueDeclare(queue: _queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
        var properties = channel.CreateBasicProperties();
        properties.Persistent = true;

        channel.BasicPublish(exchange: "",
            routingKey: _queueName,
            basicProperties: properties,
            body: body);

        _logger.LogInformation("{0} sent to queue '{1}'", typeof(T).Name, _queueName);
    }

    public void Dispose()
    {
        _connection.Close();
    }
}
