using Shop.AuthService.DBProviders;
using Shop.AuthService.Interfaces;
using Shop.AuthService.Interfaces.Auth;
using Shop.AuthService.Models;
using Shop.AuthService.Repository;
using Shop.AuthService.Services;

namespace Shop.AuthService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddJsonFile("appsettings.Development.json");
            builder.Services.AddTransient<IJwtProvider, JwtProvider>();
            builder.Services.AddTransient<IDBService, PostgresProvider>();
            builder.Services.AddTransient<IDbRepository<UserModel>, UserRepository>();
            builder.Services.AddTransient<IUserManager, UserManagmentService>();
            builder.Services.AddTransient<IPasswordHasher, PasswordHasher>();
            builder.Services.AddTransient<IAuth, AuthUserService>();
            builder.Services.AddSingleton<IRabbitMQProvider, RabbitMQProvider>();
            builder.Services.AddSingleton<IMessagePublisher, RabbitMQPublisher>();
            
           

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            var app = builder.Build();

#if DEBUG
            app.UseSwagger();
            app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });

#endif

            app.UseMiddleware<ExceptionMiddleware>();

            app.MapControllers();

            app.Run();
            
        }
    }
}
