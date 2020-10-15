using AutoMapper;
using eshop.core.DTO.Request;
using eshop.core.Entities;
using eshop.core.Interfaces.Repositories;
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
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllCategory()
        {
            var result = await _categoryRepository.GetAllCategoryAsync();
            return Ok(result);
        }

        [HttpGet("product")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllCategoryWithProduct()
        {
            var result = await _categoryRepository.GetAllCategoryWithProductAsync();
            var gr = result.GroupBy(r => r.Category_Id);
            var res = _mapper.Map<IEnumerable<IGrouping<int, ProductCategoryViewModel>>, IEnumerable<CategoryViewModel>>(gr);
            return Ok(res);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetCategory(int id)
        {
            var result = await _categoryRepository.GetCategoryAsync(id);
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
            var result = await _categoryRepository.AddCategoryAsync(newCategory);
            return CreatedAtAction(nameof(GetCategory), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryInfoRequest categoryRequest)
        {
            Category updatedCategory = _mapper.Map<Category>(categoryRequest);
            var result = await _categoryRepository.UpdateCategoryAsync(id, updatedCategory);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryRepository.DeleteCategoryAsync(id);
            if (result != -1) return NotFound();
            return NoContent();
        }
    }
}
