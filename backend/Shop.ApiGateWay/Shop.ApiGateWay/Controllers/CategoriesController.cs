using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Shop.ApiGateWay.Interfaces;
using Shop.ApiGateWay.Models;

namespace Shop.ApiGateWay.Controllers
{
  [Route("apiGateway/categories")]
  [ApiController]
  public class CategoriesController : BaseApiGatewayController
  {
    public CategoriesController(IConfiguration config, IForwardingService forwardingService)
    {
      _urlString = config.GetForwardingRoute("Categories");
      _forwardingService = forwardingService;
    }
    
    [Authorize(Roles = "Admin")]
    public override async Task<ActionResult> Create(CancellationToken ct) => await base.Create(ct);
    [Authorize(Roles = "Admin")]
    public override async Task<ActionResult> Update(CancellationToken ct) => await base.Update(ct);
    [Authorize(Roles = "Admin")]
    public override async Task<ActionResult> Delete(string id, CancellationToken ct) => await base.Delete(id, ct);
  }
}
