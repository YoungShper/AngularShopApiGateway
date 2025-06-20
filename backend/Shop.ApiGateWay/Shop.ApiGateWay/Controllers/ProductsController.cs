using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Shop.ApiGateWay.Interfaces;

namespace Shop.ApiGateWay.Controllers
{
    [ApiController]
    [Route("apiGateway/products")]
    public class ProductsController : BaseApiGatewayController
    {
        IProductRecomendationService _productRecomendationService;
        public ProductsController(IConfiguration config, IForwardingService forwardingService, IProductRecomendationService productRecomendationService)
        {
            _urlString = config.GetForwardingRoute("Products");
            _forwardingService = forwardingService;
            _productRecomendationService = productRecomendationService;
        }
        
        [Authorize(Policy = "Admin")]
        public override async Task<ActionResult> Create(CancellationToken ct) => await base.Create(ct);
        [Authorize(Policy = "Admin")]
        public override async Task<ActionResult> Update(CancellationToken ct) => await base.Update(ct);
        [Authorize(Policy = "Admin")]
        public override async Task<ActionResult> Delete(string id, CancellationToken ct) => await base.Delete(id, ct);
        
        [HttpGet("recommend")]
        public async Task<IActionResult> Recommend(CancellationToken cancellationToken)
        {
            var queryString = HttpContext.Request.QueryString.Value;
            var result = await _productRecomendationService.GetRecommendationsAsync($"{_urlString}/recommendation{queryString}", cancellationToken);
            return Ok(result);
        }
    }
}
