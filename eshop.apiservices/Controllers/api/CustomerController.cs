using AutoMapper;
using eshop.core.DTO.Request;
using eshop.core.Entities;
using eshop.core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eshop.apiservices.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customer;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customer = customerRepository;
            _mapper = mapper;
        }

        [Authorize(Policy = "Manager")]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllCustomers()
        {
            var data = await _customer.GetAllCustomersAsync();
            return Ok(data);
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var result = await _customer.GetCustomerByIdAsync(id);
            if (result == null)
                return NotFound(id);
            return Ok(result);
        }

        [Authorize(Policy = "Manager")]
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddCustomer([FromBody] CustomerInfoRequest customerRequest)
        {
            if (!ModelState.IsValid) return BadRequest();
            Customer newCustomer = _mapper.Map<Customer>(customerRequest);
            var result = await _customer.AddCustomerAsync(newCustomer);
            return CreatedAtAction(nameof(GetCustomer), new { id = result.Id }, result);
        }

        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerInfoRequest customerRequest)
        {
            if (!ModelState.IsValid) return BadRequest();
            Customer updatedCustomer = _mapper.Map<Customer>(customerRequest);
            var result = await _customer.UpdateCustomerAsync(id, updatedCustomer);
            return Ok(result);
        }

        [Authorize(Policy = "Manager")]
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var result = await _customer.DeleteCustomerAsync(id);
            if (result != -1) return NotFound();
            return NoContent();
        }
    }
}
