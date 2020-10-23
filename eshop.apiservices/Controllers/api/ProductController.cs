using AutoMapper;
using eshop.apiservices.Cache;
using eshop.apiservices.Services;
using eshop.core.DTO.Request;
using eshop.core.Entities;
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
    [Authorize(Policy = "Manager")]
    public class ProductController : ControllerBase
    {
        private readonly IProductCached _product;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public ProductController(IProductCached product, IMapper mapper, IFileService fileService)
        {
            _product = product;
            _mapper = mapper;
            _fileService = fileService;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllProduct()
        {
            var result = await _product.GetAllProductAsync();
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("category")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllProductWithCategory()
        {
            var productCategory = await _product.GetAllProductWithCategoryAsync();
            var result = _mapper.Map<IEnumerable<IGrouping<int, ProductCategoryViewModel>>, IEnumerable<ProductViewModel>>(productCategory.GroupBy(r => r.Product_Id));
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("complete")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllProductComplete()
        {
            var productCategory = await _product.GetAllProductWithCategoryAsync();
            var result = _mapper.Map<IEnumerable<IGrouping<int, ProductCategoryViewModel>>, IEnumerable<ProductViewModel>>(productCategory.GroupBy(r => r.Product_Id)).ToList();
            for (int i = 0; i < result.Count(); i++)
            {
                result[i].Images = await _product.GetProductImage(result[i].Id);
            }
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetProduct(int id)
        {
            var result = await _product.GetProductAsync(id);
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
            var result = await _product.AddProductAsync(newProduct, productRequest.Categories);
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
            var result = await _product.UpdateProductAsync(id, updatedProduct, productRequest.Categories);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var row = await _product.DeleteProductAsync(id);
            if (row != -1) return NotFound();
            return NoContent();
        }

        [HttpPut("{productId}/Category/{categoryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddProductCategory(int productId, int categoryId)
        {
            var row = await _product.AddProductCategoryAsync(productId, categoryId);
            if (row != -1) return NotFound();
            return NoContent();
        }

        [HttpDelete("{productId}/Category/{categoryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> RemoveProductCategory(int productId, int categoryId)
        {
            var row = await _product.DeleteProductCategoryAsync(productId, categoryId);
            if (row != -1) return NotFound();
            return NoContent();
        }

        [HttpGet("{productId}/Image")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetProductImages(int productId)
        {
            var imgs = await _product.GetProductImage(productId);
            return Ok(imgs);
        }

        [HttpGet("Image/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetImage(int id)
        {

            var img = await _product.GetImage(id);
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
            var img = await _product.AddProductImage(productId, filePath);
            return CreatedAtAction(nameof(GetImage), new { id = img.Id }, img);
        }

        [HttpDelete("{productId}/Image/{imageId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteProductImage(int productId, int imageId)
        {
            var row = await _product.DeleteProductImage(productId, imageId);
            if (row != -1) return NotFound();
            return NoContent();
        }
    }
}
