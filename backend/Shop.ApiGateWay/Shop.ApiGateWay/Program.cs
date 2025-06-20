

using Shop.ApiGateWay.Controllers;
using Shop.ApiGateWay.Interfaces;
using Shop.ApiGateWay.Models;

namespace Shop.ApiGateWay
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Configuration.AddJsonFile("appsettings.Development.json");
            
            builder.Services.AddAPIAutentication(builder.Configuration);
            
            builder.Services.AddHttpClient();
            builder.Services.AddTransient<IForwardingService, ForwardingService>();
            builder.Services.AddTransient<ICartAggregationService, CartAggregationService>();
            builder.Services.AddTransient<IProductRecomendationService, ProductRecomendationsService>();
            
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp", policy =>
                {
                    policy
                        .WithOrigins("http://localhost:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                    .AllowCredentials();
                });
            });
            

            builder.Services.AddControllers();
            var app = builder.Build();

            app.UseCors("AllowAngularApp");   

            app.UseAuthentication();         
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
