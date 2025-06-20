using System.Collections.Specialized;
using Dapper;
using Shop.ProductService.Interfaces;
using Shop.ProductService.Models;

namespace Shop.ProductService.Repository;

public class CategoryRepository : IDbRepository<CategoryModel>
{
    private IDBService _dBService;
    
    public CategoryRepository(IDBService dBService)
    {
        _dBService = dBService;
    }
    public async Task<List<CategoryModel>> GetAllAsync(NameValueCollection filters, CancellationToken cancellationToken)
    {
        var sqlQuery = $@"SELECT categoriestable.name AS Name,
                                 categoriestable.id AS Id
                          FROM categoriestable
                          WHERE categoriestable.delete_state_code = 0";
        
        using var connection = _dBService.CreateConnection();
        var result = await connection.QueryAsync<CategoryModel>(sqlQuery);
        return result.ToList();
    }

    public async Task<CategoryModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var sqlQuery = $@"SELECT categoriestable.name AS Name,
                                 categoriestable.id AS Id
                          FROM categoriestable
                          WHERE categoriestable.delete_state_code = 0 AND 
                                categoriestable.id = @ID";
        
        using var connection = _dBService.CreateConnection();
        var result = await connection.QueryAsync<CategoryModel>(sqlQuery, new { ID = id });
        return result.First();
    }

    public async Task<bool> CreateAsync(CategoryModel item, CancellationToken cancellationToken)
    {
        var sqlQuery = @"INSERT INTO categoriestable 
                         (id, name, delete_state_code)
                         VALUES (@Id, @Name, 0)";

        using var connection = _dBService.CreateConnection();
        var affectedRows = await connection.ExecuteAsync(sqlQuery, item);
        return affectedRows > 0;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var sqlQuery = @"UPDATE categoriestable 
                         SET delete_state_code = 1 
                         WHERE id = @Id";

        using var connection = _dBService.CreateConnection();
        var affectedRows = await connection.ExecuteAsync(sqlQuery, new { Id = id });
        return affectedRows > 0;
    }

    public async Task<bool> UpdateAsync(CategoryModel item, CancellationToken cancellationToken)
    {
        var sqlQuery = @"UPDATE categoriestable 
                         SET name = @Name
                         WHERE id = @Id AND delete_state_code = 0";

        using var connection = _dBService.CreateConnection();
        var affectedRows = await connection.ExecuteAsync(sqlQuery, new { Name = item.Name, Id = item.Id });
        return affectedRows > 0;
    }

    public Task<int> GetTotalElemetnsOfTable(NameValueCollection queryString, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}