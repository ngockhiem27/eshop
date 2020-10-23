using AutoMapper;
using eshop.apiservices.Cache;
using eshop.core.DTO.Request;
using eshop.core.Entities;
using eshop.core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.apiservices.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Manager")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryCached _category;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryCached category, IMapper mapper)
        {
            _category = category;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllCategory()
        {
            var result = await _category.GetAllCategoryAsync();
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("product")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllCategoryWithProduct()
        {
            var result = await _category.GetAllCategoryWithProductAsync();
            var gr = result.GroupBy(r => r.Category_Id);
            var res = _mapper.Map<IEnumerable<IGrouping<int, ProductCategoryViewModel>>, IEnumerable<CategoryViewModel>>(gr);
            return Ok(res);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetCategory(int id)
        {
            var result = await _category.GetCategoryAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddCategory([FromBody] CategoryInfoRequest categoryRequest)
        {
            Category newCategory = _mapper.Map<Category>(categoryRequest);
            var result = await _category.AddCategoryAsync(newCategory);
            return CreatedAtAction(nameof(GetCategory), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryInfoRequest categoryRequest)
        {
            Category updatedCategory = _mapper.Map<Category>(categoryRequest);
            var result = await _category.UpdateCategoryAsync(id, updatedCategory);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _category.DeleteCategoryAsync(id);
            if (result != -1) return NotFound();
            return NoContent();
        }
    }
}
