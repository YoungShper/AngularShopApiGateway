using System.Collections.Specialized;
using Shop.CartService.Interfaces;
using Shop.CartService.Models;

namespace Shop.CartService;

public class CartManagerService : ICartManagerService
{
    private ICartDBRepository _repository;

    public CartManagerService(ICartDBRepository repository)
    {
        _repository = repository;
    }
    public async Task<List<CartModel>> GetAllAsync(NameValueCollection filters, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync(filters, cancellationToken);
    }

    public async Task<List<CartModel>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<bool> CreateAsync(CartModel item, CancellationToken cancellationToken)
    {
        return await _repository.CreateAsync(item, cancellationToken);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _repository.DeleteAsync(id, cancellationToken);
    }

    public async Task<bool> UpdateAsync(CartModel item, CancellationToken cancellationToken)
    {
        return await _repository.UpdateAsync(item, cancellationToken);
    }

    public Task<Guid?> GetActulalCartAsync(Guid userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteUserCartAsync(string userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}