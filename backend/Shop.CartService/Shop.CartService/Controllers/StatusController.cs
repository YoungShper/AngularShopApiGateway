using System.Web;
using Microsoft.AspNetCore.Mvc;
using Shop.CartService.Interfaces;
using Shop.CartService.Models;

namespace Shop.CartService.Controllers
{
    [Route("services/status")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private IDbRepository<StatusModel> _dbRepository;

        public StatusController(IDbRepository<StatusModel> dbRepository)
        {
            _dbRepository = dbRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetProducts(CancellationToken ct)
        {
            var queryData = HttpUtility.ParseQueryString(HttpContext.Request.QueryString.Value ?? string.Empty);
            var result = await _dbRepository.GetAllAsync(queryData, ct);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id, CancellationToken ct)
        {
            var result = await _dbRepository.GetByIdAsync(id, ct);
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] StatusModel productModel, CancellationToken ct)
        {
            var result = await _dbRepository.CreateAsync(productModel, ct);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id, CancellationToken ct)
        {
            var result = await _dbRepository.DeleteAsync(id, ct);
            return Ok(new { message = result });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromBody] StatusModel status, CancellationToken ct)
        {
            var result = await _dbRepository.UpdateAsync(status, ct);
            return Ok(new { message = result });
        }
    }
}