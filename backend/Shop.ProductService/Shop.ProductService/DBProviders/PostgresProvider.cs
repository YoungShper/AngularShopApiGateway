using System.Data;
using Npgsql;
using Shop.ProductService.Interfaces;

namespace Shop.ProductService;

public class PostgresProvider : IDBService
{
    private readonly string _connectionString;
    public PostgresProvider(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("PostgresProduct") ?? throw new NullReferenceException("Postgres");
    }
    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}