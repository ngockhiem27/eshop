using AutoMapper;
using eshop.apiservices.Services;
using eshop.core.DTO.Request;
using eshop.core.Entities;
using eshop.core.Interfaces.Repositories;
using eshop.core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.apiservices.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public ProductController(IProductRepository productRepository, IMapper mapper, IFileService fileService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _fileService = fileService;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllProduct()
        {
            var result = await _productRepository.GetAllProductAsync();
            return Ok(result);
        }

        [HttpGet("category")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllProductWithCategory()
        {
            var productCategory = await _productRepository.GetAllProductWithCategoryAsync();
            var result = _mapper.Map<IEnumerable<IGrouping<int, ProductCategoryViewModel>>, IEnumerable<ProductViewModel>>(productCategory.GroupBy(r => r.Product_Id));
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetProduct(int id)
        {
            var result = await _productRepository.GetProductAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddProduct([FromBody] ProductInfoRequest productRequest)
        {
            if (!ModelState.IsValid) return BadRequest();
            var newProduct = _mapper.Map<Product>(productRequest);
            var result = await _productRepository.AddProductAsync(newProduct, productRequest.Categories);
            return CreatedAtAction(nameof(GetProduct), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductInfoRequest productRequest)
        {
            if (!ModelState.IsValid) return BadRequest();
            var updatedProduct = _mapper.Map<Product>(productRequest);
            var result = await _productRepository.UpdateProductAsync(id, updatedProduct, productRequest.Categories);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var row = await _productRepository.DeleteProductAsync(id);
            if (row != -1) return NotFound();
            return NoContent();
        }

        [HttpPut("{productId}/Category/{categoryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddProductCategory(int productId, int categoryId)
        {
            var row = await _productRepository.AddProductCategoryAsync(productId, categoryId);
            if (row != -1) return NotFound();
            return NoContent();
        }

        [HttpDelete("{productId}/Category/{categoryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> RemoveProductCategory(int productId, int categoryId)
        {
            var row = await _productRepository.DeleteProductCategoryAsync(productId, categoryId);
            if (row != -1) return NotFound();
            return NoContent();
        }

        [HttpGet("{productId}/Image")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetProductImages(int productId)
        {
            var req = Request.Host.Value;
            var imgs = await _productRepository.GetProductImage(productId);
            return Ok(imgs);
        }

        [HttpGet("Image/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetImage(int id)
        {

            var img = await _productRepository.GetImage(id);
            if (img == null) return NotFound();
            return Ok(img);
        }

        [HttpPost("{productId}/Image")]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddProductImage(int productId, IFormFile file)
        {
            var filePath = await _fileService.UploadImageAsync(file);
            if (filePath == null) return BadRequest();
            var img = await _productRepository.AddProductImage(productId, filePath);
            return CreatedAtAction(nameof(GetImage), new { id = img.Id }, img);
        }

        [HttpDelete("Image/{imageId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteProductImage(int imageId)
        {
            var row = await _productRepository.DeleteProductImage(imageId);
            if (row != -1) return NotFound();
            return NoContent();
        }
    }
}
