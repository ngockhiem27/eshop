using AutoMapper;
using eshop.core.DTO.Request;
using eshop.core.DTO.Response;
using eshop.core.Entities;
using eshop.core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eshop.apiservices.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerAuthController : ControllerBase
    {
        private readonly ICustomerAuthService _accountService;
        private readonly IMapper _mapper;

        public CustomerAuthController(ICustomerAuthService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
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
            var newCustomer = _mapper.Map<Customer>(customerRequest);
            var result = await _accountService.RegisterAsync(newCustomer);
            if (result == null) return BadRequest();
            return Ok(new CustomerLoginResponse
            {
                Customer = result.Customer,
                AccessToken = result.JwtResult.AccessToken,
                RefreshToken = result.JwtResult.RefreshToken.TokenString,
            });
        }
    }
}
