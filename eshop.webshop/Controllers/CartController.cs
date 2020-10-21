using eshop.webshop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text.Json;

namespace eshop.webshop.Controllers
{
    [Route("cart")]
    public class CartController : Controller
    {
        private const string CartKey = "cart";

        public CartController()
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
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

        [HttpPost]
        public IActionResult Clear()
        {
            SetCartCookie(new CartViewModel());
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
