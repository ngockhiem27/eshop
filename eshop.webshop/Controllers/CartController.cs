using eshop.core.ViewModels;
using eshop.webshop.Models;
using eshop.webshop.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.webshop.Controllers
{
    [Route("{controller}")]
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartService _cart;

        public CartController(IProductService productService, ICartService cart)
        {
            _productService = productService;
            _cart = cart;
        }

        public async Task<IActionResult> Index()
        {
            var cart = _cart.GetCart(Request);
            List<ProductViewModel> items = new List<ProductViewModel>();
            for (int i = 0; i < cart.Items.Count; i++)
            {
                var (_, product) = await _productService.GetProduct(cart.Items[i].ProductId);

                if (product != null)
                {
                    product.Quantity = cart.Items[i].Quantity;
                    items.Add(product);
                }
            }
            ViewData["Items"] = items;
            return View();
        }

        [HttpPost("add/{productId}")]
        public IActionResult Add(int productId)
        {
            var cart = _cart.GetCart(Request);
            var item = cart.Items.Where(items => items.ProductId == productId).FirstOrDefault();
            if (item != null)
            {
                item.Quantity++;
            }
            else
            {
                cart.Items.Add(new Item { ProductId = productId, Quantity = 1 });
            }
            _cart.SetCartCookie(cart, Response);
            return Ok();
        }

        [HttpPost("remove/{productId}")]
        public IActionResult Remove(int productId)
        {
            var cart = _cart.GetCart(Request);
            var item = cart.Items.Where(items => items.ProductId == productId).FirstOrDefault();
            if (item != null)
            {
                item.Quantity--;
                if (item.Quantity <= 0)
                {
                    cart.Items.Remove(item);
                }
            }
            _cart.SetCartCookie(cart, Response);
            return Ok();
        }

        [HttpGet("empty")]
        public IActionResult Empty()
        {
            _cart.ClearCartCookie(Response);
            return Ok();
        }

        [HttpPost("order")]
        public IActionResult CreateOrder([FromBody] List<ProductViewModel> items)
        {
            return Ok();
        }
    }
}
