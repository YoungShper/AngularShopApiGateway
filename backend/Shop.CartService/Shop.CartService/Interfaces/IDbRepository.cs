using System.Collections.Specialized;
using Shop.CartService.Models;

namespace Shop.CartService.Interfaces
{
    public interface IDbRepository<T> where T : class
    {
        public Task<List<T>> GetAllAsync(NameValueCollection filters, CancellationToken cancellationToken);
        public Task<List<T>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        public Task<bool> CreateAsync(T item, CancellationToken cancellationToken);
        public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
        public Task<bool> UpdateAsync(T item, CancellationToken cancellationToken);
    }
}