using System.Collections.Specialized;
using Dapper;
using Shop.CartService.Interfaces;
using Shop.CartService.Models;

namespace Shop.CartService.Repository;

public class StatusRepository : IDbRepository<StatusModel>
{
    private readonly IDBService _dBService;

    public StatusRepository(IDBService dBService)
    {
        _dBService = dBService;
    }

    public async Task<List<StatusModel>> GetAllAsync(NameValueCollection data, CancellationToken cancellationToken)
    {
        var sqlQuery = @"SELECT id AS Id,
                                name AS Name
                         FROM statustable";

        using var connection = _dBService.CreateConnection();
        var result = await connection.QueryAsync<StatusModel>(sqlQuery);
        return result.ToList();
    }

    public async Task<List<StatusModel>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var sqlQuery = @"SELECT id AS Id,
                                name AS Name
                         FROM statustable
                         WHERE id = @Id";

        using var connection = _dBService.CreateConnection();
        var result = await connection.QueryAsync<StatusModel>(sqlQuery, new { Id = id });
        return result.ToList();
    }

    public async Task<bool> CreateAsync(StatusModel status, CancellationToken cancellationToken)
    {
        var sqlQuery = @"INSERT INTO statustable (id, name)
                         VALUES (@Id, @Name)";

        using var connection = _dBService.CreateConnection();
        var affectedRows = await connection.ExecuteAsync(sqlQuery,
            new
            {
                Id = status.Id,
                Name = status.Name
            });

        return affectedRows > 0;
    }

    public async Task<bool> UpdateAsync(StatusModel status, CancellationToken cancellationToken)
    {
        var sqlQuery = @"UPDATE statustable 
                         SET name = @Name
                         WHERE id = @Id";

        using var connection = _dBService.CreateConnection();
        var affectedRows = await connection.ExecuteAsync(sqlQuery,
            new
            {
                Id = status.Id,
                Name = status.Name
            });

        return affectedRows > 0;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var sqlQuery = @"UPDATE statustable
                         SET delete_state_code = 1  
                         WHERE id = @Id";

        using var connection = _dBService.CreateConnection();
        var affectedRows = await connection.ExecuteAsync(sqlQuery, new { Id = id });
        return affectedRows > 0;
    }
}