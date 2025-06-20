using Shop.CartService.Interfaces;
using Shop.CartService.Models;
using Shop.CartService.Repository;
using RabbitMQ.Client;
using Shop.CartService.BackgroundServices;
using Shop.CartService.Providers;

namespace Shop.CartService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddJsonFile("appsettings.Development.json");

            builder.Services.AddTransient<IDBService, PostgresProvider>();
            builder.Services.AddTransient<ICartDBRepository, CartRepository>();
            builder.Services.AddTransient<IDbRepository<StatusModel>, StatusRepository>();
            builder.Services.AddTransient<ICartManagerService, CartManagerService>();
            builder.Services.AddSingleton<IRabbitMQProvider, RabbitMQProvider>();
            builder.Services.AddHostedService<RabbitMQConsumer>();
            

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "My API",
                    Version = "v1"
                });
            });

            var app = builder.Build();

#if DEBUG
            app.UseSwagger();
            app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });

#endif

            // app.UseMiddleware<ExceptionMiddleware>();


            app.MapControllers();

            app.Run();
        }
    }
}
