using eshop.core.DTO.Request;
using eshop.webadmin.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eshop.webadmin.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction(nameof(HomeController.Index), "Home");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm] LoginRequest data)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = await _accountService.AuthenticateAsync(data);
            if (result == null)
            {
                ViewData["LoginNotification"] = "User Authentication failed!!!";
                return View();
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, result.Manager.FirstName),
                new Claim(ClaimTypes.Email, result.Manager.Email),
                new Claim(ClaimTypes.Role, result.Manager.Role_Name),
                new Claim("UserId", result.Manager.Id.ToString()),
                new Claim("RoleId", result.Manager.Role_Id.ToString()),
                new Claim("FirstName", result.Manager.FirstName),
                new Claim("LastName", result.Manager.LastName),
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
            return RedirectToAction(nameof(AccountController.Login), "Account");
        }
    }
}
