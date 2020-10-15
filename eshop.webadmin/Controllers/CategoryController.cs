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
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var (statusCode, categories) = await categoryService.GetAllCategory();
            if (statusCode == HttpStatusCode.Unauthorized)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction(nameof(AccountController.Index), "Account");
            }
            return View(categories);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CategoryInfoRequest categoryRequest)
        {
            var (statusCode, category) = await categoryService.AddCategory(categoryRequest);
            return StatusCode((int)statusCode, category);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CategoryInfoRequest categoryRequest)
        {
            var (statusCode, category) = await categoryService.UpdateCategory(categoryRequest);
            return StatusCode((int)statusCode, category);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var statusCode = await categoryService.DeleteCategory(id);
            return StatusCode((int)statusCode);
        }
    }
}
