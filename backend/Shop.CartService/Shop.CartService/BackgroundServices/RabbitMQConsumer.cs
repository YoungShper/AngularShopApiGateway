using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shop.CartService.Interfaces;
using Shop.CartService.Models;
using Shop.CartService.Repository;

namespace Shop.CartService.BackgroundServices
{
    public class RabbitMQConsumer : BackgroundService
    {
        IRabbitMQProvider _connectionProvider;
        IConfiguration _configuration;
        IServiceProvider _serviceProvider;

        public RabbitMQConsumer(IRabbitMQProvider connectionProvider, IConfiguration configuration, IServiceProvider provider)
        {
            _connectionProvider = connectionProvider;
            _configuration = configuration;
            _serviceProvider = provider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var connection = await _connectionProvider.CreateConnectionAsync(stoppingToken);
            await using var channel = await connection.CreateChannelAsync(null, stoppingToken);
            var cartRepo = _serviceProvider.GetRequiredService<ICartManagerService>();

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = System.Text.Encoding.UTF8.GetString(body);
                await cartRepo.DeleteUserCartAsync(message, stoppingToken);
            };
                
            await channel.QueueDeclareAsync(queue: _configuration["RabbitMQServer:Queues:DeleteProduct"] ?? throw new NullReferenceException(), durable: false, exclusive: false, autoDelete: false,
                arguments: null, cancellationToken:stoppingToken);


            await channel.BasicConsumeAsync(_configuration["RabbitMQServer:Queues:DeleteProduct"] ?? throw new NullReferenceException(), true, consumer, stoppingToken);
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }
}
