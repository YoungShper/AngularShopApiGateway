using Shop.CartService.Models;

namespace Shop.CartService.Interfaces;

public interface ICartDBRepository : IDbRepository<CartModel>
{
    public Task<bool> DeleteUserCartAsync(string userId, CancellationToken cancellationToken);
}