using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.ApiGateWay.Interfaces;

namespace Shop.ApiGateWay.Controllers;

[ApiController]
[Route("apiGateway/cart")]
//[Authorize]
public class CartController : BaseApiGatewayController
{
    ICartAggregationService _cartAggregationService;
    public CartController(IConfiguration config, IForwardingService forwardingService, ICartAggregationService cartAggregationService)
    {
        _urlString = config.GetForwardingRoute("Cart");
        _forwardingService = forwardingService;
        _cartAggregationService = cartAggregationService;
    }
    [HttpGet]
    public override async Task<ActionResult> GetAll(CancellationToken ct)
    {
        var result = await _cartAggregationService.GetAllUserCartDataAsync(HttpContext.Request, ct);
        return Ok(result);
    }
    
}