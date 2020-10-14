using eshop.core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eshop.apiservices.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllProduct()
        {
            var res = await _productRepository.GetAllProductAsync();
            return Ok(res);
        }

        [HttpGet("product/category")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllProductWithCategory()
        {
            var res = await _productRepository.GetAllProductWithCategoryAsync();
            return Ok(res);
        }
    }
}
