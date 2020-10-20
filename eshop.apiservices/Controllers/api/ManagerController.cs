using AutoMapper;
using eshop.core.DTO.Request;
using eshop.core.Entities;
using eshop.core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace eshop.apiservices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Manager")]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerRepository _manager;
        private readonly IMapper _mapper;

        public ManagerController(IManagerRepository manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllManagers()
        {
            var data = await _manager.GetAllManagersAsync();
            return Ok(data);
        }

        [HttpGet("role")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllRole()
        {
            var data = await _manager.GetAllRoleAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetManager(int id)
        {
            var result = await _manager.GetManagerByIdAsync(id);
            if (result == null)
                return NotFound(id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddManager([FromBody] ManagerInfoRequest managerRequest)
        {
            if (!ModelState.IsValid) return BadRequest();
            Manager newManager = _mapper.Map<Manager>(managerRequest);
            try
            {
                var result = await _manager.AddManagerAsync(newManager);
                return CreatedAtAction(nameof(GetManager), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateManager(int id, [FromBody] ManagerInfoRequest managerRequest)
        {
            if (!ModelState.IsValid) return BadRequest();
            Manager updatedManager = _mapper.Map<Manager>(managerRequest);
            try
            {
                var result = await _manager.UpdateManagerAsync(id, updatedManager);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteManager(int id)
        {
            var result = await _manager.DeleteManagerAsync(id);
            if (result != -1) return NotFound();
            return NoContent();
        }
    }
}
