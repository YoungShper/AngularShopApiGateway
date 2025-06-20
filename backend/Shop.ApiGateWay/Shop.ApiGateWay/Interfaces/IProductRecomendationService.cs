namespace Shop.ApiGateWay.Interfaces;

public interface IProductRecomendationService
{
    public Task<string> GetRecommendationsAsync(string url, CancellationToken ct);
}