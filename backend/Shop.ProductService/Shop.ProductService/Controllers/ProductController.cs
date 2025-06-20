using Microsoft.AspNetCore.Mvc;
using Dapper;
using Shop.ProductService.Interfaces;
using Shop.ProductService.Models;
using System.Web;

namespace Shop.ProductService.Controllers
{
    [Route("services/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductManagmentService _productManagment;
        private IRecomendationEngine _recomendationEngine;
        
        public ProductController(IProductManagmentService productManagmentService, IRecomendationEngine recomendationEngine)
        {
            _productManagment = productManagmentService;
            _recomendationEngine = recomendationEngine;
        }

        [HttpGet]
        public async Task<ActionResult> GetProducts(CancellationToken ct, [FromQuery] string search = "", [FromQuery] int? priceFrom = null, [FromQuery] int? priceTo = null)
        {
            var queryData = HttpUtility.ParseQueryString(HttpContext.Request.QueryString.Value ?? string.Empty);
            
            var result = await _productManagment.GetAllAsync(queryData, ct);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id, CancellationToken ct)
        {
            var result = await _productManagment.GetByIdAsync(id, ct);
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ProductModel productModel, CancellationToken ct)
        {
            var result = await _productManagment.CreateAsync(productModel, ct);
            return Ok(new { message = result });
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id, CancellationToken ct)
        {
            var result = await _productManagment.DeleteAsync(id, ct);
            return Ok(new { message = result });
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] ProductModel product, CancellationToken ct)
        {
            var result = await _productManagment.UpdateAsync(product, ct);
            return Ok(new { message = result });
        }

        [HttpGet("recommendation")]
        public async Task<ActionResult> GetRecomendation([FromQuery] string goal,
            [FromQuery] double weight,
            [FromQuery] double height,
            [FromQuery] int age, CancellationToken ct)
        {
            var user = new UserProfileModel
            {
                Goal = goal,
                Weight = weight,
                Height = height,
                Age = age
            };
            var result = await _recomendationEngine.GetRecommendedAsync(user, 5, ct);
            return Ok(result);
        }
    }
}
