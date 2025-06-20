using System.Collections.Specialized;
using Dapper;
using Shop.CartService.Extensions;
using Shop.CartService.Interfaces;
using Shop.CartService.Models;

namespace Shop.CartService.Repository
{
    public class CartRepository : ICartDBRepository
    {
        private IDBService _dBService;

        public CartRepository(IDBService dbService)
        {
            _dBService = dbService;
        }

        public async Task<List<CartModel>> GetAllAsync(NameValueCollection data, CancellationToken cancellationToken)
        {
            var sqlQuery = $@"SELECT id AS Id,
                                    cart_id AS CartId,
                                    user_id AS UserId,
                                    product_id AS ProductId,
                                    quantity AS Quantity,
                                    status_id AS StatusId,
                                    created_on AS CreatedOn,
                                    is_active AS IsActual
                             FROM carttable 
                             WHERE delete_state_code = 0 {data.ToCartFilters()}";

            using var connection = _dBService.CreateConnection();
            var result = await connection.QueryAsync<CartModel>(sqlQuery);
            return result.ToList();
        }

        public async Task<List<CartModel>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var sqlQuery = @"SELECT id AS Id,
                                    user_id AS UserId,
                                    cart_id AS CartId,
                                    product_id AS ProductId,
                                    quantity AS Quantity,
                                    status_id AS StatusId,
                                    created_on AS CreatedOn,
                                    is_active AS IsActual
                             FROM carttable
                             WHERE id = @Id AND delete_state_code = 0";

            using var connection = _dBService.CreateConnection();
            var result = await connection.QueryAsync<CartModel>(sqlQuery, new { Id = id });
            return result.ToList();
        }

        public async Task<bool> CreateAsync(CartModel cartItem, CancellationToken cancellationToken)
        {
            var sqlQuery = @"INSERT INTO carttable 
                             (id, user_id, product_id, quantity, status_id, is_active, cart_id, delete_state_code)
                             VALUES (@Id, @UserId, @ProductId, @Quantity, @StatusId, @IsActual, @CartId, 0)";

            using var connection = _dBService.CreateConnection();
            var affectedRows = await connection.ExecuteAsync(sqlQuery,
                new
                {
                    Id = Guid.NewGuid(),
                    UserId = cartItem.UserId,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    StatusId = cartItem.StatusId,
                    IsActual = cartItem.IsActual,
                    CartId = cartItem.CartId ??Guid.NewGuid(),
                });

            return affectedRows > 0;
        }

        public async Task<bool> UpdateAsync(CartModel cartItem, CancellationToken cancellationToken)
        {
            var sqlQuery = @"UPDATE carttable 
                             SET user_id = @UserId,
                                 product_id = @ProductId,
                                 quantity = @Quantity,
                                 status_id = @StatusId,
                                 is_active = @IsActual,
                                 created_on = @CreatedOn,
                                 cart_id = @CartId
                             WHERE id = @Id";

            using var connection = _dBService.CreateConnection();
            var affectedRows = await connection.ExecuteAsync(sqlQuery,
                new
                {
                    Id = cartItem.Id,
                    UserId = cartItem.UserId,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    StatusId = cartItem.StatusId,
                    CreatedOn = DateTime.UtcNow,
                    CartId = cartItem.CartId,
                    cartItem.IsActual
                });

            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var sqlQuery = @"UPDATE carttable 
                             SET delete_state_code = 1 
                             WHERE id = @Id";

            using var connection = _dBService.CreateConnection();
            var affectedRows = await connection.ExecuteAsync(sqlQuery, new { Id = id });
            return affectedRows > 0;
        }
        public async Task<bool> DeleteUserCartAsync(string userId, CancellationToken ct)
        {
            var sqlQuery = @"UPDATE carttable 
                             SET delete_state_code = 1 
                             WHERE id = @Id";

            using var connection = _dBService.CreateConnection();
            var affectedRows = await connection.ExecuteAsync(sqlQuery, new { Id = userId });
            return affectedRows > 0;
        }
    }
}