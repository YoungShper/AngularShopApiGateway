using Shop.ProductService.Interfaces;
using Shop.ProductService.Models;
using Shop.ProductService.Repository;
using Shop.ProductService.Services;

namespace Shop.ProductService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddJsonFile("appsettings.Development.json");
            builder.Services.AddTransient<IDBService, PostgresProvider>();
            builder.Services.AddTransient<IDbRepository<ProductModel>, ProductRepository>();
            builder.Services.AddTransient<IDbRepository<CategoryModel>, CategoryRepository>();
            builder.Services.AddTransient<IProductManagmentService, Services.ProductManagmentService>();
            builder.Services.AddTransient<IRecomendationEngine, RecomendationsEngine>();

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
