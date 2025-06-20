using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.ApiGateWay.Interfaces;

namespace Shop.ApiGateWay.Controllers;

[ApiController]

[Route("apiGateway/users")]
public class AuthController : BaseApiGatewayController
{
    public AuthController(IConfiguration config, IForwardingService forwardingService)
    {
        _urlString = config.GetForwardingRoute("Users");
        _forwardingService = forwardingService;
    }
    
    [HttpGet("register")]
    public async Task<ActionResult> Register(CancellationToken ct)
    {
        var queryString = HttpContext.Request.QueryString;
        var result = await _forwardingService.GetAsync($"{_urlString}/register{queryString}", ct);
        return Ok(result);
    }
    [HttpGet("login")]
    public async Task<ActionResult> Login(CancellationToken ct)
    {
        var queryString = HttpContext.Request.QueryString;
        var token = await _forwardingService.GetAsync($"{_urlString}/login{queryString}", ct);
        HttpContext.Response.Cookies.Append("tasty-cookie", token);
        return Ok(new{message = string.IsNullOrEmpty(token) ? false : true});
    }
    
    [HttpGet("check-auth")]
    [Authorize]
    public IActionResult CheckAuth()
    {
        var id = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
        var mail = User.Claims.FirstOrDefault(c => c.Type == "mail")?.Value;
        var isAdmin = User.Claims.FirstOrDefault(c => c.Type == "isAdmin")?.Value == "True";

        return Ok(new {
            id,
            mail,
            isAdmin
        });
    }
    
    [HttpGet("logout")]
    [Authorize]
    public IActionResult Logout()
    {
        HttpContext.Response.Cookies.Append("tasty-cookie", "");
        return Ok(new { message = true });
    }
}