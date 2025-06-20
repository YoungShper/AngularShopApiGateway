using System.Collections.Specialized;
using Dapper;
using Shop.ProductService.Extensions;
using Shop.ProductService.Interfaces;
using Shop.ProductService.Models;

namespace Shop.ProductService.Repository;

public class ProductRepository : IDbRepository<ProductModel>
{
    private IDBService _dBService;

    public ProductRepository(IDBService dbService)
    {
        _dBService = dbService;
    }
    
    public async Task<List<ProductModel>> GetAllAsync(NameValueCollection queryData, CancellationToken cancellationToken)
    {
        var sqlQuery = $@"SELECT productstable.id AS Id,
                                 productstable.name AS Name,
                                 productstable.description AS Description,
                                 productstable.price AS Price,
                                 productstable.discount_price AS DiscountPrice,
                                 productstable.quantity AS Quantity,
                                 productstable.category_id AS CategoryId,
                                 productstable.protein AS Protein,
                                 productstable.fats AS Fats,
                                 productstable.carbs AS Carbs,
                                 productstable.calories AS Calories
                          FROM productstable
                          WHERE productstable.delete_state_code = 0 {queryData.ToProductFilters()}
                          ORDER BY productstable.id
                          {queryData.GetPaginationQueryString()}";
        
        using var connection = _dBService.CreateConnection();
        var result = await connection.QueryAsync<ProductModel>(sqlQuery);
        return result.ToList();
    }

    public async Task<ProductModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var sqlQuery = $@"SELECT productstable.id AS Id,
                             productstable.name AS Name,
                             productstable.discount_price AS DiscountPrice,
                             productstable.description AS Description,
                             productstable.price AS Price,
                             productstable.quantity AS Quantity,
                             productstable.category_id AS CategoryId,
                             productstable.protein AS Protein,
                             productstable.fats AS Fats,
                             productstable.carbs AS Carbs,
                             productstable.calories AS Calories
                      FROM productstable
                      WHERE productstable.delete_state_code = 0 AND productstable.id = @ID";

        using var connection = _dBService.CreateConnection();
        var result = await connection.QueryAsync<ProductModel>(sqlQuery, new { ID = id });
        return result.First();
    }

    public async Task<bool> CreateAsync(ProductModel product, CancellationToken cancellationToken)
    {
        var sqlQuery = @"INSERT INTO productstable 
                     (id, name, description, price, quantity, discount_price, category_id,
                      protein, fats, carbs, calories, delete_state_code)
                     VALUES (@Id, @Name, @Description, @Price, @Quantity, @DiscountPrice, @CategoryId,
                             @Protein, @Fats, @Carbs, @Calories, 0)";

        using var connection = _dBService.CreateConnection();
        var affectedRows = await connection.ExecuteAsync(sqlQuery,
            new
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Quantity = product.Quantity,
                DiscountPrice = product.DiscountPrice,
                CategoryId = product.CategoryId,
                Protein = product.Protein,
                Fats = product.Fats,
                Carbs = product.Carbs,
                Calories = product.Calories
            });
        return affectedRows > 0;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var sqlQuery = @"UPDATE productstable 
                         SET delete_state_code = 1 
                         WHERE id = @Id";

        using var connection = _dBService.CreateConnection();
        var affectedRows = await connection.ExecuteAsync(sqlQuery, new { Id = id });
        return affectedRows > 0;
    }

    public async Task<bool> UpdateAsync(ProductModel product, CancellationToken cancellationToken)
    {
        var sqlQuery = @"UPDATE productstable 
                         SET name = @Name,
                             description = @Description,
                             price = @Price,
                             quantity = @Quantity,
                             discount_price = @DiscountPrice,
                             category_id = @CategoryId
                         WHERE id = @Id AND delete_state_code = 0";

        using var connection = _dBService.CreateConnection();
        var affectedRows = await connection.ExecuteAsync(sqlQuery, 
            new {Name = product.Name, 
                Description = product.Description, 
                Price = product.Price, 
                Quantity = product.Quantity,
                DiscountPrice = product.DiscountPrice,
                Id = product.Id,
                CategoryId = product.CategoryId});
        return affectedRows > 0;
    }

    public async Task<int> GetTotalElemetnsOfTable(NameValueCollection queryString, CancellationToken ct)
    {
        var sqlQuery = $@"SELECT COUNT(*) 
                          FROM productstable
                          WHERE delete_state_code = 0 {queryString.ToProductFilters()}";
        using var connection = _dBService.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(sqlQuery);
    }
}