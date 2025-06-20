using System.Collections.Specialized;
using System.Data;
using Dapper;
using Shop.ArticleService.Interfaces;
using Shop.ArticleService.Models;

namespace Shop.ArticleService.Repository
{
    public class ArticleRepository : IDbRepository<ArticleModel>
    {
        private IDBService _dBService;

        public ArticleRepository(IDBService dbService)
        {
            _dBService = dbService;
        }

          public async Task<List<ArticleModel>> GetAllAsync(NameValueCollection data, CancellationToken cancellationToken)
    {
        var sqlQuery = @"SELECT id AS Id,
                                name AS Name,
                                description AS Description
                         FROM articlestable
                         WHERE delete_state_code = 0";

        using var connection = _dBService.CreateConnection();
        var result = await connection.QueryAsync<ArticleModel>(sqlQuery);
        return result.ToList();
    }

    public async Task<List<ArticleModel>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var sqlQuery = @"SELECT id AS Id,
                                name AS Name,
                                description AS Description
                         FROM articlestable
                         WHERE delete_state_code = 0 AND id = @Id";

        using var connection = _dBService.CreateConnection();
        var result = await connection.QueryAsync<ArticleModel>(sqlQuery, new { Id = id });
        return result.ToList();
    }

    public async Task<bool> CreateAsync(ArticleModel article, CancellationToken cancellationToken)
    {
        var sqlQuery = @"INSERT INTO articlestable 
                         (id, name, description, delete_state_code)
                         VALUES (@Id, @Name, @Description, 0)";

        using var connection = _dBService.CreateConnection();
        var affectedRows = await connection.ExecuteAsync(sqlQuery,
            new
            {
                Id = article.Id,
                Name = article.Name,
                Description = article.Description
            });

        return affectedRows > 0;
    }

    public async Task<bool> UpdateAsync(ArticleModel article, CancellationToken cancellationToken)
    {
        var sqlQuery = @"UPDATE articlestable 
                         SET name = @Name,
                             description = @Description
                         WHERE id = @Id AND delete_state_code = 0";

        using var connection = _dBService.CreateConnection();
        var affectedRows = await connection.ExecuteAsync(sqlQuery,
            new
            {
                Id = article.Id,
                Name = article.Name,
                Description = article.Description
            });

        return affectedRows > 0;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var sqlQuery = @"UPDATE articlestable 
                         SET delete_state_code = 1 
                         WHERE id = @Id";

        using var connection = _dBService.CreateConnection();
        var affectedRows = await connection.ExecuteAsync(sqlQuery, new { Id = id });
        return affectedRows > 0;
    }
    }
}