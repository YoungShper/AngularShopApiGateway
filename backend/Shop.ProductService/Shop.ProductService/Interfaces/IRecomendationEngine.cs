using Shop.ProductService.Models;

namespace Shop.ProductService.Interfaces;

public interface IRecomendationEngine
{
    public Task<List<ProductModel>> GetRecommendedAsync(UserProfileModel user, int topN, CancellationToken cancellationToken = default);
}