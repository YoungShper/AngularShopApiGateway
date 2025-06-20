using System.Collections.Specialized;
using Shop.ProductService.Interfaces;
using Shop.ProductService.Models;

namespace Shop.ProductService.Services;

public class ProductManagmentService : IProductManagmentService
{
    private IDbRepository<ProductModel> _productRepo;

    public ProductManagmentService(IDbRepository<ProductModel> productrepo)
    {
        _productRepo = productrepo;
    }
    public async Task<PagedResponse<ProductModel>> GetAllAsync(NameValueCollection filters, CancellationToken cancellationToken, int pageSize = 10)
    {
        var products = await _productRepo.GetAllAsync(filters, cancellationToken);
        if (filters.AllKeys.Contains("page"))
        {
            var totalElements = await _productRepo.GetTotalElemetnsOfTable(filters, cancellationToken);
        
            var totalPages = (int)Math.Ceiling((double)totalElements / pageSize);

            return new PagedResponse<ProductModel>()
            {
                Items = products,
                TotalPages = totalPages
            };
        }
        return new PagedResponse<ProductModel>()
        {
            Items = products,
            TotalPages = null
        };
    }

    public async Task<ProductModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _productRepo.GetByIdAsync(id, cancellationToken);
    }

    public Task<bool> CreateAsync(ProductModel item, CancellationToken cancellationToken)
    {
        return _productRepo.CreateAsync(item, cancellationToken);
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        return _productRepo.DeleteAsync(id, cancellationToken);
    }

    public Task<bool> UpdateAsync(ProductModel item, CancellationToken cancellationToken)
    {
        return _productRepo.UpdateAsync(item, cancellationToken);
    }
}