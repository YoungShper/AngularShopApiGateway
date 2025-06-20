using RabbitMQ.Client;
using Shop.AuthService.Interfaces;

namespace Shop.AuthService.DBProviders
{
    public class RabbitMQProvider : IRabbitMQProvider
    {
        private IConfiguration _configuration;

        public RabbitMQProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IConnection> CreateConnectionAsync(CancellationToken ct)
        {
            var factory = new ConnectionFactory
            {
                HostName = _configuration["RabbitMQServer:Address"] ?? "",
                UserName = _configuration["RabbitMQServer:User"] ?? "",
                Password = _configuration["RabbitMQServer:Password"] ?? ""
            };
            return await factory.CreateConnectionAsync(ct);
        }
    }
}
