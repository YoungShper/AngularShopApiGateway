using System.Data;
using Npgsql;
using Shop.CartService.Interfaces;

namespace Shop.CartService.Providers
{
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
}