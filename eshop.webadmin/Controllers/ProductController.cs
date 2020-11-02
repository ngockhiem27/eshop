using eshop.core.DTO.Request;
using eshop.webadmin.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace eshop.webadmin.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHookService _webhook;

        public ProductController(IProductService productService, ICategoryService categoryService, IWebHookService webhook)
        {
            _productService = productService;
            _categoryService = categoryService;
            _webhook = webhook;
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

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var statusCode = await _productService.DeleteProduct(id);
            return StatusCode((int)statusCode);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] ProductInfoRequest productRequest)
        {
            if (!ModelState.IsValid) return BadRequest();
            var (statusCode, product) = await _productService.AddProduct(productRequest);
            _webhook.PostProductUpdateAsync(product);
            return StatusCode((int)statusCode, product);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update([FromBody] ProductInfoRequest productRequest)
        {
            if (!ModelState.IsValid) return BadRequest();
            var (statusCode, product) = await _productService.UpdateProduct(productRequest);
            _webhook.PostProductUpdateAsync(product);
            return StatusCode((int)statusCode, product);
        }

        [HttpGet("{productId}/Image")]
        public async Task<IActionResult> GetProductImage(int productId)
        {
            var (statusCode, imgs) = await _productService.GetProductImage(productId);
            return StatusCode((int)statusCode, imgs);
        }

        [HttpPost("{productId}/Image")]
        public async Task<IActionResult> AddProductImage(int productId)
        {
            var file = Request.Form.Files.FirstOrDefault();
            if (!Request.HasFormContentType || file == null) return BadRequest();
            var (statusCode, img) = await _productService.AddProductImage(productId, file);
            return StatusCode((int)statusCode, img);
        }

        [HttpDelete("{productId}/Image")]
        public async Task<IActionResult> DeleteProductImage(int imgId)
        {
            var statusCode = await _productService.DeleteProductImage(imgId);
            return StatusCode((int)statusCode);
        }
    }
}
