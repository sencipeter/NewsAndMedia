using NewsAndMedia.Core.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Channels;

namespace NewsAndMedia.Infrastructure.Services
{
    public class MessageClient : IMessageClient
    {
        IModel? _channel;

        public void InitClient()
        {
            var factory = new ConnectionFactory
            {
                HostName = "host.docker.internal",
                Port = 5672,
                UserName = "user",
                Password = "Heslo1234"

            };
            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();

            _channel.QueueDeclare(queue: "NewsAndMedia",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
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
