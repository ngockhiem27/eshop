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
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var (statusCode1, products) = await _productService.GetAllProductWithCategory();
            var (statusCode2, categories) = await _categoryService.GetAllCategory();
            if (statusCode1 == HttpStatusCode.Unauthorized || statusCode2 == HttpStatusCode.Unauthorized)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction(nameof(AccountController.Index), "Account");
            }
            ViewData["Categories"] = categories;
            return View(products);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var statusCode = await _productService.DeleteProduct(id);
            return StatusCode((int)statusCode);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductInfoRequest productRequest)
        {
            if (!ModelState.IsValid) return BadRequest();
            var (statusCode, product) = await _productService.AddProduct(productRequest);
            return StatusCode((int)statusCode, product);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductInfoRequest productRequest)
        {
            if (!ModelState.IsValid) return BadRequest();
            var (statusCode, product) = await _productService.UpdateProduct(productRequest);
            return StatusCode((int)statusCode, product);
        }
    }
}
