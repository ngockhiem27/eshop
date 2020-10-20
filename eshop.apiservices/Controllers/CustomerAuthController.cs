using eshop.core.DTO.Request;
using eshop.core.DTO.Response;
using eshop.core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eshop.apiservices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAuthController : ControllerBase
    {
        private readonly ICustomerAuthService _accountService;

        public CustomerAuthController(ICustomerAuthService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = await _accountService.AuthenticateAsync(request.Email, request.Password);
            if (result == null) return NotFound();
            return Ok(new CustomerLoginResponse
            {
                Customer = result.Customer,
                AccessToken = result.JwtResult.AccessToken,
                RefreshToken = result.JwtResult.RefreshToken.TokenString,
            }); ;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CustomerInfoRequest customerRequest)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = await _accountService.RegisterAsync(customerRequest);
            if (result == null) return BadRequest();
            return Ok(new CustomerLoginResponse
            {
                Customer = result.Customer,
                AccessToken = result.JwtResult.AccessToken,
                RefreshToken = result.JwtResult.RefreshToken.TokenString,
            }); ;
        }
    }
}
