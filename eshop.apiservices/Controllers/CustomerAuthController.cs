using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eshop.core.DTO.Request;
using eshop.core.DTO.Response;
using eshop.core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

    }
}
