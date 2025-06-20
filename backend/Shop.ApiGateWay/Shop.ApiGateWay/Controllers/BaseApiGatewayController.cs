using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Shop.ApiGateWay.Interfaces;
using Shop.ApiGateWay.Models;
using MediaTypeHeaderValue = System.Net.Http.Headers.MediaTypeHeaderValue;

namespace Shop.ApiGateWay.Controllers;

public abstract class BaseApiGatewayController : ControllerBase
{
    protected IForwardingService _forwardingService;
    protected string _urlString;
    
    [HttpGet]
    public virtual async Task<ActionResult> GetAll(CancellationToken ct)
    {
        var queryString = HttpContext.Request.QueryString.Value;
        var result = await _forwardingService.GetAsync($"{_urlString}{queryString}", ct);
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public virtual async Task<ActionResult> GetById(string id, CancellationToken ct)
    {
        var result = await _forwardingService.GetByIdAsync($"{_urlString}", id, ct);
        return Ok(result);
    }
    [HttpPost]
    public virtual async Task<ActionResult> Create(CancellationToken ct)
    {
        var result = await _forwardingService.PostAsync($"{_urlString}",  Request, ct);
        return Ok(result);
    }

    [HttpPut]
    public virtual async Task<ActionResult> Update(CancellationToken ct)
    {
        var result = await _forwardingService.PutAsync($"{_urlString}", HttpContext.Request, ct);
        return Ok(result);
    }
    [HttpDelete("{id}")]
    public virtual async Task<ActionResult> Delete(string id, CancellationToken ct)
    {
        var result = await _forwardingService.DeleteAsync(_urlString, id, ct);
        return Ok(result);
    }
}