using System.Web;
using Microsoft.AspNetCore.Mvc;
using Shop.ArticleService.Interfaces;
using Shop.ArticleService.Models;

namespace Shop.ArticleService.Controllers
{
    [Route("services/articles")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private IDbRepository<ArticleModel> _dbRepository;

        public ArticleController(IDbRepository<ArticleModel> dbRepository)
        {
            _dbRepository = dbRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetProducts(CancellationToken ct, [FromQuery] string search = "", [FromQuery] string filters = "")
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
        public async Task<ActionResult> Create([FromBody] ArticleModel articleModel, CancellationToken ct)
        {
            var result = await _dbRepository.CreateAsync(articleModel, ct);
            return Ok(new { message = result });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id, CancellationToken ct)
        {
            var result = await _dbRepository.DeleteAsync(id, ct);
            return Ok(new { message = result });
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] ArticleModel article, CancellationToken ct)
        {
            var result = await _dbRepository.UpdateAsync(article, ct);
            return Ok(new { message = result });
        }
    }
}
