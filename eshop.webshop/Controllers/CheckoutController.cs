using eshop.core.DTO.Request;
using eshop.webshop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.webshop.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly IOrdersService _order;
        private readonly ICartService _cart;

        public CheckoutController(IOrdersService order, ICartService cart)
        {
            _order = order;
            _cart = cart;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var cart = _cart.GetCart(Request);
            if (cart.Items.Count < 1) return Redirect("/");
            OrderInfoRequest orderInfo = new OrderInfoRequest();
            orderInfo.CustomerId = Int32.Parse(User.Claims.Where(c => c.Type == "UserId").First().Value);
            cart.Items.ForEach(cartItem =>
            {
                orderInfo.Items.Add(new Item { ProductId = cartItem.ProductId, Quantity = cartItem.Quantity });
            });
            var (statusCode, result) = await _order.AddOrder(orderInfo);
            if (result == null)
            {
                ViewData["OrderError"] = "ERROR!!!";
                return View();
            }
            ViewData["OrderResult"] = result;
            _cart.ClearCartCookie(Response);
            return View();
        }
    }
}
