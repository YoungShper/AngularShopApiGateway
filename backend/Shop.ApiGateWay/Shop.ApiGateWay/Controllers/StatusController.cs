using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.ApiGateWay.Interfaces;

namespace Shop.ApiGateWay.Controllers;

[ApiController]
[Route("apiGateway/status")]
public class StatusController : BaseApiGatewayController
{
    public StatusController(IConfiguration config, IForwardingService forwardingService)
    {
        _urlString = config.GetForwardingRoute("Status");
        _forwardingService = forwardingService;
    }
    
    [Authorize(Policy = "Admin")]
    public override async Task<ActionResult> Create(CancellationToken ct) => await base.Create(ct);
    [Authorize(Policy = "Admin")]
    public override async Task<ActionResult> Update(CancellationToken ct) => await base.Update(ct);
    [Authorize(Policy = "Admin")]
    public override async Task<ActionResult> Delete(string id, CancellationToken ct) => await base.Delete(id, ct);
}