using Microsoft.Extensions.Logging;
using NewsAndMedia.Core.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace NewsAndMedia.Infrastructure.Services
{
    public class MessageClient : IMessageClient, IDisposable
    {
        IModel? _channel;
        IConnection? _connection;
        ILogger<MessageClient> _logger;

        public MessageClient(ILogger<MessageClient> logger)
        {
            _logger = logger;
        }

        public void Dispose()
        {
            try
            {
                _channel?.Close();
                _channel?.Dispose();
                _channel = null;

                _connection?.Close();
                _connection?.Dispose();
                _connection = null;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);
            }
        }

        public void InitClient()
        {
            var factory = new ConnectionFactory
            {
                HostName = "host.docker.internal",
                Port = 5672,
                UserName = "user",
                Password = "Heslo1234"

            };
            if (_connection is null || 
                !_connection.IsOpen)
            {
                _connection = factory.CreateConnection();
            }

            if (_channel is null || 
                !_channel.IsOpen)
            {
                _channel = _connection.CreateModel();
                _channel.QueueDeclare(queue: "NewsAndMedia",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
            }
        }
        public void ReceiveMessage()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Received: {message}");
            };
            _channel.BasicConsume(queue: "NewsAndMedia",
                                 autoAck: false,
                                 consumer: consumer);
        }

        public bool SendMessage(decimal result)
        {
            var body = Encoding.UTF8.GetBytes(result.ToString());

            _channel.BasicPublish(exchange: string.Empty,
                                 routingKey: "NewsAndMedia",
                                 basicProperties: null,
                                 body: body);
            return true;
        }
    }
}
