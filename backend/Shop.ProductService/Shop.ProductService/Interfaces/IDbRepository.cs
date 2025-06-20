using System.Collections.Specialized;
using Shop.ProductService.Models;

namespace Shop.ProductService.Interfaces;

public interface IDbRepository<T> where T : class
{
    public Task<List<T>> GetAllAsync(NameValueCollection filters, CancellationToken cancellationToken);
    public  Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<bool> CreateAsync(T item, CancellationToken cancellationToken);
    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
    public Task<bool> UpdateAsync(T item, CancellationToken cancellationToken);
    public Task<int> GetTotalElemetnsOfTable(NameValueCollection queryString, CancellationToken ct);
}