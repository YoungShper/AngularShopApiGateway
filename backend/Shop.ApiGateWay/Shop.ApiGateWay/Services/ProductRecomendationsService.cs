using Shop.ApiGateWay.Interfaces;

namespace Shop.ApiGateWay;

public class ProductRecomendationsService : IProductRecomendationService
{
    private readonly HttpClient _httpClient;

    public ProductRecomendationsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<string> GetRecommendationsAsync(string url, CancellationToken ct)
    {
        var response = await _httpClient.GetAsync(url, ct);
        return await response.Content.ReadAsStringAsync();
    }
}