using eshop.core.ViewModels;
using eshop.webshop.Models;
using eshop.webshop.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace eshop.webshop.Controllers
{
    [Route("{controller}")]
    public class CartController : Controller
    {
        private const string CartKey = "cart";
        private readonly IProductService _productService;

        public CartController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var cart = GetCart();
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
            var cart = GetCart();
            var item = cart.Items.Where(items => items.ProductId == productId).FirstOrDefault();
            if (item != null)
            {
                item.Quantity++;
            }
            else
            {
                cart.Items.Add(new Item { ProductId = productId, Quantity = 1 });
            }
            SetCartCookie(cart);
            return Ok();
        }

        [HttpPost("remove/{productId}")]
        public IActionResult Remove(int productId)
        {
            var cart = GetCart();
            var item = cart.Items.Where(items => items.ProductId == productId).FirstOrDefault();
            if (item != null)
            {
                item.Quantity--;
                if (item.Quantity <= 0)
                {
                    cart.Items.Remove(item);
                }
            }
            SetCartCookie(cart);
            return Ok();
        }

        [HttpGet("empty")]
        public IActionResult Empty()
        {
            SetCartCookie(new CartViewModel());
            return Ok();
        }

        [HttpPost("order")]
        public IActionResult CreateOrder([FromBody] List<ProductViewModel> items)
        {
            items.
            return Ok();
        }

        private CartViewModel GetCart()
        {
            CartViewModel cart;
            if (Request.Cookies.ContainsKey(CartKey))
            {
                cart = JsonSerializer.Deserialize<CartViewModel>(Request.Cookies[CartKey]);
            }
            else
            {
                cart = new CartViewModel();
            }
            return cart;
        }

        private void SetCartCookie(CartViewModel cart)
        {
            CookieOptions option = new CookieOptions
            {
                HttpOnly = false,
                MaxAge = TimeSpan.FromDays(30)
            };
            Response.Cookies.Append(CartKey, JsonSerializer.Serialize(cart), option);
        }
    }
}
