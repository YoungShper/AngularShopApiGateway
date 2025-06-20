using Shop.ApiGateWay.Models;

namespace Shop.ApiGateWay.Interfaces;

public interface ICartAggregationService
{
    Task<List<CartDataModel>> GetAllUserCartDataAsync(HttpRequest httpRequest, CancellationToken cancellationToken);
}