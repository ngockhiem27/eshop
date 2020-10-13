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
    [Authorize]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerRepository _manager;

        public ManagerController(IManagerRepository manager)
        {
            _manager = manager;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllManagers([FromQuery] int pageSize = 10, int pageIndex = 0)
        {
            var data = await _manager.GetAllManagersAsync();
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
            Manager newManager = new Manager(managerRequest);
            try {
                var result = await _manager.AddManagerAsync(newManager);
                return CreatedAtAction(nameof(GetManager), new { id = result.Id }, result);
            } catch (Exception ex) {
                return StatusCode(500);
            }
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateManager([FromBody] ManagerInfoRequest managerRequest)
        {
            if (!ModelState.IsValid) return BadRequest();
            Manager newManager = new Manager(managerRequest);
            try {
                var result = await _manager.UpdateManagerAsync(newManager);
                return Ok(result);
            } catch (Exception ex) {
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
            if (result != 1) return NotFound();
            return NoContent();
        }
    }
}
