using eshop.core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eshop.apiservices.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        public OrderController()
        {

        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] List<OrderItemsViewModel> items)
        {

            return Ok();
        }
    }
}
