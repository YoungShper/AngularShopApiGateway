using System.Web;
using Microsoft.AspNetCore.Mvc;
using Shop.ProductService.Interfaces;
using Shop.ProductService.Models;

namespace Shop.ProductService.Controllers;

[Route("services/categories")]
[ApiController]
public class CategoryController : ControllerBase
{
    private IDbRepository<CategoryModel> _dbRepository;
        
    public CategoryController(IDbRepository<CategoryModel> dbRepository)
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
    public async Task<ActionResult> Create([FromBody] CategoryModel productModel, CancellationToken ct)
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
    public async Task<ActionResult> Update([FromBody] CategoryModel category, CancellationToken ct)
    {
        var result = await _dbRepository.UpdateAsync(category, ct);
        return Ok(new { message = result });
    }
}