using eshop.core.DTO.Request;
using eshop.core.DTO.Response;
using eshop.core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eshop.apiservices.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ManagerAuthController : ControllerBase
    {
        private readonly IManagerAuthService _accountService;

        public ManagerAuthController(IManagerAuthService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = await _accountService.AuthenticateAsync(request.Email, request.Password);
            if (result == null) return NotFound();
            return Ok(new ManagerLoginResponse
            {
                Manager = result.Manager,
                AccessToken = result.JwtResult.AccessToken,
                RefreshToken = result.JwtResult.RefreshToken.TokenString,
            }); ;
        }

        // [HttpPost("logout")]
        // public async Task<IActionResult> Logout([FromBody] string req)
        // {
        //     return Ok();
        // }
    }
}
