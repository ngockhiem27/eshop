using eshop.core.DTO.Request;
using eshop.core.DTO.Response;
using eshop.core.Interfaces.Services;
using eshop.infrastructure.KafkaLog.LogHandler;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eshop.apiservices.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ManagerAuthController : ControllerBase
    {
        private readonly IManagerAuthService _accountService;
        private readonly LogHandler _logger;

        public ManagerAuthController(IManagerAuthService accountService, LogHandler logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = await _accountService.AuthenticateAsync(request.Email, request.Password);
            if (result == null) return NotFound();
            _logger.LogManagerLogin(result.Manager.Id, result.Manager.Email, result.Manager.Role_Name);
            return Ok(new ManagerLoginResponse
            {
                Manager = result.Manager,
                AccessToken = result.JwtResult.AccessToken,
                RefreshToken = result.JwtResult.RefreshToken.TokenString,
            });
        }

        //[HttpPost("logout")]
        //public  IActionResult Logout([FromBody] string req)
        //{
        //    return Ok();
        //}
    }
}
