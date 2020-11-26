using eshop.core.ViewModels;
using eshop.webshop.SignalRHub;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

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
        public void NewProductNotify([FromBody] ProductViewModel product)
        {
            _productHubCtx.Clients.All.SendAsync("NewProduct", JsonSerializer.Serialize(product));
        }

        [HttpPut("product/{id}")]
        public void UpdatedProductNotify(int id, [FromBody] ProductViewModel product)
        {
            _productHubCtx.Clients.All.SendAsync("UpdateProduct", JsonSerializer.Serialize(product));
        }

        [HttpPost("product/{id}/image")]
        public void NewProductImageNotify(int id, [FromBody] ImageViewModel image)
        {
            _productHubCtx.Clients.All.SendAsync("NewProductImage", JsonSerializer.Serialize(image));
        }

        [HttpDelete("product/{id}")]
        public void RemoveProductNotify(int id)
        {
            _productHubCtx.Clients.All.SendAsync("RemoveProduct", id);
        }
    }
}
