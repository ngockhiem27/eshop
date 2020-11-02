using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using eshop.core.ViewModels;
using eshop.webshop.SignalRHub;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace eshop.webshop.Controllers
{
    [Route("hook")]
    [ApiController]
    public class HookController : ControllerBase
    {
        private readonly IHubContext<ProductHub> _productHubCtx;

        public HookController(IHubContext<ProductHub> hubCtx)
        {
            _productHubCtx = hubCtx;
        }

        [HttpPost("product")]
        public void ProductNotify([FromBody] ProductViewModel product)
        {
            _productHubCtx.Clients.All.SendAsync("ProductUpdate", JsonSerializer.Serialize(product));
        }
    }
}
