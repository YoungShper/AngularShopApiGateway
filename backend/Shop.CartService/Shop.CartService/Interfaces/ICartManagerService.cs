using System.Collections.Specialized;
using Shop.CartService.Models;

namespace Shop.CartService;

public interface ICartManagerService
{
    public Task<List<CartModel>> GetAllAsync(NameValueCollection filters, CancellationToken cancellationToken);
    public Task<List<CartModel>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<bool> CreateAsync(CartModel item, CancellationToken cancellationToken);
    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
    public Task<bool> UpdateAsync(CartModel item, CancellationToken cancellationToken);
    public Task<bool> DeleteUserCartAsync(string userId, CancellationToken cancellationToken);
}