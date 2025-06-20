using RabbitMQ.Client;
using Shop.AuthService.Interfaces;
using System.Text;
using System.Threading.Tasks;

namespace Shop.AuthService
{
    public class RabbitMQPublisher : IMessagePublisher
    {
        IRabbitMQProvider _connectionProvider;

        public RabbitMQPublisher(IRabbitMQProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public async void Publish(string queueName, string message, CancellationToken ct)
        {
            var connection = await _connectionProvider.CreateConnectionAsync(ct);
            await using var channel = await connection.CreateChannelAsync(cancellationToken: ct);
            await channel.QueueDeclareAsync(queue: queueName, durable: false, exclusive: false, autoDelete: false, cancellationToken: ct);

            var body = Encoding.UTF8.GetBytes(message);
            await channel.BasicPublishAsync(exchange: "", routingKey: queueName, body: body, ct);
        }
    }
}
