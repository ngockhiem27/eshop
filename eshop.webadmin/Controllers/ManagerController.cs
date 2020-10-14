using eshop.core.DTO.Request;
using eshop.webadmin.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace eshop.webadmin.Controllers
{
    [Authorize]
    public class ManagerController : Controller
    {
        private readonly IManagerService _managerService;

        public ManagerController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        public async Task<IActionResult> Index()
        {
            var (statusCode, managers) = await _managerService.GetAllManagerAsync();
            return await HandleStatusCode(statusCode, managers);
            // return View(managers);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var statusCode = await _managerService.DeleteManager(id);
            return StatusCode((int)statusCode);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ManagerInfoRequest managerInfo)
        {
            if (!ModelState.IsValid) return BadRequest();
            var (statusCode, newManager) = await _managerService.AddNewManager(managerInfo);
            return Ok(newManager);
        }

        private async Task<IActionResult> HandleStatusCode(HttpStatusCode statusCode, object data)
        {
            if (statusCode == HttpStatusCode.Unauthorized)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction(nameof(AccountController.Index), "Account");
            }
            return View(data);
        }
    }
}
