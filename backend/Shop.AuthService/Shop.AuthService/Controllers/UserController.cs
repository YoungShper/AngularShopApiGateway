using System.Text.Json;
using System.Web;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shop.AuthService.Interfaces;
using Shop.AuthService.Interfaces.Auth;
using Shop.AuthService.Models;
using Shop.AuthService.Services;

namespace Shop.AuthService.Controllers
{
    [Route("services/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserManager _usermanagmentService;
        private IAuth _authUserService;

        public UserController(IUserManager userManagmentService, IAuth authUserService)
        {
            _usermanagmentService = userManagmentService;
            _authUserService = authUserService;
        }

        [HttpGet]
        public async Task<ActionResult> GetUsers(CancellationToken ct)
        {
            var queryData = HttpUtility.ParseQueryString(HttpContext.Request.QueryString.Value ?? string.Empty);

            var result = await _usermanagmentService.GetAllAsync(queryData, ct);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id, CancellationToken ct)
        {
            var result = await _usermanagmentService.GetByIdAsync(id, ct);
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] UserModel userModel, CancellationToken ct)
        {
            var result = await _usermanagmentService.CreateAsync(userModel, ct);
            return Ok(new { message = result });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id, CancellationToken ct)
        {
            var result = await _usermanagmentService.DeleteAsync(id, ct);
            return Ok(new { message = result });
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UserModel userModel, CancellationToken ct)
        {
            var result = await _usermanagmentService.UpdateAsync(userModel, ct);
            return Ok(new { message = result });
        }
        
        [HttpGet("register")]
        public async Task<ActionResult> Register([FromQuery] string mail, [FromQuery] string password, CancellationToken ct)
        {
            var result = await _authUserService.Register(mail, password, ct);
            return Ok(new { message = result });
        }
        
        [HttpGet("login")]
        public async Task<ActionResult> Login([FromQuery] string mail, [FromQuery] string password, CancellationToken ct)
        {
            var result = await _authUserService.Login(mail, password, ct);
            return Ok(result);
        }
    }
}
