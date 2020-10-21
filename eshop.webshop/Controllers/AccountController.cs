using eshop.core.DTO.Request;
using eshop.webshop.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eshop.webshop.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction(nameof(HomeController.Index), "Home");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm] CustomerInfoRequest customerRequest)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = await _accountService.RegisterAsync(customerRequest);
            if (result == null)
            {
                ViewData["RegisterNotification"] = "User Register failed!!!";
                return View();
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, result.Customer.FirstName),
                new Claim(ClaimTypes.Email, result.Customer.Email),
                new Claim("UserId", result.Customer.Id.ToString()),
                new Claim("FirstName", result.Customer.FirstName),
                new Claim("LastName", result.Customer.LastName),
                new Claim("AccessToken", result.AccessToken),
                new Claim("RefreshToken", result.RefreshToken),
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm] LoginRequest data)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = await _accountService.AuthenticateAsync(data);
            if (result == null)
            {
                return NotFound();
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, result.Customer.FirstName),
                new Claim(ClaimTypes.Email, result.Customer.Email),
                new Claim("UserId", result.Customer.Id.ToString()),
                new Claim("FirstName", result.Customer.FirstName),
                new Claim("LastName", result.Customer.LastName),
                new Claim("AccessToken", result.AccessToken),
                new Claim("RefreshToken", result.RefreshToken),
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
