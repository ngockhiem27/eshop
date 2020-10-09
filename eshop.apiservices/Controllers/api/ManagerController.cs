using eshop.core.DTO.Request;
using eshop.core.Entities;
using eshop.core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eshop.apiservices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerRepository _manager;

        public ManagerController(IManagerRepository manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllManagers([FromQuery] int pageSize = 10, int pageIndex = 0)
        {
            var data = await _manager.GetAllManagersAsync();
            return Ok(data);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetManager(int id)
        {
            var result = await _manager.GetManagerByIdAsync(id);
            if (result == null)
                return NotFound(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddManager([FromBody] AddManagerRequest managerRequest)
        {
            if (!ModelState.IsValid) return BadRequest();
            Manager newManager = new Manager(managerRequest);
            var result = await _manager.AddManagerAsync(newManager);
            if (result == null) return NotFound();
            return CreatedAtAction(nameof(GetManager), new { id = result.Id }, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateManager([FromBody] AddManagerRequest managerRequest)
        {
            if (!ModelState.IsValid) return BadRequest();
            Manager newManager = new Manager(managerRequest);
            var result = await _manager.UpdateManagerAsync(newManager);
            if (result == null) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManager(int id)
        {
            var result = await _manager.DeleteManagerAsync(id);
            return Ok(result);
        }
    }
}
