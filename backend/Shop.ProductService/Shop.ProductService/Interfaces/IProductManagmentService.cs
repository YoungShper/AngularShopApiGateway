using System.Collections.Specialized;
using Shop.ProductService.Models;

namespace Shop.ProductService.Interfaces;

public interface IProductManagmentService
{
    public Task<PagedResponse<ProductModel>> GetAllAsync(NameValueCollection filters, CancellationToken cancellationToken, int page = 10);
    public  Task<ProductModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<bool> CreateAsync(ProductModel item, CancellationToken cancellationToken);
    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
    public Task<bool> UpdateAsync(ProductModel item, CancellationToken cancellationToken);
}