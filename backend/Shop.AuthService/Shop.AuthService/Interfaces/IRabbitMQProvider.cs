using RabbitMQ.Client;

namespace Shop.AuthService.Interfaces
{
    public interface IRabbitMQProvider
    {
        Task<IConnection> CreateConnectionAsync(CancellationToken ct);
    }
}
