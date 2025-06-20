using Microsoft.AspNetCore.Mvc.ModelBinding;
using RabbitMQ.Client;

namespace Shop.CartService.Interfaces
{
    public interface IRabbitMQProvider
    {
        Task<IConnection> CreateConnectionAsync(CancellationToken cancellationToken); 
    }
}
