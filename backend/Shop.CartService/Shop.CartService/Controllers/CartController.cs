using System.Web;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Shop.CartService.Interfaces;
using Shop.CartService.Models;

namespace Shop.CartService.Controllers
{
    [Route("services/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private ICartManagerService _cartManagerService;

        public CartController(ICartManagerService cartManagerService)
        {
            _cartManagerService = cartManagerService;
        }

        [HttpGet]
        public async Task<ActionResult> GetProducts(CancellationToken ct, [FromQuery] string search = "", [FromQuery] string filters = "")
        {
            var queryData = HttpUtility.ParseQueryString(HttpContext.Request.QueryString.Value ?? string.Empty);

            var result = await _cartManagerService.GetAllAsync(queryData, ct);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id, CancellationToken ct)
        {
            var result = await _cartManagerService.GetByIdAsync(id, ct);
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CartModel cartModel, CancellationToken ct)
        {
            var result = await _cartManagerService.CreateAsync(cartModel, ct);
            return Ok(new { message = result });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id, CancellationToken ct)
        {
            var result = await _cartManagerService.DeleteAsync(id, ct);
            return Ok(new { message = result });
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] CartModel cart, CancellationToken ct)
        {
            var result = await _cartManagerService.UpdateAsync(cart, ct);
            return Ok(new { message = result });
        }
    }
}
